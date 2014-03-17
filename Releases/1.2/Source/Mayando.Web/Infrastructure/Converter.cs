using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mayando.Web.Models;

namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// Contains conversion operations.
    /// </summary>
    public static class Converter
    {
        #region Tags

        /// <summary>
        /// Converts a string to a list of tags.
        /// </summary>
        /// <param name="tagList">The comma separated list of tags.</param>
        /// <returns>The list of tags (trimmed and with any duplicates removed).</returns>
        public static IList<string> ToTags(string tagList)
        {
            if (string.IsNullOrEmpty(tagList))
            {
                return new string[0];
            }
            else
            {
                var tags = tagList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var list = new List<string>();
                foreach(var tag in tags)
                {
                    var cleanedTag = tag.Trim();
                    if (!list.Contains(cleanedTag))
                    {
                        list.Add(cleanedTag);
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// Converts a list of <see cref="Tag"/> instances to a list of tag names.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static IEnumerable<string> ToTagNames(IEnumerable<Tag> tags)
        {
            return (from t in tags
                    orderby t.Name
                    select t.Name).ToArray();
        }

        /// <summary>
        /// Converts a list of <see cref="Tag"/> instances to a comma separated tag list.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static string ToTagList(IEnumerable<Tag> tags)
        {
            var list = new StringBuilder();
            foreach (var tag in tags.OrderBy(t => t.Name))
            {
                if (list.Length > 0)
                {
                    list.Append(", ");
                }
                list.Append(tag.Name);
            }
            return list.ToString();
        }

        #endregion
    }
}