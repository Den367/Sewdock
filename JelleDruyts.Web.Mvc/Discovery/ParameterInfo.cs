using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace JelleDruyts.Web.Mvc.Discovery
{
    /// <summary>
    /// Contains information about a parameter for an action in an MVC website.
    /// </summary>
    public class ParameterInfo
    {
        #region Properties

        /// <summary>
        /// Gets the action this parameter applies to.
        /// </summary>
        public ActionInfo Action { get; private set; }

        /// <summary>
        /// Gets the name of this parameter.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a description of this parameter.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the type of this parameter.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public Type Type { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterInfo"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="action">The action.</param>
        internal ParameterInfo(ReflectedParameterDescriptor parameter, ActionInfo action)
        {
            this.Action = action;
            this.Name = parameter.ParameterName;
            this.Type = parameter.ParameterType;
            this.Description = parameter.ParameterInfo.FindAttributeValue<DescriptionAttribute, string>(d => d.Description);
        }

        #endregion
    }
}