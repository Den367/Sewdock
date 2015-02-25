using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Myembro.Properties;
using System.IO;

namespace Myembro.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="String"/> instances.
    /// </summary>
    public static class StringExtensions
    {
        #region GenerateSlug

        private const int MaxSlugLength = 45;

        // From http://predicatet.blogspot.com/2009/04/improved-c-slug-generator-or-how-to.html
        /// <summary>
        /// Generates a slug for the specified string.
        /// </summary>
        /// <param name="value">The string for which to generate the slug.</param>
        /// <returns>A URL-friendly version of the given string.</returns>
        public static string GenerateSlug(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("The given value cannot be null or empty.");
            }
            string str = value.RemoveAccent().ToLower(CultureInfo.CurrentCulture);
            str = Regex.Replace(str, @"[^a-z0-9\s-]", string.Empty); // Remove invalid chars
            str = Regex.Replace(str, @"\s+", " ").Trim(); // Convert multiple spaces into one space
            str = str.Substring(0, str.Length <= MaxSlugLength ? str.Length : MaxSlugLength).Trim(); // Cut and trim
            str = Regex.Replace(str, @"\s", "-"); // convert spaces to hyphens
            return str;
        }

        /// <summary>
        /// Removes accents from a string.
        /// </summary>
        /// <param name="value">The string from which to remove accents.</param>
        /// <returns>The string without any accents.</returns>
        private static string RemoveAccent(this string value)
        {
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            return Encoding.ASCII.GetString(bytes);
        }

        #endregion

        #region TrimWithEllipsis

        private const int DefaultMaxTrimLength = 50;
        private const string Ellipsis = "...";

        /// <summary>
        /// Trims a string and shows an ellipsis at the end if it was trimmed.
        /// </summary>
        /// <param name="value">The value to trim.</param>
        /// <returns>The trimmed string with an ellipsis at the end if it was trimmed.</returns>
        public static string TrimWithEllipsis(this string value)
        {
            return TrimWithEllipsis(value, DefaultMaxTrimLength);
        }

        /// <summary>
        /// Trims a string and shows an ellipsis at the end if it was trimmed.
        /// </summary>
        /// <param name="value">The value to trim.</param>
        /// <param name="maxLength">The maximum length for the string.</param>
        /// <returns>The trimmed string with an ellipsis at the end if it was trimmed.</returns>
        public static string TrimWithEllipsis(this string value, int maxLength)
        {
            return TrimWithEllipsis(value, maxLength, null);
        }

        /// <summary>
        /// Trims a string and shows an ellipsis at the end if it was trimmed.
        /// </summary>
        /// <param name="value">The value to trim.</param>
        /// <param name="maxLength">The maximum length for the string.</param>
        /// <param name="moreLinkUrl">A URL for which to generate a link at the end of the returned string if the value was trimmed.</param>
        /// <returns>The trimmed string with an ellipsis at the end if it was trimmed.</returns>
        public static string TrimWithEllipsis(this string value, int maxLength, string moreLinkUrl)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            {
                return value;
            }
            var trimmed = value.Substring(0, maxLength - Ellipsis.Length).Trim() + Ellipsis;
            if (!string.IsNullOrEmpty(moreLinkUrl))
            {
                var link = JelleDruyts.Web.Mvc.HtmlHelperExtensions.Link(moreLinkUrl, Myembro.Properties.Resources.GalleryPhotoLinkMore);
                trimmed += " " + link;
            }
            return trimmed;
        }

        #endregion
        public static Stream ToStream(this string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static byte[] GetBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        
    }
}