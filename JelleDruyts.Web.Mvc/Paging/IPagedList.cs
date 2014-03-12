using System.Collections.Generic;

namespace JelleDruyts.Web.Mvc.Paging
{
    /// <summary>
    /// Represents a paged list.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <remarks>
    /// Adapted from an early version of the paged list available at http://pagedlist.codeplex.com/.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public interface IPagedList<T> : IList<T>
	{
        /// <summary>
        /// Gets the page count.
        /// </summary>
		int PageCount { get; }

        /// <summary>
        /// Gets the total item count.
        /// </summary>
		int TotalItemCount { get; }

        /// <summary>
        /// Gets the current page index.
        /// </summary>
		int PageIndex { get; }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
		int PageNumber { get; }

        /// <summary>
        /// Gets the size of the pages.
        /// </summary>
		int PageSize { get; }

        /// <summary>
        /// Gets a value indicating whether there is a previous page.
        /// </summary>
		bool HasPreviousPage { get; }

        /// <summary>
        /// Gets a value indicating whether there is a next page.
        /// </summary>
		bool HasNextPage { get; }

        /// <summary>
        /// Gets a value indicating whether this is the first page.
        /// </summary>
		bool IsFirstPage { get; }

        /// <summary>
        /// Gets a value indicating whether this is the last page.
        /// </summary>
		bool IsLastPage { get; }


	}
}