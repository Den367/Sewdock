
namespace Mayando.Web.ViewModels
{
    public class PhotosPageViewModel
    {
        public string PageTitle { get; private set; }
        public PhotosViewModel Photos { get; private set; }

        public PhotosPageViewModel(string pageTitle, PhotosViewModel photos)
        {
            this.PageTitle = pageTitle;
            this.Photos = photos;
        }
    }
}