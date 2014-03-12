using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using JelleDruyts.Web.Mvc.Syndication;
using Mayando.Web.Extensions;
using Mayando.Web.Models;

namespace Mayando.Web.Controllers
{
    [Description("Handles actions that have to do with syndication feeds.")]
    public class FeedsController : SiteControllerBase
    {
        #region Constants

        public const string ControllerName = "feeds";
        private const int DefaultNumberOfFeedItems = 10;

        #endregion

        #region Actions

        [Description("Returns a syndication feed of the latest photos.")]
        public ActionResult Photos([Description("The syndication feed format, which can be \"rss\" or \"atom\". The default is \"atom\".")]string format, [Description("The number of items to show.")]int? count)
        {
            var numItems = (count ?? DefaultNumberOfFeedItems);
            var items = new List<SyndicationItem>();
            //using (var repository = GetRepository())
            //{
            //    foreach (var photo in repository.GetLatestEmbros(EmbroDateType.Published, numItems))
            //    {
            //        var photoUri = this.SiteData.ToAbsolute(Url.PhotoDetails(photo));
            //        var content = string.Format(CultureInfo.InvariantCulture, "<div><div><img src=\"{0}\" /></div><div>{1}</div></div>", photo.UrlNormal, photo.Text);
            //        var item = new SyndicationItem
            //        {
            //            Title = new TextSyndicationContent(photo.DisplayTitle, TextSyndicationContentKind.Plaintext),
            //            Content = new TextSyndicationContent(content, TextSyndicationContentKind.Html),
            //            PublishDate = photo.Published,
            //            LastUpdatedTime = photo.Published,
            //        };
            //        item.AddPermalink(photoUri);
            //        items.Add(item);
            //    }
            //}
            var feedTitle = this.SiteData.Settings.Title;
            return FeedActionResult(feedTitle, items, format);
        }

        [Description("Returns a syndication feed of the latest comments.")]
        public ActionResult Comments([Description("The syndication feed format, which can be \"rss\" or \"atom\". The default is \"atom\".")]string format, [Description("The number of items to show.")]int? count)
        {
            var numItems = (count ?? DefaultNumberOfFeedItems);
            var items = new List<SyndicationItem>();
            //using (var repository = GetRepository())
            //{
            //    foreach (var comment in repository.GetLatestCommentsWithPhoto(numItems, 0))
            //    {
            //        var commentUri = this.SiteData.ToAbsolute(Url.CommentDetails(comment));
            //        var content = this.GetCommentNotificationText(comment, false);
            //        var title = this.GetCommentNotificationTitle(comment);
            //        var item = new SyndicationItem
            //        {
            //            Title = new TextSyndicationContent(title, TextSyndicationContentKind.Plaintext),
            //            Content = new TextSyndicationContent(content, TextSyndicationContentKind.Html),
            //            PublishDate = comment.DatePublished,
            //            LastUpdatedTime = comment.DatePublished
            //        };
            //        item.AddPermalink(commentUri);
            //        items.Add(item);
            //    }
            //}
            var feedTitle = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", this.SiteData.Settings.Title, "Comments");
            return FeedActionResult(feedTitle, items, format);
        }

        private object GetCommentNotificationTitle(object comment)
        {
            throw new NotImplementedException();
        }

        private object GetCommentNotificationText(object comment, bool p)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods

        private ActionResult FeedActionResult(string feedTitle, List<SyndicationItem> items, string format)
        {
            var feed = new SyndicationFeed
            {
                Title = new TextSyndicationContent(feedTitle, TextSyndicationContentKind.Plaintext),
                Description = new TextSyndicationContent(this.SiteData.Settings.Subtitle, TextSyndicationContentKind.Plaintext),
                BaseUri = this.SiteData.AbsoluteApplicationPath,
                Copyright = new TextSyndicationContent(string.Format(CultureInfo.InvariantCulture, "(c) {0}", this.SiteData.Settings.OwnerName), TextSyndicationContentKind.Plaintext),
                Generator = this.SiteData.Generator,
                LastUpdatedTime = DateTimeOffset.UtcNow,
                Items = items
            };
            feed.Authors.Add(new SyndicationPerson(null, this.SiteData.Settings.OwnerName, null));

            if (string.Equals(format, "rss", StringComparison.OrdinalIgnoreCase))
            {
                return new Rss20FeedActionResult(feed);
            }
            else
            {
                return new Atom10FeedActionResult(feed);
            }
        }

        #endregion
    }
}