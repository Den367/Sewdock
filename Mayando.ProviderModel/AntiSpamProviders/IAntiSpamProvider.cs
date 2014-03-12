
namespace Mayando.ProviderModel.AntiSpamProviders
{
    /// <summary>
    /// Represents an Anti-Spam Provider that can be used in Mayando.
    /// </summary>
    public interface IAntiSpamProvider : IProvider
    {
        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="host">The host managing this provider.</param>
        void Initialize(IAntiSpamProviderHost host);

        /// <summary>
        /// Gets the current status of the provider.
        /// </summary>
        /// <returns>A status description of the provider.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        AntiSpamProviderStatus GetStatus();
        
        /// <summary>
        /// Checks a certain request for spam.
        /// </summary>
        /// <param name="request">The request that contains the details about the content to be checked for spam.</param>
        /// <returns>The results of the check for spam.</returns>
        SpamCheckResult CheckForSpam(SpamCheckRequest request);
    }
}