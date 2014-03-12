
namespace Mayando.Web.Models
{
    /// <summary>
    /// Represents additional information about a <see cref="Tag"/>.
    /// </summary>
    public class TagInfo
    {
        #region Properties

        /// <summary>
        /// The tag for which additional information is given.
        /// </summary>
        public Tag Tag { get; set; }

        /// <summary>
        /// The number of photos with the current tag.
        /// </summary>
        public int NumberOfPhotos { get; set; }

        /// <summary>
        /// The relative size (in percent from 1 to 100) of this tag, based on the number of photos that have the tag.
        /// </summary>
        public int RelativeSizePercent { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TagInfo"/> class.
        /// </summary>
        /// <param name="tag">The tag for which additional information is given.</param>
        /// <param name="numberOfPhotos">The number of photos with the current tag.</param>
        /// <param name="relativeSizePercent">The relative size (in percent from 1 to 100) of this tag, based on the number of photos that have the tag.</param>
        public TagInfo(Tag tag, int numberOfPhotos, int relativeSizePercent)
        {
            this.Tag = tag;
            this.NumberOfPhotos = numberOfPhotos;
            this.RelativeSizePercent = relativeSizePercent;
        }

        #endregion

        #region Convenience Properties

        /// <summary>
        /// The relative size (from 1 to 5) of this tag, based on the number of photos that have the tag.
        /// </summary>
        public int RelativeSize
        {
            get
            {
                return 1 + ((this.RelativeSizePercent - 1) / 20);
            }
        }

        #endregion
    }
}