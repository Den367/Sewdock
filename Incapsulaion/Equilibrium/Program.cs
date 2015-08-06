using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equilibrium
{
    class Program
    {
        static void Main(string[] args)
        {
            var equilibrium = new Solution();

        }
    }

    class Solution
    {
        
        public int solution(int[] A)
        {
            int len = A.Length;
            if (len == 0) return -1;
            if (len == 1) return 0;
            if (len == 2 ) {
                if (A[0] == 0 )                return 1;
                if (A[1] == 0 )                return 0;
            }            
            if (len == 3 && ((A[0] - A[2]) == 0)) return 1;
            long tailSum = 0;
            long sumTotal = calcSumTotal(A);
            if ((len > 1) && (A[len - 1] == 0) && (sumTotal == 0)) return len -1;
            Console.WriteLine("sumTotal {0}", sumTotal);
            for (int i = 0; i < A.Length; i++)
            {
                long cur = A[i];
                sumTotal -= cur;
                if ((tailSum) == (sumTotal))
                {
                    Console.WriteLine("Result {0}", i);
                    return i;
                }
                else
                {                   
                    tailSum += cur;

                }

            }
            return -1;
        }

        public long calcSumTotal(int[] A)
        {
            long result = 0;
            for (int i = 0; i < A.Length; i++)
            {
                result += A[i];               
            }
            return result;
        }
    }
}
