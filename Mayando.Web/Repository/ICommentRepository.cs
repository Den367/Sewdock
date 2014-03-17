using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mayando.Web.Models;

namespace Mayando.Web.Repository
{
    public interface ICommentRepository
    {
        JelleDruyts.Web.Mvc.Paging.IPagedList<Comment> GetCommentsForEmbro(int id, int pageNo, int pageSize);
        bool EditComment(Comment comment);
        void DeleteComment(int id);
    }
}