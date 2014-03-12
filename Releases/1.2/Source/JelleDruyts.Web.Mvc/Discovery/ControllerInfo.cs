using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace JelleDruyts.Web.Mvc.Discovery
{
    /// <summary>
    /// Contains information about a controller in an MVC website.
    /// </summary>
    public class ControllerInfo
    {
        #region Properties

        /// <summary>
        /// Gets the site this controller belongs to.
        /// </summary>
        public WebsiteInfo Site { get; private set; }

        /// <summary>
        /// Gets the name of this controller.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a description of this controller.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the type of this controller.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public Type Type { get; private set; }

        /// <summary>
        /// Gets all the actions available on this controller.
        /// </summary>
        public IEnumerable<ActionInfo> Actions { get; private set; }

        /// <summary>
        /// Gets the actions that are available through HTTP-GET on this controller.
        /// </summary>
        public IEnumerable<ActionInfo> HttpGetActions { get; private set; }

        /// <summary>
        /// Gets the actions that are not available through HTTP-GET on this controller.
        /// </summary>
        public IEnumerable<ActionInfo> NonHttpGetActions { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerInfo"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="site">The site.</param>
        internal ControllerInfo(ReflectedControllerDescriptor controller, WebsiteInfo site)
        {
            this.Site = site;
            this.Name = controller.ControllerName;
            this.Description = controller.ControllerType.FindAttributeValue<DescriptionAttribute, string>(d => d.Description);
            this.Type = controller.ControllerType;
            this.Actions = controller.GetCanonicalActions().Select(a => new ActionInfo((ReflectedActionDescriptor)a, this)).OrderBy(a => a.Name);
            this.HttpGetActions = this.Actions.Where(a => a.AllowsHttpGet);
            this.NonHttpGetActions = this.Actions.Where(a => !a.AllowsHttpGet);
        }

        #endregion
    }
}