
using System.Collections.Generic;
using System.Windows.Media;
using FontReader.Interface;

namespace FontReader.Model
{
    /// <summary>
    /// Font information
    /// </summary>
    public class FontModel :IFontModel
    {

        public IDictionary<int, ushort> CharacterMap { get; set; }
        public IEnumerable<string> TypefaceNames { get; set; }
        public IDictionary<ushort, IEnumerable<PointCollection>> GlyphPointCollection { get; set; }
    }
}
