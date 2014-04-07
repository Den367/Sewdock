using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mayando.Web.ViewModels
{
    public class EmbroConfirmDeleteViewModel
    {
        public int ID { get; private set; }
        public string ReturnUrl { get; private set; }

        public EmbroConfirmDeleteViewModel(int id, string returnUrl)
        {
            ID = id;
            ReturnUrl = returnUrl;
        }
    }
}