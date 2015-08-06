
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media;

namespace fontChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            var families = Fonts.GetFontFamilies(args[0]);
           
            foreach (FontFamily family in families)
            {
                Console.WriteLine("family:{0}",family);
                Console.WriteLine(string.Format("FamilyNames.Count:{0}",family.FamilyNames.Count));
                foreach (var names in family.FamilyNames)
                {
                   
                        Console.WriteLine("names:{0}",names.Value);
                }
                Console.WriteLine(string.Format("{0} {1} {2}",family.BaseUri,family.Baseline,family.FamilyNames));
                var typefaces = family.GetTypefaces();
                Console.WriteLine(string.Format("typefaces.Count:{0}", typefaces.Count));
                foreach (Typeface typeface in typefaces)
                {
                   
                    Console.WriteLine(string.Format("typeface.FaceNames.Count:{0}", typeface.FaceNames.Count));
                    Console.WriteLine(string.Format("typeface.Style:{0} {1} {2}", typeface.Stretch, typeface.Style, typeface.Weight));
                    foreach(var faceName in typeface.FaceNames)
                    Console.WriteLine("faceName:{0} {1}",faceName.Key,faceName.Value);
                    GlyphTypeface glyph;
                    typeface.TryGetGlyphTypeface(out glyph);
                    Console.WriteLine(string.Format("StyleSimulation:{0}", glyph.StyleSimulations));
                    IDictionary<int, ushort> characterMap = glyph.CharacterToGlyphMap;
                    Console.WriteLine("{0}",glyph.Descriptions);
                    foreach (KeyValuePair<int, ushort> kvp in characterMap)
                    {
                        GetGlyphGeometry(glyph, kvp.Value, 100.0, 100.0);
                        //Console.WriteLine(String.Format("{0}:{1}", kvp.Key, kvp.Value));
                    }

                }
            }
            Console.WriteLine("Press enter");
            Console.ReadLine();
        }

        public static void GetGlyphGeometry(GlyphTypeface glyphTypeface, ushort glyphIndex, double renderingEmSize, double hintingEmSize)
        {
            var geom = glyphTypeface.GetGlyphOutline(glyphIndex, renderingEmSize, hintingEmSize);
            PathGeometry path = geom.GetOutlinedPathGeometry();
            foreach (PathFigure figure in path.Figures)
            {
                foreach (var segment in figure.Segments)
                {
                    var type = segment.GetType();
                    
                    //Trace.WriteLine(string.Format("{0} {1}", glyphIndex, type.Name));
                }

            }
        }
    }
}