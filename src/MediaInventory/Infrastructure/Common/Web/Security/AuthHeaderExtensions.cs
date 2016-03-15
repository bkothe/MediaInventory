using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaInventory.Infrastructure.Common.Web.Security
{
    public static class AuthHeaderExtensions
    {
        public const string AuthorizationHeaderName = "Authorization";
        public const string AuthenticateHeaderName = "WWW-Authenticate";
        public const string Realm = "realm";

        public static void SetAuthenticationHeader(this IRequestHeaders requestHeaders, string authorizationType, string credentials)
        {
            requestHeaders.Add(AuthorizationHeaderName, "{0} {1}".ToFormat(authorizationType, credentials));
        }

        public static void SetAuthenticateHeader(this IResponseHeaders responseHeaders, string authorizationType, string realm = "")
        {
            responseHeaders[AuthenticateHeaderName] = $"{authorizationType} {Realm}=\"{realm}\"";
        }

        public static string GetAuthenticationCredentials(this IRequestHeaders headers, string authorizationType)
        {
            if (!headers.ContainsKey(AuthorizationHeaderName) || string.IsNullOrEmpty(headers[AuthorizationHeaderName])) return null;
            var headerParts = headers[AuthorizationHeaderName].Trim().Split(new[] { ' ' }, 2);
            return headerParts.Length == 2 && headerParts[0].Equals(authorizationType, StringComparison.OrdinalIgnoreCase) ? headerParts[1] : null;
        }

        public static IDictionary<string, string> MaskAuthorizationHeader(this IDictionary<string, string> headers)
        {
            return new Dictionary<string, string>(headers.ToDictionary(x => x.Key, x =>
            {
                if (!x.Key.Trim().Equals(AuthorizationHeaderName, StringComparison.OrdinalIgnoreCase)) return x.Value;
                var authorization = x.Value.Trim().Split(" ");
                return authorization.First() + " " + new string('*', authorization.Last().Length);
            }));
        }

        public static void ClearAuthorizationHeader(this IDictionary<string, string> headers)
        {
            headers.Remove(AuthorizationHeaderName);
        }
    }
}
