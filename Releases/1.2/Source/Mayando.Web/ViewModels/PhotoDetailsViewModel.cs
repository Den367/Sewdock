using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class PhotoDetailsViewModel
    {
        public Photo Photo { get; private set; }
        public string PreferredSizePhotoUrl { get; private set; }
        public PhotoSize PreferredSize { get; private set; }
        public bool HidePhotoText { get; private set; }
        public bool HidePhotoComments { get; private set; }
        public bool CanChangeSize { get; private set; }
        public bool CanChangeSizeToSmall { get; private set; }
        public bool CanChangeSizeToNormal { get; private set; }
        public bool CanChangeSizeToLarge { get; private set; }
        public bool ShouldLinkToExternalUrl { get; private set; }

        public PhotoDetailsViewModel(Photo photo, string preferredSizePhotoUrl, PhotoSize preferredSize, bool hidePhotoText, bool hidePhotoComments, bool shouldLinkToExternalUrl)
        {
            this.Photo = photo;
            this.PreferredSizePhotoUrl = preferredSizePhotoUrl;
            this.PreferredSize = preferredSize;
            this.HidePhotoText = hidePhotoText;
            this.HidePhotoComments = hidePhotoComments;
            this.CanChangeSizeToSmall = preferredSize != PhotoSize.Small && !string.IsNullOrEmpty(photo.UrlSmall);
            this.CanChangeSizeToNormal = preferredSize != PhotoSize.Normal && true; // The "normal" (medium size) photo url is mandatory.
            this.CanChangeSizeToLarge = preferredSize != PhotoSize.Large && !string.IsNullOrEmpty(photo.UrlLarge);
            this.CanChangeSize = this.CanChangeSizeToSmall || this.CanChangeSizeToNormal || this.CanChangeSizeToLarge;
            this.ShouldLinkToExternalUrl = (shouldLinkToExternalUrl && !string.IsNullOrEmpty(this.Photo.ExternalUrl));
        }
    }
}