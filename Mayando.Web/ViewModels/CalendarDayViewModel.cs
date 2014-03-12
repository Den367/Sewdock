using System;
using System.Collections.Generic;
using System.Linq;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class CalendarDayViewModel
    {
        public EmbroDateType Type { get; private set; }
        public ICollection<EmbroideryItem> Embros { get; private set; }
        public DateTimeOffset Day { get; private set; }
        public int TotalEmbros { get; private set; }

        public CalendarDayViewModel(IList<EmbroideryItem> embros)
        {
          
            this.Embros = embros;
            this.Day = embros[0].Created;
            this.TotalEmbros = this.Embros.Count;
        }
    }
}