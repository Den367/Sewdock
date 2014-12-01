using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myembro.Models
{
    public class CaptchaMap
    {
        Dictionary<int,char>  symbols = new Dictionary<int, char>{{1,'u'},{2,'v'},{3,'w'},{4,'x'},{5,'y'},{6,'z'},{7,'{'},{8,'t'},{9,'}'}};
    }
}