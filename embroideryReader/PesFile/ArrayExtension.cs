using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EmbroideryFile
{
    public static class ArrayExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this Array target)
        {
            foreach (var item in target)
                yield return (T)item;
        }

        public static Stream ToStream(this byte[] data)
        {
            return new MemoryStream(data);
        }

    }
}
