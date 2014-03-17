using System;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Security;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Repository;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with comments.")]
    public class CommentController : SiteControllerBase
    {
        #region [private members]

        private readonly ICommentRepository _repo;
        #endregion  [private members]
        #region [ctor]

        public CommentController(ICommentRepository repo)
        {
            _repo = repo;
        }

        #endregion [ctor]

        #region Constants

        public const string ControllerName = "comment";

        #endregion

        #region Actions

        [Description("Shows the latest comments.")]
        public ActionResult Index([Description("The number of items to show.")]int count, [Description("The page number to show.")]int page, int embroID)
        {
            var comments = _repo.GetCommentsForEmbro(embroID,page,count);
            return PartialView(ViewName.Index.ToString(),comments);
            //return ViewFor(ViewName.Index, r => r.GetCommentsForEmbro(embroID,page,count));
        }

        [Description("Shows the list of comments.")]
        [ChildActionOnly]
        public ActionResult List([Description("The number of items to show.")]int count, [Description("The page number to show.")]int page, int embroID)
        {
            var comments = _repo.GetCommentsForEmbro(embroID, page, count);
            return PartialView(ViewName.List.ToString(), comments);
            //return ViewFor(ViewName.List, r => r.GetCommentsForEmbro(embroID, page, count));
        }

        [Description("Allows the user to edit a comment.")]
        [AuthorizeAdministrator]
        public ActionResult Edit([Description("The comment id.")]int id, [Description("The URL to which to return.")]string returnUrl)
        {
            this.ViewData[ViewDataKeyReturnUrl] = returnUrl;
            
            return ViewFor(ViewName.Edit, r => r.GetCommentById(id));
        }

        [AuthorizeAdministrator]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Comment comment, string returnUrl)
        {
            ActionResult successResult;
            comment.DatePublished = DateTime.UtcNow;
            comment.UserName = User.Identity.Name;
            if (comment.IsCaptchaMatched()) _repo.EditComment(comment);
            // If the edit succeeded and a returl url was given then redirect to there, otherwise to the comments index.
            if (!string.IsNullOrEmpty(returnUrl))
            {
                successResult = Redirect(returnUrl);
            }
            else
            {
                successResult = RedirectToAction(ActionName.Index);
            }
            return successResult;
            //return PerformSave(ViewName.Edit, comment, r => r.SaveComment(comment), successResult);
        }

        [Description("Allows the user to delete a comment.")]
        [AuthorizeAdministrator]
        public ActionResult Delete([Description("The comment id.")]int id, [Description("The URL to which to return.")]string returnUrl)
        {
            this.ViewData[ViewDataKeyReturnUrl] = returnUrl;
            return ViewForDelete<Comment>("Comment", c => c.Text.TrimWithEllipsis(), r => r.GetCommentById(id));
        }

         [AcceptVerbs(HttpVerbs.Post)]
        [Compress]
        public ActionResult AddComment(Comment comment, [Description("The URL to which to return.")]string returnUrl)
         {
             if (comment.IsCaptchaMatched())
             {
                 comment.UserName = User.Identity.Name;
                 _repo.EditComment(comment);
             }
            return Redirect(returnUrl);
         
        }

         [AuthorizeAdministrator]
         [ActionName(ActionNameDelete)]
         [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult DeletePost(int id, string returnUrl)
         {
             _repo.DeleteComment(id);
             
             SetPageFlash("The comment was deleted.");
             return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction(ActionName.Index);
         }

        #endregion
    }
}