using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Routing;

namespace JelleDruyts.Web.Mvc
{
    /// <summary>
    /// A view engine that allows visual themes to be used to substitute views dynamically.
    /// </summary>
    /// <remarks>
    /// Adapted from: http://pietschsoft.com/post/2009/03/ASPNET-MVC-Implement-Theme-Folders-using-a-Custom-ViewEngine.aspx.
    /// </remarks>
    public class ThemedWebFormViewEngine : WebFormViewEngine
    {
        #region Constants

        private const string HttpContextKeyNameThemeName = "ThemeName";
        private const string HttpCacheKeyNameThemeName = "ThemeName";

        /// <summary>
        /// The path beneath the site root where themes are stored.
        /// </summary>
        public const string ThemePath = "Themes";

        /// <summary>
        /// The path with a ~/ prefix beneath the site root where themes are stored.
        /// </summary>
        public const string RootedThemePath = "~/" + ThemePath;

        #endregion

        #region Fields

        // [jelled] Assigned a non-null value to the array to avoid an exception on Union<string>.
        private static readonly string[] _emptyLocations = new string[0];

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemedWebFormViewEngine"/> class.
        /// </summary>
        public ThemedWebFormViewEngine()
        {
            // [jelled] Changed to still include default MVC locations so that you can keep
            // working the "old" way and add themes when you want later on.
            base.MasterLocationFormats = new string[] {
                RootedThemePath + "/{2}/Views/{1}/{0}.master", 
                RootedThemePath + "/{2}/Views/Shared/{0}.master",
                "~/Views/{1}/{0}.master", 
                "~/Views/Shared/{0}.master"
            };
            base.ViewLocationFormats = new string[] { 
                RootedThemePath + "/{2}/Views/{1}/{0}.aspx", 
                RootedThemePath + "/{2}/Views/{1}/{0}.ascx", 
                RootedThemePath + "/{2}/Views/Shared/{0}.aspx", 
                RootedThemePath + "/{2}/Views/Shared/{0}.ascx",
                "~/Views/{1}/{0}.aspx", 
                "~/Views/{1}/{0}.ascx", 
                "~/Views/Shared/{0}.aspx", 
                "~/Views/Shared/{0}.ascx", 
                 RootedThemePath + "/{2}/Views/{1}/{0}.schtml",                
                RootedThemePath + "/{2}/Views/Shared/{0}.schtml", 
             
                "~/Views/{1}/{0}.schtml", 
             
                "~/Views/Shared/{0}.schtml"


            };
            base.PartialViewLocationFormats = new string[] {
                RootedThemePath + "/{2}/Views/{1}/{0}.aspx",
                RootedThemePath + "/{2}/Views/{1}/{0}.ascx",
                RootedThemePath + "/{2}/Views/Shared/{0}.aspx",
                RootedThemePath + "/{2}/Views/Shared/{0}.ascx",
                "~/Views/{1}/{0}.aspx",
                "~/Views/{1}/{0}.ascx",
                "~/Views/Shared/{0}.aspx",
                "~/Views/Shared/{0}.ascx"
                ,
                  RootedThemePath + "/{2}/Views/{1}/{0}.schtml",
               
                RootedThemePath + "/{2}/Views/Shared/{0}.schtml",
       
                "~/Views/{1}/{0}.schtml",

                "~/Views/Shared/{0}.schtml"
       
            };
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Determines if a file at the given location exists for the specified controller context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns><c>true</c> if the file exists; otherwise, <c>false</c>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            try
            {
                return System.IO.File.Exists(controllerContext.HttpContext.Server.MapPath(virtualPath));
            }
            catch (HttpException exception)
            {
                if (exception.GetHttpCode() != 0x194)
                {
                    throw;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Finds the view.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="masterName">Name of the master.</param>
        /// <param name="useCache">if set to <c>true</c> [use cache].</param>
        /// <returns>The page view.</returns>
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            string[] strArray;
            string[] strArray2;

            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("viewName must be specified.", "viewName");
            }

            string themeName = GetThemeToUse(controllerContext);

            string requiredString = controllerContext.RouteData.GetRequiredString("controller");

            string viewPath = this.GetPath(controllerContext, this.ViewLocationFormats, viewName, themeName, requiredString, "View", useCache, out strArray);

            // [jelled] Added support for automatic detection of Site.Master, so that the master
            // file is *always* searched for in the theme or default directories (normally it's
            // only searched for when explicitly requested through one of the View overloads of a
            // controller). This also makes the MasterPageFile directive on a Page obsolete.
            if (string.IsNullOrEmpty(masterName))
            {
                masterName = "Site";
            }

            string masterPath = this.GetPath(controllerContext, this.MasterLocationFormats, masterName, themeName, requiredString, "Master", useCache, out strArray2);

            if (!string.IsNullOrEmpty(viewPath) && (!string.IsNullOrEmpty(masterPath) || string.IsNullOrEmpty(masterName)))
            {
                return new ViewEngineResult(this.CreateView(controllerContext, viewPath, masterPath), this);
            }
            return new ViewEngineResult(strArray.Union<string>(strArray2));
        }

        /// <summary>
        /// Finds the partial view.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="partialViewName">Partial name of the view.</param>
        /// <param name="useCache">if set to <c>true</c> [use cache].</param>
        /// <returns>The partial view.</returns>
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            string[] strArray;
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentException("partialViewName must be specified.", "partialViewName");
            }

            string themeName = GetThemeToUse(controllerContext);

            string requiredString = controllerContext.RouteData.GetRequiredString("controller");
            string partialViewPath = this.GetPath(controllerContext, this.PartialViewLocationFormats, partialViewName, themeName, requiredString, "Partial", useCache, out strArray);
            if (string.IsNullOrEmpty(partialViewPath))
            {
                return new ViewEngineResult(strArray);
            }
            return new ViewEngineResult(this.CreatePartialView(controllerContext, partialViewPath), this);

        }

        #endregion

        #region Helper Methods

        private static string GetThemeToUse(ControllerContext controllerContext)
        {
            var themeName = GetThemeForRequest(controllerContext.RequestContext);
            // [jelled] Removed need for "Default" theme since we now fall back to the default MVC locations.
            //if (themeName == null) themeName = "Default";
            return themeName;
        }

        private string GetPath(ControllerContext controllerContext, string[] locations,
            string name, string themeName, string controllerName, string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
        {
            searchedLocations = _emptyLocations;
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            if ((locations == null) || (locations.Length == 0))
            {
                throw new InvalidOperationException("locations must not be null or emtpy.");
            }

            bool flag = IsSpecificPath(name);
            string key = this.CreateCacheKey(cacheKeyPrefix, name, flag ? string.Empty : controllerName, themeName);
            if (useCache)
            {
                string viewLocation = this.ViewLocationCache.GetViewLocation(controllerContext.HttpContext, key);
                if (viewLocation != null)
                {
                    return viewLocation;
                }
            }
            if (!flag)
            {
                return this.GetPathFromGeneralName(controllerContext, locations, name, controllerName, themeName, key, ref searchedLocations);
            }
            return this.GetPathFromSpecificName(controllerContext, name, key, ref searchedLocations);
        }

        private static bool IsSpecificPath(string name)
        {
            char ch = name[0];
            if (ch != '~')
            {
                return (ch == '/');
            }
            return true;
        }

        private string CreateCacheKey(string prefix, string name, string controllerName, string themeName)
        {
            return string.Format(CultureInfo.InvariantCulture,
                ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}",
                new object[] { base.GetType().AssemblyQualifiedName, prefix, name, controllerName, themeName });
        }

        private string GetPathFromGeneralName(ControllerContext controllerContext, string[] locations, string name,
            string controllerName, string themeName, string cacheKey, ref string[] searchedLocations)
        {
            string virtualPath = string.Empty;
            searchedLocations = new string[locations.Length];
            for (int i = 0; i < locations.Length; i++)
            {
                string str2 = string.Format(CultureInfo.InvariantCulture, locations[i], new object[] { name, controllerName, themeName });

                if (this.FileExists(controllerContext, str2))
                {
                    searchedLocations = _emptyLocations;
                    virtualPath = str2;
                    this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
                    return virtualPath;
                }
                searchedLocations[i] = str2;
            }
            return virtualPath;
        }

        private string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
        {
            string virtualPath = name;
            if (!this.FileExists(controllerContext, name))
            {
                virtualPath = string.Empty;
                searchedLocations = new string[] { name };
            }
            this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
            return virtualPath;
        }

        #endregion

        #region Get, Set & Discover Themes

        // [jelled] Added method to easily set theme without requiring knowledge of the implementation details.
        /// <summary>
        /// Sets the theme for the current request.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="themeName">The name of the theme.</param>
        public static void SetThemeForRequest(RequestContext requestContext, string themeName)
        {
            requestContext.HttpContext.Items[HttpContextKeyNameThemeName] = themeName;
        }

        // [jelled] Added method to easily get theme without requiring knowledge of the implementation details.
        /// <summary>
        /// Gets the theme being used for the current request.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <returns>The theme currently being used.</returns>
        public static string GetThemeForRequest(RequestContext requestContext)
        {
            return (string)requestContext.HttpContext.Items[HttpContextKeyNameThemeName];
        }

        // [jelled] Added method to easily get available themes.
        /// <summary>
        /// Gets the available themes.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>A list of available theme names.</returns>
        public static IEnumerable<string> GetAvailableThemes(HttpContextBase httpContext)
        {
            var themes = (IEnumerable<string>)httpContext.Cache[HttpCacheKeyNameThemeName];
            if (themes == null)
            {
                var themeRootDir = httpContext.Server.MapPath(RootedThemePath);
                var themeDirs = Directory.GetDirectories(themeRootDir);
                for (var i = 0; i < themeDirs.Length; i++)
                {
                    themeDirs[i] = new DirectoryInfo(themeDirs[i]).Name;
                }
                themes = themeDirs;
                httpContext.Cache.Add(HttpCacheKeyNameThemeName, themes, new CacheDependency(themeRootDir), Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return themes;
        }

        #endregion
    }
}