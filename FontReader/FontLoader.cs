
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;


namespace FontReader
{
    public class FontLoader
    {
        private FontFamily _fontFamily;
        private Font _font;
        public FontFamily LoadFontFamily(string fileName, out PrivateFontCollection _myFonts)
        {
            //IN MEMORY _myFonts point to the myFonts created in the load event 11 lines up.
            _myFonts = new PrivateFontCollection();//here is where we assing memory space to myFonts
            _myFonts.AddFontFile(fileName);//we add the full path of the ttf file
            _fontFamily =  _myFonts.Families[0];
           
            return _fontFamily; //returns the family object as usual.
        }

        public PointF[] GetPoints(string text, float size, FontStyle style)
        {

            using (Font font = new Font(_fontFamily,  size,style))
            using (GraphicsPath gp = new GraphicsPath())
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                gp.AddString(text, font.FontFamily, (int)font.Style, font.Size, new Point(0,0),sf);
                return gp.PathPoints;               
            }
        }

      

    }
}
