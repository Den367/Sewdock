using System;
using System.Linq;
using Mayando.Web.Models;

namespace Mayando.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IQueryable"/> instances.
    /// </summary>
    public static class QueryableExtensions
    {
        #region Photo - OrderBy

        /// <summary>
        /// Orders the photos by a certain date type in ascending order.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="orderBy">The date type to order by.</param>
        /// <returns>The ordered photos.</returns>
        public static IQueryable<Photo> OrderByAscending(this IQueryable<Photo> query, PhotoDateType orderBy)
        {
            if (orderBy == PhotoDateType.Taken)
            {
                return query.OrderBy(p => (p.DateTakenUtc ?? p.DatePublishedUtc));
            }
            else
            {
                return query.OrderBy(p => p.DatePublishedUtc);
            }
        }

        /// <summary>
        /// Orders the photos by a certain date type in descending order.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="orderBy">The date type to order by.</param>
        /// <returns>The ordered photos.</returns>
        public static IQueryable<Photo> OrderByDescending(this IQueryable<Photo> query, PhotoDateType orderBy)
        {
            if (orderBy == PhotoDateType.Taken)
            {
                return query.OrderByDescending(p => (p.DateTakenUtc ?? p.DatePublishedUtc));
            }
            else
            {
                return query.OrderByDescending(p => p.DatePublishedUtc);
            }
        }

        #endregion

        #region Photo - WhereDate

        /// <summary>
        /// Selects the photos on or before a certain date type.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="type">The type of date to filter on.</param>
        /// <param name="maxDate">The maximum date (inclusive).</param>
        /// <returns>The filtered photos.</returns>
        public static IQueryable<Photo> WhereDateOnOrBefore(this IQueryable<Photo> query, PhotoDateType type, DateTimeOffset maxDate)
        {
            if (type == PhotoDateType.Taken)
            {
                return query.Where(p => (p.DateTakenUtc ?? p.DatePublishedUtc) <= maxDate.UtcDateTime);
            }
            else
            {
                return query.Where(p => p.DatePublishedUtc <= maxDate.UtcDateTime);
            }
        }

        /// <summary>
        /// Selects the photos after a certain date type.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="type">The type of date to filter on.</param>
        /// <param name="minDate">The minimum date (inclusive).</param>
        /// <returns>The filtered photos.</returns>
        public static IQueryable<Photo> WhereDateOnOrAfter(this IQueryable<Photo> query, PhotoDateType type, DateTimeOffset minDate)
        {
            if (type == PhotoDateType.Taken)
            {
                return query.Where(p => (p.DateTakenUtc ?? p.DatePublishedUtc) >= minDate.UtcDateTime);
            }
            else
            {
                return query.Where(p => p.DatePublishedUtc >= minDate.UtcDateTime);
            }
        }

        /// <summary>
        /// Selects the photos between two certain date types.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="type">The type of date to filter on.</param>
        /// <param name="minDate">The minimum date (inclusive).</param>
        /// <param name="maxDate">The maximum date (exclusive).</param>
        /// <returns>The filtered photos.</returns>
        public static IQueryable<Photo> WhereDateBetween(this IQueryable<Photo> query, PhotoDateType type, DateTimeOffset minDate, DateTimeOffset maxDate)
        {
            if (type == PhotoDateType.Taken)
            {
                return query.Where(p => (p.DateTakenUtc ?? p.DatePublishedUtc) >= minDate.UtcDateTime && (p.DateTakenUtc ?? p.DatePublishedUtc) < maxDate.UtcDateTime);
            }
            else
            {
                return query.Where(p => p.DatePublishedUtc >= minDate.UtcDateTime && p.DatePublishedUtc < maxDate.UtcDateTime);
            }
        }

        #endregion
    }
}