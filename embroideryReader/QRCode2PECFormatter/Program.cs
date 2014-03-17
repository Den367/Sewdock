using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PesFile;

namespace QRCode2PECFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length > 1)
            {
                Transfer QRCodeToEmbroideryFormatter = new Transfer();
                QRCodeToEmbroideryFormatter.CreateQRCodePesFromTextDataFile(args[0], args[1]);

                 // QRCodeToEmbroideryFormatter.CreatePesFromHtmlTableFile(args[0], args[1]);
                
            }

        }
    }
}
