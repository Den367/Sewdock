using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using JelleDruyts.Web.Mvc.Paging;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Web.Ajax.Paging;

namespace Mayando.Web.Repository
{
     [CLSCompliant(false)]
    public class CommentRepository:RepositoryBase ,ICommentRepository
    {
        #region [Comments]

        public IPagedList<Comment> GetCommentsForEmbro(int id, int pageNo, int pageSize)
        {

            using (var cmd = factory.Commands.GetGetCommentsCommand(id, pageNo, pageSize))
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (!reader.Read()) return null;
                var comments = ReadComments(reader);
                reader.Close();
                var result = new PagedList<Comment>(comments, pageNo, pageSize, (int)cmd.Parameters["CommentCount"].Value);


                return result;

            }

        }

        public bool EditComment(Comment comment)
        {
            try
            {
                var cmd = factory.Commands.GetEditCommentCommand(comment);
                cmd.ExecuteScalar();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);

                return false;
            }

        }

         public bool DeleteComment(int id, Guid? userID)
         {

             try
             {
                 var cmd = factory.Commands.GetDeleteCommentCommand(id, userID);
                 cmd.ExecuteScalar();
                 var result = cmd.Parameters["Result"].Value;
                 return Convert.ToBoolean(result);
                
             }
             catch (Exception ex)
             {
                 Logger.LogException(ex);

                 return false;
             }
         }

         #endregion[Comments]

        private IList<Comment> ReadComments(SqlDataReader reader)
        {
            var result = new List<Comment>();

            do result.Add(ReadComment(reader));
            while (reader.Read());
            return result;
        }

        private Comment ReadComment(SqlDataReader reader)
        {
            var comment = new Comment
                {
                    EmbroId = (int) GetInt32FromReader(reader, "EmbroId"),
                    Id = (int) GetInt32FromReader(reader, "Id"),
                    Text = GetStringFromReader(reader, "Text"),
                    AuthorIsOwner = (bool) GetBooleanFromReader(reader, "AuthorIsOwner"),
                    AuthorName = GetStringFromReader(reader, "AuthorName"),
                    AuthorEmail = GetStringFromReader(reader, "AuthorEmail"),
                    AuthorUrl = GetStringFromReader(reader, "AuthorUrl"),
                    DatePublished = (DateTimeOffset) GetDateTimeOffsetFromReader(reader, "DatePublished")
                };
            return comment;

        }
    }
}