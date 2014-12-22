using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using IdentitySample;
using JelleDruyts.Web.Mvc;
using Myembro.Controllers;
using Myembro.Extensions;
using Myembro.Infrastructure;
using Myembro.ViewModels;

namespace Myembro.Models
{
    /// <summary>
    /// Defines site-wide information.
    /// </summary>
    public class SiteData
    {
        #region Constants

        /// <summary>
        /// The application's name.
        /// </summary>
        public const string GlobalApplicationName = "Mayando";

        /// <summary>
        /// The application's URL.
        /// </summary>
        public const string GlobalApplicationUrl = "http://mayando.codeplex.com/";

        /// <summary>
        /// The assembly version.
        /// </summary>
        public static readonly Version GlobalAssemblyVersion;

        /// <summary>
        /// The assembly milestone.
        /// </summary>
        public static readonly string GlobalAssemblyMilestone;

        /// <summary>
        /// The name of the Administrator user.
        /// </summary>
        public const string GlobalAdministratorUserName = "Administrator";

        /// <summary>
        /// The default password of the Administrator user.
        /// </summary>
        public const string GlobalAdministratorDefaultPassword = "Mayando!";

        /// <summary>
        /// The name of the Administrator role.
        /// </summary>
        public const string GlobalAdministratorRoleName = "Administrator";

        /// <summary>
        /// Determines if the site is in "demo mode".
        /// </summary>
        public readonly static bool GlobalDemoMode;

        /// <summary>
        /// The default delay for a slideshow.
        /// </summary>
        public const int GlobalDefaultSlideshowDelay = 3;

        /// <summary>
        /// Initializes the <see cref="SiteData"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Boolean.TryParse(System.String,System.Boolean@)")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static SiteData()
        {
            GlobalAssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            GlobalAssemblyMilestone = Assembly.GetExecutingAssembly().FindAttributeValue<AssemblyMilestoneAttribute, string>(a => a.Milestone);
            bool.TryParse(ConfigurationManager.AppSettings["DemoMode"], out GlobalDemoMode);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteData"/> class.
        /// </summary>
        /// <param name="applicationSettings">The application settings.</param>
        /// <param name="menus">The menus.</param>
        /// <param name="requestContext">The request context.</param>
        public SiteData(IDictionary<string, string> applicationSettings, IEnumerable<MenuViewModel> menus, RequestContext requestContext)
            : this(applicationSettings, menus, requestContext, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteData"/> class.
        /// </summary>
        /// <param name="applicationSettings">The application settings.</param>
        /// <param name="menus">The menus.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="userSettings">The user-defined settings.</param>
        public SiteData(IDictionary<string, string> applicationSettings, IEnumerable<MenuViewModel> menus, RequestContext requestContext, IDictionary<string, string> userSettings)
        {
            try
            {
                this.ApplicationUrl = GlobalApplicationUrl;
                this.ApplicationName = GlobalApplicationName;
                this.ApplicationVersion = GlobalAssemblyVersion;
                this.ApplicationDisplayVersion = GlobalAssemblyVersion.ToString(2);
                if (!string.IsNullOrEmpty(GlobalAssemblyMilestone))
                {
                    this.ApplicationDisplayVersion += " " + GlobalAssemblyMilestone;
                }

                this.Settings = new ApplicationSettings(applicationSettings);
                this.Menus = menus;
                this.UserSettings = userSettings;
                if (this.UserSettings == null)
                {
                    this.UserSettings = new Dictionary<string, string>();
                }

                //this.AvailableThemes = ThemedWebFormViewEngine.GetAvailableThemes(requestContext.HttpContext);

                var url = requestContext.HttpContext.Request.Url;
                var uriBuilder = new UriBuilder(url.Scheme, url.Host, url.Port);
                uriBuilder.Path = requestContext.HttpContext.Request.ApplicationPath;
                this.AbsoluteApplicationPath = uriBuilder.Uri;

                this.Generator = string.Format(CultureInfo.InvariantCulture, "{0} v{1} - {2}", this.ApplicationName, this.ApplicationVersion, this.ApplicationUrl);
                this.AdministratorUserName = GlobalAdministratorUserName;
                this.AdministratorDefaultPassword = GlobalAdministratorDefaultPassword;

                if (!string.IsNullOrEmpty(this.Settings.Homepage))
                {
                    var failed = false;
                    try
                    {
                        this.HomepageUrl = VirtualPathUtility.ToAbsolute(this.Settings.Homepage);
                    }
                    // Ignore errors for invalid virtual paths, they will be logged below.
                    catch (HttpException) { failed = true; }
                    catch (ArgumentException) { failed = true; }
                    if (failed)
                    {
                        Logger.Log(LogLevel.Warning, string.Format(CultureInfo.CurrentCulture, "The home page with the URL '{0}' could not be parsed. Make sure it is either absolute or relative (i.e. starts with '~/' or '/'). If it is relative, it shouldn't contain a '?'.", this.Settings.Homepage));
                    }
                }
                if (string.IsNullOrEmpty(this.HomepageUrl))
                {
                    var home = GenerateRelativeUrl(requestContext, EmbroController.ControllerName, ActionName.Latest);
                    this.HomepageUrl = VirtualPathUtility.ToAbsolute(home);
                }

                this.DemoMode = GlobalDemoMode;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        public ApplicationSettings Settings { get; private set; }

        /// <summary>
        /// Gets the available menus.
        /// </summary>
        public IEnumerable<MenuViewModel> Menus { get; private set; }

        /// <summary>
        /// Gets the absolute URL of the current ASP.NET application.
        /// </summary>
        public Uri AbsoluteApplicationPath { get; private set; }

        /// <summary>
        /// Gets the application's name.
        /// </summary>
        public string ApplicationName { get; private set; }

        /// <summary>
        /// Gets the application's version.
        /// </summary>
        public Version ApplicationVersion { get; private set; }

        /// <summary>
        /// Gets the application's display version.
        /// </summary>
        public string ApplicationDisplayVersion { get; private set; }

        /// <summary>
        /// Gets the application's URL.
        /// </summary>
        public string ApplicationUrl { get; private set; }

        /// <summary>
        /// Gets the names of the available visual themes.
        /// </summary>
        public IEnumerable<string> AvailableThemes { get; private set; }

        /// <summary>
        /// Gets the name of the application generating dynamic content (i.e. this application).
        /// </summary>
        public string Generator { get; private set; }

        /// <summary>
        /// Gets the default password for the administrator user.
        /// </summary>
        public string AdministratorDefaultPassword { get; private set; }

        /// <summary>
        /// Gets the user name for the administrator user.
        /// </summary>
        public string AdministratorUserName { get; private set; }

        /// <summary>
        /// Gets the directly usable URL to the home page.
        /// </summary>
        public string HomepageUrl { get; private set; }

        /// <summary>
        /// Determines if the site is in "demo mode".
        /// </summary>
        public bool DemoMode { get; private set; }

        /// <summary>
        /// Gets the user-defined settings.
        /// </summary>
        public IDictionary<string, string> UserSettings { get; private set; }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Converts a relative URI to an absolute one in the current ASP.NET application.
        /// </summary>
        /// <param name="relativeUri">The relative URI.</param>
        /// <returns>An absolute URI to the specified relative URI.</returns>
        public Uri ToAbsolute(string relativeUri)
        {
            return new Uri(this.AbsoluteApplicationPath, relativeUri);
        }

        /// <summary>
        /// Generates a relative URL for a specified controller action.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <returns>A relative virtual path for the requested controller action.</returns>
        public static string GenerateRelativeUrl(RequestContext requestContext, string controller, ActionName action)
        {
            return GenerateRelativeUrl(requestContext, controller, action, new RouteValueDictionary());
        }

        /// <summary>
        /// Generates a relative URL for a specified controller action.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>A relative virtual path for the requested controller action.</returns>
        public static string GenerateRelativeUrl(RequestContext requestContext, string controller, ActionName action, object routeValues)
        {
            return GenerateRelativeUrl(requestContext, controller, action, new RouteValueDictionary(routeValues));
        }

        /// <summary>
        /// Generates a relative URL for a specified controller action.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="values">The route values.</param>
        /// <returns>A relative virtual path for the requested controller action.</returns>
        public static string GenerateRelativeUrl(RequestContext requestContext, string controller, ActionName action, RouteValueDictionary values)
        {
            // Get the virtual path from the route table.
            if (values == null)
            {
                values = new RouteValueDictionary();
            }
            values["controller"] = controller;
            values["action"] = action.ToActionString();
            var url = RouteTable.Routes["Default"].GetVirtualPath(requestContext, values).VirtualPath;

            // Prefix it with the relative path identifier so the client URL will be resolved properly.
            url = "~/" + url;

            return url;
        }

        /// <summary>
        /// Gets a user-defined setting.
        /// </summary>
        /// <param name="name">The name of the user-defined setting.</param>
        /// <returns>The value of the user-defined setting, or <see langword="null"/> if it isn't defined.</returns>
        public string GetUserSetting(string name)
        {
            return GetUserSetting(name, null);
        }

        /// <summary>
        /// Gets a user-defined setting.
        /// </summary>
        /// <param name="name">The name of the user-defined setting.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value of the user-defined setting, or the specified <paramref name="defaultValue"/> if it isn't defined.</returns>
        public string GetUserSetting(string name, string defaultValue)
        {
            if (this.UserSettings.ContainsKey(name))
            {
                return this.UserSettings[name];
            }
            return defaultValue;
        }

        #endregion
    }
}