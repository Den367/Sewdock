using System;
using System.Collections.Generic;
using System.Linq;

namespace JelleDruyts.Web.Mvc.Paging
{
    /// <summary>
    /// A list that allows paging.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <remarks>
    /// Adapted from an early version of the paged list available at http://pagedlist.codeplex.com/.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PagedList(IEnumerable<T> source, int index, int pageSize) : this(source, index, pageSize, null)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
		public PagedList(IEnumerable<T> source, int index, int pageSize, int? totalCount)
		{
			Initialize(source.AsQueryable(), index, pageSize, totalCount);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
		public PagedList(IQueryable<T> source, int index, int pageSize) : this(source, index, pageSize, null)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
		public PagedList(IQueryable<T> source, int index, int pageSize, int? totalCount)
		{
			Initialize(source, index, pageSize, totalCount);
        }

        #endregion

        #region IPagedList Members

        /// <summary>
        /// Gets the page count.
        /// </summary>
        public int PageCount { get; private set; }
        /// <summary>
        /// Gets the total item count.
        /// </summary>
		public int TotalItemCount { get; private set; }
        /// <summary>
        /// Gets the current page index.
        /// </summary>
		public int PageIndex { get; private set; }
        /// <summary>
        /// Gets the current page number.
        /// </summary>
		public int PageNumber { get { return PageIndex + 1; } }
        /// <summary>
        /// Gets the size of the pages.
        /// </summary>
		public int PageSize { get; private set; }
        /// <summary>
        /// Gets a value indicating whether there is a previous page.
        /// </summary>
		public bool HasPreviousPage { get; private set; }
        /// <summary>
        /// Gets a value indicating whether there is a next page.
        /// </summary>
		public bool HasNextPage { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this is the first page.
        /// </summary>
		public bool IsFirstPage { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this is the last page.
        /// </summary>
		public bool IsLastPage { get; private set; }

		#endregion

        #region Initialize

        /// <summary>
        /// Initializes the paged list.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
		protected void Initialize(IQueryable<T> source, int index, int pageSize, int? totalCount)
		{
			//### argument checking
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "PageIndex cannot be below 0.");
			}
			if (pageSize < 0)
			{
				throw new ArgumentOutOfRangeException("pageSize", "PageSize cannot be less than 0.");
			}

			//### set source to blank list if source is null to prevent exceptions
			if (source == null)
			{
				source = new List<T>().AsQueryable();
			}

			//### set properties
            if (!totalCount.HasValue)
            {
                TotalItemCount = source.Count();
            }
            else TotalItemCount = (int)totalCount;
			PageSize = pageSize;
			PageIndex = index;
			if (TotalItemCount > 0)
			{
				PageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
			}
			else
			{
				PageCount = 0;
			}
			HasPreviousPage = (PageIndex > 0);
			HasNextPage = (PageIndex < (PageCount - 1));
			IsFirstPage = (PageIndex <= 0);
			IsLastPage = (PageIndex >= (PageCount - 1));

			//### add all items to internal list
			if (TotalItemCount > 0)
			{
                //AddRange(source.Skip((index) * pageSize).Take(pageSize).ToList());
                AddRange(source.ToList());
			}
        }

        #endregion
    }
}