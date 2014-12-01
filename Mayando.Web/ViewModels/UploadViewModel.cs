using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Myembro.Infrastructure;

namespace Myembro.ViewModels
{
    public class UploadViewModel
    {
        public HttpPostedFileBase Data { get; set; }

        public UploadViewModel()
        { 
        
        }
    }
}