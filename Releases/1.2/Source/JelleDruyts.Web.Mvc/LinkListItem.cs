
namespace JelleDruyts.Web.Mvc
{
    /// <summary>
    /// Represents an item in a list of links.
    /// </summary>
    public class LinkListItem
    {
        #region Properties

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LinkListItem"/> is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the tool tip.
        /// </summary>
        public string ToolTip { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkListItem"/> class.
        /// </summary>
        public LinkListItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkListItem"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="text">The text.</param>
        /// <param name="active">A value indicating whether this <see cref="LinkListItem"/> is active.</param>
        public LinkListItem(string url, string text, bool active)
            : this(url, text, active, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkListItem"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="text">The text.</param>
        /// <param name="active">A value indicating whether this <see cref="LinkListItem"/> is active.</param>
        /// <param name="toolTip">The tool tip.</param>
        public LinkListItem(string url, string text, bool active, string toolTip)
        {
            this.Url = url;
            this.Text = text;
            this.Active = active;
            this.ToolTip = toolTip;
        }

        #endregion
    }
}