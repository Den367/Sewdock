using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace JelleDruyts.Web.Mvc
{
    /// <summary>
    /// Provides extension methods for <see cref="UrlHelper"/> instances.
    /// </summary>
    public static class UrlHelperExtensions
    {
        #region ThemedContent

        /// <summary>
        /// Gets a themed content URL, i.e. a URL to the themed content item if it exists in the current theme or otherwise the non-themed version of it.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="relativeUrl">The relative URL of the content for which to return a URL.</param>
        /// <returns>A URL (themed or not) to the given relative URL.</returns>
        public static string ThemedContent(this UrlHelper urlHelper, string relativeUrl)
        {
            var locationsToSearch = new List<string>();
            var theme = ThemedWebFormViewEngine.GetThemeForRequest(urlHelper.RequestContext);
            if (!string.IsNullOrEmpty(theme))
            {
                var themedRelativeUrl = relativeUrl.Replace("~", string.Format(CultureInfo.InvariantCulture, "{0}/{1}", ThemedWebFormViewEngine.RootedThemePath, theme));
                locationsToSearch.Add(themedRelativeUrl);
            }
            locationsToSearch.Add(relativeUrl);
            foreach (var location in locationsToSearch)
            {
                var physicalLocation = urlHelper.RequestContext.HttpContext.Server.MapPath(location);
                if (File.Exists(physicalLocation))
                {
                    return urlHelper.Content(location);
                }
            }
            return relativeUrl;
        }

        #endregion

        #region Gravatar

        private const string GravatarBaseUrl = "http://www.gravatar.com/avatar/";

        /// <summary>
        /// Gets a URL to a Gravatar image.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="email">The email address coupled to the Gravatar.</param>
        /// <param name="size">The requested size of the image.</param>
        /// <returns>A URL to the Gravatar image for the requested email address.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "urlHelper"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public static string Gravatar(this UrlHelper urlHelper, string email, int size)
        {
            return urlHelper.Gravatar(email, size, null);
        }

        /// <summary>
        /// Gets a URL to a Gravatar image.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="email">The email address coupled to the Gravatar.</param>
        /// <param name="size">The requested size of the image.</param>
        /// <param name="defaultImageUrl"></param>
        /// <returns>A URL to the Gravatar image for the requested email address.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "urlHelper"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public static string Gravatar(this UrlHelper urlHelper, string email, int size, string defaultImageUrl)
        {
            if (string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(defaultImageUrl))
            {
                return defaultImageUrl;
            }
            else
            {
                var gravatarUrl = new StringBuilder(GravatarBaseUrl);
                if (!string.IsNullOrEmpty(email))
                {
                    var md5Hasher = new MD5CryptoServiceProvider();
                    var hashedBytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(email.ToLowerInvariant()));
                    for (var i = 0; i < hashedBytes.Length; i++)
                    {
                        gravatarUrl.Append(hashedBytes[i].ToString("x2", CultureInfo.InvariantCulture));
                    }
                    gravatarUrl.Append(".jpg");
                }
                gravatarUrl.Append("?d=identicon");
                gravatarUrl.Append("&amp;s=").Append(size);
                return gravatarUrl.ToString();
            }
        }

        #endregion
    }
}