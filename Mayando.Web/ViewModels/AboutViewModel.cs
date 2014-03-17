using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class AboutViewModel
    {
        public SiteData SiteData { get; private set; }
        public SiteStatistics Statistics { get; private set; }

        public AboutViewModel(SiteData siteData, SiteStatistics statistics)
        {
            this.SiteData = siteData;
            this.Statistics = statistics;
        }
    }
}