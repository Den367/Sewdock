using System.Collections.Generic;
using System.Linq;
using JelleDruyts.Web.Mvc;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class CalendarViewModel
    {
        public PhotoDateType Type { get; private set; }
        public IEnumerable<CalendarYearViewModel> Years { get; private set; }
        public int TotalPhotos { get; private set; }
        public IEnumerable<LinkListItem> Links { get; private set; }

        public CalendarViewModel(IEnumerable<Photo> photos, PhotoDateType type, bool displayMonths, bool displayDays, IEnumerable<LinkListItem> links)
        {
            this.Type = type;
            this.Years = from p in photos
                         orderby p.GetAdjustedDate(type)
                         group p by p.GetAdjustedDate(type).Year into y
                         select new CalendarYearViewModel(y.ToList(), type, displayMonths, displayDays);
            this.TotalPhotos = (from y in this.Years select y.TotalPhotos).Sum();
            this.Links = links;
        }
    }
}