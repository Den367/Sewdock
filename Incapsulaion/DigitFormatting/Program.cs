using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitFormatting
{
    class Program
    {
        static void Main(string[] args)
        {
            DigitSequencer formatter = new DigitSequencer();
            var rand = new Random();
            var input = rand.Next(10000000);
            Console.WriteLine("{0} {1}", input,formatter.GetSequence(input));
            Console.ReadLine();
        }
    }
}
