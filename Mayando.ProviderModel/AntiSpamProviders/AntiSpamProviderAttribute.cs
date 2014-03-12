using System;

namespace Mayando.ProviderModel.AntiSpamProviders
{
    /// <summary>
    /// Specifies that a certain type is a Mayando Anti-Spam Provider.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AntiSpamProviderAttribute : ProviderAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AntiSpamProviderAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the provider.</param>
        /// <param name="description">A description of the provider.</param>
        public AntiSpamProviderAttribute(string name, string description)
            : this(name, description, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AntiSpamProviderAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the provider.</param>
        /// <param name="description">A description of the provider.</param>
        /// <param name="url">The location where more information about the provider can be found.</param>
        public AntiSpamProviderAttribute(string name, string description, string url)
            : base(name, description, url)
        {
        }

        #endregion
    }
}