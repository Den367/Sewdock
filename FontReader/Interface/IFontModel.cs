using System.Collections.Generic;
using System.Windows.Media;

namespace FontReader.Interface
{
    public interface IFontModel
    {
         IDictionary<int, ushort> CharacterMap { get; set; }
       IEnumerable<string> TypefaceNames { get; set; }
         IDictionary<ushort, IEnumerable<PointCollection>> GlyphPointCollection { get; set; }
    }
}
