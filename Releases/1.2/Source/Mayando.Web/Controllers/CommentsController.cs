using System.ComponentModel;
using System.Web.Mvc;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with comments.")]
    public class CommentsController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "comments";

        #endregion

        #region Actions

        [Description("Shows the latest comments.")]
        public ActionResult Index([Description("The number of items to show.")]int? count, [Description("The page number to show.")]int? page)
        {
            return ViewFor(ViewName.Index, r => r.GetLatestCommentsWithPhoto(GetActualCount(count), GetActualPage(page)));
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
            // If the edit succeeded and a returl url was given then redirect to there, otherwise to the comments index.
            if (!string.IsNullOrEmpty(returnUrl))
            {
                successResult = Redirect(returnUrl);
            }
            else
            {
                successResult = RedirectToAction(ActionName.Index);
            }
            return PerformSave(ViewName.Edit, comment, r => r.SaveComment(comment), successResult);
        }

        [Description("Allows the user to delete a comment.")]
        [AuthorizeAdministrator]
        public ActionResult Delete([Description("The comment id.")]int id, [Description("The URL to which to return.")]string returnUrl)
        {
            this.ViewData[ViewDataKeyReturnUrl] = returnUrl;
            return ViewForDelete<Comment>("Comment", c => c.Text.TrimWithEllipsis(), r => r.GetCommentById(id));
        }

        [AuthorizeAdministrator]
        [ActionName(ActionNameDelete)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeletePost(int id, string returnUrl)
        {
            PerformDelete(r => r.DeleteComment(id));
            SetPageFlash("The comment was deleted.");
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(ActionName.Index);
            }
        }

        #endregion
    }
}