using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Mayando.Web.Models;

namespace Mayando.Web.DataAccess
{
    [ CLSCompliant(false)]
    public class CommandFormer:IDisposable
    {
        protected readonly SqlConnection _sqlConnection;
        //protected readonly SqlCommand cmd;
        public CommandFormer(SqlConnection connection)
        {
            _sqlConnection =connection;
           
        }


        protected SqlCommand GetCommand()
        {
            if (_sqlConnection.State != ConnectionState.Open)
            {
                _sqlConnection.Open();
            }
            if (null != _sqlConnection) return _sqlConnection.CreateCommand();
            else return null;
        }
      

        public SqlCommand GetReadMenuCommand()
        {
            var cmd = GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.Menu";
            return cmd;
        }

        public SqlCommand GetGetCommentsCommand(int id, int pageNo, int pageSize)
        {
            var cmd = GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.GetCommentByEmbroID";
            cmd.Parameters.Add(new SqlParameter("EmbroID", id));
            cmd.Parameters.Add(new SqlParameter("PageNo", pageNo));
            cmd.Parameters.Add(new SqlParameter("PageSize", pageSize));
            var commentCountParam = new SqlParameter("CommentCount", SqlDbType.Int);
            commentCountParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(commentCountParam);
            return cmd;
        }

        public SqlCommand GetEditCommentCommand(Comment comment)
        {
            var cmd = GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.CommentEdit";
            cmd.Parameters.Add(new SqlParameter("EmbroID", comment.EmbroId));
            cmd.Parameters.Add(new SqlParameter("ExternalID", comment.ExternalID));
            cmd.Parameters.Add(new SqlParameter("Text", comment.Text));
            cmd.Parameters.Add(new SqlParameter("AuthorIsOwner", comment.AuthorIsOwner));
            cmd.Parameters.Add(new SqlParameter("AuthorName", comment.AuthorName));
            cmd.Parameters.Add(new SqlParameter("AuthorEmail", comment.AuthorEmail));
            cmd.Parameters.Add(new SqlParameter("AuthorUrl", comment.AuthorUrl));
            cmd.Parameters.Add(new SqlParameter("DatePublished", comment.DatePublished));

            return cmd;
        }

        public SqlCommand GetReadSettingCommand(string Scope)
        {
            var cmd = GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.GetSetting";
            SqlParameter param = new SqlParameter("Scope", SqlDbType.VarChar, 256);
            param.Value = Scope;
            cmd.Parameters.Add(param);
            return cmd;
        }



        public void Dispose()
        {
            if (_sqlConnection != null) _sqlConnection.Close();
        }
    }
}