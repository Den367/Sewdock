using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Myembro.DataAccess
{
     [CLSCompliant(false)]
    public class EmbroCommandFormer:CommandFormer
    {
         public EmbroCommandFormer(SqlConnection conn) : base(conn)
    {
    }
        //[emb].[GetEmbroByPageNoSize]
         public SqlCommand GetEmbroDeleteByIdCommand(int id)
         {
             var cmd = GetCommand();
             cmd.Parameters.Add(new SqlParameter("Id", id));
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "emb.EmbroDeleteById";
             return cmd;
         }

         public SqlCommand GetReadEmbroByIdCommand(int id)
         {
             var cmd = GetCommand();
             cmd.Parameters.Add(new SqlParameter("Id", id));
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "emb.GetEmbroById";
             return cmd;
         }

         public SqlCommand GetReadEmbroJsonByIdCommand(int id)
         {
             var cmd = GetCommand();
             cmd.Parameters.Add(new SqlParameter("Id", id));
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "emb.GetEmbroJsonById";
             return cmd;
         }

         public SqlCommand GetReadEmbroByPageNoSize(int page,int size, string criteria)
         {
             var cmd = GetCommand();
             cmd.Parameters.Add(new SqlParameter("PageNo", page));
             cmd.Parameters.Add(new SqlParameter("PageSize", size));
             cmd.Parameters.Add(new SqlParameter("Criteria", criteria));
             var total = new SqlParameter("TotalItem", SqlDbType.Int) {Direction = ParameterDirection.Output};
             cmd.Parameters.Add(total);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "emb.GetEmbroByPageNoSize";
             return cmd;
         }

        public SqlCommand GetReadEmbroBinaryDataByIdCommand(int id)
        {
            var cmd = GetCommand();
            cmd.Parameters.Add(new SqlParameter("Id", id));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.GetEmbroBinaryDataById";
            return cmd;
        }

        public SqlCommand GetReadEmbroByCountPageCommand(int size, int page, string criteria)
        {
            var cmd = GetCommand();
            cmd.Parameters.Add(new SqlParameter("PageNo", page));
            cmd.Parameters.Add(new SqlParameter("PageSize", size));
            cmd.Parameters.Add(new SqlParameter("Criteria", criteria));
            var total = new SqlParameter("TotalItem", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(total);

            //Uncomment this line to return the proper output value.
            //myCommand.Parameters["@out"].Direction = ParameterDirection.Output;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.GetEmbrosByPage";
            return cmd;
        }

        public SqlCommand GetReadEmbroPagingInfoCommand(int embroID, int pageSize, string criteria, string userID = null)
        {
            var cmd = GetCommand();
            cmd.Parameters.Add(new SqlParameter("EmbroID", embroID));
            cmd.Parameters.Add(new SqlParameter("PageSize", pageSize));
            cmd.Parameters.Add(new SqlParameter("Criteria", criteria));
            cmd.Parameters.Add(new SqlParameter("UserID", userID));
            cmd.Parameters.Add(new SqlParameter("TotalItem", SqlDbType.Int) { Direction = ParameterDirection.Output });
            cmd.Parameters.Add(new SqlParameter("PageNo", SqlDbType.Int) { Direction = ParameterDirection.Output });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.GetEmbroPagingInfo";
            return cmd;
        }
    }
}