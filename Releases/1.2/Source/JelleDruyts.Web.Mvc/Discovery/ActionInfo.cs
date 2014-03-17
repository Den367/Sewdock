using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace JelleDruyts.Web.Mvc.Discovery
{
    /// <summary>
    /// Contains information about a controller action in an MVC website.
    /// </summary>
    public class ActionInfo
    {
        #region Properties

        /// <summary>
        /// Gets the controller for this action.
        /// </summary>
        public ControllerInfo Controller { get; private set; }

        /// <summary>
        /// Gets the name of this action.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a description of this action.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the HTTP verbs that are explicitly allowed for this action.
        /// </summary>
        public ICollection<string> HttpVerbs { get; private set; }

        /// <summary>
        /// Gets the parameters for this action.
        /// </summary>
        public IEnumerable<ParameterInfo> Parameters { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the action allows an HTTP-GET.
        /// </summary>
        public bool AllowsHttpGet { get; private set; }

        /// <summary>
        /// Gets the users that are explicitly allowed access.
        /// </summary>
        public string AllowedUsers { get; private set; }

        /// <summary>
        /// Gets the roles that are explicitly allowed access.
        /// </summary>
        public string AllowedRoles { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this action is allowed for everyone, i.e. if no users or roles were explicitly allowed access.
        /// </summary>
        public bool AllowedForEveryone { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionInfo"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        internal ActionInfo(ReflectedActionDescriptor action, ControllerInfo controller)
        {
            this.Controller = controller;
            this.Name = action.MethodInfo.FindAttributeValue<ActionNameAttribute, string>(a => a.Name, action.ActionName);
            this.Description = action.MethodInfo.FindAttributeValue<DescriptionAttribute, string>(a => a.Description);
            this.HttpVerbs = action.MethodInfo.FindAttributeValue<AcceptVerbsAttribute, ICollection<string>>(a => a.Verbs, new string[0]);
            this.Parameters = action.GetParameters().Select(p => new ParameterInfo((ReflectedParameterDescriptor)p, this));
            this.AllowsHttpGet = (this.HttpVerbs.Count == 0 || this.HttpVerbs.Any(v => string.Equals(v, System.Web.Mvc.HttpVerbs.Get.ToString(), StringComparison.OrdinalIgnoreCase)));
            var controllerAuthorization = controller.Type.FindAttribute<AuthorizeAttribute>();
            var actionAuthorization = action.MethodInfo.FindAttribute<AuthorizeAttribute>();
            if (controllerAuthorization != null)
            {
                this.AllowedUsers = controllerAuthorization.Users;
                this.AllowedRoles = controllerAuthorization.Roles;
            }
            if (actionAuthorization != null)
            {
                if (!string.IsNullOrEmpty(this.AllowedUsers) && !string.IsNullOrEmpty(actionAuthorization.Users))
                {
                    this.AllowedUsers += ",";
                }
                if (!string.IsNullOrEmpty(actionAuthorization.Users))
                {
                    this.AllowedUsers += actionAuthorization.Users;
                }
                if (!string.IsNullOrEmpty(this.AllowedRoles) && !string.IsNullOrEmpty(actionAuthorization.Roles))
                {
                    this.AllowedRoles += ",";
                }
                if (!string.IsNullOrEmpty(actionAuthorization.Roles))
                {
                    this.AllowedRoles += actionAuthorization.Roles;
                }
            }
            this.AllowedForEveryone = (controllerAuthorization == null && actionAuthorization == null);
        }

        #endregion
    }
}