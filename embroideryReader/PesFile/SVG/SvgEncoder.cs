using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Drawing;

namespace EmbroideryFile
{
    public class SvgEncoder :ISvgEncode
    {
        private const int Width = 600;
        private const int Height = 600;
        XmlTextWriter _xWrite;
        Stream stream;
        EmbroideryData embro;

        public SvgEncoder()
        {
            _xWrite = null;
            //stream = new MemoryStream();
           
            //xWrite = new XmlTextWriter(stream,Encoding.Unicode);
        }

        /// <summary>
        /// Encodes embroidery to svg
        /// </summary>
        /// <param name="svgStream">destination svg <see cref="Stream"/></param>
        /// <param name="embroData"> <see cref="EmbroideryData"/></param>
        public SvgEncoder(Stream svgStream, EmbroideryData embroData)
        {
            _xWrite = null;
            stream = svgStream;
            embro = embroData;
        }

        #region [Public Properties]

        #endregion [Public Properties]

        #region [Public Methods]

        public void WriteSvg()
        {
            double xScale = 1.0;
            double yScale = 1.0;
            
          
            WriteSvgStartWithAttr();
           
            WriteWidth(Width);
            WriteHeight(Height);            


            WriteViewBox(0 , 0 , embro.Width , embro.Height );
            //double shiftX = (embro.Xmax - embro.Xmin) / 2;
            double shiftX = - embro.Xmin;
            //double shiftY = (embro.Ymax - embro.Ymin) / 2;
            double shiftY = - embro.Ymin;
            _xWrite.WriteStartElement("g");
       
            xScale = 1.0; yScale = 1.0;
            _xWrite.WriteAttributeString("transform", string.Format("scale({0},{1})",  xScale, yScale));
            // polylines
            WriteStitchBlocks(embro);

            _xWrite.WriteEndElement(); // g
            _xWrite.WriteEndElement(); // svg
            //xWrite.WriteEndDocument();
            _xWrite.Flush();

        }

        void WriteSvgStartWithAttr()
        {
            if (stream == null) stream = new MemoryStream();
            if (_xWrite != null) _xWrite.Close();
            _xWrite = new XmlTextWriter(stream, Encoding.UTF8);
            _xWrite.Formatting = Formatting.Indented;
          
            _xWrite.WriteStartElement("svg");
            _xWrite.WriteAttributeString("xmlns", "svg", null, "http://www.w3.org/2000/svg");
            _xWrite.WriteAttributeString("xmlns", null, null, "http://www.w3.org/2000/svg");
            _xWrite.WriteAttributeString("pagecolor", "#ffffff");

            _xWrite.WriteAttributeString("bordercolor", "#666666");
            _xWrite.WriteAttributeString("borderopacity", "1.0");

            _xWrite.WriteAttributeString("version", "1.1");
            _xWrite.WriteAttributeString("showgrid", "false");
            _xWrite.WriteAttributeString("preserveAspectRatio", "xMidYMid meet");

        }
        void WriteSvgBoundAttr(double size, List<CoordsBlock> blocks)
        {

           

            int minX = XCoordMin(blocks);
            int minY = YCoordMin(blocks);
            int maxX = XCoordMax(blocks);
            int maxY = YCoordMax(blocks);
            int width = maxX - minX; int height = maxY - minY;

            double h = (double)height;
            double w = (double)width;
            double m = (h > w) ? h : w;
            double scale = size / m;
            double xScale = scale;
            double yScale = -scale;
            double newW = w * scale;
            double newH = h * scale;
            double newMinX = minX * scale;
            double newMinY = minY * scale;
            double newMaxX = maxX * scale;
            double newMaxY = maxY * scale;
            double translateX = -newMinX;
            double translateY = -newMinY;

            WriteWidth(size);
            WriteHeight(size);
            //WriteViewBox(minX, minY, width, height);
            WriteViewBox(0, 0, width, height);
            
            _xWrite.WriteStartElement("g");           
            
            
        }

        void WriteSvgForCoordList(double size, List<CoordsBlock> blocks)
        {
            WriteSvgStartWithAttr();
            WriteSvgBoundAttr(size, blocks);
            WriteStitchBlocksZeroShifted(blocks);
            WriteG_Svg_Endelement();
        }


        void WriteG_Svg_Endelement()
        {
            _xWrite.WriteEndElement(); // g
            _xWrite.WriteEndElement(); // svg
            _xWrite.Flush();
        }

        /// <summary>
        /// Read created Svg as a string
        /// </summary>
        /// <returns></returns>
        public string ReadSvgString()
        {
            if (stream != null)
            {
                StreamReader reader = new StreamReader(stream);
                stream.Position = 0;
                return reader.ReadToEnd();
            }
            return string.Empty;
        }

        public void FillStreamWithSvgFromCoordsLists(Stream strm, int size, List<CoordsBlock> blocks)
        {
            stream = strm;

            WriteSvgForCoordList((double)size, blocks);
        
        }

        public string GetSvgFromCoordsLists(int size, List<CoordsBlock> blocks)
        {
            WriteSvgForCoordList((double)size, blocks);
            return ReadSvgString();
        }

        #endregion [Public]
        void WriteWidth(double width)
        {
            _xWrite.WriteAttributeString("width", width.ToString());
          

        }

        void WriteHeight(double height)
        {
            _xWrite.WriteAttributeString("height", height.ToString());        

        }


     
      

      

        void WriteViewBox(float minX, float minY, float width, float height)
        {
           _xWrite.WriteAttributeString("viewBox", string.Format("{0} {1} {2} {3}",minX, minY,width, height));     
        }





        void FillLinesInfo(List<Coords> coords, Color colour, int needle)
        {
            if (coords.Count == 0) return;

            StringBuilder draw = new StringBuilder();

            coords.ForEach(p => draw.AppendFormat("{0} {1},", p.X, p.Y));
            draw.Remove(draw.Length - 1, 1); // remove last comma
            _xWrite.WriteStartElement("polyline");
            _xWrite.WriteAttributeString("id", string.Format("needle_{0}", needle));
            _xWrite.WriteAttributeString("stroke",  string.Format("rgb({0},{1},{2})", colour.R, colour.G, colour.B));
            _xWrite.WriteAttributeString("fill", "none");
            _xWrite.WriteAttributeString("points", draw.ToString());
            _xWrite.WriteEndElement();

        }
     

        void WriteStitchBlocks(EmbroideryData info)
        {
            WriteStitchBlocksZeroShifted(info.Blocks);
       
        }


        void WriteStitchBlocks(List<CoordsBlock> blocks)
        {

            int ci = 0;
            if (blocks.Count > 0)
                blocks.ForEach(block =>
                {
                    if (!(block.Jumped || block.Stop))
                    {
                        FillLinesInfo(block, block.Color, ci);
                        ci++;
                    }
                });


        }

        void WriteStitchBlocksZeroShifted(List<CoordsBlock> blocks)
        {
            int minX = XCoordMin(blocks);
            int minY = YCoordMin(blocks);
            ShiftXY(blocks, - minX, -minY);
            int x = minX;
            int y = minY;
            minX = XCoordMin(blocks);
             minY = YCoordMin(blocks);
            WriteStitchBlocks(blocks);
        }

      

        int XCoordMin(List<CoordsBlock> blocks)
        {
            return blocks.SelectMany(block => block.AsEnumerable()).Min(coord => coord.X);
        }

        int YCoordMin(List<CoordsBlock> blocks)
        {
            return blocks.SelectMany(block => block.AsEnumerable()).Min(coord => coord.Y);
        }

        int XCoordMax(List<CoordsBlock> blocks)
        {
            return blocks.SelectMany(block => block.AsEnumerable()).Max(coord => coord.X);
        }

        int YCoordMax(List<CoordsBlock> blocks)
        {
            return blocks.SelectMany(block => block.AsEnumerable()).Max(coord => coord.Y);
        }

       

        void ShiftXY(List<CoordsBlock> blocks, int x, int y)
        {
            blocks.SelectMany(block => block.AsEnumerable()).ToList().ForEach(coord => { coord.Y += y; coord.X += x; });
        }

        public  void Dispose()
        {
            if (stream != null) stream.Close();
            if (_xWrite != null) _xWrite.Close();
        }
    }
}

