using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using JelleDruyts.Web.Mvc;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Properties;
using Mayando.Web.ViewModels;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with tags.")]
    public class TagsController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "tags";

        #endregion

        #region Actions

        [Description("Shows an overview of tags.")]
        public ActionResult Index([Description("The top number of tags to show.")]int? count)
        {
            var links = new List<LinkListItem>();
            links.Add(new LinkListItem(this.Url.Action(ActionName.Index), Resources.TagsShowAll, count != null));
            foreach (var i in new int[] { 100, 50, 25, 10 })
            {
                links.Add(new LinkListItem(this.Url.Action(ActionName.Index, new { count = i }), string.Format(CultureInfo.CurrentCulture, Resources.TagsShowTop, i), count != i));
            }
            return ViewFor(ViewName.Index, r => new TagsViewModel(GetTagInfos(r.GetTagCounts(count)), count, links));
        }

        #endregion

        #region Helper Methods

        private static ICollection<TagInfo> GetTagInfos(IDictionary<Tag, int> tags)
        {
            if (tags.Count == 0)
            {
                return new TagInfo[0];
            }
            var minOccurs = tags.Min(t => t.Value);
            var maxOccurs = tags.Max(t => t.Value);

            var tagInfos = new List<TagInfo>();
            foreach (var t in tags.OrderBy(t => t.Key.Name))
            {
                int relativeSizePercent;
                if (minOccurs == maxOccurs)
                {
                    // All tags have the same number of occurrences, give them all an average relative size of 100%.
                    relativeSizePercent = 100;
                }
                else
                {
                    // See http://blogs.dekoh.com/dev/2007/10/29/choosing-a-good-font-size-variation-algorithm-for-your-tag-cloud/.
                    var occurencesOfCurrentTag = t.Value;
                    // The line below would represent a linear mapping, which we will not use.
                    // var weight = (occurencesOfCurrentTag - minOccurs) / (maxOccurs - minOccurs);
                    // Take a logarithmic mapping of the occurrences to determine the weight.
                    var weight = (Math.Log(occurencesOfCurrentTag) - Math.Log(minOccurs)) / (Math.Log(maxOccurs) - Math.Log(minOccurs));
                    relativeSizePercent = (int)(weight * 100);
                }
                tagInfos.Add(new TagInfo(t.Key, t.Value, relativeSizePercent));
            }
            return tagInfos;
        }

        #endregion
    }
}