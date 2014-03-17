using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Mayando.Web.DataAccess
{
    public class SqlCmdParameter
    {
        public SqlDbType Type { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}