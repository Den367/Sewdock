using System;
using System.Globalization;
using Mayando.Web.Properties;

namespace Mayando.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="DateTimeOffset"/> instances.
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        #region Properties

        /// <summary>
        /// Gets or sets the time zone for which all dates and times should be displayed.
        /// </summary>
        public static TimeZoneInfo TimeZone { get; set; }

        #endregion

        #region ToString - Adjusted

        /// <summary>
        /// Adjusts the given time to the configured <see cref="TimeZone"/> and formats it for display.
        /// </summary>
        /// <param name="value">The time to display.</param>
        /// <returns>The adjusted and formatted time.</returns>
        public static string ToAdjustedDisplayString(this DateTimeOffset value)
        {
            return value.AdjustFromUtc().ToString("g", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Adjusts the given time to the configured <see cref="TimeZone"/> and formats it for display in a verbose way.
        /// </summary>
        /// <param name="value">The time to display.</param>
        /// <returns>The adjusted and formatted time.</returns>
        public static string ToAdjustedVerboseDisplayString(this DateTimeOffset value)
        {
            var adjusted = value.AdjustFromUtc();
            var today = DateTimeOffset.UtcNow.AdjustFromUtc();
            if (adjusted.Date == today.Date)
            {
                // Today.
                return string.Format(CultureInfo.CurrentCulture, Resources.TimeTodayAtTime, adjusted.ToShortTimeString());
            }
            else if (adjusted.Date == today.Date.AddDays(-1))
            {
                // Yesterday.
                return string.Format(CultureInfo.CurrentCulture, Resources.TimeYesterdayAtTime, adjusted.ToShortTimeString());
            }
            else if (adjusted > today.AddMonths(-10))
            {
                // In the last 10 months, show only the day and month.
                return string.Format(CultureInfo.CurrentCulture, Resources.TimeOnDateAtTime, adjusted.ToMonthDateString(), adjusted.ToShortTimeString());
            }
            else
            {
                // Older, use the full date.
                return string.Format(CultureInfo.CurrentCulture, Resources.TimeOnDateAtTime, adjusted.ToShortDateString(), adjusted.ToShortTimeString());
            }
        }

        /// <summary>
        /// Adjusts the given time to the configured <see cref="TimeZone"/> and formats it for display.
        /// </summary>
        /// <param name="value">The time to display.</param>
        /// <returns>The adjusted and formatted time.</returns>
        public static string ToAdjustedDisplayString(this DateTimeOffset? value)
        {
            return (value.HasValue ? value.Value.ToAdjustedDisplayString() : null);
        }

        /// <summary>
        /// Adjusts the given time to the configured <see cref="TimeZone"/> and formats it for display in a verbose way.
        /// </summary>
        /// <param name="value">The time to display.</param>
        /// <returns>The adjusted and formatted time.</returns>
        public static string ToAdjustedVerboseDisplayString(this DateTimeOffset? value)
        {
            return (value.HasValue ? value.Value.ToAdjustedVerboseDisplayString() : null);
        }

        #endregion

        #region ToString- Unadjusted

        /// <summary>
        /// Formats the given time for editing (without adjusting the actual time).
        /// </summary>
        /// <param name="value">The time to edit.</param>
        /// <returns>The formatted time.</returns>
        public static string ToEditString(this DateTimeOffset value)
        {
            return value.ToOffset(TimeZone.GetUtcOffset(value)).ToString();
        }

        /// <summary>
        /// Formats the given time for editing (without adjusting the actual time).
        /// </summary>
        /// <param name="value">The time to edit.</param>
        /// <returns>The formatted time.</returns>
        public static string ToEditString(this DateTimeOffset? value)
        {
            return (value.HasValue ? value.Value.ToEditString() : null);
        }

        /// <summary>
        /// Formats the given time for displaying as a long date (without adjusting the actual time).
        /// </summary>
        /// <param name="value">The date to display.</param>
        /// <returns>The formatted date.</returns>
        public static string ToLongDateString(this DateTimeOffset value)
        {
            return value.ToString("D", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Formats the given time for displaying as a short date (without adjusting the actual time).
        /// </summary>
        /// <param name="value">The date to display.</param>
        /// <returns>The formatted date.</returns>
        public static string ToShortDateString(this DateTimeOffset value)
        {
            return value.ToString("d", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Formats the given time for displaying as a short time (without adjusting the actual time).
        /// </summary>
        /// <param name="value">The time to display.</param>
        /// <returns>The formatted time.</returns>
        public static string ToShortTimeString(this DateTimeOffset value)
        {
            return value.ToString("t", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Formats the given time for displaying as month-only date (without adjusting the actual time).
        /// </summary>
        /// <param name="value">The date to display.</param>
        /// <returns>The formatted date.</returns>
        public static string ToMonthDateString(this DateTimeOffset value)
        {
            return value.ToString("M", CultureInfo.CurrentCulture);
        }

        #endregion

        #region Adjust

        /// <summary>
        /// Adjusts the given time from UTC to the configured <see cref="TimeZone"/>.
        /// </summary>
        /// <param name="value">The time to adjust.</param>
        /// <returns>The time as observed in the configured <see cref="TimeZone"/>.</returns>
        public static DateTimeOffset AdjustFromUtc(this DateTimeOffset value)
        {
            return Adjust(value, v => TimeZoneInfo.ConvertTime(v, TimeZone));
        }

        /// <summary>
        /// Adjusts the given time in the configured <see cref="TimeZone"/> to UTC.
        /// </summary>
        /// <param name="value">The time to adjust.</param>
        /// <returns>The time in UTC.</returns>
        public static DateTimeOffset AdjustToUtc(this DateTimeOffset value)
        {
            return Adjust(value, v => TimeZoneInfo.ConvertTime(v.DateTime, TimeZone, TimeZoneInfo.Utc));
        }

        /// <summary>
        /// Adjusts the specified time.
        /// </summary>
        /// <param name="value">The time to adjust.</param>
        /// <param name="adjustment">The adjustment to make.</param>
        /// <returns>The adjusted time, unless it is a "marker" value (i.e. <see cref="DateTimeOffset.MinValue"/> or <see cref="DateTimeOffset.MaxValue"/>.</returns>
        private static DateTimeOffset Adjust(DateTimeOffset value, Func<DateTimeOffset, DateTimeOffset> adjustment)
        {
            if (value == DateTimeOffset.MaxValue || value == DateTimeOffset.MinValue)
            {
                // Don't adjust min/max marker values.
                return value;
            }
            else
            {
                return adjustment(value);
            }
        }

        #endregion

        #region MinValue

        /// <summary>
        /// Gets the minimum useable value for a date time (which is still a valid database date).
        /// </summary>
        public static readonly DateTimeOffset MinValue = new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero);

        #endregion
    }
}