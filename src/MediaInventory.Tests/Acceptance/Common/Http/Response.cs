using System;
using System.Collections.Generic;
using System.Net;

namespace MediaInventory.Tests.Acceptance.Common.Http
{
    public class Response<T>
    {
        public Response(
            HttpStatusCode status,
            string statusDescription,
            IDictionary<string, string> headers,
            bool redirected,
            Uri redirectUrl,
            CookieCollection cookies, T data)
        {
            Status = status;
            StatusDescription = statusDescription;
            Headers = headers;
            Redirected = redirected;
            RedirectUrl = redirectUrl;
            Cookies = cookies;
            Data = data;
        }

        public HttpStatusCode Status { get; private set; }
        public string StatusDescription { get; private set; }
        public bool Redirected { get; private set; }
        public Uri RedirectUrl { get; private set; }
        public IDictionary<string, string> Headers { get; private set; }
        public CookieCollection Cookies { get; private set; }
        public T Data { get; private set; }
    }
}