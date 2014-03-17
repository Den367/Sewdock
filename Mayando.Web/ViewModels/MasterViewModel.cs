using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class MasterViewModel
    {
        public SiteData SiteData { get; private set; }
        public string Keywords { get; private set; }
        public DateTimeOffset Now { get; private set; }
        public string PageFlash { get; private set; }
        public string SlideshowNextUrl { get; set; }
        public int? SlideshowDelay { get; set; }

        public MasterViewModel(SiteData siteData, string keywords, DateTimeOffset now)
        {
            this.SiteData = siteData;
            this.Keywords = keywords;
            this.Now = now;
        }

        public void AddKeywords(IEnumerable<string> keywords)
        {
            if (!string.IsNullOrEmpty(this.Keywords))
            {
                this.Keywords += ",";
            }
            this.Keywords += string.Join(",", keywords.ToArray());
        }

        public void AddToPageFlash(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (!string.IsNullOrEmpty(this.PageFlash))
                {
                    this.PageFlash += "<br />";
                }
                this.PageFlash += message;
            }
        }
    }
}