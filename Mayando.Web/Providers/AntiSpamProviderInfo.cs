using System;

namespace Mayando.Web.Providers
{
    /// <summary>
    /// Provides information about an anti-spam provider.
    /// </summary>
    public class AntiSpamProviderInfo : ProviderInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AntiSpamProviderInfo"/> class.
        /// </summary>
        /// <param name="providerType">The type of the provider.</param>
        public AntiSpamProviderInfo(Type providerType)
            : base(providerType)
        {
        }
    }
}