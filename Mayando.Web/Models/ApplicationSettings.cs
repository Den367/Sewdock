using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mayando.ProviderModel;

namespace Mayando.Web.Models
{
    /// <summary>
    /// Defines the configurable application settings.
    /// </summary>
    public class ApplicationSettings
    {
        #region Constants

        public const string SettingNameTitle = "Title";
        public const string SettingNameSubtitle = "Subtitle";
        public const string SettingNameFooter = "Footer";
        public const string SettingNameDescription = "Description";
        public const string SettingNameKeywords = "Keywords";
        public const string SettingNameLinkToExternalPhotoUrl = "LinkToExternalPhotoUrl";
        public const string SettingNameShowDaysInCalendar = "ShowDaysInCalendar";
        public const string SettingNameTheme = "Theme";
        public const string SettingNameHomepage = "Homepage";
        public const string SettingNameTimeZoneName = "TimeZoneName";
        public const string SettingNameOwnerName = "OwnerName";
        public const string SettingNameOwnerEmail = "OwnerEmail";
        public const string SettingNameNotifyOnComments = "NotifyOnComments";
        public const string SettingNameNotifyOnErrors = "NotifyOnErrors";
        public const string SettingNameSmtpServer = "SmtpServer";
        public const string SettingNamePhotoProviderGuid = "PhotoProviderGuid";
        public const string SettingNamePhotoProviderLastSyncSucceeded = "PhotoProviderLastSyncSucceeded";
        public const string SettingNamePhotoProviderLastSyncTime = "PhotoProviderLastSyncTime";
        public const string SettingNamePhotoProviderLastSyncStatus = "PhotoProviderLastSyncStatus";
        public const string SettingNamePhotoProviderLastSyncNewPhotos = "PhotoProviderLastSyncNewPhotos";
        public const string SettingNamePhotoProviderLastSyncNewComments = "PhotoProviderLastSyncNewComments";
        public const string SettingNamePhotoProviderLastSyncWasSimulation = "PhotoProviderLastSyncWasSimulation";
        public const string SettingNameAntiSpamProviderGuid = "AntiSpamProviderGuid";
        public const string SettingNameEventLogLastViewedTime = "EventLogLastViewedTime";
        public const string SettingNameShowFilmstripInNavigationContext = "ShowFilmstripInNavigationContext";
        public const string SettingNameNumberOfPhotosInFilmstrip = "NumberOfPhotosInFilmstrip";
        public const string SettingNameServiceApiEnabled = "ServiceApiEnabled";
        public const string SettingNameApiKey = "ApiKey";
        public const string SettingNameLastRunVersion = "LastRunVersion";
        public const string SettingNamePhotoProviderAutoSyncEnabled = "PhotoProviderAutoSyncEnabled";
        public const string SettingNamePhotoProviderAutoSyncIntervalMinutes = "PhotoProviderAutoSyncIntervalMinutes";
        public const string SettingNamePhotoProviderAutoSyncTags = "PhotoProviderAutoSyncTags";
        public const string SettingNamePageSize = "PageSize";

        #endregion

        #region Fields

        private readonly IDictionary<string, string> settings;
        private readonly IList<string> changedSettingsKeys;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettings"/> class.
        /// </summary>
        /// <param name="settings">The setting values.</param>
        public ApplicationSettings(IDictionary<string, string> settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            this.settings = settings;
            this.changedSettingsKeys = new List<string>();
        }

        #endregion

        #region Properties

        public string Title { get { return GetSetting(SettingNameTitle); } set { SetSetting(SettingNameTitle, value); } }
        public string Subtitle { get { return GetSetting(SettingNameSubtitle); } set { SetSetting(SettingNameSubtitle, value); } }
        public string Footer { get { return GetSetting(SettingNameFooter); } set { SetSetting(SettingNameFooter, value); } }
        public string Description { get { return GetSetting(SettingNameDescription); } set { SetSetting(SettingNameDescription, value); } }
        public string Keywords { get { return GetSetting(SettingNameKeywords); } 
            set { SetSetting(SettingNameKeywords, value); } }
        public bool LinkToExternalPhotoUrl { get { return GetSetting<bool>(SettingNameLinkToExternalPhotoUrl, true); } set { SetSetting(SettingNameLinkToExternalPhotoUrl, value); } }
        public bool ShowDaysInCalendar { get { return GetSetting<bool>(SettingNameShowDaysInCalendar, false); } set { SetSetting(SettingNameShowDaysInCalendar, value); } }
        public string Theme { get { return GetSetting(SettingNameTheme); } set { SetSetting(SettingNameTheme, value); } }
        public string Homepage { get { return GetSetting(SettingNameHomepage); } set { SetSetting(SettingNameHomepage, value); } }
        public string TimeZoneName { get { return GetSetting(SettingNameTimeZoneName); } set { SetSetting(SettingNameTimeZoneName, value); } }
        public string OwnerName { get { return GetSetting(SettingNameOwnerName); } set { SetSetting(SettingNameOwnerName, value); } }
        public string OwnerEmail { get { return GetSetting(SettingNameOwnerEmail); } set { SetSetting(SettingNameOwnerEmail, value); } }
        public bool NotifyOnComments { get { return GetSetting<bool>(SettingNameNotifyOnComments, true); } set { SetSetting(SettingNameNotifyOnComments, value); } }
        public bool NotifyOnErrors { get { return GetSetting<bool>(SettingNameNotifyOnErrors, true); } set { SetSetting(SettingNameNotifyOnErrors, value); } }
        public string SmtpServer { get { return GetSetting(SettingNameSmtpServer); } set { SetSetting(SettingNameSmtpServer, value); } }
        public string PhotoProviderGuid { get { return GetSetting(SettingNamePhotoProviderGuid); } set { SetSetting(SettingNamePhotoProviderGuid, value); } }
        public bool? PhotoProviderLastSyncSucceeded { get { return GetSetting<bool>(SettingNamePhotoProviderLastSyncSucceeded); } set { SetSetting(SettingNamePhotoProviderLastSyncSucceeded, value); } }
        public DateTimeOffset? PhotoProviderLastSyncTime { get { return GetSettingAsDateTimeOffset(SettingNamePhotoProviderLastSyncTime); } set { SetSetting(SettingNamePhotoProviderLastSyncTime, value); } }
        public string PhotoProviderLastSyncStatus { get { return GetSetting(SettingNamePhotoProviderLastSyncStatus); } set { SetSetting(SettingNamePhotoProviderLastSyncStatus, value); } }
        public int? PhotoProviderLastSyncNewPhotos { get { return GetSetting<int>(SettingNamePhotoProviderLastSyncNewPhotos); } set { SetSetting(SettingNamePhotoProviderLastSyncNewPhotos, value); } }
        public int? PhotoProviderLastSyncNewComments { get { return GetSetting<int>(SettingNamePhotoProviderLastSyncNewComments); } set { SetSetting(SettingNamePhotoProviderLastSyncNewComments, value); } }
        public bool? PhotoProviderLastSyncWasSimulation { get { return GetSetting<bool>(SettingNamePhotoProviderLastSyncWasSimulation); } set { SetSetting(SettingNamePhotoProviderLastSyncWasSimulation, value); } }
        public bool PhotoProviderAutoSyncEnabled { get { return GetSetting<bool>(SettingNamePhotoProviderAutoSyncEnabled, false); } set { SetSetting(SettingNamePhotoProviderAutoSyncEnabled, value); } }
        public int? PhotoProviderAutoSyncIntervalMinutes { get { return GetSetting<int>(SettingNamePhotoProviderAutoSyncIntervalMinutes); } set { SetSetting(SettingNamePhotoProviderAutoSyncIntervalMinutes, value); } }
        public string PhotoProviderAutoSyncTags { get { return GetSetting(SettingNamePhotoProviderAutoSyncTags); } set { SetSetting(SettingNamePhotoProviderAutoSyncTags, value); } }
        public string AntiSpamProviderGuid { get { return GetSetting(SettingNameAntiSpamProviderGuid); } set { SetSetting(SettingNameAntiSpamProviderGuid, value); } }
        public DateTimeOffset? EventLogLastViewedTime { get { return GetSettingAsDateTimeOffset(SettingNameEventLogLastViewedTime); } set { SetSetting(SettingNameEventLogLastViewedTime, value); } }
        public bool ShowFilmstripInNavigationContext { get { return GetSetting<bool>(SettingNameShowFilmstripInNavigationContext, false); } set { SetSetting(SettingNameShowFilmstripInNavigationContext, value); } }
        public int NumberOfPhotosInFilmstrip { get { return GetSetting<int>(SettingNameNumberOfPhotosInFilmstrip, 4); } set { SetSetting(SettingNameNumberOfPhotosInFilmstrip, value); } }
        public bool ServiceApiEnabled { get { return GetSetting<bool>(SettingNameServiceApiEnabled, false); } set { SetSetting(SettingNameServiceApiEnabled, value); } }
        public string ApiKey { get { return GetSetting(SettingNameApiKey); } set { SetSetting(SettingNameApiKey, value); } }
        public string LastRunVersion { get { return GetSetting(SettingNameLastRunVersion); } set { SetSetting(SettingNameLastRunVersion, value); } }
        public int? PageSize { get { return GetSetting<int>(SettingNamePageSize); } set { SetSetting(SettingNamePageSize, value); } }
        #endregion

        #region Convenience Properties

        public TimeZoneInfo TimeZone
        {
            get
            {
                try
                {
                    return TimeZoneInfo.FindSystemTimeZoneById(this.TimeZoneName);
                }
                catch (TimeZoneNotFoundException)
                {
                    return TimeZoneInfo.Utc;
                }
            }
        }

        #endregion

        #region GetChangedSettings

        public IDictionary<string, string> GetChangedSettings()
        {
            return this.changedSettingsKeys.ToDictionary(k => k, k => GetSetting(k));
        }

        #endregion

        #region GetSettingDefinitions

        /// <summary>
        /// Gets the setting definitions.
        /// </summary>
        /// <returns>The definition of all available settings.</returns>
        public static SettingDefinition[] GetSettingDefinitions()
        {
            var sequence = 0;
            return new SettingDefinition[]
                {
                    // User-visible settings.
                    new SettingDefinition(ApplicationSettings.SettingNameTitle, SettingType.Plaintext, "Title", "The title of your website.", sequence++, true, "Embroidery dock"),
                    new SettingDefinition(ApplicationSettings.SettingNameSubtitle, SettingType.Plaintext, "Subtitle", "The subtitle of your website.", sequence++, true, string.Format(CultureInfo.CurrentCulture, "Powered by {0}", SiteData.GlobalApplicationName)),
                    new SettingDefinition(ApplicationSettings.SettingNameFooter, SettingType.Html, "Site Footer", "The footer HTML to appear on each page.", sequence++, true, string.Format(CultureInfo.CurrentCulture, "<p>Powered by <a href=\"{0}\" target=\"_blank\">{1}</a></p>", SiteData.GlobalApplicationUrl, SiteData.GlobalApplicationName)),
                    new SettingDefinition(ApplicationSettings.SettingNameDescription, SettingType.Plaintext, "Description", "The description to appear in the HTML header of each page.", sequence++, true, string.Format(CultureInfo.CurrentCulture, "A {0} Photoblog", SiteData.GlobalApplicationName)),
                    new SettingDefinition(ApplicationSettings.SettingNameKeywords, SettingType.Plaintext, "Keywords", "The keywords to appear in the HTML header of each page.", sequence++, true, "photo,photoblog,photography,photographer,gallery,photo gallery"),
                    new SettingDefinition(ApplicationSettings.SettingNameLinkToExternalPhotoUrl, SettingType.Boolean, "Link EmbroideryItem To External URL", "Determines if photos should link back to their external URL; by default they do (it's required for Flickr for example).", sequence++, true, bool.TrueString),
                    new SettingDefinition(ApplicationSettings.SettingNameShowFilmstripInNavigationContext, SettingType.Boolean, "Show Filmstrip In Navigation Context", "Determines if a filmstrip with 'nearby' photos should be shown in the navigation context; by default the filmstrip is shown.", sequence++, true, bool.TrueString),
                    new SettingDefinition(ApplicationSettings.SettingNameNumberOfPhotosInFilmstrip, SettingType.Integer, "Number Of Previous/Next Photos In Filmstrip", "The number of photos to show before and after the current photo in the filmstrip; by default 4 photos are shown on each side.", sequence++, true, "4"),
                    new SettingDefinition(ApplicationSettings.SettingNameShowDaysInCalendar, SettingType.Boolean, "Show Days In Calendar", "Determines if days should also be shown in the calendar overview; by default only years and months are shown.", sequence++, true, bool.FalseString),
                    new SettingDefinition(ApplicationSettings.SettingNameTheme, SettingType.Theme, "Theme", "The visual theme to use.", sequence++, true),
                    new SettingDefinition(ApplicationSettings.SettingNameHomepage, SettingType.Plaintext, "Homepage", "The URL of the home page for your website. Leave empty to show the latest photo. Use a ~ in front of the URL to represent the site's root path; e.g. ~/photos will always be correct regardless of the path.", sequence++, true),
                    new SettingDefinition(ApplicationSettings.SettingNameTimeZoneName, SettingType.TimeZone, "Time Zone", "The time zone where you live, so all dates and times will be shown correctly.", sequence++, true, TimeZoneInfo.Utc.Id),
                    new SettingDefinition(ApplicationSettings.SettingNameOwnerName, SettingType.Plaintext, "Owner Name", "Your name, or whoever owns the content on your website.", sequence++, true),
                    new SettingDefinition(ApplicationSettings.SettingNameOwnerEmail, SettingType.Plaintext, "Owner Email", "Your email address, where notifications will be sent to.", sequence++, true),
                    new SettingDefinition(ApplicationSettings.SettingNameNotifyOnComments, SettingType.Boolean, "Notify On Comments", "Determines if an email should be sent when new comments are posted.", sequence++, true, bool.TrueString),
                    new SettingDefinition(ApplicationSettings.SettingNameNotifyOnErrors, SettingType.Boolean, "Notify On Errors", "Determines if an email should be sent when errors occur in the application.", sequence++, true, bool.TrueString),
                    new SettingDefinition(ApplicationSettings.SettingNameSmtpServer, SettingType.Plaintext, "SMTP Server", "The SMTP email server through which to send emails.", sequence++, true),

                    // Non-visible settings.
                    new SettingDefinition(ApplicationSettings.SettingNamePhotoProviderGuid, SettingType.Plaintext, null, "The GUID of the selected photo provider."),
                    new SettingDefinition(ApplicationSettings.SettingNamePhotoProviderLastSyncSucceeded, SettingType.Plaintext, null, "Indicates if the last synchronization succeeded."),
                    new SettingDefinition(ApplicationSettings.SettingNamePhotoProviderLastSyncTime, SettingType.Plaintext, null, "The last time the provider has synchronized with its photo store."),
                    new SettingDefinition(ApplicationSettings.SettingNamePhotoProviderLastSyncStatus, SettingType.Html, null, "The status from last time the provider has synchronized with its photo store."),
                    new SettingDefinition(ApplicationSettings.SettingNamePhotoProviderLastSyncNewPhotos, SettingType.Integer, null, "The number of photos imported the last time the provider has synchronized with its photo store."),
                    new SettingDefinition(ApplicationSettings.SettingNamePhotoProviderLastSyncNewComments, SettingType.Integer, null, "The number of comments imported the last time the provider has synchronized with its photo store."),
                    new SettingDefinition(ApplicationSettings.SettingNamePhotoProviderLastSyncWasSimulation, SettingType.Plaintext, null, "Indicates if the last synchronization was a simulation only."),
                    new SettingDefinition(ApplicationSettings.SettingNameAntiSpamProviderGuid, SettingType.Plaintext, null, "The GUID of the selected anti-spam provider."),
                    new SettingDefinition(ApplicationSettings.SettingNameEventLogLastViewedTime, SettingType.Plaintext, null, "The last time the event log was viewed."),
                    new SettingDefinition(ApplicationSettings.SettingNameServiceApiEnabled, SettingType.Boolean, null, "Determines if the Service API is enabled."),
                    new SettingDefinition(ApplicationSettings.SettingNameApiKey, SettingType.Plaintext, null, "The Service API key used for authentication."),
                    new SettingDefinition(ApplicationSettings.SettingNameLastRunVersion, SettingType.Plaintext, null, "The version of the application at the last run."),
                    new SettingDefinition(ApplicationSettings.SettingNamePhotoProviderAutoSyncEnabled, SettingType.Boolean, null, "Determines if automatic photo provider synchronization is enabled."),
                    new SettingDefinition(ApplicationSettings.SettingNamePhotoProviderAutoSyncIntervalMinutes, SettingType.Integer, null, "The number of minutes between automatic photo provider synchronization events."),
                    new SettingDefinition(ApplicationSettings.SettingNamePhotoProviderAutoSyncTags, SettingType.Plaintext, null, "The tags that the photos must all have to be synchronized during automatic photo provider synchronization.")
                };
        }

        #endregion

        #region Helper Methods

        private string GetSetting(string name)
        {
            if (this.settings.ContainsKey(name))
            {
                return this.settings[name];
            }
            else
            {
                return null;
            }
        }

        private DateTimeOffset? GetSettingAsDateTimeOffset(string name)
        {
            var value = GetSetting(name);
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return DateTimeOffset.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }
        }

        private Nullable<T> GetSetting<T>(string name) where T : struct
        {
            var value = GetSetting(name);
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            }
        }

        private T GetSetting<T>(string name, T defaultValue)
        {
            var value = GetSetting(name);
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            }
        }

        private void SetSetting(string name, string value)
        {
            if (!this.changedSettingsKeys.Contains(name))
            {
                this.changedSettingsKeys.Add(name);
            }
            settings[name] = value;
        }

        private void SetSetting(string name, bool? value)
        {
            SetSetting(name, (value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : null));
        }

        private void SetSetting(string name, DateTimeOffset? value)
        {
            SetSetting(name, (value.HasValue ? value.Value.UtcDateTime.ToString("o", CultureInfo.InvariantCulture) : null));
        }

        private void SetSetting(string name, int? value)
        {
            SetSetting(name, (value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : null));
        }

        #endregion
    }
}