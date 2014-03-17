using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using FlickrNet;

namespace Mayando.ProviderModel.PhotoProviders.Flickr
{
    /// <summary>
    /// Retrieves photos from Flickr.
    /// </summary>
    [PhotoProvider("Flickr", "Retrieves photos from Flickr.", "http://www.flickr.com/services/api")]
    public class FlickrPhotoProvider : IPhotoProvider
    {
        #region Constants & Fields

        private const string ApiKey = "0b1d079373ddb088b7a621870a715735";
        private const string SharedSecret = "a1ae5d0b5a830ff2";
        private const string SettingNameTimeZoneName = "TimeZoneName";
        private const string SettingNameAuthenticationToken = "AuthenticationToken";
        private const string SettingNameAuthenticationFrob = "AuthenticationFrob";
        private const string SettingNameOwnerEmail = "OwnerEmail";
        private const string SettingNameOwnerUrl = "OwnerUrl";

        private IPhotoProviderHost Host { get; set; }

        #endregion

        #region GetSettingDefinitions

        /// <summary>
        /// Gets the settings that are supported by this provider.
        /// </summary>
        /// <returns>A list of setting definitions supported by this provider.</returns>
        public SettingDefinition[] GetSettingDefinitions()
        {
            int sequence = 0;
            return new SettingDefinition[]
            {
                new SettingDefinition(SettingNameTimeZoneName, SettingType.TimeZone, "Original Time Zone", "The time zone in which the imported photos were originally taken. This is needed to properly determine the exact time a photo was taken (since Flickr only provides the original time without any time zone information).", sequence++, true, TimeZoneInfo.Utc.Id),
                new SettingDefinition(SettingNameAuthenticationFrob, SettingType.Plaintext, "Authentication Frob", "The temporary authentication frob while authorizing your Flickr account.", sequence++, true),
                new SettingDefinition(SettingNameAuthenticationToken, SettingType.Plaintext, "Authentication Token", "The authentication token for accessing your Flickr account.", sequence++, true),
                new SettingDefinition(SettingNameOwnerEmail, SettingType.Plaintext, "Owner Email", "Your email address, which will be used when importing your own comments.", sequence++, true),
                new SettingDefinition(SettingNameOwnerUrl, SettingType.Plaintext, "Owner Website", "Your website address, which will be used when importing your own comments.", sequence++, true)
            };
        }

        #endregion

        #region Initialize

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="host">The host managing this provider.</param>
        public void Initialize(IPhotoProviderHost host)
        {
            this.Host = host;
        }

        #endregion

        #region GetStatus

        /// <summary>
        /// Gets the current status of the provider.
        /// </summary>
        /// <returns>A status description of the provider.</returns>
        public PhotoProviderStatus GetStatus()
        {
            return CallFlickrWithExceptionHandling(() =>
            {
                var settings = this.Host.GetSettings();
                var token = GetSetting(settings, SettingNameAuthenticationToken);
                if (string.IsNullOrEmpty(token))
                {
                    var frob = GetSetting(settings, SettingNameAuthenticationFrob);
                    if (string.IsNullOrEmpty(frob))
                    {
                        // There is no authentication token or frob. This should be the very first time the provider is used.
                        var tempFlickr = new FlickrNet.Flickr(ApiKey, SharedSecret);
                        var authenticationUrl = tempFlickr.AuthCalcWebUrl(AuthLevel.Read);
                        var authenticationStatusDescription = new StringBuilder();
                        authenticationStatusDescription.Append("<p>Please visit the following link to authenticate with Flickr.</p>");
                        authenticationStatusDescription.AppendFormat(CultureInfo.CurrentCulture, "<p><a href=\"{0}\" target=\"_blank\">Authenticate Flickr</a></p>", authenticationUrl);
                        authenticationStatusDescription.Append("<p>When you have authenticated, enter the Authentication Frob in the settings.</p>");
                        return new PhotoProviderStatus(authenticationStatusDescription.ToString(), false, false);
                    }
                    else
                    {
                        // There is no authentication token but there is a frob, so the user should have authenticated at Flickr.
                        // Get the real token from Flickr now based on the frob.
                        var tempFlickr = new FlickrNet.Flickr(ApiKey, SharedSecret);
                        var tempAuthentication = tempFlickr.AuthGetToken(frob);
                        token = tempAuthentication.Token;
                        settings[SettingNameAuthenticationFrob] = null;
                        settings[SettingNameAuthenticationToken] = token;
                        this.Host.SaveSettings(settings);
                    }
                }

                // There is an authentication token, log on to Flickr.
                var statusDescription = new StringBuilder();
                var flickr = new FlickrNet.Flickr(ApiKey, SharedSecret, token);
                var flickrAuthentication = flickr.AuthCheckToken(token);
                var flickrUser = flickr.PeopleGetInfo(flickrAuthentication.User.UserId);
                var flickrStatus = flickr.PeopleGetUploadStatus();
                statusDescription.AppendFormat(CultureInfo.CurrentCulture, "<p>Logged in to Flickr as <a href=\"{0}\" target=\"_blank\">{1}</a>.</p>", flickrUser.PhotosUrl, flickrAuthentication.User.Username);
                statusDescription.AppendFormat(CultureInfo.CurrentCulture, "<p><img src=\"{0}\" /></p>", flickrUser.BuddyIconUrl.ToString());
                statusDescription.AppendFormat(CultureInfo.CurrentCulture, "<p>You've uploaded {0} of your {1} limit this month ({2}).</p>", Converter.ToByteString(flickrStatus.BandwidthUsed), Converter.ToByteString(flickrStatus.BandwidthMax), flickrStatus.PercentageUsed.ToString("p0", CultureInfo.CurrentCulture));
                return new PhotoProviderStatus(statusDescription.ToString(), true, true);
            });
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
            var settings = this.Host.GetSettings();
            var token = GetSetting(settings, SettingNameAuthenticationToken);
            if (string.IsNullOrEmpty(token))
            {
                var statusDescription = "<p>You have not authorized the Flickr Provider to access your account yet. Please go back to the photo provider page and finish the authentication process.</p>";
                return new SynchronizationResult(false, statusDescription, null);
            }
            List<PhotoInfo> photosToAdd = null;
            List<CommentInfo> commentsToAdd = null;
            bool success = false;
            var status = CallFlickrWithExceptionHandling(() =>
            {
                var watch = new Stopwatch();
                watch.Start();

                // Authenticate with Flickr.
                var flickr = new FlickrNet.Flickr(ApiKey, SharedSecret, token);
                var flickrAuthentication = flickr.AuthCheckToken(token);

                // Get new photos.
                photosToAdd = GetNewPhotos(request, settings, flickr, flickrAuthentication);

                // Make a list of all existing and new photo id's.
                var allPhotoIds = (from p in photosToAdd
                                   select p.ExternalId).ToList();
                allPhotoIds.AddRange(request.ExistingPhotoIds);

                // Get new comments for all existing photos.
                commentsToAdd = GetNewComments(request, settings, allPhotoIds, flickr, flickrAuthentication);

                // Finish.
                watch.Stop();
                success = true;
                var statusDescription = GetStatusDescription(watch.Elapsed, photosToAdd, commentsToAdd);
                return new PhotoProviderStatus(statusDescription, true, true);
            });
            return new SynchronizationResult(success, status.StatusDescriptionHtml, photosToAdd, commentsToAdd);
        }

        private static List<PhotoInfo> GetNewPhotos(SynchronizationRequest request, IDictionary<string, string> settings, FlickrNet.Flickr flickr, Auth flickrAuthentication)
        {
            var photosToAdd = new List<PhotoInfo>();
            var timeZoneName = GetSetting(settings, SettingNameTimeZoneName);
            TimeZoneInfo timeZone;
            try
            {
                timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
            }
            catch (TimeZoneNotFoundException)
            {
                timeZone = TimeZoneInfo.Utc;
            }

            // Find new photos that have the requested tags.
            var options = new PhotoSearchOptions
            {
                UserId = flickrAuthentication.User.UserId,
                MinUploadDate = request.StartTime.UtcDateTime,
                Extras = PhotoSearchExtras.None,
                TagMode = TagMode.AllTags,
                Tags = string.Join(",", request.Tags.ToArray())
            };
            int page = 1;
            while (true)
            {
                options.Page = page;
                var foundPhotos = flickr.PhotosSearch(options);
                foreach (var foundPhoto in foundPhotos.PhotoCollection)
                {
                    if (!request.ExistingPhotoIds.Contains(foundPhoto.PhotoId))
                    {
                        // Fetch details about the photo.
                        // Assume we will have at least the Medium (normal) version of a photo since, well, it's a photoblog.
                        var photoDetails = flickr.PhotosGetInfo(foundPhoto.PhotoId, foundPhoto.Secret);
                        var photoInfo = new PhotoInfo(photoDetails.MediumUrl, new DateTimeOffset(photoDetails.Dates.PostedDate, TimeSpan.Zero)) // The PostedDate is always in UTC and must not be adjusted.
                        {
                            DateTaken = TimeZoneInfo.ConvertTimeToUtc(photoDetails.Dates.TakenDate, timeZone), // The TakenDate is always in the time zone of the photo owner and must be adjusted.
                            ExternalId = photoDetails.PhotoId,
                            WebUrl = photoDetails.WebUrl,
                            Tags = photoDetails.Tags.TagCollection.Where(t => t.IsMachineTag == 0).Select(t => t.Raw),
                            Text = photoDetails.Description,
                            Title = photoDetails.Title,
                            UrlSmall = photoDetails.SmallUrl,
                            UrlThumbnail = photoDetails.ThumbnailUrl,
                            UrlThumbnailSquare = photoDetails.SquareThumbnailUrl
                        };

                        // Fetch all the sizes for the photo to see if the Large version is present.
                        var sizes = flickr.PhotosGetSizes(foundPhoto.PhotoId);
                        foreach (var size in sizes.SizeCollection)
                        {
                            if (string.Equals("Large", size.Label, StringComparison.OrdinalIgnoreCase))
                            {
                                photoInfo.UrlLarge = size.Source;
                            }
                        }

                        photosToAdd.Add(photoInfo);
                    }
                }
                page++;
                if (foundPhotos.PageNumber >= foundPhotos.TotalPages)
                {
                    break;
                }
            }
            return photosToAdd;
        }

        private static List<CommentInfo> GetNewComments(SynchronizationRequest request, IDictionary<string, string> settings, List<string> allPhotoIds, FlickrNet.Flickr flickr, Auth flickrAuthentication)
        {
            var commentsToAdd = new List<CommentInfo>();
            int page = 1;
            while (true)
            {
                var foundPhotos = flickr.PhotosRecentlyUpdated(request.StartTime.UtcDateTime, 100, page);
                foreach (var foundPhoto in foundPhotos.PhotoCollection)
                {
                    // Only fetch comments for photos that we actually have (either existing or just added).
                    if (allPhotoIds.Exists(p => string.Equals(p, foundPhoto.PhotoId, StringComparison.OrdinalIgnoreCase)))
                    {
                        var comments = flickr.PhotosCommentsGetList(foundPhoto.PhotoId);
                        foreach (var comment in comments)
                        {
                            if (!request.ExistingCommentIds.Contains(comment.CommentId))
                            {
                                var text = comment.CommentHtml;

                                // Clean up any HTML tags. Note that this is a naive implementation which doesn't properly handle
                                // all HTML cases but Flickr only allows a few very basic HTML tags so this should be fine.
                                text = Regex.Replace(text, @"<[^>]*>", String.Empty);

                                var commentInfo = new CommentInfo(foundPhoto.PhotoId, text, new DateTimeOffset(comment.DateCreated, TimeSpan.Zero))
                                {
                                    AuthorName = comment.AuthorUserName,
                                    ExternalId = comment.CommentId
                                };
                                if (string.Equals(comment.AuthorUserId, flickrAuthentication.User.UserId, StringComparison.OrdinalIgnoreCase))
                                {
                                    // The comment is by the account owner.
                                    commentInfo.AuthorIsOwner = string.Equals(comment.AuthorUserId, flickrAuthentication.User.UserId, StringComparison.OrdinalIgnoreCase);
                                    commentInfo.AuthorEmail = GetSetting(settings, SettingNameOwnerEmail);
                                    commentInfo.AuthorUrl = GetSetting(settings, SettingNameOwnerUrl);
                                }
                                commentsToAdd.Add(commentInfo);
                            }
                        }
                    }
                }
                page++;
                if (foundPhotos.PageNumber >= foundPhotos.TotalPages)
                {
                    break;
                }
            }
            return commentsToAdd;
        }

        #endregion

        #region Reset

        /// <summary>
        /// Resets the provider to its initial state. This typically involves clearing its settings.
        /// </summary>
        public void Reset()
        {
            var settings = this.Host.GetSettings();
            settings[SettingNameAuthenticationFrob] = null;
            settings[SettingNameAuthenticationToken] = null;
            this.Host.SaveSettings(settings);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Performs a series of calls to Flickr with exception handling.
        /// </summary>
        /// <param name="action">The actions to be performed.</param>
        /// <returns>The current provider status.</returns>
        private PhotoProviderStatus CallFlickrWithExceptionHandling(Func<PhotoProviderStatus> action)
        {
            try
            {
                // Disable persistent caching since this attempts to write to a local file
                // which is probably never allowed on a shared web host.
                FlickrNet.Flickr.CacheDisabled = true;
                return action();
            }
            catch (FlickrApiException exc)
            {
                if (exc.Code == 105)
                {
                    // Service currently unavailable. The requested service is temporarily unavailable.
                    var statusDescription = "<p>The Flickr service is temporarily unavailable. Please refresh this page to try again.</p>";
                    return new PhotoProviderStatus(statusDescription, true, true);
                }
                if (exc.Code == 108)
                {
                    // On AuthGetToken: Invalid frob. The specified frob does not exist or has already been used.
                    Reset();
                    var statusDescription = "<p>You have not authorized the Flickr Provider to access your account yet. Please refresh this page and restart the authentication process.</p>";
                    return new PhotoProviderStatus(statusDescription, false, false);
                }
                else if (exc.Code == 98)
                {
                    // On AuthCheckToken: Login failed / Invalid auth token.
                    Reset();
                    var statusDescription = "<p>Your authentication token has become invalid. This might be because you revoked the permission for the Flickr Provider to access your account. Please refresh this page and restart the authentication process.</p>";
                    return new PhotoProviderStatus(statusDescription, false, false);
                }
                else
                {
                    throw;
                }
            }
            catch (WebException exc)
            {
                var statusDescription = new StringBuilder();
                statusDescription.Append("<p>An exception occurred while connecting to Flickr. Please refresh this page to try again.</p>");
                statusDescription.AppendFormat(CultureInfo.CurrentCulture, "<pre>{0}</pre>", exc.ToString());
                return new PhotoProviderStatus(statusDescription.ToString(), true, true);
            }
        }

        /// <summary>
        /// Gets a status description for a completed synchronization.
        /// </summary>
        /// <param name="elapsedTime">The elapsed time of synchronization.</param>
        /// <param name="photosToAdd">The photos to be added.</param>
        /// <param name="commentsToAdd">The comments to be added.</param>
        /// <returns>A status description.</returns>
        private static string GetStatusDescription(TimeSpan elapsedTime, List<PhotoInfo> photosToAdd, List<CommentInfo> commentsToAdd)
        {
            var statusDescription = new StringBuilder();
            statusDescription.AppendFormat(CultureInfo.CurrentCulture, "<p>Synchronization completed in {0} seconds.</p>", elapsedTime.TotalSeconds);
            statusDescription.AppendFormat(CultureInfo.CurrentCulture, "<p>{0} photo(s) synchronized from Flickr.</p>", photosToAdd.Count);
            if (photosToAdd.Count > 0)
            {
                statusDescription.Append("<ul>");
                foreach (var photoToAdd in photosToAdd)
                {
                    var title = (string.IsNullOrEmpty(photoToAdd.Title) ? photoToAdd.DatePublished.ToString() : photoToAdd.Title);
                    statusDescription.AppendFormat(CultureInfo.CurrentCulture, "<li>{0}</li>", title);
                }
                statusDescription.Append("</ul>");
            }
            statusDescription.AppendFormat(CultureInfo.CurrentCulture, "<p>{0} comment(s) synchronized from Flickr.</p>", commentsToAdd.Count);
            if (commentsToAdd.Count > 0)
            {
                statusDescription.Append("<ul>");
                foreach (var commentToAdd in commentsToAdd)
                {
                    statusDescription.AppendFormat(CultureInfo.CurrentCulture, "<li>{0} on photo #{1}: {2}</li>", commentToAdd.AuthorName, commentToAdd.ExternalPhotoId, commentToAdd.Text);
                }
                statusDescription.Append("</ul>");
            }
            return statusDescription.ToString();
        }

        /// <summary>
        /// Gets a certain setting from the dictionary, or <see langword="null"/> if the setting wasn't present.
        /// </summary>
        /// <param name="settings">The settings dictionary.</param>
        /// <param name="name">The name of the setting to retrieve.</param>
        /// <returns>The setting's value in the dictionary, or <see langword="null"/> if the setting wasn't present.</returns>
        private static string GetSetting(IDictionary<string, string> settings, string name)
        {
            if (!settings.ContainsKey(name))
            {
                return null;
            }
            return settings[name];
        }

        #endregion
    }
}