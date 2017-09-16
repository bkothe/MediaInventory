using System;
using System.Net;
using System.Text;

namespace MediaInventory.Infrastructure.Common.Web.Security
{
    public interface IBasicAuthentication
    {
        bool HasCredentials();
        BasicAuthentication.Credentials GetCredentials();
        void SetUnauthorized(string realm, string statusDescription = null);
    }

    public static class BasicAuthenticationExtensions
    {
        public static void SetBasicAuthenticationHeader(this IRequestHeaders requestHeaders, string username, string password)
        {
            requestHeaders.SetAuthenticationHeader(BasicAuthentication.AuthorizationType,
                Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password)));
        }
    }

    public class BasicAuthentication : IBasicAuthentication
    {
        public const string AuthorizationType = "Basic";

        private readonly IHttpStatus _httpStatus;
        private readonly IRequestHeaders _requestHeaders;
        private readonly IResponseHeaders _responseHeaders;
        private readonly Lazy<Credentials> _credentials;

        public BasicAuthentication(IHttpStatus httpStatus, IRequestHeaders requestHeaders, IResponseHeaders responseHeaders)
        {
            _httpStatus = httpStatus;
            _requestHeaders = requestHeaders;
            _responseHeaders = responseHeaders;
            _credentials = new Lazy<Credentials>(() => GetCredentials(requestHeaders));
        }

        public bool HasCredentials()
        {
            return _credentials.Value != null;
        }

        public Credentials GetCredentials()
        {
            return _credentials.Value;
        }

        public void SetCredentials(string username, string password)
        {
            _requestHeaders.SetBasicAuthenticationHeader(username, password);
        }

        public void ClearCredentials()
        {
            _requestHeaders.ClearAuthorizationHeader();
        }

        public void SetUnauthorized(string realm, string statusDescription = null)
        {
            _httpStatus.Set(HttpStatusCode.Unauthorized, statusDescription);
            _responseHeaders.SetAuthenticateHeader(AuthorizationType, realm);
        }

        private static Credentials GetCredentials(IRequestHeaders headers)
        {
            var credentials = headers.GetAuthenticationCredentials(AuthorizationType);
            if (credentials == null) return null;
            credentials = DecodeCredentials(credentials);
            if (credentials == null) return null;
            var credentialParts = credentials.Split(new[] { ':' }, 2);
            return credentialParts.Length == 2 ? new Credentials(credentialParts[0], credentialParts[1]) : null;
        }

        private static string DecodeCredentials(string credentials)
        {
            try
            {
                return Encoding.ASCII.GetString(Convert.FromBase64String(credentials));
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public class Credentials
        {
            public Credentials(string username, string password)
            {
                Username = username;
                Password = password;
            }

            public string Username { get; private set; }
            public string Password { get; private set; }
        }
    }
}
