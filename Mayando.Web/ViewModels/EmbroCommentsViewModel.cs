using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using JelleDruyts.Web.Mvc.Paging;
using Myembro.Models;
using Web.Ajax.Paging;

namespace Myembro.ViewModels
{
    public class EmbroCommentsViewModel
    {
        public EmbroCommentsViewModel(int embroID, IPagedList<Comment> comments)
        {
            EmbroID = embroID;
            Comments = comments;
        }
        public int EmbroID { get; private set; }
        public IPagedList<Myembro.Models.Comment> Comments { get; private set; }
    }
}