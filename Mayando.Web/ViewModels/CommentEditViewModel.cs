using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mayando.Web.ViewModels
{
    public class CommentEditViewModel:CaptchaBase
    {
        public Mayando.Web.Models.Comment Comment { get; private set; }
        public string ReturnURL {get;private set;}

        public CommentEditViewModel( int embroId,int id = 0)
        {
           
            Comment = new Models.Comment { EmbroId = embroId,Id = id};
        }
    }
}