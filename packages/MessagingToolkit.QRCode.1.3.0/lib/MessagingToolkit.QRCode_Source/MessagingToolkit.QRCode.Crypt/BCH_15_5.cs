namespace MessagingToolkit.QRCode.Crypt
{
    using System;

    public sealed class BCH_15_5
    {
        private static readonly int GX = 0x137;
        private static readonly BCH_15_5 instance = new BCH_15_5();
        private int[] trueCodes = new int[0x20];

        private BCH_15_5()
        {
            this.MakeTrueCodes();
        }

        private static int CalcDistance(int c1, int c2)
        {
            int num = 0;
            for (int i = c1 ^ c2; i != 0; i = i >> 1)
            {
                if ((i & 1) != 0)
                {
                    num++;
                }
            }
            return num;
        }

        public int Decode(int data)
        {
            data &= 0x7fff;
            for (int i = 0; i < this.trueCodes.Length; i++)
            {
                int num2 = this.trueCodes[i];
                if (CalcDistance(data, num2) <= 3)
                {
                    return num2;
                }
            }
            return -1;
        }

        public int Encode(int data)
        {
            return this.trueCodes[data & 0x1f];
        }

        public static BCH_15_5 GetInstance()
        {
            return instance;
        }

        private void MakeTrueCodes()
        {
            for (int i = 0; i < this.trueCodes.Length; i++)
            {
                this.trueCodes[i] = this.SlowEncode(i);
            }
        }

        private int SlowEncode(int data)
        {
            int num = 0;
            data = data << 5;
            for (int i = 0; i < 5; i++)
            {
                num = num << 1;
                data = data << 1;
                if (((num ^ data) & 0x400) != 0)
                {
                    num ^= GX;
                }
            }
            return ((data & 0x7c00) | (num & 0x3ff));
        }
    }
}

