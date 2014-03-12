
namespace Mayando.ProviderModel.PhotoProviders
{
    /// <summary>
    /// Represents the status of a Photo Provider.
    /// </summary>
    public class PhotoProviderStatus : ProviderStatus
    {
        #region Properties

        /// <summary>
        /// Gets a value that determines if the provider can currently synchronize.
        /// </summary>
        public bool CanSynchronize { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoProviderStatus"/> class.
        /// </summary>
        /// <param name="statusDescriptionHtml">The description of the current status, formatted as HTML.</param>
        /// <param name="canReset">A value that determines if the provider can currently be reset.</param>
        /// <param name="canSynchronize">A value that determines if the provider can currently synchronize.</param>
        public PhotoProviderStatus(string statusDescriptionHtml, bool canReset, bool canSynchronize)
            : base(statusDescriptionHtml, canReset)
        {
            this.CanSynchronize = canSynchronize;
        }

        #endregion
    }
}