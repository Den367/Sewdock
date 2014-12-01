using Myembro.Models;

namespace Myembro.ViewModels
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
           
                return defaultUrl;
           
        }
    }
}