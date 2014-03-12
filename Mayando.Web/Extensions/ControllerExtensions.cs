using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JelleDruyts.Web.Mvc.Paging;
using Mayando.ProviderModel.AntiSpamProviders;
using Mayando.Web.Controllers;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Properties;
using Mayando.Web.Providers;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Extensions
{
    public static class ControllerExtensions
    {
        #region Comment Helper Methods

        ///// <summary>
        ///// Gets a title that describes the given comment.
        ///// </summary>
        ///// <param name="controller">The controller.</param>
        ///// <param name="comment">The comment for which to generate a title.</param>
        ///// <returns>A title indicating the author name and the photo's display title.</returns>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "controller")]
        //public static string GetCommentNotificationTitle(this SiteControllerBase controller, Comment comment)
        //{
        //    return string.Format(CultureInfo.InvariantCulture, "Comment from {0} on {1}", comment.AuthorName, comment.EmbroideryItem.DisplayTitle);
        //}

        ///// <summary>
        ///// Gets a text that describes the given comment.
        ///// </summary>
        ///// <param name="controller">The controller.</param>
        ///// <param name="comment">The comment for which to generate a description.</param>
        ///// <param name="includeAdminLinks">Determines if administrative links for the comment should be included.</param>
        ///// <returns>An HTML "div" element that contains details about the comment.</returns>
        //public static string GetCommentNotificationText(this SiteControllerBase controller, Comment comment, bool includeAdminLinks)
        //{
        //    var text = new StringBuilder();
        //    text.Append("<div>");
        //    text.AppendFormat(CultureInfo.InvariantCulture, "<div><img src=\"{0}\" /></div>", comment.EmbroideryItem.UrlSmallestAvailable);
        //    text.AppendFormat(CultureInfo.InvariantCulture, "<div>{0}</div>", JelleDruyts.Web.Mvc.HtmlHelperExtensions.EncodeWithLineBreaks(comment.Text));
        //    if (includeAdminLinks)
        //    {
        //        var viewCommentUrl = controller.SiteData.ToAbsolute(controller.Url.CommentDetails(comment)).ToString();
        //        var deleteCommentUrl = controller.SiteData.ToAbsolute(controller.Url.Action(ActionName.Delete, CommentsController.ControllerName, new { id = comment.Id })).ToString();
        //        text.Append("<div style=\"border: solid red 1px; margin: 5px 0px; padding: 5px;\">");
        //        text.AppendFormat(CultureInfo.InvariantCulture, "<a href=\"{0}\">View Comment</a> | <a href=\"{1}\">Delete Comment</a>", viewCommentUrl, deleteCommentUrl);
        //        text.Append("</div>");
        //    }
        //    text.Append("</div>");
        //    return text.ToString();
        //}

        ///// <summary>
        ///// Adds a comment to a photo, but checks it for spam first.
        ///// </summary>
        ///// <param name="controller">The controller.</param>
        ///// <param name="photoId">The photo id.</param>
        ///// <param name="comment">The comment.</param>
        ///// <param name="rememberMe">Determines if the comment author's contact details should be remembered.</param>
        ///// <returns>The comment that was saved to the database, or <see langword="null"/> if it was not saved.</returns>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        //public static Comment AddCommentToPhoto(this SiteControllerBase controller, int photoId, Comment comment, bool rememberMe)
        //{
        //    // Trim some fields.
        //    comment.AuthorEmail = (comment.AuthorEmail == null ? null : comment.AuthorEmail.Trim());
        //    comment.AuthorName = (comment.AuthorName == null ? null : comment.AuthorName.Trim());
        //    comment.AuthorUrl = (comment.AuthorUrl == null ? null : comment.AuthorUrl.Trim());
        //    comment.Text = (comment.Text == null ? null : comment.Text.Trim());

        //    // Make sure the author url is absolute.
        //    if (!string.IsNullOrEmpty(comment.AuthorUrl) && !comment.AuthorUrl.StartsWith(Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
        //    {
        //        comment.AuthorUrl = string.Format(CultureInfo.InvariantCulture, "{0}://{1}", Uri.UriSchemeHttp, comment.AuthorUrl);
        //    }

        //    // Remember contact details.
        //    controller.RememberPreferences(comment.AuthorName, comment.AuthorEmail, comment.AuthorUrl, rememberMe);

        //    // Check for spam.
        //    var result = new SpamCheckResult(Classification.Unchecked);
        //    var providerWasCalled = false;
        //    if (controller.User.IsAdministrator())
        //    {
        //        // It should be safe to assume that the website administrator will not post spam to his own site.
        //        result = new SpamCheckResult(Classification.NotSpam);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var antiSpamProvider = AntiSpamProviderFactory.CreateProvider(controller.SiteData.Settings.AntiSpamProviderGuid);
        //            if (antiSpamProvider != null)
        //            {
        //                result = antiSpamProvider.CheckForSpam(new SpamCheckRequest(comment.AuthorName, comment.AuthorEmail, comment.AuthorUrl, controller.Request.UserHostAddress, comment.Text));
        //                providerWasCalled = true;
        //            }
        //        }
        //        catch (Exception exc)
        //        {
        //            Logger.LogException(exc);
        //            result = new SpamCheckResult(Classification.Unsure);
        //        }
        //    }

        //    if (result.Classification == Classification.Spam)
        //    {
        //        // Ignore spam completely.
        //        return null;
        //    }
        //    else
        //    {
        //        // If it's not spam or if we're not sure, assume that it's safe.
        //        // In a future version, we could foresee a captcha to be shown to the user.

        //        // Add the comment to the database.
        //        int commentId;
        //        using (var repository = GetRepository())
        //        {
        //            comment.DatePublished = DateTimeOffset.UtcNow;
        //            comment.AuthorIsOwner = controller.User.IsAdministrator();
        //            repository.AddComment(photoId, comment);
        //            repository.CommitChanges();
        //            commentId = comment.Id;
        //            controller.SetPageFlash(Resources.PageFlashCommentPosted);
        //        }

        //        using (var repository = GetRepository())
        //        {
        //            var savedComment = repository.GetCommentWithPhotoById(commentId);

        //            if (providerWasCalled && result.Classification == Classification.Unchecked)
        //            {
        //                Logger.Log(LogLevel.Warning, string.Format(CultureInfo.CurrentCulture, "The comment from {0} on '{1}' was not checked for spam by the provider; it will be posted anyway so please validate the comment manually.", savedComment.AuthorName, savedComment.EmbroideryItem.DisplayTitle));
        //            }
        //            if (result.Classification == Classification.Unsure)
        //            {
        //                Logger.Log(LogLevel.Warning, string.Format(CultureInfo.CurrentCulture, "The comment from {0} on '{1}' was checked for spam but the provider wasn't sure if it is spam or not; it will be posted anyway so please validate the comment manually.", savedComment.AuthorName, savedComment.EmbroideryItem.DisplayTitle));
        //            }

        //            Logger.Log(LogLevel.Information, string.Format(CultureInfo.CurrentCulture, "New comment from {0} on '{1}'", savedComment.AuthorName, savedComment.EmbroideryItem.DisplayTitle), savedComment.Text);

        //            // Send a notification email if needed.
        //            if (controller.SiteData.Settings.NotifyOnComments)
        //            {
        //                Mailer.SendNotificationMail(controller.SiteData.Settings, controller.GetCommentNotificationTitle(savedComment), controller.GetCommentNotificationText(savedComment, true), true, true, comment.AuthorEmail, comment.AuthorName);
        //            }

        //            return savedComment;
        //        }
        //    }
        //}

        #endregion

        #region EmbroideryItem Size Helper Methods

     

        public static EmbroDetailsViewModel GetEmbroDetailsViewModel(this SiteControllerBase controller, EmbroideryItem embro, bool hidePhotoText, bool hidePhotoComments)
        {
            //var preferredPhotoSize = SiteControllerBase.GetCookieValue(controller.ControllerContext.RequestContext, "PhotoSize");
            //var explicitSize = controller.Request["size"];
            //if (!string.IsNullOrEmpty(explicitSize))
            //{
            //    preferredPhotoSize = explicitSize;
            //    controller.Response.Cookies.Set(new HttpCookie("PhotoSize", explicitSize) { Expires = DateTime.Now.AddDays(2) });
            //}
            #region [photo]
            //var preferredSizePhotoUrl = embro.UrlNormal;
            //var preferredSize = EmbroSize.Normal;

            //if (string.Equals(SiteControllerBase.ParameterPhotoSizeLarge, preferredPhotoSize, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(embro.UrlLarge))
            //{
            //    preferredSizePhotoUrl = embro.UrlLarge;
            //    preferredSize = EmbroSize.Large;
            //}
            //else if (string.Equals(SiteControllerBase.ParameterPhotoSizeSmall, preferredPhotoSize, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(embro.UrlSmall))
            //{
            //    preferredSizePhotoUrl = embro.UrlSmall;
            //    preferredSize = EmbroSize.Small;
            //}
            #endregion [photo]

            var viewModel = new EmbroDetailsViewModel(embro, hidePhotoText, hidePhotoComments);
            
            return viewModel;
        }

        #endregion

        #region Gallery Helper Methods

        ///// <summary>
        ///// Gets all photos for a certain gallery.
        ///// </summary>
        ///// <param name="controller">The controller.</param>
        ///// <param name="repository">The repository.</param>
        ///// <param name="gallery">The gallery.</param>
        ///// <param name="count">The number of photos to show.</param>
        ///// <param name="page">The page number.</param>
        ///// <returns>A paged list of photos in the gallery.</returns>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "controller")]
        //public static IPagedList<EmbroideryItem> GetPhotosForGallery(this SiteControllerBase controller, EmbroRepository repository, Gallery gallery, int count, int page)
        //{
        //    var tags = gallery.Tags.Select(t => t.Name).ToList();
        //    var photos = repository.GetPhotosWithCommentsTagged(EmbroDateType.Published, tags, count, page);
        //    return photos;
        //}

        #endregion

        #region ViewData Helper Methods

        ///// <summary>
        ///// Stores the photo list in the page's view data.
        ///// </summary>
        ///// <param name="controller">The controller.</param>
        ///// <param name="selectedPhotoId">The selected photo id.</param>
        ///// <param name="allowNull">Determines if the user can also choose not select to a photo from the list.</param>
        //public static void StorePhotoListInViewData(this ControllerBase controller, int? selectedPhotoId, bool allowNull)
        //{
        //    using (var repository = GetRepository())
        //    {
        //        var photos = repository.GetPhotos(EmbroDateType.Published);
        //        var listItems = new List<SelectListItem>();
        //        if (allowNull)
        //        {
        //            listItems.Add(new SelectListItem { Value = string.Empty, Text = Resources.SelectListItemNone });
        //        }
        //        foreach (var photo in photos)
        //        {
        //            bool selected = false;
        //            if (selectedPhotoId != null && (selectedPhotoId.Value == photo.Id))
        //            {
        //                selected = true;
        //            }
        //            listItems.Add(new SelectListItem { Selected = selected, Text = photo.DisplayTitleWithDate, Value = photo.Id.ToString(CultureInfo.InvariantCulture) });
        //        }
        //        controller.ViewData[SiteControllerBase.ViewDataKeyPhotos] = listItems;
        //    }
        //}

        ///// <summary>
        ///// Stores the gallery list in the page's view data.
        ///// </summary>
        ///// <param name="controller">The controller.</param>
        ///// <param name="selectedGalleryId">The selected gallery id.</param>
        ///// <param name="allowNull">Determines if the user can also choose not select to a gallery from the list.</param>
        //public static void StoreGalleryListInViewData(this ControllerBase controller, int? selectedGalleryId, bool allowNull)
        //{
        //    using (var repository = GetRepository())
        //    {
        //        var galleries = repository.GetGalleries();
        //        var listItems = new List<SelectListItem>();
        //        if (allowNull)
        //        {
        //            listItems.Add(new SelectListItem { Value = string.Empty, Text = Resources.SelectListItemNone });
        //        }
        //        foreach (var gallery in galleries)
        //        {
        //            bool selected = false;
        //            if (selectedGalleryId != null && (selectedGalleryId.Value == gallery.Id))
        //            {
        //                selected = true;
        //            }
        //            listItems.Add(new SelectListItem { Selected = selected, Text = gallery.Title, Value = gallery.Id.ToString(CultureInfo.InvariantCulture) });
        //        }
        //        controller.ViewData[SiteControllerBase.ViewDataKeyGalleries] = listItems;
        //    }
        //}

        /// <summary>
        /// Stores the log level list in the page's view data.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="selectedLogLevel">The selected log level.</param>
        public static void StoreLogLevelListInViewData(this ControllerBase controller, LogLevel? selectedLogLevel)
        {
            var listItems = new List<SelectListItem>();
            foreach (LogLevel logLevel in Enum.GetValues(typeof(LogLevel)))
            {
                bool selected = false;
                if (selectedLogLevel != null && (selectedLogLevel.Value == logLevel))
                {
                    selected = true;
                }
                listItems.Add(new SelectListItem { Selected = selected, Text = logLevel.ToString(), Value = ((int)logLevel).ToString(CultureInfo.InvariantCulture) });
            }
            controller.ViewData[SiteControllerBase.ViewDataKeyLogLevels] = listItems;
        }

        #endregion

        #region ISupportSequence Helper Methods

        /// <summary>
        /// Moves an item in its sequence.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="direction">The direction in which to move the item.</param>
        /// <param name="items">The list of items.</param>
        /// <param name="currentItem">The item to move in the list.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "controller")]
        public static void MoveInSequence(this SiteControllerBase controller, Direction direction, IEnumerable<ISupportSequence> items, ISupportSequence currentItem)
        {
            if (currentItem != null)
            {
                // Reorder in memory.
                var orderedItems = items.OrderBy(i => i.Sequence).ToList();
                var currentIndex = orderedItems.IndexOf(currentItem);
                orderedItems.Remove(currentItem);

                if (direction == Direction.Top)
                {
                    orderedItems.Insert(0, currentItem);
                }
                if (direction == Direction.Up)
                {
                    orderedItems.Insert(Math.Max(0, currentIndex - 1), currentItem);
                }
                if (direction == Direction.Down)
                {
                    orderedItems.Insert(Math.Min(orderedItems.Count, currentIndex + 1), currentItem);
                }
                if (direction == Direction.Bottom)
                {
                    orderedItems.Insert(orderedItems.Count, currentItem);
                }

                // Reapply sequence numbers to be persisted.
                var sequence = 0;
                foreach (var item in orderedItems)
                {
                    item.Sequence = sequence++;
                }
            }
        }

        #endregion
    }
}