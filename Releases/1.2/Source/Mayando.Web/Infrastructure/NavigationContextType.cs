
namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// Defines the available types of navigation context.
    /// </summary>
    public enum NavigationContextType
    {
        /// <summary>
        /// Navigation should take place by the date a photo was taken.
        /// </summary>
        Taken,

        /// <summary>
        /// Navigation should take place by the tags of a photo.
        /// </summary>
        Tag,

        /// <summary>
        /// Navigation should take place by the results of a search.
        /// </summary>
        Search,

        /// <summary>
        /// Navigation should take place by the title of a photo.
        /// </summary>
        Title,

        /// <summary>
        /// Navigation should take place by the gallery a photo belongs to.
        /// </summary>
        Gallery
    }
}