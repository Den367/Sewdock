using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Permutation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Permutter permutter = new Permutter("abcd");
            permutter.Permute();
            Console.ReadLine();
        }

        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern int Square(int number);
    }

    internal class Permutter
    {
        private bool[] used;
        private int _length;
        private StringBuilder output;
        private string _input;

        public Permutter(string input)
        {
            _input = input;
            output = new StringBuilder();
            _length = input.Length;
            used = new bool[_length];
            result = new string[_length*_length + 1];
        }

        private string[] result;

       
        public void Permute()
        {
            if (output.Length == _length)
            {
                //result[_count++] = output.ToString();
                Console.WriteLine(output);
               
                return;
            }
            for (int i = 0; i < _length; ++i)
            {
                if (used[i]) continue;
                output.Append(_input[i]);
                used[i] = true;
                Permute();
                used[i] = false;
                output.Remove(output.Length - 1, 1);
            }
        }
    }
}
