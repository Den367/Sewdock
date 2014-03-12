using System;
using System.Collections.Generic;

namespace Mayando.ProviderModel.PhotoProviders
{
    /// <summary>
    /// Represents all the photo information that can be synchronized.
    /// </summary>
    public class PhotoInfo
    {
        #region Properties

        /// <summary>
        /// Gets the URL of the normal-sized version of the photo.
        /// </summary>
        public string UrlNormal { get; private set; }

        /// <summary>
        /// Gets the date the photo was published.
        /// </summary>
        public DateTimeOffset DatePublished { get; private set; }

        /// <summary>
        /// Gets or sets the date the photo was taken.
        /// </summary>
        public DateTimeOffset? DateTaken { get; set; }

        /// <summary>
        /// Gets or sets the id of the photo in the external service.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the title of the photo.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text accompanying the photo.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the URL of the large-sized version of the photo.
        /// </summary>
        public string UrlLarge { get; set; }

        /// <summary>
        /// Gets or sets the URL of the small-sized version of the photo.
        /// </summary>
        public string UrlSmall { get; set; }

        /// <summary>
        /// Gets or sets the URL of the thumbnail-sized version of the photo.
        /// </summary>
        public string UrlThumbnail { get; set; }

        /// <summary>
        /// Gets or sets the URL of the square thumbnail-sized version of the photo.
        /// </summary>
        public string UrlThumbnailSquare { get; set; }

        /// <summary>
        /// Gets or sets the external URL where the photo can be directly viewed online.
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// Gets or sets the tags for this photo.
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoInfo"/> class.
        /// </summary>
        /// <param name="urlNormal">The URL of the normal-sized version of the photo.</param>
        /// <param name="datePublished">The date the photo was published.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public PhotoInfo(string urlNormal, DateTimeOffset datePublished)
            : this(urlNormal, datePublished, null, null, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoInfo"/> class.
        /// </summary>
        /// <param name="urlNormal">The URL of the normal-sized version of the photo.</param>
        /// <param name="datePublished">The date the photo was published.</param>
        /// <param name="dateTaken">The date the photo was taken.</param>
        /// <param name="externalId">The id of the photo in the external service.</param>
        /// <param name="title">The title of the photo.</param>
        /// <param name="text">The text accompanying the photo.</param>
        /// <param name="urlLarge">The URL of the large-sized version of the photo.</param>
        /// <param name="urlSmall">The URL of the small-sized version of the photo.</param>
        /// <param name="urlThumbnail">The URL of the thumbnail-sized version of the photo.</param>
        /// <param name="urlThumbnailSquare">The URL of the square thumbnail-sized version of the photo.</param>
        /// <param name="webUrl">The external URL where the photo can be directly viewed online.</param>
        /// <param name="tags">The tags for this photo.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "9#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "8#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "7#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "6#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "10#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public PhotoInfo(string urlNormal, DateTimeOffset datePublished, DateTimeOffset? dateTaken, string externalId, string title, string text, string urlLarge, string urlSmall, string urlThumbnail, string urlThumbnailSquare, string webUrl, IEnumerable<string> tags)
        {
            if (string.IsNullOrEmpty(urlNormal))
            {
                throw new ArgumentException("The URL of the normal-sized version of the photo must be specified.");
            }
            this.UrlNormal = urlNormal;
            this.DatePublished = datePublished;
            this.ExternalId = externalId;
            this.WebUrl = webUrl;
            this.Title = title;
            this.Text = text;
            this.UrlLarge = urlLarge;
            this.UrlSmall = urlSmall;
            this.UrlThumbnail = urlThumbnail;
            this.UrlThumbnailSquare = urlThumbnailSquare;
            this.DateTaken = dateTaken;
            this.Tags = tags;
        }

        #endregion
    }
}