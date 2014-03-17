using System;
using System.Collections.Generic;
using Mayando.ProviderModel;
using Mayando.Web.Models;

namespace Mayando.Web.Providers
{
    /// <summary>
    /// Represents a host for a provider.
    /// </summary>
    internal abstract class ProviderHost : IProviderHost
    {
        #region Properties

        /// <summary>
        /// Gets or sets the settings scope.
        /// </summary>
        private SettingsScope Scope { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderHost"/> class.
        /// </summary>
        /// <param name="scope">The settings scope.</param>
        protected ProviderHost(SettingsScope scope)
        {
            this.Scope = scope;
        }

        #endregion

        #region IProviderHost Implementation

        /// <summary>
        /// Gets all the provider's current setting values from the host.
        /// </summary>
        /// <returns>A dictionary with all the setting values for the provider, which can be looked up by the setting's <see cref="SettingDefinition.Name"/> as key.</returns>
        public IDictionary<string, string> GetSettings()
        {
            using (var repository = MayandoRepositoryFactory.CreateRepository())
            {
                return repository.GetSettingValues(this.Scope);
            }
        }

        /// <summary>
        /// Persists the given setting values back to the host.
        /// </summary>
        /// <param name="settings">A dictionary with the setting values to be persisted for the provider, which can be looked up by the setting's <see cref="SettingDefinition.Name"/> as key.</param>
        public void SaveSettings(IDictionary<string, string> settings)
        {
            using (var repository = MayandoRepositoryFactory.CreateRepository(true))
            {
                repository.SaveSettingValues(this.Scope, settings);
                repository.CommitChanges();
            }
        }

        #endregion
    }
}