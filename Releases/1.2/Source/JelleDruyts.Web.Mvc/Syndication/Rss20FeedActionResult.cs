using System.ServiceModel.Syndication;
using System.Web.Mvc;

namespace JelleDruyts.Web.Mvc.Syndication
{
    /// <summary>
    /// Represents an <see cref="ActionResult"/> that returns an RSS 2.0 syndication feed instead of regular HTML.
    /// </summary>
    public class Rss20FeedActionResult : SyndicationFeedActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rss20FeedActionResult"/> class.
        /// </summary>
        /// <param name="feed">The feed to return.</param>
        public Rss20FeedActionResult(SyndicationFeed feed)
            : base(new Rss20FeedFormatter(feed), "application/rss+xml")
        {
        }
    }
}