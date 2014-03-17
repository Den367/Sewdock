using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with menus.")]
    [AuthorizeAdministrator]
    public class MenusController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "menus";

        #endregion

        #region Actions

        [Description("Shows all menus.")]
        public ActionResult Index()
        {
            return ViewFor(ViewName.Index, r => r.GetMenus().ToList());
        }

        [Description("Allows the user to create a new menu.")]
        public ActionResult Create([Description("The optional url to set for the menu.")]string url)
        {
            return ViewForCreate<Menu>((r, m) =>
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        m.Url = url;
                    }
                    m.Sequence = r.GetMaxMenuSequence() + 1;
                });
        }

        [Description("Allows the user to create a new menu for a page.")]
        public ActionResult CreateMenuForPage([Description("The optional url to set for the menu.")]string id)
        {
            return ViewForCreate<Menu>((r, m) =>
            {
                if (!string.IsNullOrEmpty(id))
                {
                    m.Url = SiteData.GenerateRelativeUrl(this.ControllerContext.RequestContext, PagesController.ControllerName, ActionName.Titled, new { id = id });
                    var page = r.GetPageBySlug(id);
                    if (page != null)
                    {
                        m.Title = page.Title;
                    }
                }
                m.Sequence = r.GetMaxMenuSequence() + 1;
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = EntityPropertyNameId)]Menu menu)
        {
            return PerformSave(ViewName.Create, menu, r => r.CreateMenu(menu));
        }

        [Description("Allows the user to edit a menu.")]
        public ActionResult Edit([Description("The menu id.")]int id)
        {
            return ViewFor(ViewName.Edit, r => r.GetMenuById(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Menu menu)
        {
            return PerformSave(ViewName.Edit, menu, r => r.SaveMenu(menu));
        }

        [Description("Allows the user to delete a menu.")]
        public ActionResult Delete([Description("The menu id.")]int id)
        {
            return ViewForDelete("Menu", m => m.Title, r => r.GetMenuById(id));
        }

        [ActionName(ActionNameDelete)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeletePost(int id)
        {
            return PerformDelete(r => r.DeleteMenu(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Move(int id, Direction direction)
        {
            using (var repository = GetRepository(true))
            {
                var menus = repository.GetMenus();
                var currentItem = (ISupportSequence)menus.FirstOrDefault(m => m.Id == id);
                this.MoveInSequence(direction, menus.Cast<ISupportSequence>(), currentItem);
                repository.CommitChanges();
                return RedirectToAction(ActionName.Index);
            }
        }

        #endregion
    }
}