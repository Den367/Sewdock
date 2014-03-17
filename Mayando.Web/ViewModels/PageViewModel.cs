using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class PageViewModel
    {
        public Page Page { get; private set; }
        public EmbroDetailsViewModel Embro { get; private set; }

        public PageViewModel(Page page, EmbroDetailsViewModel photo)
        {
            this.Page = page;
            this.Embro = photo;
        }
    }
}