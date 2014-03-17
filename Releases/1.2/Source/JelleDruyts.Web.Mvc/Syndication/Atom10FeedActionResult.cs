using System.ServiceModel.Syndication;
using System.Web.Mvc;

namespace JelleDruyts.Web.Mvc.Syndication
{
    /// <summary>
    /// Represents an <see cref="ActionResult"/> that returns an Atom 1.0 syndication feed instead of regular HTML.
    /// </summary>
    public class Atom10FeedActionResult : SyndicationFeedActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Atom10FeedActionResult"/> class.
        /// </summary>
        /// <param name="feed">The feed to return.</param>
        public Atom10FeedActionResult(SyndicationFeed feed)
            : base(new Atom10FeedFormatter(feed), "application/atom+xml")
        {
        }
    }
}