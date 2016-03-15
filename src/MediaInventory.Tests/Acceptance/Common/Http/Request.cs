using System.Net;

namespace MediaInventory.Tests.Acceptance.Common.Http
{
    public class Request
    {
        public enum DataFormat { None, Binary, Json, Text, Xml, Form }
        public enum RequestVerb { Get, Post, Put, Delete }

        public DataFormat ContentType { get; set; }
        public DataFormat AcceptType { get; set; }
        public RequestVerb Verb { get; set; }
        public string Url { get; set; }
        public CookieCollection Cookies { get; set; }

        public Request Clone()
        {
            return new Request
            {
                ContentType = ContentType,
                AcceptType = AcceptType,
                Verb = Verb,
                Url = Url,
                Cookies = Cookies
            };
        }
    }
}