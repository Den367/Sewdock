using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mayando.Web.DataAccess
{
     [CLSCompliant(false)]
    public class EmbroCommandFormer:CommandFormer
    {
         public EmbroCommandFormer(SqlConnection conn) : base(conn)
    {
    }
        //[emb].[GetEmbroByPageNoSize]
         public SqlCommand GetReadEmbroByIdCommand(int id)
         {
             if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
             _sqlCommand.Parameters.Clear();
             _sqlCommand.Parameters.Add(new SqlParameter("Id", id));
             _sqlCommand.CommandType = CommandType.StoredProcedure;
             _sqlCommand.CommandText = "emb.GetEmbroById";
             return _sqlCommand;
         }

         public SqlCommand GetReadEmbroByPageNoSize(int page,int size, string criteria)
         {
             if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
             _sqlCommand.Parameters.Clear();
             _sqlCommand.Parameters.Add(new SqlParameter("PageNo", page));
             _sqlCommand.Parameters.Add(new SqlParameter("PageSize", size));
             _sqlCommand.Parameters.Add(new SqlParameter("Criteria", criteria));
             var total = new SqlParameter("TotalItem", SqlDbType.Int) {Direction = ParameterDirection.Output};
             _sqlCommand.Parameters.Add(total);
             _sqlCommand.CommandType = CommandType.StoredProcedure;
             _sqlCommand.CommandText = "emb.GetEmbroByPageNoSize";
             return _sqlCommand;
         }

        public SqlCommand GetReadEmbroBinaryDataByIdCommand(int id)
        {
            if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
            _sqlCommand.Parameters.Clear();
            _sqlCommand.Parameters.Add(new SqlParameter("Id", id));
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = "emb.GetEmbroBinaryDataById";
            return _sqlCommand;
        }

        public SqlCommand GetReadEmbroByCountPageCommand(int size, int page, string criteria)
        {
            if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
            _sqlCommand.Parameters.Clear();
            _sqlCommand.Parameters.Add(new SqlParameter("PageNo", page));
            _sqlCommand.Parameters.Add(new SqlParameter("PageSize", size));
            _sqlCommand.Parameters.Add(new SqlParameter("Criteria", criteria));
            var total = new SqlParameter("TotalItem", SqlDbType.Int) {Direction = ParameterDirection.Output};
            _sqlCommand.Parameters.Add(total);

            //Uncomment this line to return the proper output value.
            //myCommand.Parameters["@out"].Direction = ParameterDirection.Output;
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = "emb.GetEmbrosByPage";
            return _sqlCommand;
        }
    }
}