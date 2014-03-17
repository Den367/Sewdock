using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace Mayando.ProviderModel.PhotoProviders.Dummy
{
    /// <summary>
    /// Creates a new dummy photo each time you synchronize and adds a comment and all the requested tags to it. Used mainly for testing purposes.
    /// </summary>
    [PhotoProvider("Dummy", "Creates a new dummy photo each time you synchronize and adds a comment and all the requested tags to it. Used mainly for testing purposes.")]
    public class DummyPhotoProvider : IPhotoProvider
    {
        #region GetSettingDefinitions

        /// <summary>
        /// Gets the settings that are supported by this provider.
        /// </summary>
        /// <returns>A list of setting definitions supported by this provider.</returns>
        public SettingDefinition[] GetSettingDefinitions()
        {
            return null;
        }

        #endregion

        #region Initialize

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="host">The host managing this provider.</param>
        public void Initialize(IPhotoProviderHost host)
        {
        }

        #endregion

        #region GetStatus

        /// <summary>
        /// Gets the current status of the provider.
        /// </summary>
        /// <returns>A status description of the provider.</returns>
        public PhotoProviderStatus GetStatus()
        {
            return new PhotoProviderStatus("<p>I will gladly create a new dummy photo for you; all you have to do is <i>ask</i>!</p>", true, true);
        }

        #endregion

        #region Synchronize

        /// <summary>
        /// Synchronizes photos between the provider and Mayando.
        /// </summary>
        /// <param name="request">The request that contains the details about the photos to be synchronized.</param>
        /// <returns>The results of the synchronization.</returns>
        public SynchronizationResult Synchronize(SynchronizationRequest request)
        {
            if (request.StartTime > DateTimeOffset.UtcNow)
            {
                // This is mostly used to test exception handling from the host.
                throw new ArgumentException("The synchronization start time should not be in the future.");
            }
            var photoUrl = VirtualPathUtility.ToAbsolute("~/Content/gallery.png");
            var dummyPhotoId = Guid.NewGuid().ToString();
            var photosToAdd = new List<PhotoInfo>()
            {
                new PhotoInfo(photoUrl, DateTimeOffset.UtcNow)
                {
                    DateTaken = DateTimeOffset.UtcNow.AddHours(-3),
                    ExternalId = dummyPhotoId,
                    Title = "Dummy Photo",
                    Text = "This photo was added by the dummy photo provider.",
                    UrlLarge = photoUrl,
                    UrlSmall = photoUrl,
                    UrlThumbnail = photoUrl,
                    UrlThumbnailSquare = photoUrl,
                    WebUrl = null,
                    Tags = request.Tags,
                }
            };
            var commentsToAdd = new List<CommentInfo>()
            {
                new CommentInfo(dummyPhotoId, "Dummy Comment", DateTimeOffset.UtcNow)
                {
                    ExternalId = Guid.NewGuid().ToString(),
                    AuthorIsOwner = false,
                    AuthorName = "Dummy Author",
                    AuthorEmail = "dummy@example.org",
                    AuthorUrl = "http://mayando.codeplex.com"
                }
            };
            return new SynchronizationResult(true, "A dummy photo was added.", photosToAdd, commentsToAdd);
        }

        #endregion

        #region Reset

        /// <summary>
        /// Resets the provider to its initial state. This typically involves clearing its settings.
        /// </summary>
        public void Reset()
        {
        }

        #endregion
    }
}