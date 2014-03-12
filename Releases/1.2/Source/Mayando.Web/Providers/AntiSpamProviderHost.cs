using Mayando.ProviderModel.AntiSpamProviders;
using Mayando.Web.Models;

namespace Mayando.Web.Providers
{
    /// <summary>
    /// Represents the host for an anti-spam provider.
    /// </summary>
    internal class AntiSpamProviderHost : ProviderHost, IAntiSpamProviderHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AntiSpamProviderHost"/> class.
        /// </summary>
        public AntiSpamProviderHost()
            : base(SettingsScope.AntiSpamProvider)
        {
        }
    }
}