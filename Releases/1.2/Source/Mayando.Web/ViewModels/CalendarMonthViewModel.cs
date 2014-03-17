using System;
using System.Collections.Generic;
using System.Linq;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class CalendarMonthViewModel
    {
        public PhotoDateType Type { get; private set; }
        public IEnumerable<CalendarDayViewModel> Days { get; private set; }
        public DateTimeOffset Month { get; private set; }
        public int TotalPhotos { get; private set; }
        public bool DisplayDays { get; private set; }

        public CalendarMonthViewModel(IList<Photo> photos, PhotoDateType type, bool displayDays)
        {
            this.Type = type;
            this.Days = from p in photos
                        orderby p.GetAdjustedDate(type)
                        group p by p.GetAdjustedDate(type).Day into d
                        select new CalendarDayViewModel(d.ToList(), type);
            var day = photos[0].GetAdjustedDate(type);
            this.Month = new DateTimeOffset(day.Year, day.Month, 1, 0, 0, 0, TimeSpan.Zero);
            this.TotalPhotos = (from d in this.Days select d.TotalPhotos).Sum();
            this.DisplayDays = displayDays;
        }
    }
}