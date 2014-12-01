using System;
using System.Globalization;

namespace Myembro.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Exception"/> instances.
    /// </summary>
    public static class ExceptionExtensions
    {
        #region Constants

        private const string DataKeyIgnoreForLogging = "IgnoreForLogging";

        #endregion

        #region Logging Helper Methods

        /// <summary>
        /// Marks an exception to be ignored for logging.
        /// </summary>
        /// <param name="exception">The exception to ignore when logging.</param>
        public static void IgnoreForLogging(this Exception exception)
        {
            if (exception != null && exception.Data != null)
            {
                exception.Data[DataKeyIgnoreForLogging] = true;
            }
        }

        /// <summary>
        /// Determines if an exception should be ignored when logging.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns><see langword="true"/> when the exception should not be logged, <see langword="false"/> otherwise.</returns>
        public static bool ShouldIgnoreForLogging(this Exception exception)
        {
            return (exception != null && exception.Data != null && exception.Data.Contains(DataKeyIgnoreForLogging));
        }

        #endregion

        #region Status Description

        public static string GetStatusDescriptionHtml(this Exception exc)
        {
            return string.Format(CultureInfo.InvariantCulture, "<pre><code>{0}</code></pre>", exc.ToString());
        }

        #endregion
    }
}