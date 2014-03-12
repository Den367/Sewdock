using Mayando.ProviderModel.PhotoProviders;
using Mayando.Web.Models;

namespace Mayando.Web.Providers
{
    /// <summary>
    /// Creates instances of photo providers.
    /// </summary>
    internal static class PhotoProviderFactory
    {
        private static ProviderFactory<IPhotoProvider, PhotoProviderInfo> innerFactory = new ProviderFactory<IPhotoProvider, PhotoProviderInfo>(t => new PhotoProviderInfo(t));

        /// <summary>
        /// Gets the available photo providers.
        /// </summary>
        /// <returns>Information about the available photo providers.</returns>
        public static PhotoProviderInfo[] GetAvailableProviders()
        {
            return innerFactory.GetAvailableProviders();
        }

        /// <summary>
        /// Gets the info for a specified provider.
        /// </summary>
        /// <param name="providerGuid">The provider GUID.</param>
        /// <returns>The provider info.</returns>
        public static PhotoProviderInfo GetProviderInfo(string providerGuid)
        {
            return innerFactory.GetProviderInfo(providerGuid);
        }

        /// <summary>
        /// Creates the specified provider.
        /// </summary>
        /// <param name="providerGuid">The provider GUID.</param>
        /// <returns>An instance of the requested provider.</returns>
        public static IPhotoProvider CreateProvider(string providerGuid)
        {
            return innerFactory.CreateProvider(providerGuid, p => p.Initialize(new PhotoProviderHost()), SettingsScope.PhotoProvider);
        }
    }
}