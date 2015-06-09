using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FontReader;
namespace FontLoaderTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLoadFont()
        {
            var fontHandler = new GlyphParser();
            PrivateFontCollection fontCollection;
            var fontFamily = fontHandler.LoadFontFamily(@"c:\Windows\Fonts\ZnakySAE.ttf ", out fontCollection);
            var result = fontHandler.GetPoints(".",20.0f, FontStyle.Regular);
           Trace.WriteLine(result);
            //Assert(result != null);
        }
    }
}
