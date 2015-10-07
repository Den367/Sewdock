using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;
using JelleDruyts.Web.Mvc;
using Myembro.Extensions;
using Myembro.Infrastructure;
using Myembro.Models;
using Myembro.Properties;
using Myembro.Repository;
using Myembro.ViewModels;

namespace Myembro.Controllers
{
    [Description("Handles actions that have to do with tags.")]
    public class TagsController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "tags";

        #endregion
        #region ctor
         private readonly ITagRepository _repo;
         public TagsController(ITagRepository repo)
        {
            _repo = repo;
        }
        #endregion
        #region Actions

        [Description("Shows an overview of tags.")]
        public ActionResult Index([Description("The top number of tags to show.")]int? count)
        {
            var tags = _repo.GetTags();
            var tagInfos = GetTagInfos(tags);
            var links = GetLinks(tagInfos, "Embro", "Index", "criteria");
            var m = new TagsViewModel(tagInfos, count, links);
            if (Request.IsAjaxRequest()) return PartialView("Main",m);
            return View(ViewName.Index,m);
        }

        #endregion

        #region Helper Methods

        private IDictionary<string, LinkListItem> GetLinks(ICollection<TagInfo> tagsInfo, string controllerName, string actionName, string paramName)
        {
            var links = new Dictionary<string, LinkListItem>();
            foreach (var tagInfo in tagsInfo)
            {
                var tagName = tagInfo.Tag;
                var routeDictionary = new RouteValueDictionary {{paramName, tagName}};
                var link = new LinkListItem(this.Url.Action(actionName, controllerName, routeDictionary), tagName,
                                            tagInfo.NumberOfEmbros > 0);
               
                links.Add(tagName,link);
            }
            //links.Add(new LinkListItem(this.Url.Action(actionName), Resources.TagsShowAll, count != null));
            //foreach (var i in new int[] { 100, 50, 25, 10 })
            //{
            //    links.Add(new LinkListItem(this.Url.Action(ActionName.Index, new { count = i }), string.Format(CultureInfo.CurrentCulture, Myembro.Properties.Resources.TagsShowTop, i), count != i));
            //}
            return links;
        }

        private static ICollection<TagInfo> GetTagInfos(IDictionary<string, int> tags)
        {
            if (tags.Count == 0)
            {
                return new TagInfo[0];
            }
            var minOccurs = tags.Min(t => t.Value);
            var maxOccurs = tags.Max(t => t.Value);

            var tagInfos = new List<TagInfo>();
            foreach (var t in tags.OrderBy(t => t.Key))
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