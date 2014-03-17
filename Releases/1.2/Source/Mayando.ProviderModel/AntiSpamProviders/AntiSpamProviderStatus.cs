
namespace Mayando.ProviderModel.AntiSpamProviders
{
    /// <summary>
    /// Represents the status of an Anti-Spam Provider.
    /// </summary>
    public class AntiSpamProviderStatus : ProviderStatus
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AntiSpamProviderStatus"/> class.
        /// </summary>
        /// <param name="statusDescriptionHtml">The description of the current status, formatted as HTML.</param>
        /// <param name="canReset">A value that determines if the provider can currently be reset.</param>
        public AntiSpamProviderStatus(string statusDescriptionHtml, bool canReset)
            : base(statusDescriptionHtml, canReset)
        {
        }

        #endregion
    }
}