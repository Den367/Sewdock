using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace JelleDruyts.Web.Mvc.Paging
{
    /// <summary>
    /// Provides extension methods for paging.
    /// </summary>
    /// <remarks>
    /// Adapted from an early version of the paged list available at http://pagedlist.codeplex.com/.
    /// </remarks>
    public static class PagingExtensions
	{
		#region HtmlHelper extensions

        /// <summary>
        /// Returns the markup for a pager control.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <returns>The markup for a pager control.</returns>
		public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount)
		{
			return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, null);
		}

        /// <summary>
        /// Returns the markup for a pager control.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <param name="actionName">The name of the action to link to.</param>
        /// <returns>The markup for a pager control.</returns>
        public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName)
		{
			return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, null);
		}

        /// <summary>
        /// Returns the markup for a pager control.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <param name="values">The route values.</param>
        /// <returns>The markup for a pager control.</returns>
        public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, object values)
		{
			return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, new RouteValueDictionary(values));
		}

        /// <summary>
        /// Returns the markup for a pager control.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <param name="actionName">The name of the action to link to.</param>
        /// <param name="values">The route values.</param>
        /// <returns>The markup for a pager control.</returns>
        public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, object values)
		{
			return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, new RouteValueDictionary(values));
		}

        /// <summary>
        /// Returns the markup for a pager control.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <param name="valuesDictionary">The route values dictionary.</param>
        /// <returns>The markup for a pager control.</returns>
        public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary)
		{
			return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, valuesDictionary);
		}

        /// <summary>
        /// Returns the markup for a pager control.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <param name="actionName">The name of the action to link to.</param>
        /// <param name="valuesDictionary">The route values dictionary.</param>
        /// <returns>The markup for a pager control.</returns>
        public static string Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, RouteValueDictionary valuesDictionary)
		{
			if (valuesDictionary == null)
			{
				valuesDictionary = new RouteValueDictionary();
			}
			if (actionName != null)
			{
				if (valuesDictionary.ContainsKey("action"))
				{
					throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
				}
				valuesDictionary.Add("action", actionName);
			}
			var pager = new Pager(htmlHelper.ViewContext, pageSize, currentPage, totalItemCount, valuesDictionary);
			return pager.RenderHtml();
		}

		#endregion

		#region IQueryable<T> extensions

        /// <summary>
        /// Creates a new <see cref="IPagedList&lt;T&gt;"/> for the specified query.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The source query.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>A paged list for the specified query.</returns>
		public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
		{
			return new PagedList<T>(source, pageIndex, pageSize);
		}

        /// <summary>
        /// Creates a new <see cref="IPagedList&lt;T&gt;"/> for the specified query.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The source query.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>A paged list for the specified query.</returns>
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, int totalCount)
		{
			return new PagedList<T>(source, pageIndex, pageSize, totalCount);
		}

		#endregion

		#region IEnumerable<T> extensions

        /// <summary>
        /// Creates a new <see cref="IPagedList&lt;T&gt;"/> for the specified list.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The source items.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>A paged list for the specified list.</returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
		{
			return new PagedList<T>(source, pageIndex, pageSize);
		}

        /// <summary>
        /// Creates a new <see cref="IPagedList&lt;T&gt;"/> for the specified list.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The source items.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>A paged list for the specified list.</returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
		{
			return new PagedList<T>(source, pageIndex, pageSize, totalCount);
		}

		#endregion
	}
}