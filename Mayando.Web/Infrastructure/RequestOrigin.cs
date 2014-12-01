
namespace Myembro.Infrastructure
{
    /// <summary>
    /// Represents the possible origins for a request.
    /// </summary>
    public enum RequestOrigin
    {
        /// <summary>
        /// The request was made by the interactive user.
        /// </summary>
        User,

        /// <summary>
        /// The request was made through the Service API.
        /// </summary>
        ServiceApi,

        /// <summary>
        /// The request was made from a timer.
        /// </summary>
        Timer
    }
}