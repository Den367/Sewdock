using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QRCode2PECFormatter
{
    public class FileOperator
    {

        public string GetFileContent(string fileName)
        {
            string result = null;

            using (var file = new FileStream(fileName, FileMode.Open))
            {
                //var reader = new StreamReader(file, Encoding.GetEncoding(1251));
                //fileEncoding = Encoding.GetEncoding(Config.CodePage);
                var reader = new StreamReader(file, Encoding.UTF8);
                result = reader.ReadToEnd();
            }

            return result;
        }

        public Stream CreateStream(string filePath)
        {
            Stream stream = File.Create(filePath);
            return stream;
        }

       

        public void SaveStreamToFile(Stream stream, string filename)
        {
            using (Stream destination = File.Create(filename))
                Write(stream, destination);
        }

        public void Write(Stream from, Stream to)
        {
            for (int a = from.ReadByte(); a != -1; a = from.ReadByte())
                to.WriteByte((byte)a);
        }


    }
}
