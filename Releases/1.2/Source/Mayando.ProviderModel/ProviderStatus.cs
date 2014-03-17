
namespace Mayando.ProviderModel
{
    /// <summary>
    /// Represents the status of a provider.
    /// </summary>
    public abstract class ProviderStatus
    {
        #region Properties

        /// <summary>
        /// Gets the description of the current status, formatted as HTML.
        /// </summary>
        public string StatusDescriptionHtml { get; private set; }

        /// <summary>
        /// Gets a value that determines if the provider can currently be reset.
        /// </summary>
        public bool CanReset { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderStatus"/> class.
        /// </summary>
        /// <param name="statusDescriptionHtml">The description of the current status, formatted as HTML.</param>
        /// <param name="canReset">A value that determines if the provider can currently be reset.</param>
        protected ProviderStatus(string statusDescriptionHtml, bool canReset)
        {
            this.StatusDescriptionHtml = statusDescriptionHtml;
            this.CanReset = canReset;
        }

        #endregion
    }
}