using Mayando.Web.Infrastructure;

namespace Mayando.Web.ViewModels
{
    public class PhotoViewModel
    {
        public PhotoDetailsViewModel PhotoDetails { get; private set; }
        public NavigationContext NavigationContext { get; private set; }

        public PhotoViewModel(PhotoDetailsViewModel photoDetails, NavigationContext navigationContext)
        {
            this.PhotoDetails = photoDetails;
            this.NavigationContext = navigationContext;
        }
    }
}