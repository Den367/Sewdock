using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContourLibrary;
using ContourLibrary.ContourDeriving;
using ContourLibrary.Data;
using ContourLibrary.Plugin;
using ContourLibrary.Prepare;
using ContourLibrary.SvgStream;
using Coord.CommonType;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        private static void Main(string[] args)
        {


            SaveCountryNames(@"D:\WEB_PRJ\Mayando.Web\Content\Xml\country-contours.xml", @"d:\SVG\country-names.asc");

            //var derive = new WorldGlobeContourDerive();
            //var contour = derive.GetZigzagVerticesByCountryName("Burundi", @"d:\SVG\globe\world-110m.json");
            //using (var fileStream = new FileStream("d:/Russia.svg", FileMode.Create, FileAccess.Write, FileShare.Read))
            //{
            //    var svg2stream = new Vertices2SvgStream(fileStream, contour, 5000);
            //    //var svg2stream = new Vertices2SvgStream(fileStream, new EnumerableVertices(result));
            //    svg2stream.WriteSvg();
            //}

            //Topology topology;

            // deserialize JSON directly from a file
            //using (var file = File.OpenText(@"d:\SVG\globe\world-110m.json"))
            //{
            //    var serializer = new JsonSerializer();
            //    topology = (Topology) serializer.Deserialize(file, typeof (Topology));
            //}



            //var contourRetriver = new ContourRetriever();

            //var contour = contourRetriver.GetContour(topology, 4);
            //Console.WriteLine(contour.Count());

        }





        private static void SaveCountryNames(string xmlFileName,string outfileName)
        {
            XmlContourLoader loader = new XmlContourLoader();

            var countries = loader.GetCountryNames(xmlFileName);
            using (var textStream = File.CreateText(outfileName))
            {
                foreach (var country in countries)
                {
                    textStream.WriteLine(country);
                }
                textStream.Flush();
            }
          

        }

        private static void CreateContour()
        {
            //XmlContourLoader loader = new XmlContourLoader();
            //ContourSearchInfo searchInfo = new ContourSearchInfo()
            //{
            //    Delimiter = ',',
            //    FilePath = @"D:\WEB_PRJ\Mayando.Web\Content\Xml\country-contours.xml"
            //    ,
            //    IteratedElementName = "trk",
            //    PolygonElementName = "poly",
            //    SearchAttributeName = "name",
            //    SearchAttributeValue = "United Kingdom",
            //    StartElementName = "xml"
            //};
            //Modifier modifier = new Modifier();
            //var contour = loader.LoadContourFromFile(searchInfo);
            //var poly = contour.Polygons;

            //modifier.ToPixelForZoom(poly, 6);
            //var result = modifier.ScaleAndShiftZero(poly, 1000.0, 1000.0);
            //ICoordPlugin zigZagger = new ZigZagger();
            ////var perpenPoint = zigZagger.
            //using (var fileStream = new FileStream("d:/AlaskaPointed.svg", FileMode.Create, FileAccess.Write, FileShare.Read))
            //{
            //    var svg2stream = new Vertices2SvgStream(fileStream, zigZagger.Apply(new EnumerableVertices(result)), 500);
            //    //var svg2stream = new Vertices2SvgStream(fileStream, new EnumerableVertices(result));
            //    svg2stream.WriteSvg();
            //}
            //Console.WriteLine(result.Count());
            ////Console.ReadLine();

        }
    }
}
