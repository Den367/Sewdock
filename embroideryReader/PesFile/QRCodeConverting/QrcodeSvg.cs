using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EmbroideryFile.QR;

namespace EmbroideryFile.QRCode
{
    public class QrcodeSvg

    {
        readonly ISvgEncode _encoder;
        readonly IQRCodeStitchGeneration _stitchGen;
       


        #region [Public Properties]
        public QRCodeStitchInfo QrStitchInfo {
            set
            {
                _stitchGen.Info = value;   

            }
        }



        #endregion [Public Properties]
        public QrcodeSvg()
        {
            _encoder = new SvgEncoder();
            _stitchGen = new QrCodeStitcher();
         
        }

        #region [Public Methods]       
        public void FillStreamWithSvg(Stream stream,int size)
        {
             _encoder.FillStreamWithSvgFromCoordsLists(stream, size, _stitchGen.GetQRCodeStitchBlocks());

        }

        public string ReadSvgStringFromStream()
        {
            return _encoder.ReadSvgString();
        }

        /// <summary>
        /// Provide Svg representation of stitches for QR code 
        /// </summary>
        /// <returns></returns>
        public string GetSvg(int size)
        {
            return _encoder.GetSvgFromCoordsLists(size,_stitchGen.GetQRCodeStitchBlocks());
        
        }
        #endregion [Public Methods]

    }
}
