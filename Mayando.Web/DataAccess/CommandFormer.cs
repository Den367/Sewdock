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
    public class CommandFormer
    {
        protected readonly SqlConnection _sqlConnection;
        protected readonly SqlCommand _sqlCommand;
        public CommandFormer(SqlConnection connection)
        {
            _sqlConnection =connection;
            _sqlCommand = _sqlConnection.CreateCommand();
            //_sqlCommand.Connection = connection;
        }

     

      

        public SqlCommand GetReadMenuCommand()
        {
            if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
            _sqlCommand.Parameters.Clear();
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = "emb.Menu";
            return _sqlCommand;
        }

        public SqlCommand GetGetCommentsCommand(int id, int pageNo, int pageSize)
        {
            if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
            _sqlCommand.Parameters.Clear();
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = "emb.GetCommentByEmbroID";

            _sqlCommand.Parameters.Add(new SqlParameter("EmbroID", id));
            _sqlCommand.Parameters.Add(new SqlParameter("PageNo", pageNo));
            _sqlCommand.Parameters.Add(new SqlParameter("PageSize", pageSize));
            var commentCountParam = new SqlParameter("CommentCount", SqlDbType.Int);
            commentCountParam.Direction = ParameterDirection.Output;
            _sqlCommand.Parameters.Add(commentCountParam);
            return _sqlCommand;
        }

        public SqlCommand GetEditCommentCommand(Comment comment)
        {
            if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
            _sqlCommand.Parameters.Clear();
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = "emb.CommentEdit";

            _sqlCommand.Parameters.Add(new SqlParameter("EmbroID", comment.EmbroId));
            _sqlCommand.Parameters.Add(new SqlParameter("ExternalID", comment.ExternalID));
            _sqlCommand.Parameters.Add(new SqlParameter("Text", comment.Text));
            _sqlCommand.Parameters.Add(new SqlParameter("UserName", comment.UserName));
            _sqlCommand.Parameters.Add(new SqlParameter("AuthorName", comment.AuthorName));
            _sqlCommand.Parameters.Add(new SqlParameter("AuthorEmail", comment.AuthorEmail));
            _sqlCommand.Parameters.Add(new SqlParameter("AuthorUrl", comment.AuthorUrl));
            _sqlCommand.Parameters.Add(new SqlParameter("DatePublished", comment.DatePublished));

            return _sqlCommand;
        }

        public SqlCommand GetReadSettingCommand(string Scope)
        {
            if (_sqlConnection.State != ConnectionState.Open) _sqlConnection.Open();
            _sqlCommand.Parameters.Clear();
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.CommandText = "dbo.GetSetting";
            SqlParameter param = new SqlParameter("Scope", SqlDbType.VarChar, 256);
            param.Value = Scope;
            _sqlCommand.Parameters.Add(param);
            return _sqlCommand;
        }
    }
}