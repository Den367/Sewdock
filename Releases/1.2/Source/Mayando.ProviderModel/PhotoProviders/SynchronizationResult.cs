using System.Collections.Generic;

namespace Mayando.ProviderModel.PhotoProviders
{
    /// <summary>
    /// Represents the results of a synchronization.
    /// </summary>
    public class SynchronizationResult
    {
        #region Properties

        /// <summary>
        /// Gets a value that determines if synchronization was successfull.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets the description of the current status, formatted as HTML.
        /// </summary>
        public string StatusDescriptionHtml { get; private set; }

        /// <summary>
        /// Gets the photos to be added to Mayando.
        /// </summary>
        public ICollection<PhotoInfo> PhotosToAdd { get; private set; }

        /// <summary>
        /// Gets the comments to be added to Mayando.
        /// </summary>
        public ICollection<CommentInfo> CommentsToAdd { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationResult"/> class.
        /// </summary>
        /// <param name="success">A value that determines if synchronization was successfull.</param>
        /// <param name="status">The description of the current status, formatted as HTML.</param>
        public SynchronizationResult(bool success, string status)
            : this(success, status, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationResult"/> class.
        /// </summary>
        /// <param name="success">A value that determines if synchronization was successfull.</param>
        /// <param name="status">The description of the current status, formatted as HTML.</param>
        /// <param name="photosToAdd">The photos to be added to Mayando.</param>
        public SynchronizationResult(bool success, string status, ICollection<PhotoInfo> photosToAdd)
            : this(success, status, photosToAdd, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationResult"/> class.
        /// </summary>
        /// <param name="success">A value that determines if synchronization was successfull.</param>
        /// <param name="status">The description of the current status, formatted as HTML.</param>
        /// <param name="photosToAdd">The photos to be added to Mayando.</param>
        /// <param name="commentsToAdd">The comments to be added to Mayando.</param>
        public SynchronizationResult(bool success, string status, ICollection<PhotoInfo> photosToAdd, ICollection<CommentInfo> commentsToAdd)
        {
            this.Success = success;
            this.StatusDescriptionHtml = status;
            this.PhotosToAdd = (photosToAdd ?? new PhotoInfo[0]);
            this.CommentsToAdd = (commentsToAdd ?? new CommentInfo[0]);
        }

        #endregion
    }
}