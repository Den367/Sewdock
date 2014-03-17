namespace MessagingToolkit.QRCode.Crypt
{
    using System;

    public class RsEncode
    {
        private int[] encodeGx;
        private static readonly Galois galois = Galois.GetInstance();
        private int npar;
        public static readonly int RS_PERM_ERROR = -1;

        public RsEncode(int npar)
        {
            this.npar = npar;
            this.MakeEncodeGx();
        }

        public int Encode(int[] data, int[] parity)
        {
            return this.Encode(data, data.Length, parity, 0);
        }

        public int Encode(int[] data, int length, int[] parity)
        {
            return this.Encode(data, length, parity, 0);
        }

        public int Encode(int[] data, int length, int[] parity, int parityStartPos)
        {
            if ((length < 0) || ((length + this.npar) > 0xff))
            {
                return RS_PERM_ERROR;
            }
            int[] sourceArray = new int[this.npar];
            for (int i = 0; i < length; i++)
            {
                int num2 = data[i];
                int a = sourceArray[0] ^ num2;
                for (int j = 0; j < (this.npar - 1); j++)
                {
                    sourceArray[j] = sourceArray[j + 1] ^ galois.Mul(a, this.encodeGx[j]);
                }
                sourceArray[this.npar - 1] = galois.Mul(a, this.encodeGx[this.npar - 1]);
            }
            if (parity != null)
            {
                Array.Copy(sourceArray, 0, parity, parityStartPos, this.npar);
            }
            return 0;
        }

        private void MakeEncodeGx()
        {
            this.encodeGx = new int[this.npar];
            this.encodeGx[this.npar - 1] = 1;
            for (int i = 0; i < this.npar; i++)
            {
                int b = galois.ToExp(i);
                for (int j = 0; j < (this.npar - 1); j++)
                {
                    this.encodeGx[j] = galois.Mul(this.encodeGx[j], b) ^ this.encodeGx[j + 1];
                }
                this.encodeGx[this.npar - 1] = galois.Mul(this.encodeGx[this.npar - 1], b);
            }
        }
    }
}

