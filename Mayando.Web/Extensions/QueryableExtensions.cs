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
        #region EmbroideryItem - OrderBy

        /// <summary>
        /// Orders the photos by a certain date type in ascending order.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="orderBy">The date type to order by.</param>
        /// <returns>The ordered photos.</returns>
        public static IQueryable<EmbroideryItem> OrderByAscending(this IQueryable<EmbroideryItem> query, EmbroDateType orderBy)
        {
            if (orderBy == EmbroDateType.Taken)
            {
                return query.OrderBy(p => (p.Created ));
            }
            else
            {
                return query.OrderBy(p => p.Published ?? p.Created);
            }
        }

        /// <summary>
        /// Orders the photos by a certain date type in descending order.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="orderBy">The date type to order by.</param>
        /// <returns>The ordered photos.</returns>
        public static IQueryable<EmbroideryItem> OrderByDescending(this IQueryable<EmbroideryItem> query, EmbroDateType orderBy)
        {
            if (orderBy == EmbroDateType.Taken)
            {
                return query.OrderByDescending(p => (p.Published ?? p.Created));
            }
            else
            {
                return query.OrderByDescending(p => p.Created);
            }
        }

        #endregion

        #region EmbroideryItem - WhereDate

        /// <summary>
        /// Selects the photos on or before a certain date type.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="type">The type of date to filter on.</param>
        /// <param name="maxDate">The maximum date (inclusive).</param>
        /// <returns>The filtered photos.</returns>
        public static IQueryable<EmbroideryItem> WhereDateOnOrBefore(this IQueryable<EmbroideryItem> query, EmbroDateType type, DateTimeOffset maxDate)
        {
            if (type == EmbroDateType.Taken)
            {
                return query.Where(p => (p.Published ?? p.Created) <= maxDate.UtcDateTime);
            }
            else
            {
                return query.Where(p => p.Created <= maxDate.UtcDateTime);
            }
        }

        /// <summary>
        /// Selects the photos after a certain date type.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="type">The type of date to filter on.</param>
        /// <param name="minDate">The minimum date (inclusive).</param>
        /// <returns>The filtered photos.</returns>
        public static IQueryable<EmbroideryItem> WhereDateOnOrAfter(this IQueryable<EmbroideryItem> query, EmbroDateType type, DateTimeOffset minDate)
        {
            if (type == EmbroDateType.Taken)
            {
                return query.Where(p => (p.Published ?? p.Created) >= minDate.UtcDateTime);
            }
            else
            {
                return query.Where(p => p.Created >= minDate.UtcDateTime);
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
        public static IQueryable<EmbroideryItem> WhereDateBetween(this IQueryable<EmbroideryItem> query, EmbroDateType type, DateTimeOffset minDate, DateTimeOffset maxDate)
        {
            if (type == EmbroDateType.Taken)
            {
                return query.Where(p => (p.Published ?? p.Created) >= minDate.UtcDateTime && (p.Published ?? p.Created) < maxDate.UtcDateTime);
            }
            else
            {
                return query.Where(p => p.Created >= minDate.UtcDateTime && p.Created < maxDate.UtcDateTime);
            }
        }

        #endregion
    }
}