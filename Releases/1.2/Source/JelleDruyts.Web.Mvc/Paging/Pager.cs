using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JelleDruyts.Web.Mvc.Paging
{
    /// <summary>
    /// Represents a pager control on a web page.
    /// </summary>
    /// <remarks>
    /// Adapted from an early version of the paged list available at http://pagedlist.codeplex.com/.
    /// </remarks>
    public class Pager
    {
        #region Fields

        private ViewContext viewContext;
        private readonly int pageSize;
        private readonly int currentPage;
        private readonly int totalItemCount;
        private readonly RouteValueDictionary linkWithoutPageValuesDictionary;

        #endregion

        #region Properties

        // [jelled] Added global properties to control markup.

        /// <summary>
        /// The number of page links to display on the pager. The default is 10.
        /// </summary>
        public static int NumPageLinksToDisplay { get; set; }

        /// <summary>
        /// The page sizes to display that the user can change. The default is 10, 25 and 50.
        /// </summary>
        public static IEnumerable<int> PageSizesToDisplay { get; set; }

        /// <summary>
        /// The parameter for the page number in the URL. The default is "page".
        /// </summary>
        public static string PageNumberParameterName { get; set; }

        /// <summary>
        /// The parameter for the page size in the URL. The default is "count".
        /// </summary>
        public static string PageSizeParameterName { get; set; }

        /// <summary>
        /// The name of the CSS style for the div element that contains the pager. The default is "pager".
        /// </summary>
        public static string StyleNamePagerDiv { get; set; }

        /// <summary>
        /// The name of the CSS style for the span element that contains the pages. The default is "pages".
        /// </summary>
        public static string StyleNamePagesSpan { get; set; }

        /// <summary>
        /// The name of the CSS style for the span element that contains the page info. The default is "pageinfo".
        /// </summary>
        public static string StyleNamePageInfoSpan { get; set; }

        /// <summary>
        /// The name of the CSS style for the span element that contains the page sizes. The default is "pagesizes".
        /// </summary>
        public static string StyleNamePageSizesSpan { get; set; }

        /// <summary>
        /// The name of the CSS style for the currently selected link. The default is "current".
        /// </summary>
        public static string StyleNameCurrentLink { get; set; }

        /// <summary>
        /// The name of the CSS style for a disabled link. The default is "disabled".
        /// </summary>
        public static string StyleNameDisabledLink { get; set; }

        /// <summary>
        /// The format string for the page info, where {0} is the first shown item, {1} is the last shown item and {2} is the total items. The default is "{0} - {1} / {2}".
        /// </summary>
        public static string PageInfoFormat { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="Pager"/> class.
        /// </summary>
        static Pager()
        {
            NumPageLinksToDisplay = 10;
            PageNumberParameterName = "page";
            PageSizeParameterName = "count";
            PageSizesToDisplay = new int[] { 10, 25, 50 };
            StyleNamePagerDiv = "pager";
            StyleNamePagesSpan = "pages";
            StyleNamePageInfoSpan = "pageinfo";
            StyleNamePageSizesSpan = "pagesizes";
            StyleNameCurrentLink = "current";
            StyleNameDisabledLink = "disabled";
            PageInfoFormat = "{0} - {1} / {2}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pager"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalItemCount">The total item count.</param>
        /// <param name="valuesDictionary">The values dictionary.</param>
        public Pager(ViewContext viewContext, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary)
        {
            this.viewContext = viewContext;
            this.pageSize = pageSize;
            this.currentPage = currentPage;
            this.totalItemCount = totalItemCount;
            this.linkWithoutPageValuesDictionary = valuesDictionary;
        }

        #endregion

        #region Rendering

        /// <summary>
        /// Renders the HTML.
        /// </summary>
        /// <returns>The HTML for the pager.</returns>
        public string RenderHtml()
        {
            int pageCount = (int)Math.Ceiling(this.totalItemCount / (double)this.pageSize);

            // [jelled] Changed so that no pager displays if not necessary.
            if (pageCount <= 1)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.AppendFormat("<div class=\"{0}\">", StyleNamePagerDiv);

            // Pages
            sb.AppendFormat("<span class=\"{0}\">", StyleNamePagesSpan);
            RenderPagerHtml(sb, pageCount);
            sb.Append("</span>");

            // Page Info
            sb.AppendFormat("<span class=\"{0}\">", StyleNamePageInfoSpan);
            RenderPageInfoHtml(sb);
            sb.Append("</span>");

            // Page Sizes
            sb.AppendFormat("<span class=\"{0}\">", StyleNamePageSizesSpan);
            RenderPageSizerHtml(sb);
            sb.Append("</span>");

            // Close
            sb.Append("</div>");
            return sb.ToString();
        }

        private void RenderPagerHtml(StringBuilder sb, int pageCount)
        {
            int nrOfPagesToDisplay = NumPageLinksToDisplay;

            // Previous
            if (this.currentPage > 1)
            {
                sb.Append(GeneratePageLink("&lt;", this.currentPage - 1));
            }
            else
            {
                sb.AppendFormat("<span class=\"{0}\">&lt;</span>", StyleNameDisabledLink);
            }

            int start = 1;
            int end = pageCount;

            if (pageCount > nrOfPagesToDisplay)
            {
                int middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                int below = (this.currentPage - middle);
                int above = (this.currentPage + middle);

                if (below < 4)
                {
                    above = nrOfPagesToDisplay;
                    below = 1;
                }
                else if (above > (pageCount - 4))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay);
                }

                start = below;
                end = above;
            }

            if (start > 3)
            {
                sb.Append(GeneratePageLink("1", 1));
                sb.Append(GeneratePageLink("2", 2));
                sb.Append("...");
            }
            for (int i = start; i <= end; i++)
            {
                if (i == this.currentPage)
                {
                    sb.AppendFormat("<span class=\"{0}\">{1}</span>", StyleNameCurrentLink, i);
                }
                else
                {
                    sb.Append(GeneratePageLink(i.ToString(CultureInfo.CurrentCulture), i));
                }
            }
            if (end < (pageCount - 3))
            {
                sb.Append("...");
                sb.Append(GeneratePageLink((pageCount - 1).ToString(CultureInfo.CurrentCulture), pageCount - 1));
                sb.Append(GeneratePageLink(pageCount.ToString(CultureInfo.CurrentCulture), pageCount));
            }

            // Next
            if (this.currentPage < pageCount)
            {
                sb.Append(GeneratePageLink("&gt;", (this.currentPage + 1)));
            }
            else
            {
                sb.AppendFormat("<span class=\"{0}\">&gt;</span>", StyleNameDisabledLink);
            }
        }

        private void RenderPageInfoHtml(StringBuilder sb)
        {
            var totalItems = this.totalItemCount;
            var firstItem = 1 + ((this.currentPage - 1) * this.pageSize);
            var lastItem = Math.Min(totalItems, firstItem + this.pageSize - 1);
            sb.AppendFormat(PageInfoFormat, firstItem, lastItem, totalItems);
        }

        private void RenderPageSizerHtml(StringBuilder sb)
        {
            foreach (var pageSizeToDisplay in PageSizesToDisplay)
            {
                if (pageSizeToDisplay == this.pageSize)
                {
                    sb.AppendFormat("<span class=\"{0}\">{1}</span>", StyleNameCurrentLink, pageSizeToDisplay);
                }
                else
                {
                    // Always go to page 1 when changing page size.
                    sb.Append(GeneratePageLink(pageSizeToDisplay.ToString(CultureInfo.CurrentCulture), 1, pageSizeToDisplay));
                }
            }
        }

        private string GeneratePageLink(string linkText, int pageNumber)
        {
            return GeneratePageLink(linkText, pageNumber, this.pageSize);
        }

        private string GeneratePageLink(string linkText, int pageNumber, int pageSizeToDisplay)
        {
            var pageLinkValueDictionary = new RouteValueDictionary(this.linkWithoutPageValuesDictionary);
            pageLinkValueDictionary.Add(PageNumberParameterName, pageNumber);
            pageLinkValueDictionary.Add(PageSizeParameterName, pageSizeToDisplay);
            var virtualPathData = RouteTable.Routes.GetVirtualPath(this.viewContext.RequestContext, pageLinkValueDictionary);

            if (virtualPathData != null)
            {
                string linkFormat = "<a href=\"{0}\">{1}</a>";
                return String.Format(CultureInfo.InvariantCulture, linkFormat, HttpUtility.HtmlAttributeEncode(virtualPathData.VirtualPath), linkText);
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}