using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IdentitySample.Controllers;
using Myembro.Controllers;
using Myembro.Extensions;
using Myembro.Models;
using Myembro.Properties;

namespace Myembro.Infrastructure
{
    /// <summary>
    /// Blocks HTTP POST requests when in demo mode.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class BlockHttpPostInDemoModeAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            // Block the request when in demo mode...
            if (SiteData.GlobalDemoMode)
            {
                // ...except when doing an HTTP GET...
                if (!string.Equals("get", filterContext.HttpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase))
                {
                    // ...and except when an administrator...
                    if (!filterContext.HttpContext.User.IsAdministrator())
                    {
                        // ...and except for the account controller (so the administrator can still log on).
                        if (!(filterContext.Controller is AccountController))
                        {
                            // Show an error page with the demo mode page flash message to indicate that this action was not allowed.
                            var controllerName = (string)filterContext.RouteData.Values["controller"];
                            var actionName = (string)filterContext.RouteData.Values["action"];
                            var model = new HandleErrorInfo(new UnauthorizedAccessException(Myembro.Properties.Resources.DemoModePageFlash), controllerName, actionName);
                            filterContext.Result = new ViewResult { ViewName = ViewName.Error.ToString(), MasterName = MasterPageName.Basic.ToString(), ViewData = new ViewDataDictionary<HandleErrorInfo>(model), TempData = filterContext.Controller.TempData };
                        }
                    }
                }
            }
        }
    }
}