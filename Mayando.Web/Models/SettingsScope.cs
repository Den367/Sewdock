
namespace Myembro.Models
{
    /// <summary>
    /// Defines the available scopes for settings.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    public enum SettingsScope
    {
        Application = 1,
        PhotoProvider = 2,
        AntiSpamProvider = 3,
        User = 4
    }
}