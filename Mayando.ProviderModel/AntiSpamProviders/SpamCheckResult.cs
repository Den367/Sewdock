
namespace Mayando.ProviderModel.AntiSpamProviders
{
    /// <summary>
    /// Represents the results of a check for spam.
    /// </summary>
    public class SpamCheckResult
    {
        #region Properties

        /// <summary>
        /// Gets the classification of the content that was submitted.
        /// </summary>
        public Classification Classification { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SpamCheckResult"/> class.
        /// </summary>
        /// <param name="classification">The classification of the content that was submitted.</param>
        public SpamCheckResult(Classification classification)
        {
            this.Classification = classification;
        }

        #endregion
    }
}