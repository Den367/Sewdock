using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.Xml.Linq;
namespace Mayando.Web.ViewModels
{
    public class EmbroideryBounds
    {
        public int Width { get; set; }
        /// <summary>
        /// Height of embroidery design (0.1 mm)
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Minimum value of horizontal stitch coords 
        /// </summary>
        public int Xmin { get; set; }
        /// <summary>
        /// Minimum value of vertical stitch coords 
        /// </summary>
        public int Ymin { get; set; }

        /// <summary>
        /// Maximum value of Y stitch coords
        /// </summary>
        public int Xmax { get; set; }
        /// <summary>
        /// Maximum value of Y stitch coords
        /// </summary>
        public int Ymax { get; set; }
        #region [ctor]
        public EmbroideryBounds(XElement summary)
        {
            Width = Convert.ToInt32(summary.Element("Width").Value);
            Height = GetElementText(summary, "Height");
            Xmin = GetElementText(summary, "Xmin");
            Xmax = GetElementText(summary, "Xmax");
            Ymin = GetElementText(summary, "Ymin");
            Ymax = GetElementText(summary, "Ymax");
        }
        #endregion [ctor]

        int GetElementText(XElement e, string Name)
        {

            return Convert.ToInt32(e.Element(Name).Value);
        }
    }
}