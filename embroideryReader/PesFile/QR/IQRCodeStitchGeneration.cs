using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmbroideryFile
{
    interface IQRCodeStitchGeneration
    {
        QRCodeStitchInfo Info { get; set; }      
       List<List<Coords>> GetQRCodeStitches();
       List<CoordsBlock> GetQRCodeStitchBlocks();
    }
}
