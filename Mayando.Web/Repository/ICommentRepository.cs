using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mayando.Web.Models;
using Web.Ajax.Paging;

namespace Mayando.Web.Repository
{
    public interface ICommentRepository
    {
       IPagedList<Comment> GetCommentsForEmbro(int id, int pageNo, int pageSize);
        bool EditComment(Comment comment);
        bool DeleteComment(int id, Guid? userId);
    }
}