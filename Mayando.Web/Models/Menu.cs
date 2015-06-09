using System.Collections.Generic;
using System.ComponentModel;
using Myembro.Properties;

namespace Myembro.Models
{
    public partial class Menu 
    {
       

        public int Id { get; set; }

        public string Title { get; set; }

        public bool OpenInNewWindow { get; set; }

        public int Sequence { get; set; }

        public string ToolTip { get; set; }

        public string Url { get; set; }

        public string cssClass { get; set; }

        public bool Selected { get; set; }
    }
}