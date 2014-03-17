using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;

namespace EmbroideryFile
{
    //[SafeCritical]
    public interface IGetEmbroideryData
    {

        EmbroideryData Design { get; }
        void LoadEmbroidery(Stream stream);
    }
}
