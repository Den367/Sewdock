using System;
using System.IO;
using System.Text;


namespace QuickPerm
{
    class Program
    {
        static void Main(string[] args)
        {
            var anagram = new StringBuilder();
            using (TextReader stream = File.OpenText(@"d:\anagram.asc"))
            {               
                while (stream.Peek() >= 0)
                {
                    var c = stream.ReadLine();
                    anagram.Append(c);
                }
            }
            var len = anagram.Length;
            var chars = new char[len];
            anagram.CopyTo(0,chars,0,len);
            var perm = new Permutter();
            perm.solution3<char>(chars);
            Console.WriteLine();
            perm.solution2(new int[] { 1, 2, 3, 4 });
            Console.ReadLine();
        }
    }
}
