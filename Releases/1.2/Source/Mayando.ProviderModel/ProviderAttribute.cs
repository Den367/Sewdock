using System;

namespace Mayando.ProviderModel
{
    /// <summary>
    /// Specifies that a certain type is a Mayando provider.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class ProviderAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Gets the name of the provider.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a description of the provider.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the location where more information about the provider can be found.
        /// </summary>
        public string Url { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the provider.</param>
        /// <param name="description">A description of the provider.</param>
        /// <param name="url">The location where more information about the provider can be found.</param>
        protected ProviderAttribute(string name, string description, string url)
        {
            this.Name = name;
            this.Description = description;
            this.Url = url;
        }

        #endregion
    }
}