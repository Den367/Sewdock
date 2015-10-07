
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ContourLibrary.Data;
using ContourLibrary.Glyph;
using ContourLibrary.SvgStream;
using EmbroideryFile;

namespace GlyphTest
{
    internal class GlyphStreamer
    {
        internal void WriteZigzagifiedGlyph2SvgStream(GlyphZigzagifier zigZagger, string fileName, int uniCode)
        {

            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                ushort glyphIndex = zigZagger.CharacterMap[uniCode];
                var svg2stream = new Vertices2SvgStream(fileStream, zigZagger.GetGlyphZigzags(glyphIndex, 1000.0, 1000.0, 3.0, 30.0), 1000);
                //var svg2stream = new Vertices2SvgStream(fileStream, new EnumerableVertices(result));
                svg2stream.WriteSvg();
            }
        }

        public void WriteZigzagifiedGlyph2SvgStream(GlyphZigzagifier zigZagger, Stream svgStream, int uniCode)
        {

            using (var fileStream = new MemoryStream())
            {
                ushort glyphIndex = zigZagger.CharacterMap[uniCode];
                var svg2stream = new Vertices2SvgStream(fileStream, zigZagger.GetGlyphZigzags(glyphIndex, 1000.0, 1000.0, 3.0, 30.0), 1000);
                //var svg2stream = new Vertices2SvgStream(fileStream, new EnumerableVertices(result));
                svg2stream.WriteSvg();
            }
        }

        internal void WriteZigzagifiedGlyph2DstStream(GlyphZigzagifier zigZagger, string fileName, int uniCode)
        {
            var dstFile = new DstFile {Design = {DesignName = uniCode.ToString()}};

            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                ushort glyphIndex = zigZagger.CharacterMap[uniCode];
                 dstFile.WriteStitchesToDstStream(GetCoordsBlockList(zigZagger.GetGlyphZigzags(glyphIndex, 1000.0, 1000.0, 10.0, 25.0), Color.BurlyWood), fileStream);

                
            }
        }

        private List<EmbroideryFile.CoordsBlock> GetCoordsBlockList(IEnumerable<IEnumerable<DoubleVertice>> polygons, Color color)
        {
            if (null == polygons) return null;
            var result = new List<EmbroideryFile.CoordsBlock>();
            foreach (var polygon in polygons)
            {
                var regionPoints = new EmbroideryFile.CoordsBlock();
                foreach (var point in polygon)
                {
                    regionPoints.Add(new EmbroideryFile.Coords { X = (int)point.X, Y = -(int)point.Y });
                }
                regionPoints.Color = color;
                result.Add(regionPoints);
            }
            return result;
        }

      
    }
}
