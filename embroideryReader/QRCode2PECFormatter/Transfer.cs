using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PesFile;

namespace QRCode2PECFormatter
{
    public class Transfer
    {

        public void CreatePecFromHtmlTableFile(string srcFileName, string dstFileName)
        {
            FileOperator fileOperator = new FileOperator();
   
            QRMatrix matrix = new QRMatrix(fileOperator.GetFileContent(srcFileName));

            QRCodeStitchInfo qrInfo = new QRCodeStitchInfo() { dX = 20, dY = 2, cellSize = 25, Matrix = matrix, DesignName = "QR_CODE" };

            QRCodeStitcher stitcher = new QRCodeStitcher(qrInfo);

            PecBuilder pec = new PecBuilder(stitcher, fileOperator.CreateStream(dstFileName));
            pec.WritePecStructureToStream();
        }

        public void CreatePesFromHtmlTableFile(string srcFileName, string dstFileName)
        {
            FileOperator fileOperator = new FileOperator();
            string table = string.Empty;


            QRMatrix matrix = new QRMatrix(fileOperator.GetFileContent(srcFileName));

            QRCodeStitchInfo qrInfo = new QRCodeStitchInfo() { dX = 20, dY = 2, cellSize = 25, Matrix = matrix, DesignName = "Qr_Code" };

            QRCodeStitcher stitcher = new QRCodeStitcher(qrInfo);


            PesBuilder pes = new PesBuilder(stitcher, fileOperator.CreateStream(dstFileName));
            pes.WritePesStructureToStream();
        }


        public void CreateQRCodePesFromDataString(string DataToEncode, string dstFileName)
        {
            FileOperator fileOperator = new FileOperator();
            string table = string.Empty;

            QRCodeCreator qrCreator = new QRCodeCreator();

            QRMatrix matrix = new QRMatrix();
            matrix.Matrix = qrCreator.GetQRCodeMatrix(DataToEncode);

            QRCodeStitchInfo qrInfo = new QRCodeStitchInfo() { dX = 20, dY = 2, cellSize = 25, Matrix = matrix, DesignName = "Qr_Code" };

            QRCodeStitcher stitcher = new QRCodeStitcher(qrInfo);


            PesBuilder pes = new PesBuilder(stitcher, fileOperator.CreateStream(dstFileName));
            pes.WritePesStructureToStream();
        }


        public void CreateQRCodePesFromTextDataFile(string srcFileName, string dstFileName)
        {
            FileOperator fileOperator = new FileOperator();

            QRCodeCreator qrCreator = new QRCodeCreator();

            QRMatrix matrix = new QRMatrix();
            matrix.Matrix = qrCreator.GetQRCodeMatrix(fileOperator.GetFileContent(srcFileName));

            QRCodeStitchInfo qrInfo = new QRCodeStitchInfo() { dX = 25, dY = 2, cellSize = 25, Matrix = matrix, DesignName = "QR_CODE" };

            QRCodeStitcher stitcher = new QRCodeStitcher(qrInfo);


            PesBuilder pes = new PesBuilder(stitcher, fileOperator.CreateStream(dstFileName));
            pes.WritePesStructureToStream();
        }
    }
}
