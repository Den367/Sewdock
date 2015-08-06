using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeforeAfter
{
    class Program
    {
        static void Main(string[] args)
        {
            var diferencesMEqualPoint = new FindDiffrentNumPos();
            Console.WriteLine(diferencesMEqualPoint.solution(new int[] { 1, 5, 5, 3, 4, 2 }, 5));
            Console.WriteLine(diferencesMEqualPoint.solution(new int[] { 1, 5, 4, 2 }, 5));
            Console.WriteLine(diferencesMEqualPoint.solution(new int[] { 1, 5, 4, 2, 5}, 5));
            Console.ReadLine();
        }
    }
}
