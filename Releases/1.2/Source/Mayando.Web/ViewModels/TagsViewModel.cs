using System.Collections.Generic;
using JelleDruyts.Web.Mvc;
using Mayando.Web.Models;

namespace Mayando.Web.ViewModels
{
    public class TagsViewModel
    {
        public IEnumerable<TagInfo> Tags { get; private set; }
        public int? Count { get; private set; }
        public IEnumerable<LinkListItem> Links { get; private set; }

        public TagsViewModel(ICollection<TagInfo> tags, int? count, IEnumerable<LinkListItem> links)
        {
            this.Tags = tags;
            this.Count = count;
            this.Links = links;
        }
    }
}