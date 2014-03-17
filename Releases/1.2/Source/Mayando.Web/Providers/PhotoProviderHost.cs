using Mayando.ProviderModel.PhotoProviders;
using Mayando.Web.Models;

namespace Mayando.Web.Providers
{
    /// <summary>
    /// Represents the host for a photo provider.
    /// </summary>
    internal class PhotoProviderHost : ProviderHost, IPhotoProviderHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoProviderHost"/> class.
        /// </summary>
        public PhotoProviderHost()
            : base(SettingsScope.PhotoProvider)
        {
        }
    }
}