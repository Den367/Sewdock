using Mayando.Web.Infrastructure;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class PhotoThumbnailViewModel
    {
        public Photo Photo { get; private set; }
        public NavigationContextType? NavigationContextType { get; private set; }
        public string NavigationContextCriteria { get; private set; }
        public int? NavigationContextSlideshowDelay { get; private set; }

        public PhotoThumbnailViewModel(Photo photo)
            : this(photo, null)
        {
        }

        public PhotoThumbnailViewModel(Photo photo, NavigationContext navigationContext)
        {
            this.Photo = photo;
            if (navigationContext != null)
            {
                this.NavigationContextType = navigationContext.Type;
                this.NavigationContextCriteria = navigationContext.Criteria;
                this.NavigationContextSlideshowDelay = navigationContext.SlideshowDelay;
            }
        }
    }
}