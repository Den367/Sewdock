using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mayando.Web.Models;
using System.Xml.Linq;

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
        /// Converts a list of <see cref="string"/> instances to a list of tag names.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static IEnumerable<string> ToTagNames(IEnumerable<string> tags)
        {
            return (from t in tags
                    orderby t
                    select t).ToArray();
        }

        /// <summary>
        /// Converts a list of <see cref="string"/> instances to a comma separated tag list.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static string ToTagList(IEnumerable<string> tags)
        {
            var list = new StringBuilder();
            foreach (var tag in tags.OrderBy(t => t))
            {
                if (list.Length > 0)
                {
                    list.Append(", ");
                }
                list.Append(tag);
            }
            return list.ToString();
        }



        public static string ToTagCommaDelimited(IList<string> tags)
        {
            var list = new StringBuilder();
            foreach (var tag in tags)
            {
                if (list.Length > 0)
                {
                    list.Append(", ");
                }
                list.Append(tag);
            }
            return list.ToString();
        }




        /// <summary>
        /// Converts tag list in to Xml structured text
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static string ToTagXmled(IEnumerable<string> tags)
        {
            if (tags == null) return null;
            var setTags = new HashSet<string>();
            string cleanedTag;
            var xTags = new XElement("Tags");
            foreach (var tag in tags)
            {
                cleanedTag = tag.Trim();
                if (!setTags.Contains(cleanedTag))
                {
                    setTags.Add(cleanedTag);
                    xTags.Add(new XElement("tag", cleanedTag));
                }
            }
            return xTags.ToString();
        }

        public static IList<string> FromTagXmled(string tags)
        {
            XElement e = XElement.Parse(tags);
            var result = from t in e.Descendants("tag")
                         select (string)t;
            return result.ToList();
            
        }

        /// <summary>
        /// Converts comma delimited tag string in to list
        /// </summary>
        /// <param name="tags">comma delimited tag string type of <see cref="string"/></param>
        /// <returns></returns>
        public static IList<string> ToTagList(string tags)
        {
            tags.Replace(',', ';');
            var list = tags.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var setTags = new HashSet<string>();
            string cleanedTag;

            foreach (var tag in list)
            {
                cleanedTag = tag.Trim();
                if (!setTags.Contains(cleanedTag))
                {
                    setTags.Add(cleanedTag);
                }
            }
            return setTags.ToList<string>();
        }
        #endregion
    }
}