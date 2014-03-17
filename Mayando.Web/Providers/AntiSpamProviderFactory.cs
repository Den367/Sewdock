using Mayando.ProviderModel.AntiSpamProviders;
using Mayando.Web.Models;

namespace Mayando.Web.Providers
{
    /// <summary>
    /// Creates instances of anti-spam providers.
    /// </summary>
    internal static class AntiSpamProviderFactory
    {
        private static ProviderFactory<IAntiSpamProvider, AntiSpamProviderInfo> innerFactory = new ProviderFactory<IAntiSpamProvider, AntiSpamProviderInfo>(t => new AntiSpamProviderInfo(t));

        /// <summary>
        /// Gets the available anti-spam providers.
        /// </summary>
        /// <returns>Information about the available anti-spam providers.</returns>
        public static AntiSpamProviderInfo[] GetAvailableProviders()
        {
            return innerFactory.GetAvailableProviders();
        }

        /// <summary>
        /// Gets the info for a specified provider.
        /// </summary>
        /// <param name="providerGuid">The provider GUID.</param>
        /// <returns>The provider info.</returns>
        public static AntiSpamProviderInfo GetProviderInfo(string providerGuid)
        {
            return innerFactory.GetProviderInfo(providerGuid);
        }

        /// <summary>
        /// Creates the specified provider.
        /// </summary>
        /// <param name="providerGuid">The provider GUID.</param>
        /// <returns>An instance of the requested provider.</returns>
        public static IAntiSpamProvider CreateProvider(string providerGuid)
        {
            return innerFactory.CreateProvider(providerGuid, p => p.Initialize(new AntiSpamProviderHost()), SettingsScope.AntiSpamProvider);
        }
    }
}