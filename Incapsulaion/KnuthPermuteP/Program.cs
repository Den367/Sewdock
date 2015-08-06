using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnuthPermuteP
{
    class Program
    {
        static void Main(string[] args)
        {
            var perm = new SimpleChange();
            perm.solution2<char>(new char[] { 'a', 'b' });
            Console.ReadLine();
        }
    }
}
