using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
//using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace EmbroideryFile
{
    /// <summary>
    /// Contains stitch blocks and color map info to create svg
    /// </summary>
    public class EmbroideryData : IDesignInfo
    {
        static XmlSerializer serializer = new XmlSerializer(typeof(EmbroideryData));
        #region [ctor]
        public EmbroideryData()
        {
            ColourInfo = new Dictionary<int, Color>();
            ColorMap = new Dictionary<int, int>();
            //stitchBlocks = new List<stitchBlock>();
            Blocks = new List<CoordsBlock>();
        }
        #endregion [ctor]

        
        #region [Lists]
        //[XmlIgnore]
        //public List<stitchBlock> stitchBlocks { get; set; }
        /// <summary>
        /// List of embroidery blocks
        /// </summary>
        /// 
        [XmlIgnore]
        public List<CoordsBlock> Blocks { get; set; }
        #endregion [Lists]
        #region [Dictionaries]
        /// <summary>
        /// Map of block (stitches between stops) to color index 
        /// </summary>
        [XmlIgnore]
        public Dictionary<int, int> ColorMap { get; set; }
        /// <summary>
        /// Dictionary of #RGB color representation by color index
        /// </summary>        
        [XmlIgnore]
        public Dictionary<int, Color> ColourInfo { get; set; }
        #endregion [Dictionaries]

        public string DesignName { get; set; }
        /// <summary>
        /// Width of embroidery design (0.1 mm)
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height of embroidery design (0.1 mm)
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Minimum value of horizontal stitch coords 
        /// </summary>
        public int Xmin { get; set; }
        /// <summary>
        /// Minimum value of vertical stitch coords 
        /// </summary>
        public int Ymin { get; set; }

        /// <summary>
        /// Maximum value of Y stitch coords
        /// </summary>
        public int Xmax { get; set; }
        /// <summary>
        /// Maximum value of Y stitch coords
        /// </summary>
        public int Ymax { get; set; }
        int _totalStiches;
        public int TotalStitchCount { get { return _totalStiches; } set { _totalStiches = value; } }
        int _colorChanges;
        public int ColorChangeCount { get { return _colorChanges; } set { _colorChanges = value; } }
        int _stitchBlocks;
        public int StitchBlockCount { get { return _stitchBlocks; } set { _stitchBlocks = value; } }
        int _jumpStitches;
        public int JumpsBlocksCount { get { return _jumpStitches; } set { _jumpStitches = value; } }
        
        public EmbroType Type { get; set; }


        public int GetTotalStitches()
        {
           
                _totalStiches = Blocks.SelectMany(block => block.AsEnumerable()).Count();
                return _totalStiches;
           
        }

        public int GetColorChanges()
        {
           
                _colorChanges = Blocks.Count(block => block.Stop == true);
                return _colorChanges;
           
        }

      

        public int GetBlocksCount()
        {
          
                _stitchBlocks = Blocks.Count();
                return _stitchBlocks;
            
        }

        public int GetJumpStitches()
        {
           
                _jumpStitches =Blocks.Count(block => block.Jumped == true);
                return _jumpStitches;
           
        }

        public int GetXCoordMin()
        {
           
                return Blocks.SelectMany(block => block.AsEnumerable()).Min(coord => coord.X);
            
        }

        public int GetYCoordMin()
        {
          
                return Blocks.SelectMany(block => block.AsEnumerable()).Min(coord => coord.Y);
           
        }
        public int GetXCoordMax()
        {
           
                return Blocks.SelectMany(block => block.AsEnumerable()).Max(coord => coord.X);
           
        }

        public int GetYCoordMax()
        {
           
                return Blocks.SelectMany(block => block.AsEnumerable()).Max(coord => coord.Y);
           
        }

        public void ShiftXY( int x, int y)
        {
            Blocks.SelectMany(block => block.AsEnumerable()).ToList().ForEach(coord => { coord.Y += y; coord.X += x; });
        }



        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(builder, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                serializer.Serialize(writer, this);
            }
            return builder.ToString();
        }
    }
}
