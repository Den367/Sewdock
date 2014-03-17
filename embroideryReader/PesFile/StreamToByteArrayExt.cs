using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EmbroideryFile
{
 
     public static class StreamToByteArrayExt
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            using (stream)
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    stream.Position = 0;
                     stream.CopyTo(memStream);
                     return memStream.ToArray();
                }
            }
        }

      

    }

}
