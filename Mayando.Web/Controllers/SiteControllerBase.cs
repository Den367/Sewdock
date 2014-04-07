using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using JelleDruyts.Web.Mvc;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Properties;
using Mayando.Web.ViewModels;
using Mayando.Web.DataAccess;

namespace Mayando.Web.Controllers
{



   
    /// <summary>
    /// Providers a base class for controllers.
    /// </summary>
    [LogError]
    [HandleError(View = "Error", Master = "Basic")]    
    [SessionState(SessionStateBehavior.Disabled)]
    public abstract class SiteControllerBase : Controller
    {
        protected readonly IEmbroRepository Repo;

        #region Constants

        /// <summary>
        /// The key in the ViewData under which the Master ViewModel is stored.
        /// </summary>
        public const string ViewDataKeyMasterViewModel = "MasterViewModel";

        /// <summary>
        /// The key in the ViewData under which the return URL is stored.
        /// </summary>
        public const string ViewDataKeyReturnUrl = "ReturnUrl";

        /// <summary>
        /// The key in the ViewData under which the photos list is stored.
        /// </summary>
        public const string ViewDataKeyPhotos = "Photos";

        /// <summary>
        /// The key in the ViewData under which the gallery list is stored.
        /// </summary>
        public const string ViewDataKeyGalleries = "Galleries";

        /// <summary>
        /// The key in the ViewData under which the log level list is stored.
        /// </summary>
        public const string ViewDataKeyLogLevels = "LogLevels";

        /// <summary>
        /// The parameter value to request the large photo size.
        /// </summary>
        public const string ParameterPhotoSizeLarge = "l";

        /// <summary>
        /// The parameter value to request the normal (medium) photo size.
        /// </summary>
        public const string ParameterPhotoSizeNormal = "m";

        /// <summary>
        /// The parameter value to request the small photo size.
        /// </summary>
        public const string ParameterPhotoSizeSmall = "s";

        /// <summary>
        /// The name of the Delete action.
        /// </summary>
        protected const string ActionNameDelete = "Delete";

        /// <summary>
        /// The name of the ID property for all entities.
        /// </summary>
        protected const string EntityPropertyNameId = "Id";

        /// <summary>
        /// The default page size.
        /// </summary>
        protected const int DefaultPageSize = 5;

        /// <summary>
        /// The value that indicates an infinite page size.
        /// </summary>
        protected const int InfinitePageSize = int.MaxValue;

        /// <summary>
        /// The key in the TempData under which the page flash is stored.
        /// </summary>
        private const string TempDataKeyPageFlash = "PageFlash";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the site data for this request.
        /// </summary>
        internal SiteData SiteData { get; private set; }

        /// <summary>
        /// Gets or sets the master view model.
        /// </summary>
        protected MasterViewModel MasterViewModel { get; set; }

        #endregion

      

        #region [Constructor]

        protected SiteControllerBase()
        {
            Repo = new EmbroRepository();
        }

        #endregion [Constructor]
        #region Execute

        /// <summary>
        /// Executes the specified request context.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        protected override void Execute(RequestContext requestContext)
        {
            try
            {

              
                // Initialize culture.
                SetCultureForHttpRequest(requestContext.HttpContext.Request);

                // Ensure that initial settings are created.
                //CheckFirstTimeInitialization();

                // Get settings and menus.
                IDictionary<string, string> applicationSettings;
                IDictionary<string, string> userSettings;
                IEnumerable<Menu> menus;
                using (var repository = GetRepository())
                {
                    applicationSettings = repository.GetSettingValues(SettingsScope.Application);
                    userSettings = repository.GetSettingValues(SettingsScope.User);
                    menus = repository.GetMenu();
                }
                var viewMenus = GetMenuViewModel(requestContext, menus);

                    // Create and store SiteData.
                    this.SiteData = new SiteData(applicationSettings, viewMenus, requestContext, userSettings);

                    //// Check if a photo provider synchronization should occur.
                    //if (this.SiteData.Settings.PhotoProviderAutoSyncEnabled && this.SiteData.Settings.PhotoProviderAutoSyncIntervalMinutes.HasValue)
                    //{
                    //    if (!this.SiteData.Settings.PhotoProviderLastSyncTime.HasValue || (DateTimeOffset.UtcNow - this.SiteData.Settings.PhotoProviderLastSyncTime.Value).TotalMinutes >= this.SiteData.Settings.PhotoProviderAutoSyncIntervalMinutes.Value)
                    //    {
                    //        // Synchronization has not yet occurred or it was more than the configured number of minutes ago.
                    //        var start = (this.SiteData.Settings.PhotoProviderLastSyncTime.HasValue ? this.SiteData.Settings.PhotoProviderLastSyncTime.Value : DateTimeOffsetExtensions.MinValue);

                    //        // Start synchronization on a background thread.
                    //        ThreadPool.QueueUserWorkItem(state =>
                    //        {
                    //            Tasks.SynchronizePhotoProvider(start, this.SiteData.Settings.PhotoProviderAutoSyncTags, false, RequestOrigin.Timer);
                    //        });
                    //    }
                    //}
                    try
                    {
                        // Make the timezone globally available.
                        DateTimeOffsetExtensions.TimeZone = this.SiteData.Settings.TimeZone;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex);
                    }
                    try
                    {
                        // Create and store master view model.
                       // if (!ControllerContext.IsChildAction)
                        this.MasterViewModel = new MasterViewModel(this.SiteData, this.SiteData.Settings.Keywords,
                                                                   DateTimeOffset.UtcNow.AdjustFromUtc());
                        this.ViewData[ViewDataKeyMasterViewModel] = this.MasterViewModel;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex);
                    }
                    // Set the theme.
                    var theme = requestContext.HttpContext.Request.Params["theme"];
                    if (string.IsNullOrEmpty(theme))
                    {
                        theme = this.SiteData.Settings.Theme;
                    }
                    ThemedWebFormViewEngine.SetThemeForRequest(requestContext, theme);

                    // Set default form values.
                    SetViewDataFromCookie(requestContext, "AuthorName");
                    SetViewDataFromCookie(requestContext, "AuthorEmail");
                    SetViewDataFromCookie(requestContext, "AuthorUrl");
                    SetViewDataFromCookie(requestContext, "RememberMe");

                    // Execute the actual request.
                    base.Execute(requestContext);
               
            }
            catch (Exception exc)
            {
                Logger.LogException(exc);
              //  throw;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// GeD:\workshop\Mayando.Web\Controllers\FeedsController.csts the menu view model.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="menus">The menus.</param>
        /// <returns>The menu view model.</returns>
        private static List<MenuViewModel> GetMenuViewModel(RequestContext requestContext, IEnumerable<Menu> menus)
        {
            var viewMenus = new List<MenuViewModel>();
            MenuViewModel selectedViewMenu = null;
            foreach (var menu in menus)
            {
                // Fix up the relative URL if needed.
                string url = null;

                Uri parsedUrl;
                var parsed = Uri.TryCreate(menu.Url, UriKind.RelativeOrAbsolute, out parsedUrl);
                if (parsed)
                {
                    if (parsedUrl.IsAbsoluteUri)
                    {
                        url = menu.Url;
                    }
                    else
                    {
                        try
                        {
                            if (VirtualPathUtility.IsAppRelative(menu.Url))
                            {
                                url = VirtualPathUtility.ToAbsolute(menu.Url);
                            }
                            else
                            {
                                url = menu.Url;
                            }
                        }
                        // Ignore errors for invalid virtual paths, they will be logged below.
                        catch (HttpException) { }
                        catch (ArgumentException) { }
                    }
                }
                if (string.IsNullOrEmpty(url))
                {
                    // The URL could not be parsed properly, log a warning but use it anyway.
                    Logger.Log(LogLevel.Warning, string.Format(CultureInfo.CurrentCulture, "The menu with the URL '{0}' could not be parsed. Make sure it is either absolute or relative (i.e. starts with '~/' or '/'). If it is relative, it shouldn't contain a '?'.", menu.Url));
                    url = menu.Url;
                }

                var viewMenu = new MenuViewModel(url, menu.Title, menu.OpenInNewWindow, menu.ToolTip);
                // Select the menu with the longest URL that matches the current request URL.
                if (selectedViewMenu == null || viewMenu.Url.Length > selectedViewMenu.Url.Length)
                {
                    if (requestContext.HttpContext.Request.Path.StartsWith(viewMenu.Url, StringComparison.OrdinalIgnoreCase))
                    {
                        selectedViewMenu = viewMenu;
                    }
                }
                viewMenus.Add(viewMenu);
            }
            if (selectedViewMenu != null)
            {
                selectedViewMenu.Selected = true;
            }
            return viewMenus;
        }

        private void CheckFirstTimeInitialization()
        {
            try
            {
                using (var repositoryRead = GetRepository())
                {
                    var logDetails = new StringBuilder();

                    // Check if first-time database initialization must be performed.
                    var currentVersion = SiteData.GlobalAssemblyVersion.ToString();
                    //var shouldWriteVersion = false;
                    var settings = repositoryRead.GetSettings(SettingsScope.Application);
                      if (settings.Count == 0)
                    {
                        using (var repositoryWrite = GetRepository())
                        {
                            // No settings are present yet so the application has never run before. Load some default data.
                            repositoryWrite.EnsureSettings(SettingsScope.Application, ApplicationSettings.GetSettingDefinitions());
                            // shouldWriteVersion = true;

                            var photosMenu = new Menu
                            {
                                Sequence = 0,
                                Title = "Embroideries",
                                Url = "~/embro/latest",
                                ToolTip = "Go to my latest embros",
                                OpenInNewWindow = false
                            };
                            repositoryWrite.CreateMenu(photosMenu);
                            logDetails.AppendFormat(CultureInfo.CurrentCulture, "Created \"{0}\" menu.", photosMenu.Title);
                            logDetails.AppendLine();

                            var archiveMenu = new Menu
                            {
                                Sequence = 1,
                                Title = "Archives",
                                Url = "~/embros/published",
                                ToolTip = "Show a chronological overview of my photos",
                                OpenInNewWindow = false
                            };
                            repositoryWrite.CreateMenu(archiveMenu);
                            logDetails.AppendFormat(CultureInfo.CurrentCulture, "Created \"{0}\" menu.", archiveMenu.Title);
                            logDetails.AppendLine();

                            var contactMenu = new Menu
                            {
                                Sequence = 2,
                                Title = "Contact",
                                Url = "~/contact",
                                ToolTip = "Contact me",
                                OpenInNewWindow = false
                            };
                            repositoryWrite.CreateMenu(contactMenu);

                            var logonMenu = new Menu
                            {
                                Sequence = 2,
                                Title = Resources.AccountLogOn,
                                Url = "~/Account/Logon",
                                ToolTip = "Logging in",
                                OpenInNewWindow = false
                            };
                            repositoryWrite.CreateMenu(logonMenu);
                            logDetails.AppendFormat(CultureInfo.CurrentCulture, "Created \"{0}\" menu.", contactMenu.Title);
                            logDetails.AppendLine();
                            //repositoryWrite.CommitChanges();
                        }
                    }




                    // Create the Administrator user and role if needed.
                    var administrator = Membership.GetUser(SiteData.GlobalAdministratorUserName);
                    if (administrator == null)
                    {
                        administrator = Membership.CreateUser(SiteData.GlobalAdministratorUserName, SiteData.GlobalAdministratorDefaultPassword);
                        logDetails.AppendFormat(CultureInfo.CurrentCulture, "Created \"{0}\" user.", SiteData.GlobalAdministratorUserName);
                        logDetails.AppendLine();
                    }
                    if (!Roles.RoleExists(SiteData.GlobalAdministratorRoleName))
                    {
                        Roles.CreateRole(SiteData.GlobalAdministratorRoleName);
                        logDetails.AppendFormat(CultureInfo.CurrentCulture, "Created \"{0}\" role.", SiteData.GlobalAdministratorRoleName);
                        logDetails.AppendLine();
                    }
                    if (!Roles.IsUserInRole(SiteData.GlobalAdministratorUserName, SiteData.GlobalAdministratorRoleName))
                    {
                        Roles.AddUserToRole(SiteData.GlobalAdministratorUserName, SiteData.GlobalAdministratorRoleName);
                        logDetails.AppendFormat(CultureInfo.CurrentCulture, "Added the \"{0}\" user to the \"{1}\" role.", SiteData.GlobalAdministratorUserName, SiteData.GlobalAdministratorRoleName);
                        logDetails.AppendLine();
                    }

                    if (logDetails.Length > 0)
                    {
                        Logger.Log(LogLevel.Information, "Initialized the application for the first time.", logDetails.ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        #endregion

        #region ViewFor

        /// <summary>
        /// Returns a view for a create action with a new instance of a model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <returns>The Create view for a new instance of the specified model type.</returns>
        protected ActionResult ViewForCreate<TModel>() where TModel : class, new()
        {
            return ViewForCreate<TModel>((Action<TModel>)null);
        }

        protected ActionResult ViewForUpload<TModel>() where TModel : class, new()
        {
            return ViewForUpload<TModel>((Action<TModel>)null);
        }

        /// <summary>
        /// Returns a view for a create action with a new instance of a model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="initializer">The initializer to run for the new model instance.</param>
        /// <returns>The Create view for a new instance of the specified model type.</returns>
        protected ActionResult ViewForCreate<TModel>(Action<TModel> initializer) where TModel : class, new()
        {
            TModel model = new TModel();
            if (initializer != null)
            {
                initializer(model);
            }
            return View(ViewName.Create, model);
        }

        protected ActionResult ViewForUpload<TModel>(Action<TModel> initializer) where TModel : class, new()
        {
            TModel model = new TModel();
            if (initializer != null)
            {
                initializer(model);
            }
            return View(ViewName.Upload, model);
        }

        /// <summary>
        /// Returns a view for a create action with a new instance of a model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="initializer">The initializer to run for the new model instance.</param>
        /// <returns>The Create view for a new instance of the specified model type.</returns>
        protected ActionResult ViewForCreate<TModel>(Action<IEmbroRepository, TModel> initializer) where TModel : class, new()
        {
            using (var repository = GetRepository())
            {
                TModel model = new TModel();
                if (initializer != null)
                {
                    initializer(repository, model);
                }
                return View(ViewName.Create, model);
            }
        }

        protected ActionResult ViewForUpload<TModel>(Action<IEmbroRepository, TModel> initializer) where TModel : class, new()
        {
            using (var repository = GetRepository())
            {
                TModel model = new TModel();
                if (initializer != null)
                {
                    initializer(repository, model);
                }
                return View(ViewName.Upload, model);
            }
        }

        /// <summary>
        /// Returns a view for a delete action of a certain entity.
        /// </summary>
        /// <typeparam name="TModel">The type of the entity to be deleted.</typeparam>
        /// <param name="itemName">The name of the item to be deleted (shown in the confirmation page).</param>
        /// <param name="itemDescriptionSelector">Returns a description for the item to be deleted (shown in the confirmation page).</param>
        /// <param name="itemSelector">Selects the item to be deleted.</param>
        /// <returns>The Delete view for the selected item, or a "Not Found" view if the item was not found.</returns>
        protected ActionResult ViewForDelete<TModel>(string itemName, Func<TModel, string> itemDescriptionSelector, Func<IEmbroRepository, TModel> itemSelector)
        {
            using (var repository = GetRepository())
            {
                TModel item = itemSelector(repository);
                if (item == null)
                {
                    return View(ViewName.NotFound);
                }
                string itemDescription = itemDescriptionSelector(item);
                return View(ViewName.Delete, new DeleteViewModel(itemName, itemDescription));
            }
        }

        /// <summary>
        /// Returns a view for a certain entity.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="viewName">The name of the view to be returned.</param>
        /// <param name="itemSelector">Selects the item to be used as the model for the view.</param>
        /// <returns>The specified view for the selected item, or a "Not Found" view if the item was not found.</returns>
        protected ActionResult ViewFor<TModel>(ViewName viewName, Func<IEmbroRepository, TModel> itemSelector)
        {
            using (var repository = GetRepository())
            {
                TModel item = itemSelector(repository);
                if (item == null)
                {
                    return View(ViewName.NotFound);
                }
                return View(viewName, item);
            }
        }

        #endregion


        #region View & RedirectToAction Typed Overloads

        /// <summary>
        /// Returns a <see cref="ViewResult"/> that renders a view to the response.
        /// </summary>
        /// <param name="viewName">The name of the view to render.</param>
        /// <returns>The <see cref="ViewResult"/> that renders a view to the response.</returns>
        protected ActionResult View(ViewName viewName)
        {
            return View(viewName, null);
        }

        /// <summary>
        /// Returns a <see cref="ViewResult"/> that renders a view to the response.
        /// </summary>
        /// <param name="viewName">The name of the view to render.</param>
        /// <param name="model">The model rendered by the view.</param>
        /// <returns>The <see cref="ViewResult"/> that renders a view to the response.</returns>
        protected ActionResult View(ViewName viewName, object model)
        {
            return View(viewName.ToString(), model);
        }

        /// <summary>
        /// Returns a <see cref="ViewResult"/> that renders a view to the response.
        /// </summary>
        /// <param name="viewName">The name of the view to render.</param>
        /// <param name="masterPageName">The name of the master page to render.</param>
        /// <param name="model">The model rendered by the view.</param>
        /// <returns>The <see cref="ViewResult"/> that renders a view to the response.</returns>
        protected ActionResult View(MasterPageName masterPageName, ViewName viewName, object model)
        {
            return View(masterPageName.ToString(), viewName.ToString(), model);
        }

        /// <summary>
        /// Returns a <see cref="RedirectToRouteResult"/> that redirects to the specified action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The <see cref="RedirectToRouteResult"/> that redirects to the specified action.</returns>
        protected ActionResult RedirectToAction(ActionName actionName)
        {
            return RedirectToAction(actionName, null, null);
        }

        /// <summary>
        /// Returns a <see cref="RedirectToRouteResult"/> that redirects to the specified action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>The <see cref="RedirectToRouteResult"/> that redirects to the specified action.</returns>
        protected ActionResult RedirectToAction(ActionName actionName, string controllerName)
        {
            return RedirectToAction(actionName, controllerName, null);
        }

        /// <summary>
        /// Returns a <see cref="RedirectToRouteResult"/> that redirects to the specified action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="routeValues">An object containing the parameters for a route. The parameters are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <returns>The <see cref="RedirectToRouteResult"/> that redirects to the specified action.</returns>
        protected ActionResult RedirectToAction(ActionName actionName, object routeValues)
        {
            return RedirectToAction(actionName, null, routeValues);
        }

        /// <summary>
        /// Returns a <see cref="RedirectToRouteResult"/> that redirects to the specified action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object containing the parameters for a route. The parameters are retrieved via reflection by examining the properties of the object. Typically created using object initializer syntax.</param>
        /// <returns>The <see cref="RedirectToRouteResult"/> that redirects to the specified action.</returns>
        protected ActionResult RedirectToAction(ActionName actionName, string controllerName, object routeValues)
        {
            return RedirectToAction(actionName.ToActionString(), controllerName, routeValues);
        }

        /// <summary>
        /// Redirects to the homepage defined by the user.
        /// </summary>
        /// <returns>A redirect to the homepage defined by the user, or to the latest photo if the user did not define a homepage.</returns>
        protected ActionResult RedirectToHomepage()
        {
            return Redirect(this.SiteData.HomepageUrl);
        }

        #endregion

        #region Page Flash Handling

        /// <summary>
        /// Sets the page flash, i.e. the informational message shown on the next page the user visits.
        /// </summary>
        /// <param name="message">The page flash to show.</param>
        internal void SetPageFlash(string message)
        {
            this.TempData[TempDataKeyPageFlash] = message;
        }

        ///// <summary>
        ///// Method called before the action method is invoked.
        ///// </summary>
        ///// <param name="filterContext">Contains information about the current request and action.</param>
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    // Make sure to get the pageflash from the PREVIOUS page here before the action is executed
        //    // (so before a new one can be set).
        //    var pageFlash = (string)this.TempData[TempDataKeyPageFlash];
        //    this.MasterViewModel.AddToPageFlash(pageFlash);
        //}

        /// <summary>
        /// Method called after the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Contains information about the current request and action.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Append a logging pageflash to the CURRENT page if needed.
            if (this.User.CanSeeAdministratorContent())
            {
                var notificationLevel = Logger.GetHighestLogLevel(this.SiteData.Settings.EventLogLastViewedTime);
                if (notificationLevel.HasValue && notificationLevel.Value > LogLevel.Information)
                {
                    var loggingPageFlash = string.Format(CultureInfo.CurrentCulture, "There are new {0}s. Go to the <a href=\"{1}\">event log</a> to see the details.", notificationLevel.Value.ToString().ToLowerInvariant(), Url.Action(ActionName.EventLog, AdminController.ControllerName));
                    if (MasterViewModel != null) this.MasterViewModel.AddToPageFlash(loggingPageFlash);
                }
            }

            // Append a demo site pageflash if needed (except for the logged on administrator).
            if (SiteData.DemoMode && !this.User.IsAdministrator())
            {
                if (MasterViewModel != null) this.MasterViewModel.AddToPageFlash(Resources.DemoModePageFlash);
            }
        }

        #endregion

        #region GetRepository

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <returns>The repository.</returns>
        protected IEmbroRepository GetRepository()
        {
            //if (Repo == null) Repo = new EmbroRepository();
            return Repo;
        }

       

        #endregion
      
        #region Paging Helper Methods

        /// <summary>
        /// Gets the actual page to use from an optional page number.
        /// </summary>
        /// <param name="page">The optional page number.</param>
        /// <returns>If a page number is actually given, the page number minus one; otherwise zero.</returns>
        protected static int GetActualPage(int? page)
        {
            return Math.Max(0, (page.HasValue ? page.Value - 1 : 0));
        }

        /// <summary>
        /// Gets the actual page size to use based on an optional count.
        /// </summary>
        /// <param name="count">The optional count.</param>
        /// <returns>If a count is actually given, the count; otherwise the default page size.</returns>
        protected static int GetActualCount(int? count)
        {
            return GetActualCount(count, DefaultPageSize);
        }

        /// <summary>
        /// Gets the actual page size to use based on an optional count.
        /// </summary>
        /// <param name="count">The optional count.</param>
        /// <param name="defaultCount">The default page size if no count is actually given.</param>
        /// <returns>If a count is actually given, the count; otherwise the specified default count.</returns>
        protected static int GetActualCount(int? count, int defaultCount)
        {
            return Math.Max(1, (count.HasValue ? count.Value : defaultCount));
        }

        #endregion

        #region Cookie Helper Methods

        /// <summary>
        /// Remembers the user's preferences.
        /// </summary>
        /// <param name="authorName">The name of the author.</param>
        /// <param name="authorEmail">The email address of the author.</param>
        /// <param name="authorUrl">The URL of the author.</param>
        /// <param name="rememberMe">Determines if the preferences should actually be saved or deleted.</param>
        internal void RememberPreferences(string authorName, string authorEmail, string authorUrl, bool rememberMe)
        {
            if (rememberMe)
            {
                SetCookieValues(authorName, authorEmail, authorUrl, bool.TrueString, DateTime.Now.AddDays(14));
            }
            else
            {
                SetCookieValues(null, null, null, null, DateTime.Now.AddDays(-1));
            }
        }

        /// <summary>
        /// Sets a view data element from a cookie value.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="viewDataKey">The view data key in which to store the cookie value.</param>
        private void SetViewDataFromCookie(RequestContext requestContext, string viewDataKey)
        {
            string cookieValue = GetCookieValue(requestContext, viewDataKey);
            if (!string.IsNullOrEmpty(cookieValue))
            {
                this.ViewData[viewDataKey] = cookieValue;
            }
        }

        /// <summary>
        /// Gets a cookie value from the request context.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <returns>The value of the requested cookie or <see langword="null"/> if the cookie isn't present.</returns>
        internal static string GetCookieValue(RequestContext requestContext, string cookieName)
        {
            var cookie = requestContext.HttpContext.Request.Cookies.Get(cookieName);
            return (cookie == null || string.IsNullOrEmpty(cookie.Value) ? null : cookie.Value);
        }

        /// <summary>
        /// Sets the cookie values.
        /// </summary>
        /// <param name="authorName">The name of the author.</param>
        /// <param name="authorEmail">The email address of the author.</param>
        /// <param name="authorUrl">The URL of the author.</param>
        /// <param name="rememberMe">Determines if the preferences should be remembered.</param>
        /// <param name="expireDate">The cookie's expiration date.</param>
        private void SetCookieValues(string authorName, string authorEmail, string authorUrl, string rememberMe, DateTime expireDate)
        {
            this.Response.Cookies.Set(new HttpCookie("AuthorName", authorName) { Expires = expireDate });
            this.Response.Cookies.Set(new HttpCookie("AuthorEmail", authorEmail) { Expires = expireDate });
            this.Response.Cookies.Set(new HttpCookie("AuthorUrl", authorUrl) { Expires = expireDate });
            this.Response.Cookies.Set(new HttpCookie("RememberMe", rememberMe) { Expires = expireDate });
        }

        #endregion

        #region SetCultureForHttpRequest

        /// <summary>
        /// Sets the culture on the thread for the given HTTP request (based on the user's configured languages in the browser).
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        public static void SetCultureForHttpRequest(HttpRequestBase request)
        {


            string cultureName = null;
            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = request.UserLanguages[0]; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

          


            //var browserLanguages = request.UserLanguages;
            //if (browserLanguages == null)
            //{
            //    return;
            //}
            //foreach (var language in browserLanguages)
            //{
            //    try
            //    {
            //        var languageName = language;
            //        if (languageName.Contains(";"))
            //        {
            //            languageName = languageName.Split(';')[0];
            //        }
            //        var culture = CultureInfo.GetCultureInfo(languageName);
            //        if (!culture.IsNeutralCulture)
            //        {
            //            Thread.CurrentThread.CurrentCulture = culture;
            //            Thread.CurrentThread.CurrentUICulture = culture;
            //            break;
            //        }
            //    }
            //    catch (ArgumentException)
            //    {
            //        // The culture doesn't exist, try the next one.
            //    }
            //}
        }

        #endregion
    }
}