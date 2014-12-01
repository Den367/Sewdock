using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myembro.Models;
using Web.Ajax.Paging;

namespace Myembro.Repository
{
    public interface ICommentRepository
    {
       IPagedList<Comment> GetCommentsForEmbro(int id, int pageNo, int pageSize,string userId);
        bool EditComment(Comment comment);
        bool DeleteComment(int id, string userId);
        /// <summary>
        /// Returns <see cref="Comment"/> by id
        /// </summary>
        /// <param name="id">CommentID</param>
        /// <returns></returns>
        Comment GetCommentByID(int id);
    }
}