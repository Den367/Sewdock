namespace MessagingToolkit.QRCode.Codec
{
    using MessagingToolkit.QRCode.Codec.Data;
    using MessagingToolkit.QRCode.Codec.Reader;
    using MessagingToolkit.QRCode.Crypt;
    using MessagingToolkit.QRCode.ExceptionHandler;
    using MessagingToolkit.QRCode.Geom;
    using MessagingToolkit.QRCode.Helper;
    using System;
    using System.Collections;
    using System.Text;

    public class QRCodeDecoder
    {
        internal static DebugCanvas canvas = new DebugCanvasAdapter();
        internal QRCodeImageReader imageReader;
        internal ArrayList lastResults = ArrayList.Synchronized(new ArrayList(10));
        internal int numLastCorrectionFailures;
        internal int numTryDecode = 0;
        internal QRCodeSymbol qrCodeSymbol;
        internal ArrayList results = ArrayList.Synchronized(new ArrayList(10));

        internal virtual int[] CorrectDataBlocks(int[] blocks)
        {
            RsDecode decode;
            int num7;
            int num10;
            int num11;
            int num12;
            int num = 0;
            int num2 = 0;
            int dataCapacity = this.qrCodeSymbol.DataCapacity;
            int[] numArray = new int[dataCapacity];
            int numErrorCollectionCode = this.qrCodeSymbol.NumErrorCollectionCode;
            int numRSBlocks = this.qrCodeSymbol.NumRSBlocks;
            int num6 = numErrorCollectionCode / numRSBlocks;
            if (numRSBlocks == 1)
            {
                decode = new RsDecode(num6 / 2);
                num7 = decode.Decode(blocks);
                if (num7 > 0)
                {
                    num += num7;
                    return blocks;
                }
                if (num7 < 0)
                {
                    num2++;
                }
                return blocks;
            }
            int num8 = dataCapacity % numRSBlocks;
            if (num8 == 0)
            {
                int num9 = dataCapacity / numRSBlocks;
                int[][] numArray2 = new int[numRSBlocks][];
                for (num10 = 0; num10 < numRSBlocks; num10++)
                {
                    numArray2[num10] = new int[num9];
                }
                int[][] numArray3 = numArray2;
                for (num10 = 0; num10 < numRSBlocks; num10++)
                {
                    num11 = 0;
                    while (num11 < num9)
                    {
                        numArray3[num10][num11] = blocks[(num11 * numRSBlocks) + num10];
                        num11++;
                    }
                    decode = new RsDecode(num6 / 2);
                    num7 = decode.Decode(numArray3[num10]);
                    if (num7 > 0)
                    {
                        num += num7;
                    }
                    else if (num7 < 0)
                    {
                        num2++;
                    }
                }
                num12 = 0;
                for (num10 = 0; num10 < numRSBlocks; num10++)
                {
                    num11 = 0;
                    while (num11 < (num9 - num6))
                    {
                        numArray[num12++] = numArray3[num10][num11];
                        num11++;
                    }
                }
            }
            else
            {
                int num13 = dataCapacity / numRSBlocks;
                int num14 = (dataCapacity / numRSBlocks) + 1;
                int num15 = numRSBlocks - num8;
                int[][] numArray4 = new int[num15][];
                for (int i = 0; i < num15; i++)
                {
                    numArray4[i] = new int[num13];
                }
                int[][] numArray5 = numArray4;
                int[][] numArray6 = new int[num8][];
                for (int j = 0; j < num8; j++)
                {
                    numArray6[j] = new int[num14];
                }
                int[][] numArray7 = numArray6;
                for (num10 = 0; num10 < numRSBlocks; num10++)
                {
                    int num18;
                    if (num10 < num15)
                    {
                        num18 = 0;
                        num11 = 0;
                        while (num11 < num13)
                        {
                            if (num11 == (num13 - num6))
                            {
                                num18 = num8;
                            }
                            numArray5[num10][num11] = blocks[((num11 * numRSBlocks) + num10) + num18];
                            num11++;
                        }
                        decode = new RsDecode(num6 / 2);
                        num7 = decode.Decode(numArray5[num10]);
                        if (num7 > 0)
                        {
                            num += num7;
                        }
                        else if (num7 < 0)
                        {
                            num2++;
                        }
                    }
                    else
                    {
                        num18 = 0;
                        num11 = 0;
                        while (num11 < num14)
                        {
                            if (num11 == (num13 - num6))
                            {
                                num18 = num15;
                            }
                            numArray7[num10 - num15][num11] = blocks[((num11 * numRSBlocks) + num10) - num18];
                            num11++;
                        }
                        num7 = new RsDecode(num6 / 2).Decode(numArray7[num10 - num15]);
                        if (num7 > 0)
                        {
                            num += num7;
                        }
                        else if (num7 < 0)
                        {
                            num2++;
                        }
                    }
                }
                num12 = 0;
                for (num10 = 0; num10 < numRSBlocks; num10++)
                {
                    if (num10 < num15)
                    {
                        num11 = 0;
                        while (num11 < (num13 - num6))
                        {
                            numArray[num12++] = numArray5[num10][num11];
                            num11++;
                        }
                    }
                    else
                    {
                        for (num11 = 0; num11 < (num14 - num6); num11++)
                        {
                            numArray[num12++] = numArray7[num10 - num15][num11];
                        }
                    }
                }
            }
            if (num > 0)
            {
                canvas.Print(Convert.ToString(num) + " data errors corrected successfully.");
            }
            else
            {
                canvas.Print("No errors found.");
            }
            this.numLastCorrectionFailures = num2;
            return numArray;
        }

        public virtual string Decode(QRCodeImage qrCodeImage)
        {
            sbyte[] src = this.DecodeBytes(qrCodeImage);
            byte[] dst = new byte[src.Length];
            Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
            return Encoding.GetEncoding(StringHelper.GuessEncoding(dst)).GetString(dst);
        }

        internal virtual DecodeResult Decode(QRCodeImage qrCodeImage, Point adjust)
        {
            DecodeResult result;
            try
            {
                if (this.numTryDecode == 0)
                {
                    canvas.Print("Decoding started");
                    int[][] image = this.imageToIntArray(qrCodeImage);
                    this.imageReader = new QRCodeImageReader();
                    this.qrCodeSymbol = this.imageReader.GetQRCodeSymbol(image);
                }
                else
                {
                    canvas.Print("--");
                    canvas.Print("Decoding restarted #" + this.numTryDecode);
                    this.qrCodeSymbol = this.imageReader.GetQRCodeSymbolWithAdjustedGrid(adjust);
                }
            }
            catch (SymbolNotFoundException exception)
            {
                throw new DecodingFailedException(exception.Message);
            }
            canvas.Print("Created QRCode symbol.");
            canvas.Print("Reading symbol.");
            canvas.Print("Version: " + this.qrCodeSymbol.VersionReference);
            canvas.Print("Mask pattern: " + this.qrCodeSymbol.MaskPatternRefererAsString);
            int[] blocks = this.qrCodeSymbol.Blocks;
            canvas.Print("Correcting data errors.");
            blocks = this.CorrectDataBlocks(blocks);
            try
            {
                sbyte[] decodedBytes = this.GetDecodedByteArray(blocks, this.qrCodeSymbol.Version, this.qrCodeSymbol.NumErrorCollectionCode);
                result = new DecodeResult(this, decodedBytes, this.numLastCorrectionFailures);
            }
            catch (InvalidDataBlockException exception2)
            {
                canvas.Print(exception2.Message);
                throw new DecodingFailedException(exception2.Message);
            }
            return result;
        }

        public virtual string Decode(QRCodeImage qrCodeImage, Encoding encoding)
        {
            sbyte[] src = this.DecodeBytes(qrCodeImage);
            byte[] dst = new byte[src.Length];
            Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
            return encoding.GetString(dst);
        }

        public virtual sbyte[] DecodeBytes(QRCodeImage qrCodeImage)
        {
            DecodeResult result;
            Point[] adjustPoints = this.AdjustPoints;
            ArrayList list = ArrayList.Synchronized(new ArrayList(10));
            this.numTryDecode = 0;
            while (this.numTryDecode < adjustPoints.Length)
            {
                try
                {
                    result = this.Decode(qrCodeImage, adjustPoints[this.numTryDecode]);
                    if (result.IsCorrectionSucceeded)
                    {
                        return result.DecodedBytes;
                    }
                    list.Add(result);
                    canvas.Print("Decoding succeeded but could not correct");
                    canvas.Print("all errors. Retrying..");
                }
                catch (DecodingFailedException exception)
                {
                    if (exception.Message.IndexOf("Finder Pattern") >= 0)
                    {
                        throw exception;
                    }
                }
                finally
                {
                    this.numTryDecode++;
                }
            }
            if (list.Count == 0)
            {
                throw new DecodingFailedException("Give up decoding");
            }
            int num = -1;
            int numCorrectionFailures = 0x7fffffff;
            for (int i = 0; i < list.Count; i++)
            {
                result = (DecodeResult) list[i];
                if (result.NumCorrectionFailures < numCorrectionFailures)
                {
                    numCorrectionFailures = result.NumCorrectionFailures;
                    num = i;
                }
            }
            canvas.Print("All trials need for correct error");
            canvas.Print("Reporting #" + num + " that,");
            canvas.Print("corrected minimum errors (" + numCorrectionFailures + ")");
            canvas.Print("Decoding finished.");
            return ((DecodeResult) list[num]).DecodedBytes;
        }

        internal virtual sbyte[] GetDecodedByteArray(int[] blocks, int version, int numErrorCorrectionCode)
        {
            sbyte[] dataByte;
            QRCodeDataBlockReader reader = new QRCodeDataBlockReader(blocks, version, numErrorCorrectionCode);
            try
            {
                dataByte = reader.DataByte;
            }
            catch (InvalidDataBlockException exception)
            {
                throw exception;
            }
            return dataByte;
        }

        internal virtual string GetDecodedString(int[] blocks, int version, int numErrorCorrectionCode)
        {
            string dataString = null;
            QRCodeDataBlockReader reader = new QRCodeDataBlockReader(blocks, version, numErrorCorrectionCode);
            try
            {
                dataString = reader.DataString;
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new InvalidDataBlockException(exception.Message);
            }
            return dataString;
        }

        internal virtual int[][] imageToIntArray(QRCodeImage image)
        {
            int width = image.Width;
            int height = image.Height;
            int[][] numArray = new int[width][];
            for (int i = 0; i < width; i++)
            {
                numArray[i] = new int[height];
            }
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {
                    numArray[k][j] = image.GetPixel(k, j);
                }
            }
            return numArray;
        }

        internal virtual Point[] AdjustPoints
        {
            get
            {
                ArrayList list = ArrayList.Synchronized(new ArrayList(10));
                for (int i = 0; i < 4; i++)
                {
                    list.Add(new Point(1, 1));
                }
                int num2 = 0;
                int num3 = 0;
                for (int j = 0; j > -4; j--)
                {
                    for (int m = 0; m > -4; m--)
                    {
                        if ((m != j) && (((m + j) % 2) == 0))
                        {
                            list.Add(new Point(m - num2, j - num3));
                            num2 = m;
                            num3 = j;
                        }
                    }
                }
                Point[] pointArray = new Point[list.Count];
                for (int k = 0; k < pointArray.Length; k++)
                {
                    pointArray[k] = (Point) list[k];
                }
                return pointArray;
            }
        }

        public static DebugCanvas Canvas
        {
            get
            {
                return canvas;
            }
            set
            {
                canvas = value;
            }
        }

        internal class DecodeResult
        {
            internal sbyte[] decodedBytes;
            private QRCodeDecoder enclosingInstance;
            private int numCorrectionFailures;

            public DecodeResult(QRCodeDecoder enclosingInstance, sbyte[] decodedBytes, int numCorrectionFailures)
            {
                this.InitBlock(enclosingInstance);
                this.decodedBytes = decodedBytes;
                this.numCorrectionFailures = numCorrectionFailures;
            }

            private void InitBlock(QRCodeDecoder enclosingInstance)
            {
                this.enclosingInstance = enclosingInstance;
            }

            public virtual sbyte[] DecodedBytes
            {
                get
                {
                    return this.decodedBytes;
                }
            }

            public QRCodeDecoder EnclosingInstance
            {
                get
                {
                    return this.enclosingInstance;
                }
            }

            public virtual bool IsCorrectionSucceeded
            {
                get
                {
                    return (this.enclosingInstance.numLastCorrectionFailures == 0);
                }
            }

            public virtual int NumCorrectionFailures
            {
                get
                {
                    return this.numCorrectionFailures;
                }
            }
        }
    }
}

