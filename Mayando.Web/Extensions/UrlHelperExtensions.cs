using System.Web.Mvc;
using JelleDruyts.Web.Mvc.Discovery;
using Mayando.Web.Controllers;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;

namespace Mayando.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="UrlHelper"/> instances.
    /// </summary>
    public static class UrlHelperExtensions
    {
        #region Action

        /// <summary>
        /// Returns a virtual path for the specified route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The virtual path to the action.</returns>
        public static string Action(this UrlHelper url, ActionName actionName)
        {
            return url.Action(actionName, null, null);
        }

        /// <summary>
        /// Returns a virtual path for the specified route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>The virtual path to the action.</returns>
        public static string Action(this UrlHelper url, ActionName actionName, string controllerName)
        {
            return url.Action(actionName, controllerName, null);
        }

        /// <summary>
        /// Returns a virtual path for the specified route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="routeValues">An object containing the parameters for a route. The parameters are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <returns>The virtual path to the action.</returns>
        public static string Action(this UrlHelper url, ActionName actionName, object routeValues)
        {
            return url.Action(actionName, null, routeValues);
        }

        /// <summary>
        /// Returns a virtual path for the specified route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object containing the parameters for a route. The parameters are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <returns>The virtual path to the action.</returns>
        public static string Action(this UrlHelper url, ActionName actionName, string controllerName, object routeValues)
        {
            return url.Action(actionName.ToActionString(), controllerName, routeValues);
        }

        #endregion

        #region Discovery

        /// <summary>
        /// Returns a virtual path for the specified route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionInfo">The action to link to.</param>
        /// <returns>The virtual path to the action.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public static string DiscoveryAction(this UrlHelper url, ActionInfo actionInfo)
        {
            return url.Action(actionInfo.Name, actionInfo.Controller.Name).ToLowerInvariant();
        }

        #endregion

        #region Photos


        public static string EmbroDetails(this UrlHelper urlHelper, EmbroideryItem photo)
        {
            return urlHelper.EmbroDetails(photo, null, null, null);
        }

     

        public static string EmbroDetails(this UrlHelper urlHelper, EmbroideryItem photo, NavigationContextType? navigationContextType, string navigationContextCriteria)
        {
            return urlHelper.EmbroDetails(photo, navigationContextType, navigationContextCriteria, null);
        }

     

        public static string EmbroDetails(this UrlHelper urlHelper, EmbroideryItem photo, NavigationContextType? navigationContextType, string navigationContextCriteria, int? slideshowDelay)
        {
            return urlHelper.Action(ActionName.Details, EmbroController.ControllerName, new { id = photo.Id, context = navigationContextType.ToActionString(), criteria = navigationContextCriteria, slideshow = slideshowDelay });
        }

        #endregion

        #region Comments

        /// <summary>
        /// Returns a virtual path for the specified comment.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="comment">The comment to link to.</param>
        /// <returns>The virtual path to the comment.</returns>
        public static string CommentDetails(this UrlHelper urlHelper, Comment comment)
        {
           // var photoUrl = urlHelper.PhotoDetails(comment.EmbroideryItem);
           // return photoUrl + "#comment-" + comment.Id;
            return string.Empty;
        }

        #endregion

        #region Galleries

        /// <summary>
        /// Returns a virtual path for the specified gallery.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="gallery">The gallery to link to.</param>
        /// <returns>The virtual path to the gallery.</returns>
        public static string GalleryDetails(this UrlHelper urlHelper, Gallery gallery)
        {
            return urlHelper.Action(ActionName.Titled, GalleriesController.ControllerName, new { id = gallery.Slug });
        }

        #endregion

        #region Feeds

        /// <summary>
        /// Returns a virtual path for the photos feed.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>The virtual path to the photos feed.</returns>
        public static string FeedPhotos(this UrlHelper urlHelper)
        {
            return urlHelper.Action(ActionName.Photos, FeedsController.ControllerName);
        }

        /// <summary>
        /// Returns a virtual path for the photos feed.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>The virtual path to the photos feed.</returns>
        public static string FeedComments(this UrlHelper urlHelper)
        {
            return urlHelper.Action(ActionName.Comments, FeedsController.ControllerName);
        }

        #endregion
    }
}