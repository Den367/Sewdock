using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using JelleDruyts.Web.Mvc.Discovery;
using Mayando.Web.Controllers;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Properties;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="HtmlHelper"/> instances.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        #region ActionLink

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the specified action.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLink(this HtmlHelper html, string linkText, ActionName actionName)
        {
            return ActionLink(html, linkText, actionName, null, null, null);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the specified action.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLink(this HtmlHelper html, string linkText, ActionName actionName, string controllerName)
        {
            return ActionLink(html, linkText, actionName, controllerName, null, null);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the specified action.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="routeValues">An object containing the parameters for a route. The parameters are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLink(this HtmlHelper html, string linkText, ActionName actionName, object routeValues)
        {
            return ActionLink(html, linkText, actionName, null, routeValues, null);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the specified action.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="routeValues">An object containing the parameters for a route. The parameters are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <param name="htmlAttributes">An object containing the HTML attributes for the element. The attributes are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLink(this HtmlHelper html, string linkText, ActionName actionName, object routeValues, object htmlAttributes)
        {
            return ActionLink(html, linkText, actionName, null, routeValues, htmlAttributes);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the specified action.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object containing the parameters for a route. The parameters are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLink(this HtmlHelper html, string linkText, ActionName actionName, string controllerName, object routeValues)
        {
            return html.ActionLink(linkText, actionName, controllerName, routeValues, null);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the specified action.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object containing the parameters for a route. The parameters are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <param name="htmlAttributes">An object containing the HTML attributes for the element. The attributes are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLink(this HtmlHelper html, string linkText, ActionName actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            return html.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        }

        #endregion

        #region BeginForm

        /// <summary>
        /// Writes an opening form tag to the response while returning a <see cref="MvcForm"/> instance. Can be used in a using block, in which case it renders the closing form tag at the end of the using block.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginForm(this HtmlHelper html, ActionName actionName)
        {
            return html.BeginForm(actionName, null, null, FormMethod.Post, null);
        }

        /// <summary>
        /// Writes an opening form tag to the response while returning a <see cref="MvcForm"/> instance. Can be used in a using block, in which case it renders the closing form tag at the end of the using block.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginForm(this HtmlHelper html, ActionName actionName, string controllerName)
        {
            return html.BeginForm(actionName, controllerName, null, FormMethod.Post, null);
        }

        /// <summary>
        /// Writes an opening form tag to the response while returning a <see cref="MvcForm"/> instance. Can be used in a using block, in which case it renders the closing form tag at the end of the using block.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="formId">The ID of the form.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginForm(this HtmlHelper html, ActionName actionName, string controllerName, string formId)
        {
            return html.BeginForm(actionName, controllerName, null, FormMethod.Post, new { id = formId });
        }

        /// <summary>
        /// Writes an opening form tag to the response while returning a <see cref="MvcForm"/> instance. Can be used in a using block, in which case it renders the closing form tag at the end of the using block.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object containing the parameters for a route. The parameters are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <param name="elementToDisableOnSubmit">The ID of the HTML element (typically the submit button) to disable when the form is submitted.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginForm(this HtmlHelper html, ActionName actionName, string controllerName, object routeValues, string elementToDisableOnSubmit)
        {
            var onsubmit = string.Format(CultureInfo.InvariantCulture, "document.getElementById('{0}').disabled = 'disabled';", elementToDisableOnSubmit);
            return html.BeginForm(actionName, controllerName, routeValues, FormMethod.Post, new { onsubmit = onsubmit });
        }

        /// <summary>
        /// Writes an opening form tag to the response while returning a <see cref="MvcForm"/> instance. Can be used in a using block, in which case it renders the closing form tag at the end of the using block.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object containing the parameters for a route. The parameters are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <param name="method">The HTTP method for the form post, either Get or Post.</param>
        /// <param name="htmlAttributes">An object containing the HTML attributes for the element. The attributes are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginForm(this HtmlHelper html, ActionName actionName, string controllerName, object routeValues, FormMethod method, object htmlAttributes)
        {
            return html.BeginForm(actionName.ToActionString(), controllerName, routeValues, method, htmlAttributes);
        }

        #endregion

        #region Date & Time

        /// <summary>
        /// Returns a string that describes a certain time.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="time">The time to display.</param>
        /// <returns>A timezone-adjusted and formatted time.</returns>
        public static string DateTimeText(this HtmlHelper html, DateTimeOffset? time)
        {
            return html.DateTimeText(time, null);
        }

        /// <summary>
        /// Returns a string that describes a certain time.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="time">The time to display.</param>
        /// <param name="prefix">The prefix to add to the beginning of the returned string.</param>
        /// <returns>A timezone-adjusted and formatted time.</returns>
        public static string DateTimeText(this HtmlHelper html, DateTimeOffset? time, string prefix)
        {
            if (!time.HasValue)
            {
                return string.Empty;
            }
            else
            {
                var text = time.ToAdjustedVerboseDisplayString();
                if (!string.IsNullOrEmpty(prefix))
                {
                    text = string.Format(CultureInfo.CurrentCulture, "{0} {1}", prefix, text);
                }
                return html.Encode(text);
            }
        }

        /// <summary>
        /// Returns a string that describes a certain time.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="time">The time to display.</param>
        /// <param name="type">The type of time to be displayed.</param>
        /// <returns>A timezone-adjusted and formatted time.</returns>
        public static string DateTimeText(this HtmlHelper html, DateTimeOffset? time, DateTimeDisplayType type)
        {
            if (!time.HasValue)
            {
                return string.Empty;
            }
            else
            {
                var text = time.ToAdjustedVerboseDisplayString();
                if (type == DateTimeDisplayType.PhotoTaken)
                {
                    var link = html.ActionLinkPhotoDay(PhotoDateType.Taken, time.Value, text);
                    return string.Format(CultureInfo.CurrentCulture, "{0} {1}", html.Encode(Resources.PhotoTaken), link);
                }
                else if (type == DateTimeDisplayType.PhotoPublished)
                {
                    var link = html.ActionLinkPhotoDay(PhotoDateType.Published, time.Value, text);
                    return string.Format(CultureInfo.CurrentCulture, "{0} {1}", html.Encode(Resources.PhotoPublished), link);
                }
                else
                {
                    return html.Encode(text);
                }
            }
        }

        /// <summary>
        /// Returns a string that describes the time zone for which all displayed times are valid.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>A description of the time zone.</returns>
        public static string TimeZoneInfo(this HtmlHelper html)
        {
            var offset = DateTimeOffsetExtensions.TimeZone.GetUtcOffset(DateTimeOffset.UtcNow);
            var offsetDescription = string.Format(CultureInfo.CurrentCulture, "UTC{0}{1:00}:{2:00}", (offset >= TimeSpan.Zero ? "+" : "-"), offset.Duration().Hours, offset.Duration().Minutes);
            var text = string.Format(CultureInfo.CurrentCulture, Resources.TimeZoneOffsetInfo, offsetDescription);
            return html.Encode(text);
        }

        #endregion

        #region RenderPartial

        /// <summary>
        /// Renders the specified partial view.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="partialViewName">The name of the partial view.</param>
        public static void RenderPartial(this HtmlHelper html, PartialViewName partialViewName)
        {
            html.RenderPartial(partialViewName, null);
        }

        /// <summary>
        /// Renders the specified partial view, passing in a copy of the current <see cref="ViewDataDictionary"/> but with the Model property set to the specified model.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="partialViewName">The name of the partial view.</param>
        /// <param name="model">The model.</param>
        public static void RenderPartial(this HtmlHelper html, PartialViewName partialViewName, object model)
        {
            html.RenderPartial(partialViewName.ToString(), model);
        }

        /// <summary>
        /// Renders a list of public controller actions.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="model">The model.</param>
        public static void RenderPartialListControllerActionsForPublic(this HtmlHelper html, IEnumerable<ControllerInfo> model)
        {
            html.RenderPartial(PartialViewName.ListControllerActions, model.SelectMany(c => c.HttpGetActions).Where(a => a.AllowedForEveryone));
        }

        /// <summary>
        /// Renders a list of administrator controller actions.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="model">The model.</param>
        public static void RenderPartialListControllerActionsForAdministrator(this HtmlHelper html, IEnumerable<ControllerInfo> model)
        {
            html.RenderPartial(PartialViewName.ListControllerActions, model.SelectMany(c => c.HttpGetActions).Where(a => !a.AllowedForEveryone));
        }

        /// <summary>
        /// Renders a photo thumbnail.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="model">The model.</param>
        public static void RenderPartialPhotoThumbnail(this HtmlHelper html, Photo model)
        {
            html.RenderPartialPhotoThumbnail(model, null);
        }

        /// <summary>
        /// Renders a photo thumbnail.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="model">The model.</param>
        /// <param name="navigationContext">The navigation context.</param>
        public static void RenderPartialPhotoThumbnail(this HtmlHelper html, Photo model, NavigationContext navigationContext)
        {
            html.RenderPartial(PartialViewName.PhotoThumbnail, new PhotoThumbnailViewModel(model, navigationContext));
        }

        /// <summary>
        /// Renders an HTML editor.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="formFieldName">The name of the form field.</param>
        /// <param name="text">The text to display in the editor.</param>
        public static void RenderPartialHtmlEditor(this HtmlHelper html, string formFieldName, string text)
        {
            html.RenderPartial(PartialViewName.HtmlEditor, new HtmlEditorViewModel(formFieldName, text));
        }

        /// <summary>
        /// Renders an HTML editor.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="formFieldName">The name of the form field.</param>
        /// <param name="text">The text to display in the editor.</param>
        /// <param name="height">The height of the editor text.</param>
        public static void RenderPartialHtmlEditor(this HtmlHelper html, string formFieldName, string text, int height)
        {
            html.RenderPartial(PartialViewName.HtmlEditor, new HtmlEditorViewModel(formFieldName, text, height));
        }
        
        #endregion

        #region Photos

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the previous photo.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="context">The navigation context.</param>
        /// <returns>An anchor tag for the previous photo, or just the navigation text if there is no previous photo.</returns>
        public static string ActionLinkPhotoPrevious(this HtmlHelper html, NavigationContext context)
        {
            if (context.Previous != null)
            {
                var title = string.Format(CultureInfo.CurrentCulture, Resources.PhotoNavigateTo, context.Previous.DisplayTitle);
                return html.ActionLink(Resources.PhotoNavigatePrevious, ActionName.Details, new { id = context.Previous.Id, context = context.Type.ToActionString(), criteria = context.Criteria, slideshow = context.SlideshowDelay }, new { title = title });
            }
            else
            {
                return HttpUtility.HtmlEncode(Resources.PhotoNavigatePrevious);
            }
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the next photo.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="context">The navigation context.</param>
        /// <returns>An anchor tag for the next photo, or just the navigation text if there is no next photo.</returns>
        public static string ActionLinkPhotoNext(this HtmlHelper html, NavigationContext context)
        {
            if (context.Next != null)
            {
                var title = string.Format(CultureInfo.CurrentCulture, Resources.PhotoNavigateTo, context.Next.DisplayTitle);
                return html.ActionLink(Resources.PhotoNavigateNext, ActionName.Details, new { id = context.Next.Id, context = context.Type.ToActionString(), criteria = context.Criteria, slideshow = context.SlideshowDelay }, new { title = title });
            }
            else
            {
                return HttpUtility.HtmlEncode(Resources.PhotoNavigateNext);
            }
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to a given photo.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="photo">The photo to link to.</param>
        /// <param name="navigationContextType">Optionally specifies the type of navigation context to use in the target page.</param>
        /// <param name="navigationContextCriteria">Optionally specified criteria for the navigation context to use in the target page.</param>
        /// <returns>An anchor tag for the photo.</returns>
        public static string ActionLinkPhoto(this HtmlHelper html, Photo photo, NavigationContextType? navigationContextType, string navigationContextCriteria)
        {
            var title = string.Format(CultureInfo.CurrentCulture, Resources.PhotoNavigateTo, photo.DisplayTitle);
            return html.ActionLink(photo.DisplayTitle, ActionName.Details, new { id = photo.Id, context = navigationContextType.ToActionString(), criteria = navigationContextCriteria }, new { title = title });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to a given year of photos.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="type">The type of the year of photos.</param>
        /// <param name="year">The year for which to display the photos.</param>
        /// <returns>An anchor tag for the photos.</returns>
        public static string ActionLinkPhotoYear(this HtmlHelper html, PhotoDateType type, DateTimeOffset year)
        {
            var linkText = year.Year.ToString(CultureInfo.CurrentCulture);
            var actionName = (type == PhotoDateType.Taken ? ActionName.Taken : ActionName.Published);
            return html.ActionLink(linkText, actionName, new { year = year.Year });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to a given month of photos.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="type">The type of the year of photos.</param>
        /// <param name="month">The month for which to display the photos.</param>
        /// <returns>An anchor tag for the photos.</returns>
        public static string ActionLinkPhotoMonth(this HtmlHelper html, PhotoDateType type, DateTimeOffset month)
        {
            var linkText = month.ToString("MMMM", CultureInfo.CurrentCulture);
            var actionName = (type == PhotoDateType.Taken ? ActionName.Taken : ActionName.Published);
            return html.ActionLink(linkText, actionName, new { year = month.Year, month = month.Month });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to a given day of photos.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="type">The type of the day of photos.</param>
        /// <param name="day">The day for which to display the photos.</param>
        /// <returns>An anchor tag for the photos.</returns>
        public static string ActionLinkPhotoDay(this HtmlHelper html, PhotoDateType type, DateTimeOffset day)
        {
            return html.ActionLinkPhotoDay(type, day, day.ToLongDateString());
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to a given day of photos.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="type">The type of the day of photos.</param>
        /// <param name="day">The day for which to display the photos.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag for the photos.</returns>
        public static string ActionLinkPhotoDay(this HtmlHelper html, PhotoDateType type, DateTimeOffset day, string linkText)
        {
            var actionName = (type == PhotoDateType.Taken ? ActionName.Taken : ActionName.Published);
            return html.ActionLink(linkText, actionName, new { year = day.Year, month = day.Month, day = day.Day });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to a specific size of the current photo.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="size">The size of the photo to link to.</param>
        /// <returns>An anchor tag for the size of the photo.</returns>
        //public static string ActionLinkPhotoSize(this HtmlHelper html, PhotoSize size)
        //{
        //    var sizeName = SiteControllerBase.ParameterPhotoSizeNormal;
        //    var linkText = Resources.PhotoLinkNormalText;
        //    var title = Resources.PhotoLinkNormalTitle;
        //    if (size == PhotoSize.Large)
        //    {
        //        sizeName = SiteControllerBase.ParameterPhotoSizeLarge;
        //        linkText = Resources.PhotoLinkLargeText;
        //        title = Resources.PhotoLinkLargeTitle;
        //    }
        //    else if (size == PhotoSize.Small)
        //    {
        //        sizeName = SiteControllerBase.ParameterPhotoSizeSmall;
        //        linkText = Resources.PhotoLinkSmallText;
        //        title = Resources.PhotoLinkSmallTitle;
        //    }
        //    return html.RouteLink(linkText, new { size = Enum.Parse(typeof(PhotoSize), sizeName, true) }, new { title = title });
        //}

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the photo index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPhotosIndex(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, PhotosController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the hidden photos page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPhotosHidden(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Hidden, PhotosController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the photo create page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPhotoCreate(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Create, PhotosController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the photo edit page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="photoId">The ID of the photo.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPhotoEdit(this HtmlHelper html, string linkText, int photoId)
        {
            return html.ActionLink(linkText, ActionName.Edit, new { id = photoId });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the photo delete page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="photoId">The ID of the photo.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPhotoDelete(this HtmlHelper html, string linkText, int photoId)
        {
            return html.ActionLink(linkText, ActionName.Delete, new { id = photoId });
        }

        /// <summary>
        /// Returns a string to be displayed for a number of photos.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="count">The number of photos to display.</param>
        /// <returns>The singular or plural description of the number of photos, depending on the specified count.</returns>
        public static string NumberOfPhotos(this HtmlHelper html, int count)
        {
            if (count == 1)
            {
                return html.Encode(string.Format(CultureInfo.CurrentCulture, Resources.NumberOfPhotosSingular, count));
            }
            else
            {
                return html.Encode(string.Format(CultureInfo.CurrentCulture, Resources.NumberOfPhotosPlural, count));
            }
        }

        /// <summary>
        /// Returns a string to be displayed for a number of galleries.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="count">The number of galleries to display.</param>
        /// <returns>The singular or plural description of the number of galleries, depending on the specified count.</returns>
        public static string NumberOfGalleries(this HtmlHelper html, int count)
        {
            if (count == 1)
            {
                return html.Encode(string.Format(CultureInfo.CurrentCulture, Resources.NumberOfGalleriesSingular, count));
            }
            else
            {
                return html.Encode(string.Format(CultureInfo.CurrentCulture, Resources.NumberOfGalleriesPlural, count));
            }
        }

        /// <summary>
        /// Writes an opening form tag for the photos create form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotosCreateForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Create, PhotosController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the photos edit form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotosEditForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Edit, PhotosController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the photo search form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotosSearchForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Search, PhotosController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the photo bulk edit form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="formId">The ID of the form.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotosBulkEditForm(this HtmlHelper html, string formId)
        {
            return html.BeginForm(ActionName.BulkEdit, PhotosController.ControllerName, formId);
        }

        /// <summary>
        /// Writes an opening form tag for the photo bulk edit execute form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotosBulkEditExecuteForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.BulkEditExecute, PhotosController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the photo add comment form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="submitButtonName">The ID of the HTML submit button to disable when the form is submitted.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotosAddCommentForm(this HtmlHelper html, string submitButtonName)
        {
            return html.BeginForm(ActionName.AddComment, PhotosController.ControllerName, null, submitButtonName);
        }

        #endregion

        #region Tags

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the tags index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkTagsIndex(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, TagsController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to a given tag.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="tag">The tag to link to.</param>
        /// <returns>An anchor tag for the tag.</returns>
        public static string ActionLinkTag(this HtmlHelper html, Tag tag)
        {
            var title = string.Format(CultureInfo.CurrentCulture, Resources.TagNavigateTo, tag.Name);
            return html.ActionLinkTag(tag, title);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to a given tag.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="tagInfo">The tag to link to.</param>
        /// <returns>An anchor tag for the tag.</returns>
        public static string ActionLinkTag(this HtmlHelper html, TagInfo tagInfo)
        {
            var title = string.Format(CultureInfo.CurrentCulture, Resources.TagNavigateTo, tagInfo.Tag.Name);
            title = string.Format(CultureInfo.CurrentCulture, "{0} ({1})", title, tagInfo.NumberOfPhotos);
            return html.ActionLinkTag(tagInfo.Tag, title);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to a given tag.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="tag">The tag to link to.</param>
        /// <param name="title">The title to use for the anchor.</param>
        /// <returns>An anchor tag for the tag.</returns>
        private static string ActionLinkTag(this HtmlHelper html, Tag tag, string title)
        {
            return html.ActionLink(tag.Name, ActionName.Tagged, PhotosController.ControllerName, new { id = tag.Name }, new { title = title });
        }

        #endregion

        #region Discovery

        ///// <summary>
        ///// Returns an anchor tag containing the virtual path to a given action.
        ///// </summary>
        ///// <param name="html">The HTML helper.</param>
        ///// <param name="linkText">The text to display as the anchor.</param>
        ///// <param name="actionInfo">The action to link to.</param>
        ///// <returns>An anchor tag for the action.</returns>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        //public static string ActionLinkAction(this HtmlHelper html, string linkText, ActionInfo actionInfo)
        //{
        //    return html.ActionLink(linkText, actionInfo.Name.ToLowerInvariant(), actionInfo.Controller.Name.ToLowerInvariant());
        //}

        #endregion

        #region Comments

        /// <summary>
        /// Returns a string to be displayed for a number of comments.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="count">The number of comments to display.</param>
        /// <returns>The singular or plural description of the number of comments, depending on the specified count.</returns>
        public static string NumberOfComments(this HtmlHelper html, int count)
        {
            if (count == 1)
            {
                return html.Encode(string.Format(CultureInfo.CurrentCulture, Resources.NumberOfCommentsSingular, count));
            }
            else
            {
                return html.Encode(string.Format(CultureInfo.CurrentCulture, Resources.NumberOfCommentsPlural, count));
            }
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the comments index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkCommentsIndex(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, CommentsController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the comments index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="commentId">The ID of the comment.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="cssClass">The CSS class to apply to the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkCommentEdit(this HtmlHelper html, string linkText, int commentId, string returnUrl, string cssClass)
        {
            return html.ActionLink(linkText, ActionName.Edit, CommentsController.ControllerName, new { id = commentId, returnUrl = returnUrl }, new { @class = cssClass });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the comments index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="commentId">The ID of the comment.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="cssClass">The CSS class to apply to the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkCommentDelete(this HtmlHelper html, string linkText, int commentId, string returnUrl, string cssClass)
        {
            return html.ActionLink(linkText, ActionName.Delete, CommentsController.ControllerName, new { id = commentId, returnUrl = returnUrl }, new { @class = cssClass });
        }

        /// <summary>
        /// Writes an opening form tag for the comments edit form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginCommentsEditForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Edit, CommentsController.ControllerName);
        }

        #endregion

        #region Admin

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the admin index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkAdminIndex(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, AdminController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the admin about page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkAdminAbout(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.About, AdminController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the admin event log page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkAdminEventLog(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.EventLog, AdminController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the admin urls page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkAdminUrls(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Urls, AdminController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the admin email test form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginAdminEmailTestForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.EmailTest, AdminController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the admin clear event log form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginAdminClearEventLogForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.ClearEventLog, AdminController.ControllerName);
        }

        #endregion

        #region Settings

        /// <summary>
        /// Writes an opening form tag for the settings edit form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginSettingsEditForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Edit, SettingsController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the settings edit user settings form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginSettingsEditUserSettingsForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.EditUserSettings, SettingsController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the settings add user setting form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginSettingsAddUserSettingForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Add, SettingsController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the user settings page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkSettingsUser(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.User, SettingsController.ControllerName);
        }

        #endregion

        #region Galleries

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the galleries index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkGalleriesIndex(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, GalleriesController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the gallery details page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="gallerySlug">The slug of the gallery to link to.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkGalleryDetails(this HtmlHelper html, string linkText, string gallerySlug)
        {
            return html.ActionLink(linkText, ActionName.Titled, GalleriesController.ControllerName, new { id = gallerySlug });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the gallery create page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkGalleryCreate(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Create, GalleriesController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the gallery edit page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="galleryId">The ID of the gallery.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkGalleryEdit(this HtmlHelper html, string linkText, int galleryId)
        {
            return html.ActionLinkGalleryEdit(linkText, galleryId, null);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the gallery edit page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="galleryId">The ID of the gallery.</param>
        /// <param name="cssClass">The CSS class to apply to the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkGalleryEdit(this HtmlHelper html, string linkText, int galleryId, string cssClass)
        {
            return html.ActionLink(linkText, ActionName.Edit, GalleriesController.ControllerName, new { id = galleryId }, new { @class = cssClass });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the gallery delete page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="galleryId">The ID of the gallery.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkGalleryDelete(this HtmlHelper html, string linkText, int galleryId)
        {
            return html.ActionLinkGalleryDelete(linkText, galleryId, null);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the gallery delete page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="galleryId">The ID of the gallery.</param>
        /// <param name="cssClass">The CSS class to apply to the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkGalleryDelete(this HtmlHelper html, string linkText, int galleryId, string cssClass)
        {
            return html.ActionLink(linkText, ActionName.Delete, GalleriesController.ControllerName, new { id = galleryId }, new { @class = cssClass });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the gallery create page with a parent gallery.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="parentGalleryId">The ID of the parent gallery.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkGalleryCreateChildGallery(this HtmlHelper html, string linkText, int parentGalleryId)
        {
            return html.ActionLink(linkText, ActionName.Create, GalleriesController.ControllerName, new { parentGalleryId = parentGalleryId });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the gallery manage page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkGalleriesManage(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.All, GalleriesController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the galleries create form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginGalleriesCreateForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Create, GalleriesController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the galleries create form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginGalleriesEditForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Edit, GalleriesController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the galleries move form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="formId">The ID of the form.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginGalleriesMoveForm(this HtmlHelper html, string formId)
        {
            return html.BeginForm(ActionName.Move, GalleriesController.ControllerName, formId);
        }

        #endregion

        #region Menus

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the menu manage page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkMenusManage(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, MenusController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the menu edit page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="menuId">The ID of the menu.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkMenuEdit(this HtmlHelper html, string linkText, int menuId)
        {
            return html.ActionLink(linkText, ActionName.Edit, MenusController.ControllerName, new { id = menuId });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the menu edit page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkMenuCreate(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Create, MenusController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the menu delete page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="menuId">The ID of the menu.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkMenuDelete(this HtmlHelper html, string linkText, int menuId)
        {
            return html.ActionLink(linkText, ActionName.Delete, MenusController.ControllerName, new { id = menuId });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the menu edit page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="pageSlug">The slug of the page.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkMenuCreateForPage(this HtmlHelper html, string linkText, string pageSlug)
        {
            return html.ActionLink(linkText, ActionName.CreateMenuForPage, MenusController.ControllerName, new { id = pageSlug });
        }

        /// <summary>
        /// Writes an opening form tag for the menu create form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginMenusCreateForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Create, MenusController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the menu edit form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginMenusEditForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Edit, MenusController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the menu move form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="formId">The ID of the form.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginMenusMoveForm(this HtmlHelper html, string formId)
        {
            return html.BeginForm(ActionName.Move, MenusController.ControllerName, formId);
        }

        #endregion

        #region Pages

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the page index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPagesIndex(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, PagesController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the page details page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="pageId">The ID of the page.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPageDetails(this HtmlHelper html, string linkText, int pageId)
        {
            return html.ActionLink(linkText, ActionName.Details, PagesController.ControllerName, new { id = pageId });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the page create page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPageCreate(this HtmlHelper html, string linkText)
        {
            return html.ActionLinkPageCreate(linkText, null);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the page create page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <param name="photoId">The optional photo ID to use as the photo for the page.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPageCreate(this HtmlHelper html, string linkText, int? photoId)
        {
            return html.ActionLink(linkText, ActionName.Create, PagesController.ControllerName, new { photoId = photoId });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the page edit page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="pageId">The ID of the page.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPageEdit(this HtmlHelper html, string linkText, int pageId)
        {
            return html.ActionLink(linkText, ActionName.Edit, new { id = pageId });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the page delete page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="pageId">The ID of the page.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPageDelete(this HtmlHelper html, string linkText, int pageId)
        {
            return html.ActionLink(linkText, ActionName.Delete, new { id = pageId });
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the page manage page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPagesManage(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, PagesController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the page create form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPagesCreateForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Create, PagesController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the page edit form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPagesEditForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Edit, PagesController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the page add comment form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="pageId">The ID of the page.</param>
        /// <param name="submitButtonName">The ID of the HTML submit button to disable when the form is submitted.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPagesAddCommentForm(this HtmlHelper html, int pageId, string submitButtonName)
        {
            return html.BeginForm(ActionName.AddComment, PagesController.ControllerName, new { pageId = pageId }, submitButtonName);
        }

        #endregion

        #region Anti-Spam Provider

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the anti-spam provider index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkAntiSpamProviderIndex(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, AntiSpamProviderController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the anti-spam provider selection form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginAntiSpamProviderSelectProviderForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.SelectProvider, AntiSpamProviderController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the anti-spam provider reset form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginAntiSpamProviderResetForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Reset, AntiSpamProviderController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the anti-spam provider edit form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginAntiSpamProviderEditForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Edit, AntiSpamProviderController.ControllerName);
        }

        #endregion

        #region Photo Provider

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the photo provider index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkPhotoProviderIndex(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, PhotoProviderController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the photo provider selection form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotoProviderSelectProviderForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.SelectProvider, PhotoProviderController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the photo provider reset form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotoProviderResetForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Reset, PhotoProviderController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the photo provider edit form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotoProviderEditForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.Edit, PhotoProviderController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the photo provider synchronize form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="submitButtonName">The ID of the HTML submit button to disable when the form is submitted.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotoProviderSynchronizeForm(this HtmlHelper html, string submitButtonName)
        {
            return html.BeginForm(ActionName.Synchronize, PhotoProviderController.ControllerName, null, submitButtonName);
        }

        /// <summary>
        /// Writes an opening form tag for the photo provider auto sync form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginPhotoProviderSaveAutoSyncForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.SaveAutoSync, PhotoProviderController.ControllerName);
        }

        #endregion

        #region Contact

        /// <summary>
        /// Writes an opening form tag for the contact send message form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginContactSendMessageForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.SendMessage, ContactController.ControllerName);
        }

        #endregion

        #region Account

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the account log off page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkAccountLogOff(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.LogOff, AccountController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the account log on page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkAccountLogOn(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.LogOn, AccountController.ControllerName);
        }

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the account change password page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkAccountChangePassword(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.ChangePassword, AccountController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the account change password form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginAccountChangePasswordForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.ChangePassword, AccountController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the account logon form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginAccountLogOnForm(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.LogOn, AccountController.ControllerName);
        }

        #endregion

        #region Services

        /// <summary>
        /// Returns an anchor tag containing the virtual path to the services index page.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="linkText">The inner text of the anchor tag.</param>
        /// <returns>An anchor tag.</returns>
        public static string ActionLinkServicesIndex(this HtmlHelper html, string linkText)
        {
            return html.ActionLink(linkText, ActionName.Index, ServicesController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the services enable Service API form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginServicesEnableServiceApi(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.EnableServiceApi, ServicesController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the services disable Service API form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginServicesDisableServiceApi(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.DisableServiceApi, ServicesController.ControllerName);
        }

        /// <summary>
        /// Writes an opening form tag for the services request new API key form.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>An <see cref="MvcForm"/> instance.</returns>
        public static MvcForm BeginServicesRequestNewApiKey(this HtmlHelper html)
        {
            return html.BeginForm(ActionName.RequestNewApiKey, ServicesController.ControllerName);
        }

        #endregion
    }
}