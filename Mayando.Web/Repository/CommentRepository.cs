using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using JelleDruyts.Web.Mvc.Paging;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;

namespace Mayando.Web.Repository
{
     [CLSCompliant(false)]
    public class CommentRepository:RepositoryBase ,ICommentRepository
    {
        #region [Comments]

        public JelleDruyts.Web.Mvc.Paging.IPagedList<Comment> GetCommentsForEmbro(int id, int pageNo, int pageSize)
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

        public void DeleteComment(int id)
        {
            throw new NotImplementedException();
        }

        #endregion[Comments]

        private IList<Comment> ReadComments(SqlDataReader reader)
        {
            var result = new List<Comment>();

            do
            {

                result.Add(ReadComment(reader));

            } while (reader.Read());
            return result;
        }

        private Comment ReadComment(SqlDataReader reader)
        {
            var comment = new Comment();
            if (!IsDBNull(reader, "EmbroId")) comment.Id = reader.GetInt32(reader.GetOrdinal("EmbroId"));

            if (!IsDBNull(reader, "Text")) comment.Text = reader.GetString(reader.GetOrdinal("Text"));
            if (!IsDBNull(reader, "AuthorIsOwner"))
                comment.AuthorIsOwner = reader.GetBoolean(reader.GetOrdinal("AuthorIsOwner"));
            if (!IsDBNull(reader, "AuthorName")) comment.AuthorName = reader.GetString(reader.GetOrdinal("AuthorName"));
            if (!IsDBNull(reader, "AuthorEmail"))
                comment.AuthorEmail = reader.GetString(reader.GetOrdinal("AuthorEmail"));
            if (!IsDBNull(reader, "AuthorUrl")) comment.AuthorUrl = reader.GetString(reader.GetOrdinal("AuthorUrl"));
            if (!IsDBNull(reader, "DatePublished"))
                comment.DatePublished = reader.GetDateTimeOffset(reader.GetOrdinal("DatePublished"));
            return comment;

        }
    }
}