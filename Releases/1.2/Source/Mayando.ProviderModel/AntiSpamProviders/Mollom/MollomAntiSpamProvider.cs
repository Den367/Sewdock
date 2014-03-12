using System.Collections.Generic;
using System.Globalization;
using System.Text;
using JelleDruyts.Mollom.Client;

namespace Mayando.ProviderModel.AntiSpamProviders.Mollom
{
    /// <summary>
    /// Checks content for spam with Mollom.
    /// </summary>
    [AntiSpamProvider("Mollom", "Checks comments for spam with Mollom.", "http://mollom.com")]
    public class MollomAntiSpamProvider : IAntiSpamProvider
    {
        #region Constants & Fields

        private const string SettingNamePrivateKey = "PrivateKey";
        private const string SettingNamePublicKey = "PublicKey";
        private readonly static string UserAgent = string.Format(CultureInfo.InvariantCulture, "Mayando/{0}", typeof(MollomAntiSpamProvider).Assembly.GetName().Version.ToString(2));

        private IAntiSpamProviderHost Host { get; set; }

        #endregion

        #region GetSettingDefinitions

        /// <summary>
        /// Gets the settings that are supported by this provider.
        /// </summary>
        /// <returns>A list of setting definitions supported by this provider.</returns>
        public SettingDefinition[] GetSettingDefinitions()
        {
            int sequence = 0;
            return new SettingDefinition[]
            {
                new SettingDefinition(SettingNamePublicKey, SettingType.Plaintext, "Public Key", "The public authentication key associated with your Mollom account.", sequence++, true),
                new SettingDefinition(SettingNamePrivateKey, SettingType.Plaintext, "Private Key", "The private authentication key associated with your Mollom account.", sequence++, true)
            };
        }

        #endregion

        #region Initialize

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="host">The host managing this provider.</param>
        public void Initialize(IAntiSpamProviderHost host)
        {
            this.Host = host;
        }

        #endregion

        #region GetStatus

        /// <summary>
        /// Gets the current status of the provider.
        /// </summary>
        /// <returns>A status description of the provider.</returns>
        public AntiSpamProviderStatus GetStatus()
        {
            var mollom = CreateMollomClient();
            if (mollom == null)
            {
                return new AntiSpamProviderStatus("<p>Please apply for an account at <a href=\"http://mollom.com\" target=\"_blank\">Mollom</a> and enter your public and private keys in the provider settings.</p>", true);
            }
            else
            {
                var status = new StringBuilder();
                var keyValid = mollom.VerifyKey();
                status.AppendFormat(CultureInfo.CurrentCulture, "<p>Your key is {0}valid.</p>", (keyValid ? string.Empty : "<b>NOT</b> "));
                var totalDays = mollom.GetStatistics(StatisticsTypes.TotalDays);
                var totalAccepted = mollom.GetStatistics(StatisticsTypes.TotalAccepted);
                var totalRejected = mollom.GetStatistics(StatisticsTypes.TotalRejected);
                status.AppendFormat(CultureInfo.CurrentCulture, "<p>In a total of {0} days, Mollom accepted {1} and rejected {2} comments.</p>", totalDays, totalAccepted, totalRejected);
                return new AntiSpamProviderStatus(status.ToString(), true);
            }
        }

        #endregion

        #region CheckForSpam

        /// <summary>
        /// Checks a certain request for spam.
        /// </summary>
        /// <param name="request">The request that contains the details about the content to be checked for spam.</param>
        /// <returns>The results of the check for spam.</returns>
        public SpamCheckResult CheckForSpam(SpamCheckRequest request)
        {
            var mollom = CreateMollomClient();
            if (mollom == null)
            {
                // If the settings aren't configured yet, indicate that we haven't checked the content.
                return new SpamCheckResult(Classification.Unchecked);
            }
            else
            {
                // Call Mollom with the details of the content to be checked.
                var result = mollom.CheckContent(null, request.Text, request.AuthorName, request.AuthorEmail, request.AuthorUrl, request.AuthorIPAddress);

                // Map Mollom's classification onto the internal classification.
                switch (result.Classification)
                {
                    case ContentClassification.Spam:
                        return new SpamCheckResult(Classification.Spam);
                    case ContentClassification.Unsure:
                        return new SpamCheckResult(Classification.Unsure);
                    default:
                        return new SpamCheckResult(Classification.NotSpam);
                }
            }
        }

        #endregion

        #region Reset

        /// <summary>
        /// Resets the provider to its initial state. This typically involves clearing its settings.
        /// </summary>
        public void Reset()
        {
            // Clear all settings.
            var settings = this.Host.GetSettings();
            settings[SettingNamePrivateKey] = null;
            settings[SettingNamePublicKey] = null;
            this.Host.SaveSettings(settings);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates the Mollom client.
        /// </summary>
        /// <returns>A configured Mollom client, or <see langword="null"/> if the settings aren't properly configured.</returns>
        private MollomClient CreateMollomClient()
        {
            var settings = this.Host.GetSettings();
            var privateKey = GetSetting(settings, SettingNamePrivateKey);
            var publicKey = GetSetting(settings, SettingNamePublicKey);
            if (string.IsNullOrEmpty(privateKey) && string.IsNullOrEmpty(publicKey))
            {
                return null;
            }
            else
            {
                return new MollomClient(privateKey, publicKey, UserAgent);
            }
        }

        /// <summary>
        /// Gets a certain setting from the dictionary, or <see langword="null"/> if the setting wasn't present.
        /// </summary>
        /// <param name="settings">The settings dictionary.</param>
        /// <param name="name">The name of the setting to retrieve.</param>
        /// <returns>The setting's value in the dictionary, or <see langword="null"/> if the setting wasn't present.</returns>
        private static string GetSetting(IDictionary<string, string> settings, string name)
        {
            if (!settings.ContainsKey(name))
            {
                return null;
            }
            return settings[name];
        }

        #endregion
    }
}