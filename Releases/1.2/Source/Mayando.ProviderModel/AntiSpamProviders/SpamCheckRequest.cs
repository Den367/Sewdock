
namespace Mayando.ProviderModel.AntiSpamProviders
{
    /// <summary>
    /// Represents the information to be used when checking content for spam.
    /// </summary>
    public class SpamCheckRequest
    {
        #region Properties

        /// <summary>
        /// Gets the name of the author that submitted the content.
        /// </summary>
        public string AuthorName { get; private set; }

        /// <summary>
        /// Gets the email address of the author that submitted the content.
        /// </summary>
        public string AuthorEmail { get; private set; }

        /// <summary>
        /// Gets the URL of the author that submitted the content.
        /// </summary>
        public string AuthorUrl { get; private set; }

        /// <summary>
        /// Gets the IP address of the author that submitted the content.
        /// </summary>
        public string AuthorIPAddress { get; private set; }

        /// <summary>
        /// Gets the content that was submitted.
        /// </summary>
        public string Text { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SpamCheckRequest"/> class.
        /// </summary>
        /// <param name="authorName">The name of the author that submitted the content.</param>
        /// <param name="authorEmail">The email address of the author that submitted the content.</param>
        /// <param name="authorUrl">The URL of the author that submitted the content.</param>
        /// <param name="authorIPAddress">The IP address of the author that submitted the content.</param>
        /// <param name="text">The content that was submitted.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public SpamCheckRequest(string authorName, string authorEmail, string authorUrl, string authorIPAddress, string text)
        {
            this.AuthorName = authorName;
            this.AuthorEmail = authorEmail;
            this.AuthorUrl = authorUrl;
            this.AuthorIPAddress = authorIPAddress;
            this.Text = text;
        }

        #endregion
    }
}