using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Myembro.Extensions;
using Myembro.Models;

namespace Myembro.Infrastructure
{
    /// <summary>
    /// Allows logging of events.
    /// </summary>
    internal static class Logger
    {
        #region Log

        /// <summary>
        /// Logs an event.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">The log message.</param>
        public static void Log(LogLevel level, string message)
        {
            Log(level, message, null);
        }

        /// <summary>
        /// Logs an exception.
        /// </summary>
        /// <param name="exc">The exception.</param>
        public static void LogException(Exception exc)
        {
            // Do not log "Controller.HandleUnknownAction", which is thrown when a URL matches a controller but not an action.
            // This prevents the event log from filling up due to misformed URLs.
            if (exc.TargetSite.DeclaringType == typeof(Controller) && string.Equals(exc.TargetSite.Name, "HandleUnknownAction", StringComparison.Ordinal))
            {
                exc.IgnoreForLogging();
            }
            // Do not log "ReflectedActionDescriptor.ExtractParameterFromDictionary", which is thrown when a URL does not provide a required routing parameter.
            // This prevents the event log from filling up due to misformed URLs.
            if (exc.TargetSite.DeclaringType == typeof(ReflectedActionDescriptor) && string.Equals(exc.TargetSite.Name, "ExtractParameterFromDictionary", StringComparison.Ordinal))
            {
                exc.IgnoreForLogging();
            }

            // Make sure to only log the exception if not ignored and just once by checking a custom data entry.
            if (!exc.ShouldIgnoreForLogging())
            {
                var detail = new StringBuilder();
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    detail.AppendFormat(CultureInfo.CurrentCulture, "Requested URL: {0}", context.Request.RawUrl ?? string.Empty).AppendLine();
                    if (context.Request.UrlReferrer != null)
                    {
                        detail.AppendFormat(CultureInfo.CurrentCulture, "Referrer: {0}", context.Request.UrlReferrer).AppendLine();
                    }
                    detail.AppendFormat(CultureInfo.CurrentCulture, "User Host: {0} ({1})", context.Request.UserHostAddress ?? "?", context.Request.UserHostName ?? "?").AppendLine();
                    if (context.Request.UserLanguages != null)
                    {
                        detail.AppendFormat(CultureInfo.CurrentCulture, "User Languages: {0}", string.Join(", ", context.Request.UserLanguages)).AppendLine();
                    }
                    detail.AppendLine();
                }
                detail.AppendFormat(CultureInfo.CurrentCulture, "Exception Stack Trace: {0}", exc.ToString());
                Log(LogLevel.Error, exc.Message, detail.ToString());
                exc.IgnoreForLogging();
            }
        }

        /// <summary>
        /// Logs an event.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">The log message.</param>
        /// <param name="detail">The log detail.</param>
        public static void Log(LogLevel level, string message, string detail)
        {
            var entry = new Log
            {
                Time = DateTimeOffset.UtcNow,
                LogLevel = level,
                Message = message,
                Detail = detail
            };
            Log(entry);
        }

        /// <summary>
        /// Logs an event.
        /// </summary>
        /// <param name="entry">The log entry.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static void Log(Log entry)
        {
            try
            {
                using (var repository = GetRepository())
                {
                    repository.CreateLog(entry);

                    // Send an error notification mail if necessary.
                    if (entry.LogLevel == LogLevel.Error)
                    {
                        var settingValues = repository.GetSettingValues(SettingsScope.Application);
                        var settings = new ApplicationSettings(settingValues);
                        if (settings.NotifyOnErrors)
                        {
                            var subject = string.Format(CultureInfo.CurrentCulture, "An error occurred on your \"{0}\" {1} website.", settings.Title, SiteData.GlobalApplicationName);
                            var body = string.Format(CultureInfo.CurrentCulture, "An error occurred on your \"{1}\" {2} website. See the event log on your website for more information.{0}{0}{3}", Environment.NewLine, settings.Title, SiteData.GlobalApplicationName, entry.Message);
                            if (!string.IsNullOrEmpty(entry.Detail))
                            {
                                body += string.Format(CultureInfo.CurrentCulture, "{0}{0}{1}", Environment.NewLine, entry.Detail);
                            }
                            Mailer.SendNotificationMail(settings, subject, body, false, true, true);
                        }
                    }

                   // repository.CommitChanges();
                }
            }
            catch (Exception)
            {
                // Ignore any exception during logging.
            }
        }

        #endregion

        #region GetLogs

        /// <summary>
        /// Gets the logs that satisfy the specified criteria.
        /// </summary>
        /// <param name="count">The number of items per page.</param>
        /// <param name="page">The page number.</param>
        /// <param name="minLevel">The optional minimum log level</param>
        /// <param name="searchText">The text to find in the log message or detail.</param>
        /// <returns>All log entries that satisfy the specified criteria.</returns>
        public static IEnumerable<Log> GetLogs(int count, int page, LogLevel? minLevel, string searchText)
        {
            using (var repository = GetRepository())
            {
                return repository.GetLogs(count, page, minLevel, searchText);
            }
        }

        private static IEmbroRepository GetRepository()
        {
            return new EmbroRepository();
        }

        #endregion

        #region Clear

        /// <summary>
        /// Clears the event log entries older than a certain age and at or below a certain log level.
        /// </summary>
        /// <param name="minAge">The minimum days the entry should be old to be deleted.</param>
        /// <param name="maxLevel">The maximum log level the entry should have to be deleted.</param>
        /// <returns>The number of logs that were deleted.</returns>
        public static int Clear(int minAge, LogLevel maxLevel)
        {
            using (var repository = GetRepository())
            {
                var count = repository.DeleteLogs(minAge, maxLevel);
               // repository.CommitChanges();
                return count;
            }
        }

        #endregion

        #region GetHighestLogLevel

        public static LogLevel? GetHighestLogLevel(DateTimeOffset? minTime)
        {
            using (var repository = GetRepository())
            {
                return repository.GetHighestLogLevel(minTime);
            }
        }

        #endregion
    }
}