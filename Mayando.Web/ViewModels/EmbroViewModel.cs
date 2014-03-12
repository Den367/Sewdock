using Mayando.Web.Infrastructure;

namespace Mayando.Web.ViewModels
{
    public class EmbroNavigationViewModel
    {
        public EmbroDetailsViewModel EmbroDetails { get; private set; }
        public EmbroNavigationContext NavigationContext { get; private set; }

        public EmbroNavigationViewModel(EmbroDetailsViewModel embroDetails, EmbroNavigationContext navigationContext)
        {
            this.EmbroDetails = embroDetails;
            this.NavigationContext = navigationContext;
        }
    }
}