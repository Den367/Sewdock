﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace EmbroideryFile
{
    public class stitchBlock
    {

        public Color color;
        public Int32 colorIndex;
        public Int32 stitchesInBlock;
        public Point[] stitches;
        public stitchBlock()
        {
            color = System.Drawing.Color.Black;
        }
    }
}
