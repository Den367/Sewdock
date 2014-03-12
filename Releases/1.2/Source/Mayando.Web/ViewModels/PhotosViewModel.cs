using JelleDruyts.Web.Mvc.Paging;
using Mayando.Web.Models;
using Mayando.Web.Infrastructure;

namespace Mayando.Web.ViewModels
{
    public class PhotosViewModel
    {
        public IPagedList<Photo> Photos { get; private set; }
        public NavigationContextType? NavigationContextType { get; private set; }
        public string NavigationContextCriteria { get; private set; }
        public NavigationContext NavigationContext { get; private set; }

        public PhotosViewModel(IPagedList<Photo> photos, bool showFilmstrip)
            : this(photos, null, null, NavigationDirection.Forward, showFilmstrip)
        {
        }

        public PhotosViewModel(IPagedList<Photo> photos, NavigationDirection initialSlideshowDirection, bool showFilmstrip)
            : this(photos, null, null, initialSlideshowDirection, showFilmstrip)
        {
        }

        public PhotosViewModel(IPagedList<Photo> photos, NavigationContextType? navigationContextType, bool showFilmstrip)
            : this(photos, navigationContextType, null, NavigationDirection.Forward, showFilmstrip)
        {
        }

        public PhotosViewModel(IPagedList<Photo> photos, NavigationContextType? navigationContextType, string navigationContextCriteria, bool showFilmstrip)
            : this(photos, navigationContextType, navigationContextCriteria, NavigationDirection.Forward, showFilmstrip)
        {
        }

        public PhotosViewModel(IPagedList<Photo> photos, NavigationContextType? navigationContextType, string navigationContextCriteria, NavigationDirection initialSlideshowDirection, bool showFilmstrip)
        {
            this.Photos = photos;
            this.NavigationContextType = navigationContextType;
            this.NavigationContextCriteria = navigationContextCriteria;
            Photo current = null;
            if (photos != null && photos.Count > 0)
            {
                current = photos[0];
            }
            this.NavigationContext = new NavigationContext(photos, current, navigationContextType, navigationContextCriteria, initialSlideshowDirection, showFilmstrip);
        }
    }
}