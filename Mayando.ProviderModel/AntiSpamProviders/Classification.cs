
namespace Mayando.ProviderModel.AntiSpamProviders
{
    /// <summary>
    /// Defines the possible classifications of a piece of content.
    /// </summary>
    public enum Classification
    {
        /// <summary>
        /// The content is considered spam.
        /// </summary>
        Spam,

        /// <summary>
        /// The content is not considered spam.
        /// </summary>
        NotSpam,

        /// <summary>
        /// The content classification cannot be determined with certainty.
        /// </summary>
        Unsure,

        /// <summary>
        /// The content was not checked for spam.
        /// </summary>
        Unchecked
    }
}