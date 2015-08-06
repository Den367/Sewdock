using System;
using System.Collections.Generic;
using System.Linq;


namespace MissingNumberFinder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sol = new Solution();
            Console.WriteLine("{0}", sol.solution(new int[] {1, 3, 6, 4, 1, 2}));
            Console.ReadLine();
        }
    }

    internal class Solution
    {
        private bool[] indexes;
        private int _length;
        public int solution(int[] A)
        {
            _length = A.Length;
            if (_length == 1)
            {
                var a0 = A[0];
                if ( a0 < 1 || a0 > 1) return 1;
                if (a0 == 1) return 2;
            }
            var minimum = findMin(A);
            if (minimum > 1) return 1;
            indexes = new bool[_length];
            
            fillIndexes(A);
            return GetMissing(A);
        }

        private int getIndex(int key)
        {
            return (key.GetHashCode())  % _length;
        }

        private int findMin(int[] A)
        {
            int min = A[0];
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] < min)
                {
                    min = A[i];

                }
            }
            return min;
        }

        private void fillIndexes(int[] A)
        {
           
            for (int i = 0; i < A.Length; i++)
            {
                indexes[getIndex(A[i])] = true;
            }
           
        }

        bool CheckIndex(int number)
        {
            return indexes[getIndex(number)];

        }

        private int GetMissing(int[] A)
        {
            var max = findMax(A);
            int i;
            for ( i = 1; i < max; i++)
            {
                if (CheckIndex(i) == false) return i;
            }
            return i++;
        }
        private int findMax(int[] A)
        {
            int max = A[0];
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] > max)
                {
                    max = A[i];

                }
            }
            return max;
        }
    }


}
