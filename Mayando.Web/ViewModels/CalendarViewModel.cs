using System.Collections.Generic;
using System.Linq;
using JelleDruyts.Web.Mvc;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class CalendarViewModel
    {
        public EmbroDateType Type { get; private set; }
        public IEnumerable<CalendarYearViewModel> Years { get; private set; }
        public int TotalPhotos { get; private set; }
        public IEnumerable<LinkListItem> Links { get; private set; }

        public CalendarViewModel(IEnumerable<EmbroideryItem> embros, EmbroDateType type, bool displayMonths, bool displayDays, IEnumerable<LinkListItem> links)
        {
            this.Type = type;
            this.Years = from p in embros
                         orderby p.Created
                         group p by p.Created.Year into y
                         select new CalendarYearViewModel(y.ToList(), type, displayMonths, displayDays);
            this.TotalPhotos = (from y in this.Years select y.TotalPhotos).Sum();
            this.Links = links;
        }
    }
}