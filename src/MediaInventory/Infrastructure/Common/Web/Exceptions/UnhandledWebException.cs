using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FubuMVC.Core.Http;
using NHibernate;
using ApplicationException = MediaInventory.Infrastructure.Common.Exceptions.ApplicationException;

namespace MediaInventory.Infrastructure.Common.Web.Exceptions
{
    public class UnhandledWebException : ApplicationException
    {
        private readonly string _id;

        public UnhandledWebException(
            string message,
            IRequestHeaders requestHeaders,
            Exception exception = null,
            string platformCode = null) :
            base(message, exception)
        {
            //_id = GetId(platformCode);
            _id = base.Id;

            //ServerName = serverVariables.ServerName;
            //Url = serverVariables.Url;
            //Method = serverVariables.HttpMethod;
            //Https = serverVariables.Https == "yes";
            //ContentType = serverVariables.ContentType;
            //Accept = serverVariables.HttpAccept;
            //ContentLength = int.Parse(serverVariables.ContentLength);
            //Cookies = serverVariables.HttpCookie;
            //Referrer = serverVariables.HttpReferrer;
            //Host = serverVariables.HttpHost;
            //ServerPort = int.Parse(serverVariables.ServerPort);
            //LocalAddress = serverVariables.LocalAddress;
            //RemoteAddress = serverVariables.RemoteAddress;
            //UserAgent = serverVariables.HttpUserAgent;
            //Headers = requestHeaders.ToHeaderBlock();
            //Querystring = querystring.ToString();
            //Form = FormatForm(form);
            //SessionId = session.Exists ? session.Id : null;
            //Session = session.Exists ? session.ToString() : null;
            //Files = postedFiles.ToString();
            MachineName = Environment.MachineName;
        }

        public override string Id { get { return _id; } }
        public string ServerName { get; private set; }
        public string MachineName { get; private set; }
        public string Method { get; private set; }
        public string Url { get; private set; }
        public bool Https { get; private set; }
        public string Headers { get; private set; }
        public string ContentType { get; private set; }
        public string Accept { get; private set; }
        public long ContentLength { get; private set; }
        public string Cookies { get; private set; }
        public string Referrer { get; private set; }
        public string Host { get; private set; }
        public string Querystring { get; private set; }
        public string Form { get; private set; }
        public string Session { get; private set; }
        public string Files { get; private set; }
        public string SessionId { get; private set; }
        public int ServerPort { get; private set; }
        public string LocalAddress { get; private set; }
        public string RemoteAddress { get; private set; }
        public string UserAgent { get; private set; }

        //private string GetId(string platformCode = null)
        //{
        //    return string.Format("{0}-{1}{2}", base.Id, GetServerNumber(), platformCode ?? "N");
        //}

        //private static string FormatForm(IDictionary<string, string> form)
        //{
        //    if (form == null || !form.Any()) return null;
        //    return form.ToUrlEncodedString((key, value) => key != null &&
        //        (key.ToLower().Contains("password") ||
        //         key.ToLower().Contains("pwd")) ?
        //        "**********" : value);
        //}

        //private static string GetServerNumber()
        //{
        //    var serverNumber = Environment.MachineName.StripNonNumeric();
        //    return string.IsNullOrEmpty(serverNumber) ? "0" : serverNumber;
        //}
    }
}
