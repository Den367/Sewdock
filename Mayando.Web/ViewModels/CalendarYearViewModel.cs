using System;
using System.Collections.Generic;
using System.Linq;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class CalendarYearViewModel
    {
        public EmbroDateType Type { get; private set; }
        public IEnumerable<CalendarMonthViewModel> Months { get; private set; }
        public DateTimeOffset Year { get; private set; }
        public int TotalPhotos { get; private set; }
        public bool DisplayMonths { get; private set; }

        public CalendarYearViewModel(IList<EmbroideryItem> photos, EmbroDateType type, bool displayMonths, bool displayDays)
        {
            this.Type = type;
            this.Months = from p in photos
                          orderby p.Created
                          group p by p.Created.Month into m
                          select new CalendarMonthViewModel(m.ToList(), type, displayDays);
            var day = photos[0].Created;
            this.Year = new DateTimeOffset(day.Year, 1, 1, 0, 0, 0, TimeSpan.Zero);
            this.TotalPhotos = (from m in this.Months select m.TotalPhotos).Sum();
            this.DisplayMonths = displayMonths;
        }
    }
}