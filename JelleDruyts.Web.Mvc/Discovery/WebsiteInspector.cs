using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;

namespace JelleDruyts.Web.Mvc.Discovery
{
    /// <summary>
    /// Discovers information on an MVC website.
    /// </summary>
    public static class WebsiteInspector
    {
        #region GetWebsiteInfo

        /// <summary>
        /// Gets the website info for the given website assembly.
        /// </summary>
        /// <param name="websiteAssembly">The website assembly.</param>
        /// <returns>All the discovered information in the given assembly.</returns>
        public static WebsiteInfo GetWebsiteInfo(Assembly websiteAssembly)
        {
            return GetWebsiteInfo(new Assembly[] { websiteAssembly });
        }

        /// <summary>
        /// Gets the website info for the currently running website.
        /// </summary>
        /// <returns>All the discovered information in the currently running website.</returns>
        public static WebsiteInfo GetWebsiteInfo()
        {
            return GetWebsiteInfo(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Gets the website info for the given website assemblies.
        /// </summary>
        /// <param name="websiteAssemblies">The website assemblies.</param>
        /// <returns>All the discovered information in the given assemblies.</returns>
        public static WebsiteInfo GetWebsiteInfo(IEnumerable<Assembly> websiteAssemblies)
        {
            return new WebsiteInfo(websiteAssemblies);
        }

        #endregion
    }
}