using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JelleDruyts.Web.Mvc
{
    /// <summary>
    /// Provides extension methods for <see cref="HtmlHelper"/> instances.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        #region Link

        /// <summary>
        /// Returns an anchor tag to a specified URL.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="text">The text to display in the link.</param>
        /// <returns>An anchor tag, or just the text if no URL was given.</returns>
        public static string Link(this HtmlHelper htmlHelper, string url, string text)
        {
            return Link(htmlHelper, url, text, false, null, null);
        }

        /// <summary>
        /// Returns an anchor tag to a specified URL.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="text">The text to display in the link.</param>
        /// <param name="openInNewWindow">Determines if the link should be opened in a new window.</param>
        /// <returns>An anchor tag, or just the text if no URL was given.</returns>
        public static string Link(this HtmlHelper htmlHelper, string url, string text, bool openInNewWindow)
        {
            return Link(htmlHelper, url, text, openInNewWindow, null, null);
        }

        /// <summary>
        /// Returns an anchor tag to a specified URL.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="text">The text to display in the link.</param>
        /// <param name="openInNewWindow">Determines if the link should be opened in a new window.</param>
        /// <param name="toolTip">The tool tip for the link.</param>
        /// <returns>An anchor tag, or just the text if no URL was given.</returns>
        public static string Link(this HtmlHelper htmlHelper, string url, string text, bool openInNewWindow, string toolTip)
        {
            return Link(htmlHelper, url, text, openInNewWindow, toolTip, null);
        }

        /// <summary>
        /// Returns an anchor tag to a specified URL.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="text">The text to display in the link.</param>
        /// <param name="openInNewWindow">Determines if the link should be opened in a new window.</param>
        /// <param name="toolTip">The tool tip for the link.</param>
        /// <param name="cssClass">The CSS class for the link.</param>
        /// <returns>An anchor tag, or just the text if no URL was given.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper")]
        public static string Link(this HtmlHelper htmlHelper, string url, string text, bool openInNewWindow, string toolTip, string cssClass)
        {
            return Link(url, text, openInNewWindow, toolTip, cssClass, null);
        }

        /// <summary>
        /// Returns an anchor tag to a specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="text">The text to display in the link.</param>
        /// <returns>An anchor tag, or just the text if no URL was given.</returns>
        public static string Link(string url, string text)
        {
            return Link(url, text, false, null, null, null);
        }

        /// <summary>
        /// Returns an anchor tag to a specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="text">The text to display in the link.</param>
        /// <param name="openInNewWindow">Determines if the link should be opened in a new window.</param>
        /// <param name="toolTip">The tool tip for the link.</param>
        /// <param name="cssClass">The CSS class for the link.</param>
        /// <param name="htmlAttributes">The HTML attributes for the link.</param>
        /// <returns>An anchor tag, or just the text if no URL was given.</returns>
        public static string Link(string url, string text, bool openInNewWindow, string toolTip, string cssClass, IDictionary<string, object> htmlAttributes)
        {
            string linkText = (string.IsNullOrEmpty(text) ? url : HttpUtility.HtmlEncode(text));
            if (string.IsNullOrEmpty(url))
            {
                return linkText;
            }
            else
            {
                TagBuilder builder = new TagBuilder("a")
                {
                    InnerHtml = linkText
                };
                builder.MergeAttributes<string, object>(htmlAttributes);
                if (openInNewWindow)
                {
                    builder.MergeAttribute("target", "_blank");
                }
                if (!string.IsNullOrEmpty(cssClass))
                {
                    builder.MergeAttribute("class", cssClass);
                }
                if (!string.IsNullOrEmpty(toolTip))
                {
                    builder.MergeAttribute("title", toolTip);
                }
                builder.MergeAttribute("href", url);
                return builder.ToString(TagRenderMode.Normal);
            }
        }

        #endregion

        #region LinkList

        /// <summary>
        /// Returns a list of anchor (for active) or span (for inactive) tags for a list of links.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="items">The list items.</param>
        /// <returns>A list of span or anchor tags for a list of links.</returns>
        public static string LinkList(this HtmlHelper htmlHelper, IEnumerable<LinkListItem> items)
        {
            return LinkList(htmlHelper, items, " ");
        }

        /// <summary>
        /// Returns a list of anchor (for active) or span (for inactive) tags for a list of links.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="items">The list items.</param>
        /// <param name="separator">The separator to place between all items.</param>
        /// <returns>A list of span or anchor tags for a list of links.</returns>
        public static string LinkList(this HtmlHelper htmlHelper, IEnumerable<LinkListItem> items, string separator)
        {
            var sb = new StringBuilder();
            foreach (var item in items)
            {
                if (sb.Length > 0)
                {
                    sb.Append(separator);
                }
                if (item.Active)
                {
                    // Render a link for active items.
                    sb.Append(htmlHelper.Link(item.Url, item.Text, false, item.ToolTip));
                }
                else
                {
                    // Render a span for inactive items.
                    var span = new TagBuilder("span");
                    if (!string.IsNullOrEmpty(item.ToolTip))
                    {
                        span.MergeAttribute("title", item.ToolTip);
                    }
                    span.InnerHtml = htmlHelper.Encode(item.Text);
                    sb.Append(span.ToString());
                }
            }
            return sb.ToString();
        }

        #endregion

        #region EncodeWithLineBreaks

        /// <summary>
        /// Encodes the given value with line breaks.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="value">The value to encode.</param>
        /// <returns>The HTML encoded version of the given value, with line breaks encoded as HTML break tags (&lt;br /&gt;).</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper")]
        public static string EncodeWithLineBreaks(this HtmlHelper htmlHelper, object value)
        {
            return EncodeWithLineBreaks(value, null);
        }

        /// <summary>
        /// Encodes the given value with line breaks.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="value">The value to encode.</param>
        /// <param name="breakTag">The break tag to use for line breaks.</param>
        /// <returns>The HTML encoded version of the given value, with line breaks encoded as the given break tag.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper")]
        public static string EncodeWithLineBreaks(this HtmlHelper htmlHelper, object value, string breakTag)
        {
            return EncodeWithLineBreaks(value, breakTag);
        }

        /// <summary>
        /// Encodes the given value with line breaks.
        /// </summary>
        /// <param name="value">The value to encode.</param>
        /// <returns>The HTML encoded version of the given value, with line breaks encoded as the given break tag.</returns>
        public static string EncodeWithLineBreaks(object value)
        {
            return EncodeWithLineBreaks(value, null);
        }

        /// <summary>
        /// Encodes the given value with line breaks.
        /// </summary>
        /// <param name="value">The value to encode.</param>
        /// <param name="breakTag">The break tag to use for line breaks.</param>
        /// <returns>The HTML encoded version of the given value, with line breaks encoded as the given break tag.</returns>
        public static string EncodeWithLineBreaks(object value, string breakTag)
        {
            if (string.IsNullOrEmpty(breakTag))
            {
                breakTag = "<br />";
            }
            var encoded = HttpUtility.HtmlEncode(Convert.ToString(value, CultureInfo.CurrentCulture));
            encoded = encoded.Replace(Environment.NewLine, breakTag);
            return encoded;
        }

        #endregion
    }
}