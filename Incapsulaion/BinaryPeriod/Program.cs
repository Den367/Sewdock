using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BinaryPeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            var detector = new BinaryPeriodDetector();
            Console.WriteLine(detector.Solution(102));
            Console.WriteLine(detector.Solution(21));
            Console.WriteLine(detector.Solution(955));
            Console.ReadLine();
        }

        //[MethodImpl(MethodImplOptions.ForwardRef)]
        //public static extern int Square(int number);
    }
}
