using System;
using System.Collections.Generic;

namespace Mayando.ProviderModel.PhotoProviders
{
    /// <summary>
    /// Represents the information to be used when synchronizing.
    /// </summary>
    public class SynchronizationRequest
    {
        #region Properties

        /// <summary>
        /// Gets the ID's of the photos that already exist in Mayando.
        /// </summary>
        public IList<string> ExistingPhotoIds { get; private set; }

        /// <summary>
        /// Gets the ID's of the comments that already exist in Mayando.
        /// </summary>
        public IList<string> ExistingCommentIds { get; private set; }

        /// <summary>
        /// Gets the start time for the synchronization, i.e. the time before which no photos should be synchronized.
        /// </summary>
        public DateTimeOffset StartTime { get; private set; }

        /// <summary>
        /// Gets the tags of the photos to be synchronized, i.e. only photos should be included that have all the given tags.
        /// </summary>
        public IList<string> Tags { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizationRequest"/> class.
        /// </summary>
        /// <param name="existingPhotoIds">The ID's of the photos that already exist in Mayando.</param>
        /// <param name="existingCommentIds">The ID's of the comments that already exist in Mayando.</param>
        /// <param name="startTime">The start time for the synchronization, i.e. the time before which no photos should be synchronized.</param>
        /// <param name="tags">The tags of the photos to be synchronized, i.e. only photos should be included that have all the given tags.</param>
        public SynchronizationRequest(IList<string> existingPhotoIds, IList<string> existingCommentIds, DateTimeOffset startTime, IList<string> tags)
        {
            this.ExistingPhotoIds = existingPhotoIds;
            this.ExistingCommentIds = existingCommentIds;
            this.StartTime = startTime;
            this.Tags = tags;
        }

        #endregion
    }
}