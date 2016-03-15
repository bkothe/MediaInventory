using System;
using MediaInventory.Infrastructure.Common.Web;

namespace MediaInventory.Infrastructure.Application.Configuration
{
    public class UiSite
    {
        public string DomainName { get; set; }
        public GoSiteUrls Urls { get; set; }
    }

    public class GoSiteUrls
    {
        private readonly UiSite _site;

        public GoSiteUrls(UiSite site)
        {
            _site = site;
        }

        public string Root { get; set; }
        public Uri GetRootUrl() { return Root.ToUri(_site.DomainName); }
    }

    public class Web
    {
        public string ErrorPage { get; set; }

        public bool ErrorPageSpecified
        {
            get { return !string.IsNullOrEmpty(ErrorPage); }
        }

        public int SessionTimeout { get; set; }
        public UiSite Ui { get; set; }
    }
}