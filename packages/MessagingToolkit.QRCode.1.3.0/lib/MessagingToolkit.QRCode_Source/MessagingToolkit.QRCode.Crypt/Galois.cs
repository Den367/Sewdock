namespace MessagingToolkit.QRCode.Crypt
{
    using System;

    public sealed class Galois
    {
        private int[] expTbl = new int[510];
        private static readonly Galois instance = new Galois();
        private int[] logTbl = new int[0x100];
        public static readonly int POLYNOMIAL = 0x1d;

        private Galois()
        {
            this.InitGaloisTable();
        }

        public bool CalcSyndrome(int[] data, int length, int[] syn)
        {
            int num = 0;
            for (int i = 0; i < syn.Length; i++)
            {
                int index = 0;
                for (int j = 0; j < length; j++)
                {
                    index = data[j] ^ ((index == 0) ? 0 : this.expTbl[this.logTbl[index] + i]);
                }
                syn[i] = index;
                num |= index;
            }
            return (num == 0);
        }

        public int Div(int a, int b)
        {
            return ((a == 0) ? 0 : this.expTbl[(this.logTbl[a] - this.logTbl[b]) + 0xff]);
        }

        public int DivExp(int a, int b)
        {
            return ((a == 0) ? 0 : this.expTbl[(this.logTbl[a] - b) + 0xff]);
        }

        public static Galois GetInstance()
        {
            return instance;
        }

        private void InitGaloisTable()
        {
            int index = 1;
            for (int i = 0; i < 0xff; i++)
            {
                this.expTbl[i] = this.expTbl[0xff + i] = index;
                this.logTbl[index] = i;
                index = index << 1;
                if ((index & 0x100) != 0)
                {
                    index = (index ^ POLYNOMIAL) & 0xff;
                }
            }
        }

        public int Inv(int a)
        {
            return this.expTbl[0xff - this.logTbl[a]];
        }

        public int Mul(int a, int b)
        {
            return (((a == 0) || (b == 0)) ? 0 : this.expTbl[this.logTbl[a] + this.logTbl[b]]);
        }

        public int MulExp(int a, int b)
        {
            return ((a == 0) ? 0 : this.expTbl[this.logTbl[a] + b]);
        }

        public void MulPoly(int[] seki, int[] a, int[] b)
        {
            for (int i = 0; i < seki.Length; i++)
            {
                seki[i] = 0;
            }
            for (int j = 0; j < a.Length; j++)
            {
                if (a[j] != 0)
                {
                    int num3 = this.logTbl[a[j]];
                    int num4 = Math.Min(b.Length, seki.Length - j);
                    for (int k = 0; k < num4; k++)
                    {
                        if (b[k] != 0)
                        {
                            seki[j + k] ^= this.expTbl[num3 + this.logTbl[b[k]]];
                        }
                    }
                }
            }
        }

        public int ToExp(int a)
        {
            return this.expTbl[a];
        }

        public int ToLog(int a)
        {
            return this.logTbl[a];
        }

        public int ToPos(int length, int a)
        {
            return ((length - 1) - this.logTbl[a]);
        }
    }
}

