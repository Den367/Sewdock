namespace MessagingToolkit.QRCode.Crypt
{
    using System;

    public class RsDecode
    {
        private static readonly Galois galois = Galois.GetInstance();
        private int npar;
        public static readonly int RS_CORRECT_ERROR = -2;
        public static readonly int RS_PERM_ERROR = -1;

        public RsDecode(int npar)
        {
            this.npar = npar;
        }

        public int CalcSigmaMBM(int[] sigma, int[] omega, int[] syn)
        {
            int[] sourceArray = new int[this.npar];
            int[] a = new int[this.npar];
            sourceArray[1] = 1;
            a[0] = 1;
            int num = 1;
            int num2 = 0;
            int num3 = -1;
            for (int i = 0; i < this.npar; i++)
            {
                int num5 = syn[i];
                int index = 1;
                while (index <= num2)
                {
                    num5 ^= galois.Mul(a[index], syn[i - index]);
                    index++;
                }
                if (num5 != 0)
                {
                    int b = galois.ToLog(num5);
                    int[] numArray3 = new int[this.npar];
                    index = 0;
                    while (index <= i)
                    {
                        numArray3[index] = a[index] ^ galois.MulExp(sourceArray[index], b);
                        index++;
                    }
                    int num8 = i - num3;
                    if (num8 > num2)
                    {
                        num3 = i - num2;
                        num2 = num8;
                        if (num2 > (this.npar / 2))
                        {
                            return -1;
                        }
                        for (index = 0; index <= num; index++)
                        {
                            sourceArray[index] = galois.DivExp(a[index], b);
                        }
                        num = num2;
                    }
                    a = numArray3;
                }
                Array.Copy(sourceArray, 0, sourceArray, 1, Math.Min(sourceArray.Length - 1, num));
                sourceArray[0] = 0;
                num++;
            }
            galois.MulPoly(omega, a, syn);
            Array.Copy(a, 0, sigma, 0, Math.Min(a.Length, sigma.Length));
            return num2;
        }

        private int ChienSearch(int[] pos, int n, int jisu, int[] sigma)
        {
            int a = sigma[1];
            if (jisu == 1)
            {
                if (galois.ToLog(a) >= n)
                {
                    return RS_CORRECT_ERROR;
                }
                pos[0] = a;
                return 0;
            }
            int num2 = jisu - 1;
            for (int i = 0; i < n; i++)
            {
                int num4 = 0xff - i;
                int num5 = 1;
                for (int j = 1; j <= jisu; j++)
                {
                    num5 ^= galois.MulExp(sigma[j], (num4 * j) % 0xff);
                }
                if (num5 == 0)
                {
                    int num7 = galois.ToExp(i);
                    a ^= num7;
                    pos[num2--] = num7;
                    if (num2 == 0)
                    {
                        if (galois.ToLog(a) >= n)
                        {
                            return RS_CORRECT_ERROR;
                        }
                        pos[0] = a;
                        return 0;
                    }
                }
            }
            return RS_CORRECT_ERROR;
        }

        public int Decode(int[] data)
        {
            return this.Decode(data, data.Length, false);
        }

        public int Decode(int[] data, int length)
        {
            return this.Decode(data, length, false);
        }

        public int Decode(int[] data, int length, bool noCorrect)
        {
            if ((length < this.npar) || (length > 0xff))
            {
                return RS_PERM_ERROR;
            }
            int[] syn = new int[this.npar];
            if (galois.CalcSyndrome(data, length, syn))
            {
                return 0;
            }
            int[] sigma = new int[(this.npar / 2) + 2];
            int[] omega = new int[(this.npar / 2) + 1];
            int jisu = this.CalcSigmaMBM(sigma, omega, syn);
            if (jisu <= 0)
            {
                return RS_CORRECT_ERROR;
            }
            int[] pos = new int[jisu];
            int num2 = this.ChienSearch(pos, length, jisu, sigma);
            if (num2 < 0)
            {
                return num2;
            }
            if (!noCorrect)
            {
                this.DoForney(data, length, jisu, pos, sigma, omega);
            }
            return jisu;
        }

        private void DoForney(int[] data, int length, int jisu, int[] pos, int[] sigma, int[] omega)
        {
            for (int i = 0; i < jisu; i++)
            {
                int a = pos[i];
                int num3 = 0xff - galois.ToLog(a);
                int num4 = omega[0];
                int index = 1;
                while (index < jisu)
                {
                    num4 ^= galois.MulExp(omega[index], (num3 * index) % 0xff);
                    index++;
                }
                int b = sigma[1];
                for (index = 2; index < jisu; index += 2)
                {
                    b ^= galois.MulExp(sigma[index + 1], (num3 * index) % 0xff);
                }
                data[galois.ToPos(length, a)] ^= galois.Mul(a, galois.Div(num4, b));
            }
        }
    }
}

