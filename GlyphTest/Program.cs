using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContourLibrary.Glyph;
using ContourLibrary.SvgStream;

namespace GlyphTest
{
    class Program
    {
        static void Main(string[] args)
        {

            GlyphStreamer _glyphStreamer = new GlyphStreamer();
            GlyphZigzagifier zigZagger = new GlyphZigzagifier(args[0],"Regular");
            _glyphStreamer.WriteZigzagifiedGlyph2SvgStream(zigZagger, "D:/glyph_e0a2.svg", 0xe0a2);
            _glyphStreamer.WriteZigzagifiedGlyph2DstStream(zigZagger, "D:/glyph_e0a2.dst", 0xe0a2);
            //_glyphStreamer.WriteZigzagifiedGlyph2SvgStream(zigZagger, "D:/glyph_e12f.svg", 0xe12f);
            //_glyphStreamer.WriteZigzagifiedGlyph2SvgStream(zigZagger, "D:/glyph_e130.svg", 0xe130);
            //_glyphStreamer.WriteZigzagifiedGlyph2SvgStream(zigZagger, "D:/glyph_e131.svg", 0xe131);
            //_glyphStreamer.WriteZigzagifiedGlyph2SvgStream(zigZagger, "D:/glyph_e132.svg", 0xe132);
            //_glyphStreamer.WriteZigzagifiedGlyph2SvgStream(zigZagger, "D:/glyph_e133.svg", 0xe133);
            //_glyphStreamer.WriteZigzagifiedGlyph2SvgStream(zigZagger, "D:/glyph_e137.svg", 0xe137);
            //_glyphStreamer.WriteZigzagifiedGlyph2SvgStream(zigZagger, "D:/glyph_e138.svg", 0xe138);

        }


    }
}
