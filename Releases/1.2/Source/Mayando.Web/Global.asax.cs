using System.Globalization;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using JelleDruyts.Web.Mvc;
using Mayando.Web.Controllers;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;

namespace Mayando.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Constants

        internal const string RouteNameDefault = "Default";

        #endregion

        #region Register Routes

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "photos/published/year/month/day",
                string.Format(CultureInfo.InvariantCulture, "{0}/{1}", PhotosController.ControllerName, ActionName.Published.ToActionString()) + "/{year}/{month}/{day}",
                new { controller = PhotosController.ControllerName, action = ActionName.Published.ToActionString(), month = string.Empty, day = string.Empty }
            );

            routes.MapRoute(
                "photos/taken/year/month/day",
                string.Format(CultureInfo.InvariantCulture, "{0}/{1}", PhotosController.ControllerName, ActionName.Taken.ToActionString()) + "/{year}/{month}/{day}",
                new { controller = PhotosController.ControllerName, action = ActionName.Taken.ToActionString(), month = string.Empty, day = string.Empty }
            );

            routes.MapRoute(
                "photos/search/searchText",
                string.Format(CultureInfo.InvariantCulture, "{0}/{1}", PhotosController.ControllerName, ActionName.Search.ToActionString()) + "/{searchText}",
                new { controller = PhotosController.ControllerName, action = ActionName.Search.ToActionString() }
            );

            routes.MapRoute(
                "photos/details/id/by/context/criteria",
                string.Format(CultureInfo.InvariantCulture, "{0}/{1}", PhotosController.ControllerName, ActionName.Details.ToActionString()) + "/{id}/by/{context}/{criteria}",
                new { controller = PhotosController.ControllerName, action = ActionName.Details.ToActionString(), criteria = string.Empty }
            );

            routes.MapRoute(
                RouteNameDefault,
                "{controller}/{action}/{id}",
                new { action = ActionName.Index.ToActionString(), id = string.Empty }
            );

            routes.MapRoute(
                "Root",
                "{controller}/{action}",
                new { controller = HomeController.ControllerName, action = ActionName.Index.ToActionString() }
            );
        }

        #endregion

        #region Application Start & End

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected void Application_Start()
        {
            Logger.Log(LogLevel.Information, string.Format(CultureInfo.InvariantCulture, "Application started (v{0}).", Assembly.GetExecutingAssembly().GetName().Version.ToString()));

            RegisterRoutes(RouteTable.Routes);

            // Replace the Default WebFormViewEngine with our custom ThemedWebFormViewEngine
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemedWebFormViewEngine());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected void Application_End()
        {
            Logger.Log(LogLevel.Information, string.Format(CultureInfo.InvariantCulture, "Application shut down for the following reason: {0}.", HostingEnvironment.ShutdownReason.ToString()));
        }

        #endregion
    }
}