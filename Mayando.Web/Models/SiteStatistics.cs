
namespace Myembro.Models
{
    /// <summary>
    /// Contains statistics on the current site.
    /// </summary>
    public class SiteStatistics
    {
        #region Properties

        /// <summary>
        /// Gets the number of photos.
        /// </summary>
        public int NumberOfPhotos { get; private set; }

        /// <summary>
        /// Gets the number of hidden photos.
        /// </summary>
        public int NumberOfHiddenPhotos { get; private set; }

        /// <summary>
        /// Gets the number of galleries.
        /// </summary>
        public int NumberOfGalleries { get; private set; }

        /// <summary>
        /// Gets the number of pages.
        /// </summary>
        public int NumberOfPages { get; private set; }

        /// <summary>
        /// Gets the number of comments.
        /// </summary>
        public int NumberOfComments { get; private set; }

        /// <summary>
        /// Gets the number of tags.
        /// </summary>
        public int NumberOfTags { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteStatistics"/> class.
        /// </summary>
        /// <param name="numberOfPhotos">The number of photos.</param>
        /// <param name="numberOfHiddenPhotos">The number of hidden photos.</param>
        /// <param name="numberOfGalleries">The number of galleries.</param>
        /// <param name="numberOfPages">The number of pages.</param>
        /// <param name="numberOfComments">The number of comments.</param>
        /// <param name="numberOfTags">The number of tags.</param>
        public SiteStatistics(int numberOfPhotos, int numberOfHiddenPhotos, int numberOfGalleries, int numberOfPages, int numberOfComments, int numberOfTags)
        {
            this.NumberOfPhotos = numberOfPhotos;
            this.NumberOfHiddenPhotos = numberOfHiddenPhotos;
            this.NumberOfGalleries = numberOfGalleries;
            this.NumberOfPages = numberOfPages;
            this.NumberOfComments = numberOfComments;
            this.NumberOfTags = numberOfTags;
        }

        #endregion
    }
}