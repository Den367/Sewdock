using System.Collections.Generic;


namespace EmbroideryFile
{
    public interface IQRCodeStitchGeneration
    {
        QRCodeStitchInfo Info { get; set; }      
       List<List<Coords>> GetQRCodeStitches();
       List<CoordsBlock> GetQRCodeStitchBlocks();
       List<CoordsBlock> GetQRCodeInvertedYStitchBlocks();
       int GetDesignXOffset { get; set; }
       int GetDesignYOffset{ get; set; }
    }
}
