using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class PageViewModel
    {
        public Page Page { get; private set; }
        public PhotoDetailsViewModel Photo { get; private set; }

        public PageViewModel(Page page, PhotoDetailsViewModel photo)
        {
            this.Page = page;
            this.Photo = photo;
        }
    }
}