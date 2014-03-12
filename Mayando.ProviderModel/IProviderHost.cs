using System;
using System.Collections.Generic;

namespace Mayando.ProviderModel
{
    /// <summary>
    /// Represents the host environment for a Mayando provider.
    /// </summary>
    public interface IProviderHost
    {
        /// <summary>
        /// Gets all the provider's current setting values from the host.
        /// </summary>
        /// <returns>A dictionary with all the setting values for the provider, which can be looked up by the setting's <see cref="SettingDefinition.Name"/> as key.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IDictionary<string, string> GetSettings();

        /// <summary>
        /// Persists the given setting values back to the host.
        /// </summary>
        /// <param name="settings">A dictionary with the setting values to be persisted for the provider, which can be looked up by the setting's <see cref="SettingDefinition.Name"/> as key.</param>
        void SaveSettings(IDictionary<string, string> settings);
    }
}