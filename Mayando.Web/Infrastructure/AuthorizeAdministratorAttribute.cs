using System;
using System.Web.Mvc;
using Myembro.Models;

namespace Myembro.Infrastructure
{
    /// <summary>
    /// Restricts access of callers to the administrator role.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class AuthorizeAdministratorAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeAdministratorAttribute"/> class.
        /// </summary>
        public AuthorizeAdministratorAttribute()
        {
            // Allow only the administrator role to authorize.
            this.Roles = SiteData.GlobalAdministratorRoleName;
        }

        /// <summary>
        /// Authorizes the core.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns><c>true</c> if authorized; otherwise, <c>false</c>.</returns>
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            // Allow anyone when in demo mode.
            if (SiteData.GlobalDemoMode)
            {
                return true;
            }
            else
            {
                return base.AuthorizeCore(httpContext);
            }
        }
    }
}