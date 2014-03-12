using System;

namespace Mayando.ProviderModel.PhotoProviders
{
    /// <summary>
    /// Specifies that a certain type is a Mayando EmbroideryItem Provider.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PhotoProviderAttribute : ProviderAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoProviderAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the provider.</param>
        /// <param name="description">A description of the provider.</param>
        public PhotoProviderAttribute(string name, string description)
            : this(name, description, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoProviderAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the provider.</param>
        /// <param name="description">A description of the provider.</param>
        /// <param name="url">The location where more information about the provider can be found.</param>
        public PhotoProviderAttribute(string name, string description, string url)
            : base(name, description, url)
        {
        }

        #endregion
    }
}