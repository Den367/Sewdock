using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPerm
{
    internal class Permutter
    {
        internal void solution1<T>(T[] A)
        {
            int N = A.Length;
            int[] p = new int[N + 1];
            for (int i = 0; i < N + 1; i++)
            {
                p[i] = i;
            }
            int index = 1;
            int j;
            Display(A, -1, -1);
            Console.WriteLine();
            while (index < N)
            {
                p[index]--;
                if ((index%2) != 0) j = p[index]; // if index is odd then use number from p
                else j = 0;
                var buff = A[index];
                A[index] = A[j];
                A[j] = buff;
                Display(A, index, j);
                Display(p, index, -1);
               
                index = 1;
                while (p[index] == 0)
                {
                    p[index] = index;
                   
                    index++;
                }
                Display(p, index, -1);
                Console.WriteLine();
            }
        }

        private void Display<T>(T[] A, int j, int k)
        {Console.Write("\t");
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
            //Console.WriteLine();
        }


        /*
         The Countdown QuickPerm Algorithm:

       let a[] represent an arbitrary list of objects to permute
       let N equal the length of a[]
       create an integer array p[] of size N+1 to control the iteration     
       initialize p[0] to 0, p[1] to 1, p[2] to 2, ..., p[N] to N
       initialize index variable i to 1
       while (i < N) do {
          decrement p[i] by 1
          if i is odd, then let j = p[i] otherwise let j = 0
          swap(a[j], a[i])
          let i = 1
          while (p[i] is equal to 0) do {
             let p[i] = i
             increment i by 1
          } // end while (p[i] is equal to 0)
       } // end while (i < N)
         * 
         * 
         * The Counting QuickPerm Algorithm:

       let a[] represent an arbitrary list of objects to permute
       let N equal the length of a[]
       create an integer array p[] of size N to control the iteration       
       initialize p[0] to 0, p[1] to 0, p[2] to 0, ..., and p[N-1] to 0
       initialize index variable i to 1
       while (i < N) do {
          if (p[i] < i) then {
             if i is odd, then let j = p[i] otherwise let j = 0
             swap(a[j], a[i])
             increment p[i] by 1
             let i = 1 (reset i to 1)
          } // end if
          else { // (p[i] equals i)
             let p[i] = 0 (reset p[i] to 0)
             increment i by 1
          } // end else (p[i] equals i)
       } // end while (i < N)
         */

        internal void solution2<T>(T[] A)
        {
            int N = A.Length;
            int[] p = new int[N ];// already zero filled;
            //for (int i = 0; i < N; i++)
            //{
            //    p[i] = 0;
            //}
            int index = 1;
            int j;
            Display(A, -1, -1);
            Console.WriteLine();
            while (index < N)
            {
               
                if (p[index] < index)
                {
                    Console.WriteLine();
                    if ((index % 2) != 0) j = p[index];
                    else j = 0;
                    var buff = A[index];
                    A[index] = A[j];
                    A[j] = buff;
                    Display(A, index, j);
                    
                    p[index]++;
                    Display(p, index, -1);
                   
                    index = 1;                    
                }
                else
                {
                    p[index] = 0;
                    Display(p, index, -1);
                    index++;
                }
               
            }
        }

        /*
         let a[] represent an arbitrary list of objects to permute
   let N equal the length of a[]
   create an integer array p[] of size N+1 to represent the Base-N-Odometer
   initialize p[0] to 0, p[1] to 1, p[2] to 2, ..., p[N] to N
   initialize index variable i to 1
   while (i < N) do {
      decrement p[i] by 1
      reverse(a[0], a[i])
      let i = 1
      while (p[i] is equal to 0) do {  // Set i Using the Base-N-Odometer
         let p[i] = i
         increment i by 1
      } // end while (p[i] is equal to 0)
   } // end while (i < N)
         */
        /// <summary>
        /// not unique
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="A"></param>
        internal void solution3<T>(T[] A)
        {
            int N = A.Length;
            int[] p = new int[N + 1];
            for (int i = 0; i < N + 1; i++)
            {
                p[i] = i;
            }
            int index = 1;
            while( index < N)
            {
                p[index]--;
                T buff = A[index];
                A[index] = A[0];
                A[0] = buff;
                Display(A,index,0);
                index = 1;
                while (p[index] == 0)
                {
                    p[index] = index;
                    index++;

                }
            }
        }
    }
}
