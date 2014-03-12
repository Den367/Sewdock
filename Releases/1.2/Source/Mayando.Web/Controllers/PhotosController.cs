using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using JelleDruyts.Web.Mvc;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Properties;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with photos.")]
    public class PhotosController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "photos";

        #endregion

        #region Actions

        [Description("Shows an overview of the latest photos.")]
        public ActionResult Index([Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page)
        {
            return ViewFor(ViewName.Index, r => new PhotosPageViewModel(Resources.PhotosOverview, new PhotosViewModel(r.GetPhotosWithComments(PhotoDateType.Published, GetActualCount(count), GetActualPage(page)), NavigationDirection.Backward, this.SiteData.Settings.ShowFilmstripInNavigationContext)));
        }

        [Description("Shows an overview of the hidden photos.")]
        [AuthorizeAdministrator]
        public ActionResult Hidden([Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page)
        {
            return ViewFor(ViewName.Index, r => new PhotosPageViewModel(Resources.PhotosOverview, new PhotosViewModel(r.GetHiddenPhotosWithComments(PhotoDateType.Published, GetActualCount(count), GetActualPage(page)), this.SiteData.Settings.ShowFilmstripInNavigationContext)));
        }

        [Description("Shows the latest photo.")]
        public ActionResult Latest()
        {
            var result = ViewForPhotoByDate(null, PhotoDateType.Published, r => r.GetMostRecentPhotoWithTagsAndComments(PhotoDateType.Published), NavigationDirection.Backward);

            // Since this is the site root action and will be shown as the first page after
            // installing the application, show a welcome page if no photos are found.
            var viewResult = result as ViewResult;
            if (viewResult != null && string.Equals(viewResult.ViewName, ViewName.NotFound.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return View(ViewName.Welcome, this.SiteData);
            }
            else
            {
                return result;
            }
        }

        [Description("Shows a random photo.")]
        public ActionResult Random()
        {
            return ViewForPhoto(null, null, null, null, null, r => r.GetRandomPhotoWithTagsAndComments(), null);
        }

        [Description("Shows all photos published in a certain year, month or day.")]
        public ActionResult Published([Description("The year in which the photo is published.")]int? year, [Description("The month in which the photo is published.")]int? month, [Description("The day on which the photo is published.")]int? day, [Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page)
        {
            if (year.HasValue)
            {
                DateTimeOffset minDate, maxDate;
                string titleDatePart;
                GetBetweenDates(year, month, day, out minDate, out maxDate, out titleDatePart);
                return ViewFor(ViewName.Index, r => new PhotosPageViewModel(GetPageTitleForPhotosPublished(titleDatePart), new PhotosViewModel(r.GetPhotosWithCommentsBetween(PhotoDateType.Published, minDate, maxDate, GetActualCount(count, InfinitePageSize), GetActualPage(page)), this.SiteData.Settings.ShowFilmstripInNavigationContext)));
            }
            else
            {
                var links = new List<LinkListItem>();
                links.Add(new LinkListItem(this.Url.Action(ActionName.Published), Resources.CalendarByDatePublished, false));
                links.Add(new LinkListItem(this.Url.Action(ActionName.Taken), Resources.CalendarByDateTaken, true));
                return ViewFor(ViewName.Calendar, r => new CalendarViewModel(r.GetPhotos(PhotoDateType.Published), PhotoDateType.Published, true, this.SiteData.Settings.ShowDaysInCalendar, links));
            }
        }

        [Description("Shows all photos taken in a certain year, month or day.")]
        public ActionResult Taken([Description("The year in which the photo is taken.")]int? year, [Description("The month in which the photo is taken.")]int? month, [Description("The day on which the photo is taken.")]int? day, [Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page)
        {
            if (year.HasValue)
            {
                DateTimeOffset minDate, maxDate;
                string titleDatePart;
                GetBetweenDates(year, month, day, out minDate, out maxDate, out titleDatePart);
                return ViewFor(ViewName.Index, r => new PhotosPageViewModel(GetPageTitleForPhotosTaken(titleDatePart), new PhotosViewModel(r.GetPhotosWithCommentsBetween(PhotoDateType.Taken, minDate, maxDate, GetActualCount(count, InfinitePageSize), GetActualPage(page)), NavigationContextType.Taken, this.SiteData.Settings.ShowFilmstripInNavigationContext)));
            }
            else
            {
                var links = new List<LinkListItem>();
                links.Add(new LinkListItem(this.Url.Action(ActionName.Published), Resources.CalendarByDatePublished, true));
                links.Add(new LinkListItem(this.Url.Action(ActionName.Taken), Resources.CalendarByDateTaken, false));
                return ViewFor(ViewName.Calendar, r => new CalendarViewModel(r.GetPhotos(PhotoDateType.Taken), PhotoDateType.Taken, true, this.SiteData.Settings.ShowDaysInCalendar, links));
            }
        }

        [Description("Shows all photos with a certain text in their title.")]
        public ActionResult Titled([Description("The text to find in the photo's title.")]string id, [Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page)
        {
            using (var repository = GetRepository())
            {
                var photos = repository.GetPhotosWithCommentsTitled(PhotoDateType.Published, id, GetActualCount(count, InfinitePageSize), GetActualPage(page));
                if (photos.Count == 1)
                {
                    return ViewForPhotoByDate(null, PhotoDateType.Published, r => r.GetPhotoWithTagsAndCommentsById(photos[0].Id));
                }
                else
                {
                    return View(ViewName.Index, new PhotosPageViewModel(GetPageTitleForPhotosTitled(id), new PhotosViewModel(photos, NavigationContextType.Title, id, this.SiteData.Settings.ShowFilmstripInNavigationContext)));
                }
            }
        }

        [Description("Shows all photos with a certain tag or combination of tags.")]
        public ActionResult Tagged([Description("The tags to find, which can be combined with a + sign in between.")]string id, [Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page)
        {
            var tags = GetTagsFromQueryString(id);
            using (var repository = GetRepository())
            {
                var photos = repository.GetPhotosWithCommentsTagged(PhotoDateType.Published, tags, GetActualCount(count, InfinitePageSize), GetActualPage(page));
                return View(ViewName.Index, new PhotosPageViewModel(GetPageTitleForPhotosTagged(tags), new PhotosViewModel(photos, NavigationContextType.Tag, id, this.SiteData.Settings.ShowFilmstripInNavigationContext)));
            }
        }

        [Description("Finds all photos with a certain text in their title, description, tags or comments.")]
        public ActionResult Search([Description("The text to find.")] string searchText, [Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page)
        {
            return ViewFor(ViewName.Index, r => new PhotosPageViewModel(GetPageTitleForPhotosFoundFromSearch(searchText), new PhotosViewModel(r.FindPhotosWithComments(PhotoDateType.Published, searchText, GetActualCount(count, InfinitePageSize), GetActualPage(page)), NavigationContextType.Search, searchText, this.SiteData.Settings.ShowFilmstripInNavigationContext)));
        }

        [Description("Shows a single photo.")]
        public ActionResult Details([Description("The photo id.")]int id, [Description("The optional type of navigation context for the photo. Can be: taken, tag, search, title, gallery.")]NavigationContextType? context, [Description("The criteria for the navigation context type, if one is specified; i.e. the tag name, search text, title or gallery slug.")]string criteria, [Description("The optional delay in seconds to go to the next photo in the slideshow.")]int? slideshow)
        {
            if (context == NavigationContextType.Taken)
            {
                return ViewForPhotoByDate(slideshow, PhotoDateType.Taken, r => r.GetPhotoWithTagsAndCommentsById(id));
            }
            else if (context == NavigationContextType.Gallery)
            {
                return ViewForPhotoByGallery(slideshow, r => r.GetPhotoWithTagsAndCommentsById(id), criteria);
            }
            else if (context == NavigationContextType.Tag)
            {
                return ViewForPhotoByTag(slideshow, r => r.GetPhotoWithTagsAndCommentsById(id), criteria);
            }
            else if (context == NavigationContextType.Search)
            {
                return ViewForPhotoBySearch(slideshow, r => r.GetPhotoWithTagsAndCommentsById(id), criteria);
            }
            else if (context == NavigationContextType.Title)
            {
                return ViewForPhotoByTitle(slideshow, r => r.GetPhotoWithTagsAndCommentsById(id), criteria);
            }
            return ViewForPhotoByDate(slideshow, PhotoDateType.Published, r => r.GetPhotoWithTagsAndCommentsById(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult AddComment([Bind(Exclude = EntityPropertyNameId)]Comment comment, int photoId, bool rememberMe)
        {
            if (!this.ModelState.IsValid)
            {
                return Details(photoId, null, null, null);
            }
            var savedComment = this.AddCommentToPhoto(photoId, comment, rememberMe);
            if (savedComment == null)
            {
                // The comment was not saved, probably because it was spam.
                return RedirectToAction(ActionName.Details, PhotosController.ControllerName, new { id = photoId });
            }
            else
            {
                return Redirect(Url.CommentDetails(savedComment));
            }
        }

        [Description("Allows the user to create a new photo.")]
        [AuthorizeAdministrator]
        public ActionResult Create()
        {
            return ViewForCreate<Photo>(p => p.DatePublished = DateTimeOffset.UtcNow);
        }

        [AuthorizeAdministrator]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Exclude = EntityPropertyNameId)]Photo photo, string tagList)
        {
            var tags = Converter.ToTags(tagList);
            return PerformSave(ViewName.Create, photo, r => r.CreatePhoto(photo, tags));
        }

        [Description("Allows the user to edit a photo.")]
        [AuthorizeAdministrator]
        public ActionResult Edit([Description("The photo id.")]int id)
        {
            return ViewFor(ViewName.Edit, r => r.GetPhotoWithTagsById(id));
        }

        [AuthorizeAdministrator]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(Photo photo, string tagList)
        {
            var tags = Converter.ToTags(tagList);
            return PerformSave(ViewName.Edit, photo, r => r.SavePhoto(photo, tags), RedirectToAction(ActionName.Details, new { id = photo.Id }));
        }

        [Description("Allows the user to delete a photo.")]
        [AuthorizeAdministrator]
        public ActionResult Delete([Description("The photo id.")]int id)
        {
            return ViewForDelete<Photo>("Photo", p => p.DisplayTitle, r => r.GetPhotoById(id));
        }

        [AuthorizeAdministrator]
        [ActionName(ActionNameDelete)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeletePost(int id)
        {
            return PerformDelete(r => r.DeletePhoto(id), ActionName.Latest);
        }

        [AuthorizeAdministrator]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BulkEdit(BulkEditOperation operation, IDictionary<int, bool> photos, string tagList, string returnUrl)
        {
            var selectedPhotoIds = (from p in photos
                                    where p.Value
                                    select p.Key).ToList();
            var noWorkNeeded = (selectedPhotoIds.Count == 0);
            var operationDescription = string.Empty;
            var description = string.Empty;
            switch (operation)
            {
                case BulkEditOperation.Delete:
                    operationDescription = "Delete";
                    description = string.Format(CultureInfo.CurrentCulture, "Are you sure you want to delete the {0} selected photos?", selectedPhotoIds.Count);
                    break;
                case BulkEditOperation.AddTags:
                    noWorkNeeded = noWorkNeeded || string.IsNullOrEmpty(tagList);
                    operationDescription = "Adding Tags";
                    description = string.Format(CultureInfo.CurrentCulture, "Are you sure you want to add the tags \"{0}\" to the {1} selected photos?", tagList, selectedPhotoIds.Count);
                    break;
                case BulkEditOperation.RemoveTags:
                    noWorkNeeded = noWorkNeeded || string.IsNullOrEmpty(tagList);
                    operationDescription = "Removing Tags";
                    description = string.Format(CultureInfo.CurrentCulture, "Are you sure you want to remove the tags \"{0}\" from the {1} selected photos?", tagList, selectedPhotoIds.Count);
                    break;
                case BulkEditOperation.Hide:
                    operationDescription = "Hide";
                    description = string.Format(CultureInfo.CurrentCulture, "Are you sure you want to hide the {0} selected photos?", selectedPhotoIds.Count);
                    break;
                case BulkEditOperation.Unhide:
                    operationDescription = "Unhide";
                    description = string.Format(CultureInfo.CurrentCulture, "Are you sure you want to make the {0} selected photos visible again?", selectedPhotoIds.Count);
                    break;
                default:
                    throw new ArgumentException("The requested bulk edit operation is not supported: " + operation.ToString());
            }
            if (noWorkNeeded)
            {
                return Redirect(returnUrl);
            }
            else
            {
                var model = new ConfirmBulkEditViewModel(operation, operationDescription, description, selectedPhotoIds, tagList, returnUrl);
                this.TempData["model"] = model;
                return View(ViewName.Confirm, model);
            }
        }

        [AuthorizeAdministrator]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BulkEditExecute()
        {
            var model = (ConfirmBulkEditViewModel)this.TempData["model"];

            using (var repository = GetRepository(true))
            {
                switch (model.Operation)
                {
                    case BulkEditOperation.Delete:
                        repository.DeletePhotos(model.PhotoIds);
                        break;
                    case BulkEditOperation.AddTags:
                        repository.AddTagsToPhotos(model.PhotoIds, Converter.ToTags(model.Tags));
                        break;
                    case BulkEditOperation.RemoveTags:
                        repository.RemoveTagsFromPhotos(model.PhotoIds, Converter.ToTags(model.Tags));
                        break;
                    case BulkEditOperation.Hide:
                        repository.HidePhotos(model.PhotoIds);
                        break;
                    case BulkEditOperation.Unhide:
                        repository.UnhidePhotos(model.PhotoIds);
                        break;
                }
                repository.CommitChanges();
            }

            return Redirect(model.ReturnUrl);
        }

        #endregion

        #region Helper Methods

        private static string[] GetTagsFromQueryString(string tags)
        {
            if (string.IsNullOrEmpty(tags))
            {
                return new string[0];
            }
            else
            {
                return tags.Split('+');
            }
        }

        private static string GetPageTitleForPhotosPublished(string titleDatePart)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.PhotosPublished, titleDatePart);
        }

        private static string GetPageTitleForPhotosTaken(string titleDatePart)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.PhotosTaken, titleDatePart);
        }

        private static string GetPageTitleForPhotosTagged(string[] tags)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.PhotosTagged, string.Join(" ", tags));
        }

        private static string GetPageTitleForPhotosTitled(string title)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.PhotosTitled, title);
        }

        private static string GetPageTitleForPhotosFoundFromSearch(string searchText)
        {
            return string.Format(CultureInfo.CurrentCulture, Resources.PhotosFoundFromSearch, searchText);
        }

        private ActionResult ViewForPhotoByDate(int? slideshow, PhotoDateType type, Func<MayandoRepository, Photo> photoSelector)
        {
            return ViewForPhotoByDate(slideshow, type, photoSelector, NavigationDirection.Forward);
        }

        private ActionResult ViewForPhotoByDate(int? slideshow, PhotoDateType type, Func<MayandoRepository, Photo> photoSelector, NavigationDirection initialSlideshowDirection)
        {
            NavigationContextType? navigationContextType = null;
            string navigationContextOverviewUrl = null;
            Func<MayandoRepository, string> navigationContextDescriptionSelector = null;
            if (type == PhotoDateType.Taken)
            {
                navigationContextType = NavigationContextType.Taken;
                navigationContextOverviewUrl = Url.Action(ActionName.Taken, PhotosController.ControllerName);
                navigationContextDescriptionSelector = (r => Resources.PhotosByDateTaken);
            }
            return ViewForPhoto(slideshow, navigationContextType, null, navigationContextOverviewUrl, initialSlideshowDirection, navigationContextDescriptionSelector, photoSelector, (repository, photo) =>
            {
                var navigationContextPhotos = new List<Photo>();

                // Retrieve all photos on or before the requested photo's date.
                var previousPhotos = repository.GetPhotosOnOrBefore(type, photo.GetDate(type), 1 + this.SiteData.Settings.NumberOfPhotosInFilmstrip);
                foreach (var previousPhoto in previousPhotos)
                {
                    // Filter out the current photo and any photo that has the same published date but a larger Id.
                    // This enforces an absolute ordering based on PublishedDate (not unique) and Id (unique).
                    if (previousPhoto.DatePublished == photo.DatePublished && previousPhoto.Id >= photo.Id)
                    {
                        continue;
                    }
                    navigationContextPhotos.Add(previousPhoto);
                }

                // Add the current photo.
                navigationContextPhotos.Add(photo);

                // Retrieve all photos on or after the requested photo's date.
                var nextPhotos = repository.GetPhotosOnOrAfter(type, photo.GetDate(type), 1 + this.SiteData.Settings.NumberOfPhotosInFilmstrip);
                foreach (var nextPhoto in nextPhotos)
                {
                    // Filter out the current photo and any photo that has the same published date but a smaller Id.
                    // This enforces an absolute ordering based on PublishedDate (not unique) and Id (unique).
                    if (nextPhoto.DatePublished == photo.DatePublished && nextPhoto.Id <= photo.Id)
                    {
                        continue;
                    }
                    navigationContextPhotos.Add(nextPhoto);
                }
                return navigationContextPhotos;
            });
        }

        private ActionResult ViewForPhotoByTag(int? slideshow, Func<MayandoRepository, Photo> photoSelector, string navigationContextCriteria)
        {
            var tags = GetTagsFromQueryString(navigationContextCriteria);
            return ViewForPhoto(slideshow, NavigationContextType.Tag, navigationContextCriteria, Url.Action(ActionName.Tagged, PhotosController.ControllerName, new { id = navigationContextCriteria }), r => GetPageTitleForPhotosTagged(tags), photoSelector, (repository, photo) =>
            {
                return repository.GetPhotosWithCommentsTagged(PhotoDateType.Published, tags, int.MaxValue, 0);
            });
        }

        private ActionResult ViewForPhotoBySearch(int? slideshow, Func<MayandoRepository, Photo> photoSelector, string searchText)
        {
            return ViewForPhoto(slideshow, NavigationContextType.Search, searchText, Url.Action(ActionName.Search, PhotosController.ControllerName, new { searchText = searchText }), r => GetPageTitleForPhotosFoundFromSearch(searchText), photoSelector, (repository, photo) =>
            {
                return repository.FindPhotosWithComments(PhotoDateType.Published, searchText, int.MaxValue, 0);
            });
        }

        private ActionResult ViewForPhotoByTitle(int? slideshow, Func<MayandoRepository, Photo> photoSelector, string title)
        {
            return ViewForPhoto(slideshow, NavigationContextType.Title, title, Url.Action(ActionName.Titled, PhotosController.ControllerName, new { id = title }), r => GetPageTitleForPhotosTitled(title), photoSelector, (repository, photo) =>
            {
                return repository.GetPhotosWithCommentsTitled(PhotoDateType.Published, title, int.MaxValue, 0);
            });
        }

        private ActionResult ViewForPhotoByGallery(int? slideshow, Func<MayandoRepository, Photo> photoSelector, string gallery)
        {
            return ViewForPhoto(slideshow, NavigationContextType.Gallery, gallery, Url.Action(ActionName.Titled, GalleriesController.ControllerName, new { id = gallery }), r => { var g = r.GetGalleryBySlug(gallery); return (g == null ? gallery : g.Title); }, photoSelector, (repository, photo) =>
            {
                var requestedGallery = repository.GetGalleryWithTagsAndCoverPhotoBySlug(gallery);
                if (requestedGallery == null)
                {
                    return new Photo[0];
                }
                return this.GetPhotosForGallery(repository, requestedGallery, int.MaxValue, 0);
            });
        }

        private ActionResult ViewForPhoto(int? slideshow, NavigationContextType? navigationContextType, string navigationContextCriteria, string navigationContextOverviewUrl, Func<MayandoRepository, string> navigationContextDescriptionSelector, Func<MayandoRepository, Photo> photoSelector, Func<MayandoRepository, Photo, IList<Photo>> navigationContextPhotoSelector)
        {
            return ViewForPhoto(slideshow, navigationContextType, navigationContextCriteria, navigationContextOverviewUrl, NavigationDirection.Forward, navigationContextDescriptionSelector, photoSelector, navigationContextPhotoSelector);
        }

        private ActionResult ViewForPhoto(int? slideshow, NavigationContextType? navigationContextType, string navigationContextCriteria, string navigationContextOverviewUrl, NavigationDirection initialSlideshowDirection, Func<MayandoRepository, string> navigationContextDescriptionSelector, Func<MayandoRepository, Photo> photoSelector, Func<MayandoRepository, Photo, IList<Photo>> navigationContextPhotoSelector)
        {
            using (var repository = GetRepository())
            {
                // Select the photo.
                var photo = photoSelector(repository);
                if (photo == null)
                {
                    return View(ViewName.NotFound);
                }
                this.MasterViewModel.AddKeywords(photo.TagNames);

                // Determine the navigation context.
                IList<Photo> navigationContextPhotos = null;
                if (navigationContextPhotoSelector != null)
                {
                    navigationContextPhotos = navigationContextPhotoSelector(repository, photo);
                }
                string navigationContextDescription = null;
                if (navigationContextDescriptionSelector != null)
                {
                    navigationContextDescription = navigationContextDescriptionSelector(repository);
                }

                // Create the ViewModel and show the View.
                var photoDetailsViewModel = this.GetPhotoDetailsViewModel(photo, false, false);
                var navigationContext = new NavigationContext(navigationContextPhotos, photo, navigationContextType, navigationContextCriteria, navigationContextDescription, navigationContextOverviewUrl, slideshow, initialSlideshowDirection, this.SiteData.Settings.ShowFilmstripInNavigationContext);
                var model = new PhotoViewModel(photoDetailsViewModel, navigationContext);
                if (slideshow.HasValue)
                {
                    this.MasterViewModel.SlideshowDelay = slideshow;
                    if (slideshow.Value > 0 && navigationContext.Next != null)
                    {
                        this.MasterViewModel.SlideshowNextUrl = Url.PhotoDetails(navigationContext.Next, navigationContext.Type, navigationContext.Criteria, slideshow.Value);
                    }
                    if (slideshow.Value < 0 && navigationContext.Previous != null)
                    {
                        this.MasterViewModel.SlideshowNextUrl = Url.PhotoDetails(navigationContext.Previous, navigationContext.Type, navigationContext.Criteria, slideshow.Value);
                    }
                    return View(MasterPageName.Slideshow, ViewName.Slideshow, model);
                }
                else
                {
                    return View(ViewName.Details, model);
                }
            }
        }

        private static void GetBetweenDates(int? year, int? month, int? day, out DateTimeOffset minDate, out DateTimeOffset maxDate, out string titleDatePart)
        {
            if (!year.HasValue)
            {
                minDate = DateTimeOffsetExtensions.MinValue;
                maxDate = DateTimeOffset.MaxValue;
                titleDatePart = null;
            }
            else
            {
                if (!month.HasValue)
                {
                    minDate = new DateTimeOffset(year.Value, 1, 1, 0, 0, 0, TimeSpan.Zero);
                    maxDate = minDate.AddYears(1);
                    titleDatePart = string.Format(CultureInfo.CurrentCulture, Resources.PhotosDateTitleInYear, year.Value);
                }
                else
                {
                    if (!day.HasValue)
                    {
                        minDate = new DateTimeOffset(year.Value, month.Value, 1, 0, 0, 0, TimeSpan.Zero);
                        maxDate = minDate.AddMonths(1);
                        titleDatePart = string.Format(CultureInfo.CurrentCulture, Resources.PhotosDateTitleInMonth, minDate.ToString("y", CultureInfo.CurrentCulture));
                    }
                    else
                    {
                        minDate = new DateTimeOffset(year.Value, month.Value, day.Value, 0, 0, 0, TimeSpan.Zero);
                        maxDate = minDate.AddDays(1);
                        titleDatePart = string.Format(CultureInfo.CurrentCulture, Resources.PhotosDateTitleOnDay, minDate.ToLongDateString());
                    }
                }
            }
            // The actual dates to use need to be adjusted for the configured time zone.
            minDate = minDate.AdjustToUtc();
            maxDate = maxDate.AdjustToUtc();
        }

        #endregion
    }
}