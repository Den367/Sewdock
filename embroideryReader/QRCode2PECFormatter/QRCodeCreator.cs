using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MessagingToolkit.QRCode.Codec;
//using MessagingToolkit.QRCode.Codec.Data;
//using MessagingToolkit.QRCode.Helper;
 
namespace QRCode2PECFormatter
{
   internal class QRCodeCreator
    {

        public bool[][] GetQRCodeMatrix(string DataToEncode)
        {

            if (string.IsNullOrEmpty(DataToEncode))
            return null;

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.CharacterSet = "UTF-8";          
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;           
                qrCodeEncoder.QRCodeScale = 1;                     
                qrCodeEncoder.QRCodeVersion = 0;          
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;                 
            return qrCodeEncoder.CalQrcode(Encoding.UTF8.GetBytes (DataToEncode));
          
        }
    }
}
