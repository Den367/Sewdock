using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Myembro.Extensions;
using Myembro.Infrastructure;
using Myembro.Models;
using Myembro.ViewModels;

namespace Myembro.Controllers
{
    [Description("Handles actions that have to do with pages.")]
    public class PagesController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "pages";

        #endregion

        #region Actions

        [Description("Shows all pages.")]
        [AuthorizeAdministrator]
        public ActionResult Index()
        {
            return ViewFor(ViewName.Index, r => r.GetPages().ToList());
        }

        [Description("Shows the page with a certain title.")]
        public ActionResult Titled([Description("The slug (URL-friendly version) of the page's title.")]string id)
        {
            return ViewFor(ViewName.Index, r => r.GetPages().ToList());
            //return ViewForPage(r => r.GetPageWithPhotoAndTagsAndCommentsBySlug(id));
        }

        [Description("Shows a single page.")]
        public ActionResult Details([Description("The page id.")]int id)
        {
            return ViewFor(ViewName.Index, r => r.GetPages().ToList());
           // return ViewForPage(r => r.GetPageWithPhotoAndTagsAndCommentsById(id));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[ValidateInput(false)]
        //public ActionResult AddComment([Bind(Exclude = EntityPropertyNameId)]Comment comment, int photoId, int pageId, bool rememberMe)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return ViewForPage(r => r.GetPageWithPhotoAndTagsAndCommentsById(pageId));
        //    }
        //    this.AddCommentToPhoto(photoId, comment, rememberMe);
        //    return RedirectToAction(ActionName.Details, new { id = pageId });
        //}

        //[Description("Allows the user to create a new page.")]
        //[AuthorizeAdministrator]
        //public ActionResult Create([Description("The optional photo id to show in the page.")]int? photoId)
        //{
        //    this.StorePhotoListInViewData(photoId, true);
        //    return ViewForCreate<Page>();
        //}

        //[AuthorizeAdministrator]
        //[AcceptVerbs(HttpVerbs.Post)]
        //[ValidateInput(false)]
        //public ActionResult Create([Bind(Exclude = EntityPropertyNameId)]Page page, int? photoId)
        //{
        //    this.StorePhotoListInViewData(photoId, true);
        //    return PerformSave(ViewName.Create, page, r =>
        //        {
        //            r.CreatePage(page, photoId);
        //        });
        //}

        //[Description("Allows the user to edit a page.")]
        //[AuthorizeAdministrator]
        //public ActionResult Edit([Description("The page id.")]int id)
        //{
        //    return ViewFor(ViewName.Edit, r =>
        //        {
        //            var page = r.GetPageWithPhotoById(id);
        //            int? photoId = null;
        //            if (page.EmbroideryItem != null)
        //            {
        //                photoId = page.EmbroideryItem.Id;
        //            }
        //            this.StorePhotoListInViewData(photoId, true);
        //            return page;
        //        });
        //}

        //[AuthorizeAdministrator]
        //[AcceptVerbs(HttpVerbs.Post)]
        //[ValidateInput(false)]
        //public ActionResult Edit(Page page, int? photoId)
        //{
        //    this.StorePhotoListInViewData(photoId, true);
        //    return PerformSave(ViewName.Edit, page, r => r.SavePage(page, photoId));
        //}

        [Description("Allows the user to delete a page.")]
        [AuthorizeAdministrator]
        public ActionResult Delete([Description("The page id.")]int id)
        {
            return ViewForDelete("Page", p => p.Title, r => r.GetPageById(id));
        }

        //[AuthorizeAdministrator]
        //[ActionName(ActionNameDelete)]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult DeletePost(int id)
        //{
        //    return PerformDelete(r => r.DeletePage(id));
        //}

        #endregion

        #region Helper Methods

        //private ActionResult ViewForPage(Func<EmbroRepository, Page> pageSelector)
        //{
        //    using (var repository = GetRepository())
        //    {
        //        var page = pageSelector(repository);
        //        if (page == null)
        //        {
        //            return View(ViewName.NotFound);
        //        }

        //        EmbroDetailsViewModel photo = null;
        //        if (page != null)
        //        {
        //            photo = this.GetEmbroDetailsViewModel(page.EmbroideryItem, page.HidePhotoText, page.HidePhotoComments);
        //        }
        //        return View(ViewName.Details, new PageViewModel(page, photo));
        //    }
        //}

        #endregion
    }
}