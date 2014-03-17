using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EmbroideryFile.QR;

namespace EmbroideryFile.QRCode
{
    public class QrcodeDst
    {
        DstFile _dst;

        readonly IQRCodeStitchGeneration _stitchGen;
        #region [Public Properties]
        public QRCodeStitchInfo QrStitchInfo
        {
            set
            {
                _stitchGen.Info = value;

            }
        }



        #endregion [Public Properties]


          public QrcodeDst()
        {
            _dst = new DstFile();
            _stitchGen = new QrCodeStitcher();

        }

        #region [Public Methods]       
        public void FillStreamWithDst(Stream stream)
        {          
             
                 
                 _dst.WriteStitchesToDstStream(_stitchGen.GetQRCodeStitchBlocks(),stream );
            
        }

      

     
        #endregion [Public Methods]

    }
}
