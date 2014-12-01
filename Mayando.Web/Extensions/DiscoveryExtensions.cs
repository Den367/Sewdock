using System;
using System.Globalization;
using JelleDruyts.Web.Mvc.Discovery;
using System.Collections;
using JelleDruyts.Web.Mvc;
using System.Collections.Generic;
using System.Web;

namespace Myembro.Extensions
{
    /// <summary>
    /// Provides extension methods for discovery types.
    /// </summary>
    public static class DiscoveryExtensions
    {
        #region ParameterInfo Extensions

        /// <summary>
        /// Gets a collection of <see cref="LinkListItem"/> instances for the collection of parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A list of <see cref="LinkListItem"/> instances for the given parameters.</returns>
        public static IEnumerable<LinkListItem> ToLinkList(this IEnumerable<ParameterInfo> parameters)
        {
            var items = new List<LinkListItem>();
            foreach (var parameter in parameters)
            {
                items.Add(new LinkListItem(null, parameter.Name, false, GetToolTip(parameter)));
            }
            return items;
        }

        /// <summary>
        /// Gets the tool tip that displays additional information about a parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Information about the parameter's description and type.</returns>
        private static string GetToolTip(ParameterInfo parameter)
        {
            string type;
            if (parameter.Type.IsGenericType && parameter.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var nullableType = parameter.Type.GetGenericArguments()[0];
                type = string.Format(CultureInfo.CurrentCulture, "Nullable (i.e. optional) {0}", nullableType.FullName);
            }
            else
            {
                type = parameter.Type.FullName;
            }
            return string.Format(CultureInfo.CurrentCulture, "{1}{0}Type: {2}", Environment.NewLine, parameter.Description.Trim(), type);
        }

        #endregion
    }
}