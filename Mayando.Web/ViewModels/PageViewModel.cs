using Myembro.Models;

namespace Myembro.ViewModels
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