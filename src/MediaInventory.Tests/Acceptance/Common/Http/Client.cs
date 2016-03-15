using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Bender;
using Bender.Collections;
using MediaInventory.Infrastructure.Common;

namespace MediaInventory.Tests.Acceptance.Common.Http
{
    public class Client
    {
        private readonly Request _context;

        public Client(Request context)
        {
            _context = context;
        }

        public Response<string> Execute()
        {
            return Execute<object, string>(null);
        }

        public Response<T> Execute<T>(Dictionary<string, string> headers = null)
        {
            return Execute<T, T>(default(T), headers: headers);
        }

        public Response<T> Execute<T>(T data)
        {
            return Execute<T, T>(data);
        }

        public Response<TResponse> Execute<TResponse>(string data)
        {
            return Execute<string, TResponse>(data);
        }

        public Response<TResponse> Execute<TResponse>(Stream data)
        {
            return Execute<Stream, TResponse>(data);
        }

        public Response<TResponse> Execute<TRequest, TResponse>(
            TRequest data,
            string url = null,
            CookieCollection cookies = null,
            Dictionary<string, string> headers = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url ?? _context.Url);
            request.Method = _context.Verb.ToString().ToUpper();
            request.Accept = GetFormat(_context.AcceptType) + ", */*";
            request.AllowAutoRedirect = false;

            headers?.ForEach(x => request.Headers[x.Key] = x.Value);

            request.CookieContainer = new CookieContainer();
            if (cookies != null || _context.Cookies != null)
                request.CookieContainer.Add(cookies ?? _context.Cookies);

            if (_context.ContentType != Request.DataFormat.None)
            {
                request.ContentType = GetFormat(_context.ContentType);
                using (Stream sourceStream = FormatRequest(data, _context.ContentType), requestStream = request.GetRequestStream())
                    sourceStream.CopyTo(requestStream);
            }
            else request.ContentLength = 0;

            Console.WriteLine(request.Method + ":" + request.RequestUri);

            Func<HttpWebResponse, bool> isRedirect = x => x.StatusCode == HttpStatusCode.MultipleChoices ||
                x.StatusCode == HttpStatusCode.MovedPermanently || x.StatusCode == HttpStatusCode.Found ||
                x.StatusCode == HttpStatusCode.SeeOther || x.StatusCode == HttpStatusCode.TemporaryRedirect;

            using (var response = GetResponse(request))
            {
                var responseData = default(TResponse);
                using (var responseStream = response.GetResponseStream())
                {
                    if ((int)response.StatusCode >= 300 && !isRedirect(response))
                    {
                        Console.WriteLine("Status of {0} returned by {1}. {2}", response.StatusCode, request.RequestUri, response.StatusDescription);
                        Console.WriteLine(new StreamReader(responseStream).ReadToEnd());
                    }
                    else if (response.ContentLength != 0) responseData = FormatResponse<TResponse>(responseStream, _context.AcceptType);
                }
                if (isRedirect(response))
                {
                    var redirectUrl = GetAbsoluteUrl(response.Headers["location"], request.RequestUri);
                    Console.WriteLine("Redirected {0} to {1}", request.RequestUri, redirectUrl);
                    return Execute<TRequest, TResponse>(data, redirectUrl, response.Cookies);
                }
                var redirect = url != null && url != _context.Url;
                return new Response<TResponse>(
                    response.StatusCode,
                    response.StatusDescription,
                    response.Headers.ToDictionary(),
                    redirect,
                    redirect ? response.ResponseUri : null,
                    cookies ?? response.Cookies,
                    responseData);
            }
        }

        private static string GetAbsoluteUrl(string url, Uri baseUrl)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri) return uri.ToString();
            return new Uri(baseUrl, url).ToString();
        }

        private static HttpWebResponse GetResponse(WebRequest request)
        {
            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                var response = e.Response as HttpWebResponse;
                if (response == null) throw;
                return response;
            }
        }

        private static string GetFormat(Request.DataFormat format)
        {
            switch (format)
            {
                case Request.DataFormat.Xml: return "application/xml";
                case Request.DataFormat.Binary: return "application/binary";
                case Request.DataFormat.Json: return "application/json";
                case Request.DataFormat.Text: return "text/plain";
                case Request.DataFormat.Form: return "application/x-www-form-urlencoded";
                default: return "*/*";
            }
        }

        private static Stream FormatRequest<TRequest>(TRequest data, Request.DataFormat contentType)
        {
            if (data is string) return new MemoryStream(Encoding.UTF8.GetBytes((string)(object)data));
            switch (contentType)
            {
                case Request.DataFormat.Xml: return new MemoryStream(Encoding.UTF8.GetBytes(Serialize.Xml(data)));
                case Request.DataFormat.Form:
                    var dictionary = data as IDictionary<string, string>;
                    if (dictionary != null)
                        return new MemoryStream(Encoding.UTF8.GetBytes(dictionary
        .Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value))
        .Aggregate((a, i) => a + "&" + i)));
                    throw new NotImplementedException("Form data can only be serialized from a dictionary");
                case Request.DataFormat.Binary:
                    if (data is Stream) return (Stream)(object)data;
                    throw new NotImplementedException("Binary data can only be read from a stream");
                case Request.DataFormat.Json: return new MemoryStream(Encoding.UTF8.GetBytes(Serialize.Json(data)));
                case Request.DataFormat.Text: return new MemoryStream(Encoding.UTF8.GetBytes(data.ToString()));
                case Request.DataFormat.None: return null;
                default: throw new NotImplementedException("No formatter found for this content type.");
            }
        }

        private static T FormatResponse<T>(Stream stream, Request.DataFormat acceptType)
        {
            if (typeof(T) == typeof(string)) return (T)(object)new StreamReader(stream).ReadToEnd();
            switch (acceptType)
            {
                case Request.DataFormat.Xml: return (T)new XmlSerializer(typeof(T)).Deserialize(stream);
                case Request.DataFormat.Binary:
                    if (typeof(T) == typeof(Stream)) return (T)(object)stream;
                    throw new NotImplementedException("Binary data can only be returned as a stream");
                case Request.DataFormat.Json:
                    var data = new StreamReader(stream).ReadToEnd();
                    try
                    {
                        return Deserialize.Json<T>(data, x => x.Deserialization(y => y
                            .AddReader((v, s, t, o) => v.ToString().StartsWith("/Date(") ?
                                v.ToString().ParseMicrosoftJsonDateFormat() : DateTime.Parse(v.ToString()), true)));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(data);
                        throw;
                    }
                case Request.DataFormat.Text: return new StreamReader(stream).ReadToEnd().Parse<T>();
                case Request.DataFormat.None: return default(T);
                default: throw new NotImplementedException("No formatter found for this accept type.");
            }
        }

        public class FullElementXmlWriter : XmlTextWriter
        {
            public FullElementXmlWriter(TextWriter writer) : base(writer) { }
            public override void WriteEndElement() { base.WriteFullEndElement(); }
        }
    }
}