using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

using System.Drawing;

namespace EmbroideryFile
{

   
    public class StitchToBmp
    {
        Point _translateStart;
        private readonly List<CoordsBlock> _blocks;
        readonly int _width;
        readonly int _height;
        float _xscale;
        float _yscale;



        public StitchToBmp(List<CoordsBlock> blocks, int width, int height)
        {

            _blocks = blocks;
            _width = width;
            _height = height;

            CalcTranslate();
        }

        public StitchToBmp(List<CoordsBlock> blocks, int size)
        {

            _blocks = blocks;
            _width = width;
            _height = height;

            CalcTranslate();
        }

        /// <summary>
        /// 
        /// </summary>
        void CalcTranslate()
        {
            int xmin = _blocks.SelectMany(block => block.AsEnumerable()).Min(coord => coord.X);
            int ymin = _blocks.SelectMany(block => block.AsEnumerable()).Min(coord => coord.Y);
            int xmax = _blocks.SelectMany(block => block.AsEnumerable()).Max(coord => coord.X);
            int ymax = _blocks.SelectMany(block => block.AsEnumerable()).Max(coord => coord.Y);
            
               _xscale =((float)_width) / ((float)(xmax - xmin))   ;
               _yscale = ((float)_width) / ((float)(ymax - ymin)) ;
           
            _translateStart.X = - (int)(xmin * _xscale);
            _translateStart.Y = -(int)(ymin * _yscale); 

        }
   
        public Bitmap DesignToBitmap(Single threadThickness)
        {
            Bitmap drawArea = new Bitmap(_width, _height);
            
                using (Graphics xGraph = Graphics.FromImage(drawArea))
                {
                    Pen tempPen;
                    xGraph.TranslateTransform(_translateStart.X, _translateStart.Y);
                    xGraph.FillRectangle(Brushes.Transparent, 0, 0, _width, _height);                   
                    IEnumerable<stitchBlock> tmpblocks = GetScaledPointBlock(_xscale,_yscale);
                    //for (int i = 0; i < tmpblocks.Count; i++)
                        foreach (var stitchBlock in tmpblocks)
                       
                    {
                        if ((stitchBlock.stitches.Length > 1) ) //must have 2 points to make a line
                        {
                            tempPen = new Pen(stitchBlock.color)
                            {
                                Width = threadThickness,
                            StartCap = LineCap.Round,
                            EndCap = LineCap.Round,
                            LineJoin = LineJoin.Round
                        }
                        ;
                            xGraph.SmoothingMode = SmoothingMode.AntiAlias;
                            xGraph.DrawLines(tempPen, stitchBlock.stitches);
                        }
                    }                   
                }
                
                return drawArea;
            
        }

       

        IEnumerable<stitchBlock> GetScaledPointBlock(float xscale, float yscale)
        {
            int ci = 0;
            return (_blocks.Where(b => b.Jumped != true).Select(b =>
                                 new stitchBlock()
                                     {
                                         color = b.color,
                                         colorIndex = ci++,
                                         stitches = b.AsPoints(xscale, yscale),
                                         stitchesInBlock = b.Count
                                     })).ToList();
        
        }

        public void FillStreamWithPng(Stream stream)
        {
            using (var bitmap = DesignToBitmap(0.1F))
            {



                var format = bitmap.GetImageFormat();
                //create a collection of all parameters that we will pass to the encoder
               // EncoderParameters encoderParams = new EncoderParameters();

                //set the quality parameter for the codec
                //encoderParams.Param[0] = qualityParam;
                //var encoder = GetEncoderInfo("image/png");
                //encoderParams.Param[0] = new EncoderParameter(encoder, 0x00);
                bitmap.Save(stream, ImageFormat.Png);
                //bitmap.Save(@"c:\test.png", ImageFormat.Png);

            }
        }

    }

   

}
