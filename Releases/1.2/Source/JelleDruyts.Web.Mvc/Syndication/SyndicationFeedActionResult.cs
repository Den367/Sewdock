using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace JelleDruyts.Web.Mvc.Syndication
{
    /// <summary>
    /// Represents an <see cref="ActionResult"/> that returns a syndication feed instead of regular HTML.
    /// </summary>
    public abstract class SyndicationFeedActionResult : ActionResult
    {
        #region Properties

        /// <summary>
        /// Gets the syndication feed formatter to use.
        /// </summary>
        public SyndicationFeedFormatter Formatter { get; private set; }

        /// <summary>
        /// Gets the content type of the feed.
        /// </summary>
        public string ContentType { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicationFeedActionResult"/> class.
        /// </summary>
        /// <param name="formatter">The syndication feed formatter to use.</param>
        /// <param name="contentType">The content type of the feed.</param>
        protected SyndicationFeedActionResult(SyndicationFeedFormatter formatter, string contentType)
        {
            this.Formatter = formatter;
            this.ContentType = contentType;
        }

        #endregion

        #region ExecuteResult

        /// <summary>
        /// Serializes the <see cref="Formatter"/> to the output stream.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = this.ContentType;
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                this.Formatter.WriteTo(writer);
            }
        }

        #endregion
    }
}