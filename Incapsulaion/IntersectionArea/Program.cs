using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace IntersectionArea
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new SquareOfRectIntersectonCalculator();
            int[] vertices = new int[8]{20,40,40,20,30,30,50,10};
            Console.WriteLine("{0}", calc.solution(vertices));
            Console.WriteLine("{0}", calc.solution(new int[8] { 7, -3, 9, -8, 5, -4, 13, -6 }));
            Console.WriteLine("{0}", calc.solution(new int[8] { 3, 10, 6, 7, 2, 9, 5, 8 }));
            Console.WriteLine("{0}", calc.solution(new int[8] { 7, 8, 12, 3, 8, 4, 11, 7 }));
            Console.ReadLine();
        }

        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern int Square(int number);
    }
}
