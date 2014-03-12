using System;
using System.Collections.Generic;
using System.Linq;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class CalendarDayViewModel
    {
        public PhotoDateType Type { get; private set; }
        public ICollection<Photo> Photos { get; private set; }
        public DateTimeOffset Day { get; private set; }
        public int TotalPhotos { get; private set; }

        public CalendarDayViewModel(IList<Photo> photos, PhotoDateType type)
        {
            this.Type = type;
            this.Photos = photos;
            this.Day = photos[0].GetAdjustedDate(type).Date;
            this.TotalPhotos = this.Photos.Count;
        }
    }
}