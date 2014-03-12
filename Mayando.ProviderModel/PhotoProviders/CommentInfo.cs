using System;

namespace Mayando.ProviderModel.PhotoProviders
{
    /// <summary>
    /// Represents all the comment information that can be synchronized.
    /// </summary>
    public class CommentInfo
    {
        #region Properties

        /// <summary>
        /// Gets the id of the photo this comment is for in the external service.
        /// </summary>
        public string ExternalPhotoId { get; private set; }

        /// <summary>
        /// Gets the actual text of the comment.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the date the comment was published.
        /// </summary>
        public DateTimeOffset DatePublished { get; private set; }

        /// <summary>
        /// Gets or sets the id of the comment in the external service.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if this comment was posted by the owner of this website.
        /// </summary>
        public bool AuthorIsOwner { get; set; }

        /// <summary>
        /// Gets or sets the name of the comment author.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the comment author.
        /// </summary>
        public string AuthorEmail { get; set; }

        /// <summary>
        /// Gets or sets the URL of the comment author.
        /// </summary>
        public string AuthorUrl { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentInfo"/> class.
        /// </summary>
        /// <param name="externalPhotoId">The id of the photo this comment is for in the external service.</param>
        /// <param name="text">The actual text of the comment.</param>
        /// <param name="datePublished">The date the comment was published.</param>
        public CommentInfo(string externalPhotoId, string text, DateTimeOffset datePublished)
            : this(externalPhotoId, text, datePublished, null, false, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentInfo"/> class.
        /// </summary>
        /// <param name="externalPhotoId">The id of the photo this comment is for in the external service.</param>
        /// <param name="text">The actual text of the comment.</param>
        /// <param name="datePublished">The date the comment was published.</param>
        /// <param name="externalId">The id of the comment in the external service.</param>
        /// <param name="authorIsOwner">A value that determines if this comment was posted by the owner of this website.</param>
        /// <param name="authorName">The name of the comment author.</param>
        /// <param name="authorEmail">The email address of the comment author.</param>
        /// <param name="authorUrl">The URL of the comment author.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "7#")]
        public CommentInfo(string externalPhotoId, string text, DateTimeOffset datePublished, string externalId, bool authorIsOwner, string authorName, string authorEmail, string authorUrl)
        {
            if (string.IsNullOrEmpty(externalPhotoId))
            {
                throw new ArgumentException("The external photo id must be specified.");
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("The comment text must be specified.");
            }
            this.ExternalPhotoId = externalPhotoId;
            this.Text = text;
            this.DatePublished = datePublished;
            this.ExternalId = externalId;
            this.AuthorIsOwner = authorIsOwner;
            this.AuthorName = authorName;
            this.AuthorEmail = authorEmail;
            this.AuthorUrl = authorUrl;
        }

        #endregion
    }
}