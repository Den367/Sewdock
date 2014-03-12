using System.Collections.Generic;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class GalleryViewModel
    {
        public Gallery Gallery { get; private set; }
        public PhotosViewModel Photos { get; private set; }
        public ICollection<GalleryInfoViewModel> ChildGalleries { get; private set; }
        public ICollection<Gallery> ParentGalleries { get; private set; }

        public GalleryViewModel(Gallery gallery, PhotosViewModel photos, ICollection<GalleryInfoViewModel> childGalleries, ICollection<Gallery> parentGalleries)
        {
            this.Gallery = gallery;
            this.Photos = photos;
            this.ChildGalleries = childGalleries;
            this.ParentGalleries = parentGalleries;
        }
    }
}