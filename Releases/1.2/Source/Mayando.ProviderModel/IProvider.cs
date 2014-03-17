
namespace Mayando.ProviderModel
{
    /// <summary>
    /// Represents a provider that can be used in Mayando.
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// Gets the settings that are supported by this provider.
        /// </summary>
        /// <returns>A list of setting definitions supported by this provider.</returns>
        SettingDefinition[] GetSettingDefinitions();
        
        /// <summary>
        /// Resets the provider to its initial state. This typically involves clearing its settings.
        /// </summary>
        void Reset();
    }
}