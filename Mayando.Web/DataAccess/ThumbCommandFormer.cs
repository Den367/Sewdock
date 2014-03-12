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

        public SqlCommand GetReadThumbsByCountPageCommand(int size, int page, string criteria)
        {
            if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
            _sqlCommand.Parameters.Clear();
            _sqlCommand.Parameters.Add(new SqlParameter("PageNo", page));
            _sqlCommand.Parameters.Add(new SqlParameter("PageSize", size));
            _sqlCommand.Parameters.Add(new SqlParameter("Criteria", criteria));
            var total = new SqlParameter("TotalItem", SqlDbType.Int);
            total.Direction = ParameterDirection.Output;
            _sqlCommand.Parameters.Add(total);

            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = "emb.GetThumbsByPage";
            return _sqlCommand;
        }
    }
}