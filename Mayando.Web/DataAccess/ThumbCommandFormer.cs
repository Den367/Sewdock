using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mayando.Web.DataAccess
{
     [CLSCompliant(false)]
    public class ThumbCommandFormer:CommandFormer
    {
        public ThumbCommandFormer(SqlConnection conn) : base(conn)
    {
    }

        public SqlCommand GetReadThumbsByCountPageCommand(int size, int page, string criteria, Guid userID)
        {
            var cmd = GetCommand();
            cmd.Parameters.Add(new SqlParameter("PageNo", page));
            cmd.Parameters.Add(new SqlParameter("PageSize", size));
            cmd.Parameters.Add(new SqlParameter("Criteria", criteria));
            cmd.Parameters.Add(new SqlParameter("UserID", userID));
            var total = new SqlParameter("TotalItem", SqlDbType.Int);
            total.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(total);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.GetThumbsByPage";
            return cmd;
        }
    }
}