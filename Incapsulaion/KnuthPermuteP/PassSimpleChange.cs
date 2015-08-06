using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnuthPermuteP
{
    class PassSimpleChange
    {
        void solution2(int[] A)
        {
            int n = A.Length;
            int N = getFact(n);
            int d = N / 2;
            int[] t = new int[N];
            t[d] = 1;
            int m = 2;
            int k = 0;
            int j = 0;
            T2:
            {
                while (m != n)
                {
                    m = m + 1;
                    d = d / m;
                }
                k = 0;
            }
            T3:
            {
                k = k + d;
                j = m - 1;
                while (j > 0)
                {
                    t[k] = j;
                    j = j - 1;
                    k = k + d;
                }
            }
            T4:
            {
                t[k] += 1;
                k = k + d;
            }
            T5:
            {
                while (j < m - 1)
                {
                    j = j + 1;
                    t[k] = j;
                    k = k + d;
                    if (k < N) goto T3;
                    else goto T2;
                }
            }
        }

        int getFact(int N)
        {
            int result = 1;
            for (int i = 1; i < N; i++) result *= i;
            return result; 
        }
    }
}
