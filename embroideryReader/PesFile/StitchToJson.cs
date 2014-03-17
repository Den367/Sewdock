using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace EmbroideryFile
{
    public static class StitchToJson
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        public static string ToJsonCoords(this EmbroideryData design)
        {


            var minX = design.GetXCoordMin();
            var minY = design.GetYCoordMin();
            design.Blocks.SelectMany(block => block.AsEnumerable()).ToList().ForEach(coord =>
                {
                    coord.Y -= minY;
                    coord.X -= minX;
                });
            var result = from needle in design.Blocks
                         select new {color = needle.color.Name, needle};
            return Serializer.Serialize(result);
        }
    }
}