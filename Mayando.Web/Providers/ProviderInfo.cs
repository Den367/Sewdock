using System;
using JelleDruyts.Web.Mvc;
using Mayando.ProviderModel;

namespace Mayando.Web.Providers
{
    /// <summary>
    /// Provides information about a provider.
    /// </summary>
    public abstract class ProviderInfo
    {
        #region Properties

        /// <summary>
        /// Gets the type of the provider.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Gets the name of the provider.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description of the provider.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the URL of the provider where more information about it can be obtained.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Gets the GUID of the provider.
        /// </summary>
        public string Guid { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderInfo"/> class.
        /// </summary>
        /// <param name="providerType">The type of the provider.</param>
        protected ProviderInfo(Type providerType)
        {
            if (providerType == null)
            {
                throw new ArgumentNullException("providerType");
            }
            var providerAttribute = providerType.FindAttribute<ProviderAttribute>();
            if (providerAttribute == null)
            {
                throw new ArgumentException("The given provider type is invalid since it doesn't declare the proper ProviderAttribute attribute.");
            }
            this.Type = providerType;
            this.Name = providerAttribute.Name;
            this.Description = providerAttribute.Description;
            this.Url = providerAttribute.Url;
            this.Guid = providerType.GUID.ToString();
        }

        #endregion
    }
}