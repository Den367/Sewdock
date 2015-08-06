using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnuthPermuteP
{
    internal class SimpleChange
    {
        internal void solution(int[] A)
        {
            int len = A.Length;
            int j = len;
            int s = 0;
            int q;
            int[] c = new int[len + 1];
            int[] o = new int[len + 1];
            int[] a = new int[len + 1];
            for (int i = 1; i <= len; i++)
            {
                c[i] = i;
                a[i] = A[i-1];
                o[i] = -1;
            }
            int z = a[len];
            while (j != 1)
            {
                P2: if (a[1] == z) goto P35;
            P3:
                {
                    for (j = len -1; j>= 1; j--)
                    {
                        a[j + 1] = a[j];
                        a[j] = z;
                        Display(a, j, j+1);
                        Console.WriteLine();
                    }
                    j = len - 1;
                    s = 1;
                    goto P4;
                }
                P35:
                {
                    for(j = 1; j< len; j++)
                    {
                        a[j] = a[j + 1];
                        a[j + 1] = z;
                        Display(a, j+1, j );
                        Console.WriteLine();
                    }
                    j = len - 1;
                    s = 0;
                }
            P4:
                {
                    q = c[j] + o[j];
                    if (q == 0) goto P6;
                    if (q > j) goto P7;
                }
            P5:
                {
                    var n =  c[j] + s;
                    var m =  q + s;
                    var buff = a[n];
                    a[n] = a[m];
                    a[m] = buff;
                    c[j] = q;
                    Display(a, n, m);
                    Console.WriteLine();
                    goto P2;
                }
            P6:
                {
                    /* P6 */
                    if (j == 1) break;
                    else s = s + 1;
                }
            P7:

                {
                    /*P7*/
                    o[j] = -o[j];
                    j = j - 1;
                    goto P4;
                }

            }

        }

        /// <summary>
        /// Plain chain transition
        /// </summary>
        /// <param name="A"></param>
        internal void solution2<T>(T[] A)
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

            while (m != n)
            {
                m = m + 1;
                d = d/m;
                k = 0;
                T3:
                while (k < N)
                {
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
                        }
                     //   if (k < N) goto T3;
                     //   else goto T2;
                    }
                }
            }

            for (int i = 1; i < t.Length; i ++)
            {
                var u = t[i] - 1;
                if (!A[u].Equals(A[u + 1])  )
                {
                    var buff = A[u];
                    A[u] = A[u + 1];
                    A[u + 1] = buff;
                    Display(A, u, u + 1);
                    Console.WriteLine();
                }
            }

        }

        int getFact(int N)
        {
            int result = 1;
            for (int i = 2; i <= N; i++) result = result * i;
            return result;
        }

        void permutation(int k, int[] A)
        {
            for (int j = 1; j < A.Length; ++j)
            {
                var buff = A[k % (j + 1)];
                A[k % (j + 1)] = A[j];
                A[j] = buff;              
                k = k / (j + 1);
            }
        }
        private void Display<T>(T[] A, int j, int k)
        {
           
            for (int i = 0; i < A.Length; i++)
            {

                if (i == j)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(A[i]);
                    Console.ResetColor();
                }
                else if (i == k)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(A[i]);
                    Console.ResetColor();
                }
                else
                    Console.Write(A[i]);
            }
            //Console.Write("\t");
            //Console.WriteLine();
        }


    }
}
