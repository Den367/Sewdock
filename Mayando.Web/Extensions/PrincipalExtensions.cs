using System.Security.Principal;
using Myembro.Models;

namespace Myembro.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IPrincipal"/> instances.
    /// </summary>
    public static class PrincipalExtensions
    {
        #region IsAdministrator

        /// <summary>
        /// Determines if a certain principal can see content only to be seen by an administrator.
        /// </summary>
        /// <param name="principal">The principal to check.</param>
        /// <returns><see langword="true"/> if the principal is an administrator or if the site is in demo mode, <see langword="false"/> otherwise.</returns>
        public static bool CanSeeAdministratorContent(this IPrincipal principal)
        {
            if (SiteData.GlobalDemoMode)
            {
                return true;
            }
            else
            {
                return principal.IsAdministrator();
            }
        }

        /// <summary>
        /// Determines if a certain principal is an administrator.
        /// </summary>
        /// <param name="principal">The principal to check.</param>
        /// <returns><see langword="true"/> if the principal is an administrator, <see langword="false"/> otherwise.</returns>
        public static bool IsAdministrator(this IPrincipal principal)
        {
            var result = (principal == null ? false : principal.IsInRole(SiteData.GlobalAdministratorRoleName));
            return result;
        }

        #endregion
    }
}