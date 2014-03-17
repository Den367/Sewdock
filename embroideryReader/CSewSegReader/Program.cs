using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSewSegReader
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var file = new FileStream(args[0], FileMode.Open))
            {
                //var reader = new StreamReader(file, Encoding.GetEncoding(1251));
                //fileEncoding = Encoding.GetEncoding(Config.CodePage);
               BinaryReader reader = new BinaryReader(file, Encoding.UTF8);
               var sewSeg = new CSewSeg(reader);
               sewSeg.Read();
               Console.ReadLine();

            }
        }
    }
}
