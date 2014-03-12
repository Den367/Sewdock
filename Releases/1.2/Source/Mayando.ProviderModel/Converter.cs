using System.Globalization;

namespace Mayando.ProviderModel
{
    /// <summary>
    /// Contains methods for converting values.
    /// </summary>
    public static class Converter
    {
        #region ToByteString

        /// <summary>
        /// Gets the number of bytes as a string in kilobytes or megabytes.
        /// </summary>
        /// <param name="bytes">The number of bytes.</param>
        /// <returns>The size in kilobytes or megabytes of the given number of bytes.</returns>
        public static string ToByteString(long bytes)
        {
            double kiloBytes = (bytes / ((double)1024));
            if (kiloBytes > 1024)
            {
                return string.Format(CultureInfo.CurrentCulture, "{0} MB", (kiloBytes / ((double)1024)).ToString("f2", CultureInfo.CurrentCulture));
            }
            else
            {
                return string.Format(CultureInfo.CurrentCulture, "{0} KB", kiloBytes.ToString("f2", CultureInfo.CurrentCulture));
            }
        }

        #endregion
    }
}