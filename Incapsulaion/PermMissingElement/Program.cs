using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermMissingElement
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        class Solution
        {
            public int solution(int[] A)
            {
                // write your code in C# 6.0 with .NET 4.5 (Mono)
                var len = A.Length;
                if (len == 0) return 1;
                var wholeSum = GetWholeSum(len);
                Console.WriteLine("wholeSum:{0}", wholeSum);
                var arraySum = GetArraySum(A);
                Console.WriteLine("arraySum:{0}", arraySum);

                return wholeSum - arraySum;
            }

            int GetArraySum(int[] A)
            {
                int result = 0;
                for (int i = 0; i < A.Length; i++)
                { result = result + A[i]; }
                return result;
            }

            int GetWholeSum(int len)
            {
                int result = 0;
                int i;
                for (i = 1; i <= len; i++)
                { result = result + i; }
                return result + i;
            }

        }
    }
}
