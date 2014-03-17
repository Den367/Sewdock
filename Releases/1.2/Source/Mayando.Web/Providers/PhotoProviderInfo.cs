using System;

namespace Mayando.Web.Providers
{
    /// <summary>
    /// Provides information about a photo provider.
    /// </summary>
    public class PhotoProviderInfo : ProviderInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoProviderInfo"/> class.
        /// </summary>
        /// <param name="photoProviderType">The type of the photo provider.</param>
        internal PhotoProviderInfo(Type photoProviderType)
            : base(photoProviderType)
        {
        }
    }
}