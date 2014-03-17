using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with galleries.")]
    public class GalleriesController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "galleries";

        #endregion

        #region Actions

        //[Description("Shows all top-level galleries.")]
        //public ActionResult Index()
        //{
        //    return ViewFor(ViewName.Index, r =>
        //        {
        //            var galleries = r.GetTopLevelGalleriesWithTagsAndCoverPhotoAndChildGalleriesWithTheirTags();
        //            return GetGalleryInfo(galleries, r);
        //        });
        //}

        //[Description("Shows all galleries.")]
        //public ActionResult All()
        //{
        //    return ViewFor(ViewName.Index, r =>
        //    {
        //        var galleries = r.GetGalleriesWithTagsAndCoverPhotoAndChildGalleriesWithTheirTags();
        //        return GetGalleryInfo(galleries, r);
        //    });
        //}

        //[Description("Shows a single gallery.")]
        //public ActionResult Details([Description("The gallery id.")]int id, [Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page)
        //{
        //    return ViewForGallery(r => r.GetGalleryWithCoverPhotoByIdAndLoadAllGalleriesWithTags(id), count, page);
        //}

        //[Description("Shows the gallery with a certain title.")]
        //public ActionResult Titled([Description("The slug (URL-friendly version) of the gallery's title.")]string id, [Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page)
        //{
        //    return ViewForGallery(r => r.GetGalleryWithCoverPhotoBySlugAndLoadAllGalleriesWithTags(id), count, page);
        //}

        //[Description("Allows the user to create a new gallery.")]
        //[AuthorizeAdministrator]
        //public ActionResult Create([Description("The photo id to show as the cover for the gallery.")]int? coverPhotoId, [Description("The gallery id to show as the parent gallery for the gallery.")]int? parentGalleryId)
        //{
        //    this.StorePhotoListInViewData(coverPhotoId, true);
        //    this.StoreGalleryListInViewData(parentGalleryId, true);
        //    return ViewForCreate<Gallery>((r, g) =>
        //    {
        //        g.Sequence = r.GetMaxGallerySequence() + 1;
        //    });
        //}

        //[AuthorizeAdministrator]
        //[AcceptVerbs(HttpVerbs.Post)]
        //[ValidateInput(false)]
        //public ActionResult Create([Bind(Exclude = EntityPropertyNameId)]Gallery gallery, int? coverPhotoId, int? parentGalleryId, string tagList)
        //{
        //    var tags = Converter.ToTags(tagList);
        //    this.StorePhotoListInViewData(coverPhotoId, true);
        //    this.StoreGalleryListInViewData(parentGalleryId, true);
        //    return PerformSave(ViewName.Create, gallery, r => r.CreateGallery(gallery, coverPhotoId, tags, parentGalleryId), RedirectToAction(ActionName.All));
        //}

        //[Description("Allows the user to edit a gallery.")]
        //[AuthorizeAdministrator]
        //public ActionResult Edit([Description("The gallery id.")]int id)
        //{
        //    return ViewFor(ViewName.Edit, r =>
        //    {
        //        var gallery = r.GetGalleryWithTagsAndCoverPhotoAndParentGalleryById(id);
        //        int? selectedPhotoId = null;
        //        if (gallery.CoverPhoto != null)
        //        {
        //            selectedPhotoId = gallery.CoverPhoto.Id;
        //        }
        //        int? selectedParentGalleryId = null;
        //        if (gallery.ParentGallery != null)
        //        {
        //            selectedParentGalleryId = gallery.ParentGallery.Id;
        //        }
        //        this.StorePhotoListInViewData(selectedPhotoId, true);
        //        this.StoreGalleryListInViewData(selectedParentGalleryId, true);
        //        return gallery;
        //    });
        //}

        //[AuthorizeAdministrator]
        //[AcceptVerbs(HttpVerbs.Post)]
        //[ValidateInput(false)]
        //public ActionResult Edit(Gallery gallery, int? coverPhotoId, int? parentGalleryId, string tagList)
        //{
        //    var tags = Converter.ToTags(tagList);
        //    this.StorePhotoListInViewData(coverPhotoId, true);
        //    this.StoreGalleryListInViewData(parentGalleryId, true);
        //    return PerformSave(ViewName.Edit, gallery, r => r.SaveGallery(gallery, coverPhotoId, tags, parentGalleryId), RedirectToAction(ActionName.All));
        //}

        //[Description("Allows the user to delete a gallery.")]
        //[AuthorizeAdministrator]
        //public ActionResult Delete([Description("The gallery id.")]int id)
        //{
        //    return ViewForDelete("Gallery", g => g.Title, r => r.GetGalleryById(id));
        //}

        //[AuthorizeAdministrator]
        //[ActionName(ActionNameDelete)]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult DeletePost(int id)
        //{
        //    return PerformDelete(r => r.DeleteGallery(id), ActionName.All);
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Move(int id, Direction direction)
        //{
        //    using (var repository = GetRepository(true))
        //    {
        //        var galleries = repository.GetGalleries();
        //        var currentItem = (ISupportSequence)galleries.FirstOrDefault(m => m.Id == id);
        //        this.MoveInSequence(direction, galleries.Cast<ISupportSequence>(), currentItem);
        //        repository.CommitChanges();
        //        return RedirectToAction(ActionName.All);
        //    }
        //}

        #endregion

        #region Helper Methods

        //private ActionResult ViewForGallery(Func<EmbroRepository, Gallery> gallerySelector, int? count, int? page)
        //{
        //    using (var repository = GetRepository())
        //    {
        //        var gallery = gallerySelector(repository);
        //        if (gallery == null)
        //        {
        //            return View(ViewName.NotFound);
        //        }
        //        var photos = this.GetPhotosForGallery(repository, gallery, GetActualCount(count), GetActualPage(page));
        //        var childGalleries = GetGalleryInfo(gallery.ChildGalleries, repository);
        //        var parentGalleries = new List<Gallery>();
        //        var currentParent = gallery.ParentGallery;
        //        while (currentParent != null && !parentGalleries.Contains(currentParent) && currentParent != gallery)
        //        {
        //            parentGalleries.Insert(0, currentParent);
        //            currentParent = currentParent.ParentGallery;
        //        }
        //        return View(ViewName.Details, new GalleryViewModel(gallery, new PhotosViewModel(photos, NavigationContextType.Gallery, gallery.Slug, this.SiteData.Settings.ShowFilmstripInNavigationContext), childGalleries, parentGalleries));
        //    }
        //}

        //private ICollection<GalleryInfoViewModel> GetGalleryInfo(IEnumerable<Gallery> galleries, EmbroRepository r)
        //{
        //    return galleries.Select(g => new GalleryInfoViewModel(g, this.GetPhotosForGallery(r, g, 0, 0).TotalItemCount, g.ChildGalleries.Count)).ToList();
        //}

        #endregion
    }
}