using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ILApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Square(-2));
            Console.ReadLine();
        }

        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern int Square(int number);
    }
}
