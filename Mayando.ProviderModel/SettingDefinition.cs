
namespace Mayando.ProviderModel
{
    /// <summary>
    /// Defines a setting that is persisted by the Mayando host.
    /// </summary>
    public class SettingDefinition
    {
        #region Properties

        /// <summary>
        /// Gets the name of the setting, which serves as its unique ID.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of the setting.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public SettingType Type { get; private set; }

        /// <summary>
        /// Gets the title of the setting, which is the name displayed to the user.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets a description of the setting.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets an optional sequence number for the setting, which is be used to order the settings in the user interface.
        /// </summary>
        public int? Sequence { get; private set; }

        /// <summary>
        /// Gets a value that determines if the setting is visible to the user.
        /// </summary>
        public bool UserVisible { get; private set; }

        /// <summary>
        /// Gets the initial value for the setting, which is applied when the setting is first used.
        /// </summary>
        public string InitialValue { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingDefinition"/> class.
        /// </summary>
        /// <param name="name">The name of the setting, which serves as its unique ID.</param>
        /// <param name="type">The type of the setting.</param>
        /// <param name="title">The title of the setting, which is the name displayed to the user.</param>
        /// <param name="description">A description of the setting.</param>
        public SettingDefinition(string name, SettingType type, string title, string description)
            : this(name, type, title, description, null, false, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingDefinition"/> class.
        /// </summary>
        /// <param name="name">The name of the setting, which serves as its unique ID.</param>
        /// <param name="type">The type of the setting.</param>
        /// <param name="title">The title of the setting, which is the name displayed to the user.</param>
        /// <param name="description">A description of the setting.</param>
        /// <param name="sequence">An optional sequence number for the setting, which is be used to order the settings in the user interface.</param>
        /// <param name="userVisible">A value that determines if the setting is visible to the user.</param>
        public SettingDefinition(string name, SettingType type, string title, string description, int? sequence, bool userVisible)
            : this(name, type, title, description, sequence, userVisible, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingDefinition"/> class.
        /// </summary>
        /// <param name="name">The name of the setting, which serves as its unique ID.</param>
        /// <param name="type">The type of the setting.</param>
        /// <param name="title">The title of the setting, which is the name displayed to the user.</param>
        /// <param name="description">A description of the setting.</param>
        /// <param name="sequence">An optional sequence number for the setting, which is be used to order the settings in the user interface.</param>
        /// <param name="userVisible">A value that determines if the setting is visible to the user.</param>
        /// <param name="initialValue">The initial value for the setting, which is applied when the setting is first used.</param>
        public SettingDefinition(string name, SettingType type, string title, string description, int? sequence, bool userVisible, string initialValue)
        {
            this.Name = name;
            this.Type = type;
            this.Title = title;
            this.Description = description;
            this.Sequence = sequence;
            this.UserVisible = userVisible;
            this.InitialValue = initialValue;
        }

        #endregion

        #region Convenience Properties

        /// <summary>
        /// Gets the title of the setting to be displayed to the user.
        /// </summary>
        public string DisplayTitle
        {
            get
            {
                return (string.IsNullOrEmpty(this.Title) ? this.Name : this.Title);
            }
        }

        #endregion
    }
}