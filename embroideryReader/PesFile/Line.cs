using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmbroideryFile
{
    public struct Lane
    {
        public Coords Dot1 { get; set; }
        public Coords Dot2 { get; set; }
        public int Length { get; set; }
        public bool Lowest { get; set; }
    }
}
