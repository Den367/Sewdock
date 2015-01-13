using System;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Myembro.Models;

namespace Myembro.DataAccess
{
    [ CLSCompliant(false)]
    public class CommandFormer:IDisposable
    {
        protected SqlConnection _sqlConnection;
        //protected readonly SqlCommand cmd;
        private object obj;
        public CommandFormer(SqlConnection connection)
        {
            _sqlConnection =connection;
           obj = new object();
        }


        protected SqlCommand GetCommand()
        {
            _sqlConnection = new SqlConnection { ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString };

            if (null != _sqlConnection)
            {

                _sqlConnection.Open();

                return _sqlConnection.CreateCommand();
            }
            return null;


            //return _sqlConnection.CreateCommand();
        }


        public SqlCommand GetSPCommand(string spName)
        {
            var cmd = GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spName;
            return cmd;
        }

        public SqlCommand GetReadMenuCommand(string culture)
        {
            var cmd = GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.Menu";
            cmd.Parameters.Add(new SqlParameter("Culture", culture));
            return cmd;
        }

        public SqlCommand GetGetCommentsCommand(int id, int pageNo, int pageSize, string userId)
        {
            var cmd = GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.GetCommentByEmbroID";
            cmd.Parameters.Add(new SqlParameter("EmbroID", id));
            cmd.Parameters.Add(new SqlParameter("PageNo", pageNo));
            cmd.Parameters.Add(new SqlParameter("PageSize", pageSize));
            if (userId != null) cmd.Parameters.Add(new SqlParameter("UserID", userId));
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
            cmd.Parameters.Add(new SqlParameter("CommentID", comment.Id));
            cmd.Parameters.Add(new SqlParameter("UserID", comment.UserID));
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

        public SqlCommand GetDeleteCommentCommand(int id, string userID)
        {
            var cmd = GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.CommentDelete";
            cmd.Parameters.Add(new SqlParameter("CommentID", id));
            cmd.Parameters.Add(new SqlParameter("userID", userID));
            var resultParam = new SqlParameter("Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(resultParam);

            return cmd;
        }


        public SqlCommand GetGetCommentCommand(int id)
        {
            var cmd = GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "emb.GetCommentByID";
            cmd.Parameters.Add(new SqlParameter("CommentID", id));
          
            return cmd;
        }




        protected void AddPAram(SqlCommand cmd, string paramName, SqlDbType paramValueType, ParameterDirection direction = ParameterDirection.Input, int size = Int32.MaxValue)
        {
            var param = new SqlParameter(paramName, paramValueType,size);
            param.Direction = direction;
            cmd.Parameters.Add(param);

        }

        public void Dispose()
        {
            if (_sqlConnection != null) _sqlConnection.Close();
        }
    }
}