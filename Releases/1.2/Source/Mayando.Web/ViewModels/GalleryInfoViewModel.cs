using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class GalleryInfoViewModel
    {
        public Gallery Gallery { get; private set; }
        public int PhotoCount { get; private set; }
        public int ChildGalleryCount { get; private set; }

        public GalleryInfoViewModel(Gallery gallery, int photoCount, int childGalleryCount)
        {
            this.Gallery = gallery;
            this.PhotoCount = photoCount;
            this.ChildGalleryCount = childGalleryCount;
        }

        public string GetCoverPhotoUrl(string defaultUrl)
        {
            if (this.Gallery.CoverPhoto != null)
            {
                return this.Gallery.CoverPhoto.UrlSmallestAvailable;
            }
            else
            {
                return defaultUrl;
            }
        }
    }
}