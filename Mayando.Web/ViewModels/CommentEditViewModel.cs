using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mayando.Web.ViewModels
{
    public class CommentEditViewModel:CaptchaBase
    {
        public Mayando.Web.Models.Comment Comment { get; private set; }
        public string PageURL {get;private set;}

        public CommentEditViewModel(string url, int id)
        {
            PageURL = url;

            Comment = new Models.Comment() { EmbroId = id };
        }
    }
}