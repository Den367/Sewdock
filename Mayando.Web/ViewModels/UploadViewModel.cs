using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mayando.Web.Infrastructure;

namespace Mayando.Web.ViewModels
{
    public class UploadViewModel
    {
        public HttpPostedFileBase Data { get; set; }

        public UploadViewModel()
        { 
        
        }
    }
}