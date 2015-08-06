using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace PowerOfMinusTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            for (int i = -10; i < 10; i++)
            {
                WeightList list = new WeightList();
                list.Solution(i);
            }
            Console.ReadLine();
        }

       
         
    }
}
