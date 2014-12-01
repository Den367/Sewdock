using System.ComponentModel;
using System.Web.Mvc;
using Myembro.Infrastructure;

namespace Myembro.Controllers
{
    [Description("Handles actions that have to do with redirecting to the proper home page.")]
    public class HomeController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "home";

        #endregion

        #region Actions

        [Description("Redirects the user to the proper home page.")]
        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}