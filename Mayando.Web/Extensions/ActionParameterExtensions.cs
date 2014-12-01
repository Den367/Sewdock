using Myembro.Infrastructure;

namespace Myembro.Extensions
{
    /// <summary>
    /// Provides extension methods for action parameters in a route URL.
    /// </summary>
    public static class ActionParameterExtensions
    {
        #region ActionName

        /// <summary>
        /// Converts an <see cref="ActionName"/> into a string to be used in a route URL.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <returns>The lower case string representation of the given <see cref="ActionName"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public static string ToActionString(this ActionName actionName)
        {
            return actionName.ToString().ToLowerInvariant();
        }

        #endregion

        #region NavigationContextType

        /// <summary>
        /// Converts a <see cref="NavigationContextType"/> into a string to be used in a route URL.
        /// </summary>
        /// <param name="navigationContextType">The type of navigation context.</param>
        /// <returns>The lower case string representation of the given <see cref="NavigationContextType"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public static string ToActionString(this NavigationContextType? navigationContextType)
        {
            if (navigationContextType.HasValue)
            {
                return navigationContextType.Value.ToActionString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="NavigationContextType"/> into a string to be used in a route URL.
        /// </summary>
        /// <param name="navigationContextType">The type of navigation context.</param>
        /// <returns>The lower case string representation of the given <see cref="NavigationContextType"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public static string ToActionString(this NavigationContextType navigationContextType)
        {
            return navigationContextType.ToString().ToLowerInvariant();
        }

        #endregion

        #region Direction

        /// <summary>
        /// Converts a <see cref="Direction"/> into a string to be used in a route URL.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns>The lower case string representation of the given <see cref="Direction"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public static string ToActionString(this Direction direction)
        {
            return direction.ToString().ToLowerInvariant();
        }

        #endregion
    }
}