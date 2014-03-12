
namespace Mayando.ProviderModel
{
    /// <summary>
    /// Defines the possible types for the value of a <see cref="SettingDefinition"/>.
    /// </summary>
    public enum SettingType
    {
        /// <summary>
        /// The value is plain text.
        /// </summary>
        Plaintext,

        /// <summary>
        /// The value is formatted as HTML.
        /// </summary>
        Html,

        /// <summary>
        /// The value is a boolean value.
        /// </summary>
        Boolean,

        /// <summary>
        /// The value is a system time zone.
        /// </summary>
        TimeZone,

        /// <summary>
        /// The value is a visual theme.
        /// </summary>
        Theme,

        /// <summary>
        /// The value is an integer value.
        /// </summary>
        Integer,
    }
}