using System;
using System.Collections.Generic;
using System.Linq;
using Myembro.Models;

namespace Myembro.ViewModels
{
    public class CalendarMonthViewModel
    {
        public EmbroDateType Type { get; private set; }
        public IEnumerable<CalendarDayViewModel> Days { get; private set; }
        public DateTimeOffset Month { get; private set; }
        public int TotalPhotos { get; private set; }
        public bool DisplayDays { get; private set; }

        public CalendarMonthViewModel(IList<EmbroideryItem> photos, EmbroDateType type, bool displayDays)
        {
            this.Type = type;
            this.Days = from p in photos
                        orderby p.Created
                        group p by p.Created.Day into d
                        select new CalendarDayViewModel(d.ToList());
           DateTimeOffset day = photos[0].Created;
            this.Month = new DateTimeOffset(day.Year, day.Month, 1, 0, 0, 0, TimeSpan.Zero);
            this.TotalPhotos = (from d in this.Days select d.Embros.Count).Sum();
            this.DisplayDays = displayDays;
        }
    }
}