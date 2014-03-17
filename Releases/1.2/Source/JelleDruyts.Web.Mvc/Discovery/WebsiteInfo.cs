using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace JelleDruyts.Web.Mvc.Discovery
{
    /// <summary>
    /// Contains information about an MVC website.
    /// </summary>
    public class WebsiteInfo
    {
        #region Properties

        /// <summary>
        /// Gets all the available controllers in the website.
        /// </summary>
        public IEnumerable<ControllerInfo> Controllers { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WebsiteInfo"/> class.
        /// </summary>
        /// <param name="websiteAssemblies">The website assemblies.</param>
        internal WebsiteInfo(IEnumerable<Assembly> websiteAssemblies)
        {
            var uniqueAssemblies = websiteAssemblies.Distinct(new AssemblyComparer());
            var controllerDescriptors = from c in uniqueAssemblies.SelectMany(a => a.GetTypes())
                                        where (!c.IsAbstract) && typeof(Controller).IsAssignableFrom(c)
                                        orderby c.Name
                                        select new ControllerInfo(new ReflectedControllerDescriptor(c), this);
            this.Controllers = controllerDescriptors.ToList();
        }

        /// <summary>
        /// Compares assemblies to see if they are the same, regardless of their location.
        /// </summary>
        private class AssemblyComparer : EqualityComparer<Assembly>
        {
            public override bool Equals(Assembly x, Assembly y)
            {
                return string.Equals(x.FullName, y.FullName);
            }

            public override int GetHashCode(Assembly obj)
            {
                return obj.FullName.GetHashCode();
            }
        }

        #endregion
    }
}