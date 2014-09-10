using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContourLibrary.Data;
using Coord.CommonType;

namespace ConsoleApplication1
{
    public class ContourRetriever
    {
        public IEnumerable<IEnumerable<Vertice>> GetContour(Topology topology, int id)
        {
            List<List<Vertice>> result = new List<List<Vertice>>();
            Geometry geometry = topology.objects.countries.geometries.FirstOrDefault(contour => contour.id == id);

            if (geometry != null)
            {
                // список полигонов страны
                var arcList = geometry.arcs;
                foreach (var polygonIds in arcList)
                {
                    foreach (var o in polygonIds)
                    {
                        var poly = new List<Vertice>();
                        var polygons = topology.arcs;
                        //
                        foreach (var coords in polygons[(int)o])
                        {
                            poly.Add(new Vertice { X = coords[0], Y = coords[1] });
                        }
                        result.Add(poly);
                    }


                }



            }
            return result;
        }
    }
}
