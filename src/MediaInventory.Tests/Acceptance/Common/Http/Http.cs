using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaInventory.Tests.Common;

namespace MediaInventory.Tests.Acceptance.Common.Http
{
    public class UiDsl
    {
        private readonly Http _http;

        public UiDsl(Http http)
        {
            _http = http;
        }

        public ContentTypeSelector AsPublic(bool https, bool trailingSlash)
        {
            return _http.GetSelector(https, trailingSlash, null, Configuration.Current.Web.Ui.Urls.GetRootUrl(), null);
        }

        public ContentTypeSelector AsPublic(bool https, bool trailingSlash, string relativeUrl, params object[] args)
        {
            return _http.GetSelector(https, trailingSlash, null, Configuration.Current.Web.Ui.Urls.GetRootUrl(), relativeUrl, args);
        }

        public ContentTypeSelector AsPublic(string relativeUrl, params object[] args)
        {
            return _http.GetSelector(true, true, null, Configuration.Current.Web.Ui.Urls.GetRootUrl(), relativeUrl, args);
        }
        

        public string BuildUrl(bool https, bool trailingSlash, string relativeUrl, params object[] args)
        {
            return _http.GetUrl(https, trailingSlash, Configuration.Current.Web.Ui.Urls.GetRootUrl(), relativeUrl, args);
        }
    }

    public class Http
    {
        public static UiDsl ForUi
        {
            get { return new UiDsl(new Http()); }
        }

        public ContentTypeSelector GetSelector(bool https, bool trailingSlash, Action<Request> configure, Uri baseUrl, string relativeUrl, params object[] args)
        {
            var context = new Request { Url = GetUrl(https, trailingSlash, baseUrl, relativeUrl, args) };
            if (configure != null) configure(context);
            return new ContentTypeSelector(context);
        }

        public string GetUrl(bool https, bool trailingSlash, Uri baseUrl, string relativeUrl, params object[] args)
        {
            var url = baseUrl + (!string.IsNullOrEmpty(relativeUrl) ? string.Format(relativeUrl, args) : "");
            url = https ? url.Replace("http://", "https://") : url.Replace("https://", "http://");
            if (trailingSlash && !url.Contains("?")) url = url.EndsWith("/") ? url : url + "/";
            else url = url.EndsWith("/") ? url.Substring(0, url.Length - 1) : url;
            return url;
        }
    }
}
