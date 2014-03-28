using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JelleDruyts.Web.Mvc.Paging;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class EmbroCommentsViewModel
    {
        public EmbroCommentsViewModel(int embroID, IPagedList<Comment> comments)
        {
            EmbroID = embroID;
            Comments = comments;
        }
        public int EmbroID { get; private set; }
        public IPagedList<Mayando.Web.Models.Comment> Comments { get; private set; }
    }
}