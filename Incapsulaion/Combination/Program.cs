using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Combination
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Combination combine = new Combination("abc");
            combine.Combine(0);
            Console.ReadLine();
        }

        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern int Square(int number);
    }

    internal class Combination
    {
        internal Combination(string input)
        {
            _in = input;
            _out = new StringBuilder();
        }
        private StringBuilder _out;
        private string _in;


        internal void Combine(int start)
        {
            for (int i = start; i < _in.Length - 1; ++i)
            {
                _out.Append(_in[i]);
                Console.WriteLine(_out);
                Combine(i + 1);
                _out.Remove(_out.Length - 1, 1);
            }
            _out.Append(_in[_in.Length - 1]);
            Console.WriteLine(_out);
            _out.Remove(_out.Length - 1, 1);
        }
    }
}
