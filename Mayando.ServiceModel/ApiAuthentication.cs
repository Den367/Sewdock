using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Mayando.ServiceModel
{
    /// <summary>
    /// Represents the Service API authentication parameters and allows them to be calculated and validated.
    /// </summary>
    public class ApiAuthentication
    {
        #region Properties

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        public string Hash { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiAuthentication"/> class.
        /// </summary>
        public ApiAuthentication()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiAuthentication"/> class and calculates the <see cref="Hash"/> value.
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="apiKey">The Service API Key to calculate the hash with.</param>
        public ApiAuthentication(DateTimeOffset timestamp, string apiKey)
        {
            this.Timestamp = timestamp;
            this.Hash = GetAuthenticationHash(apiKey);
        }

        #endregion

        #region Validation

        /// <summary>
        /// Determines whether the API authentication is valid.
        /// </summary>
        /// <param name="apiKey">The Service API key.</param>
        /// <returns><see langword="true"/> if the API authentication parameters are valid, <see langword="false"/> otherwise.</returns>
        public bool IsValid(string apiKey)
        {
            if (!string.IsNullOrEmpty(apiKey))
            {
                if (!string.IsNullOrEmpty(this.Hash))
                {
                    // The timestamp may not be more than 10 minutes apart from the server time (to avoid replay attacks).
                    if (Math.Abs((DateTimeOffset.Now - this.Timestamp).TotalMinutes) < 10)
                    {
                        // Verify the current hash with the validation hash.
                        var validationHash = GetAuthenticationHash(apiKey);
                        if (string.Equals(this.Hash, validationHash, StringComparison.Ordinal))
                        {
                            // The hashes match, authentication succeeded.
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the authentication hash.
        /// </summary>
        /// <param name="apiKey">The Service API key.</param>
        /// <returns>The cryptographic hash of the timestamp and the key.</returns>
        private string GetAuthenticationHash(string apiKey)
        {
            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(apiKey));
            using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write))
            {
                string stringData = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", SerializationProvider.FormatDateIso8601(this.Timestamp), apiKey);
                byte[] rawData = Encoding.UTF8.GetBytes(stringData);
                cryptoStream.Write(rawData, 0, rawData.Length);
                cryptoStream.FlushFinalBlock();
            }
            return Convert.ToBase64String(hmac.Hash);
        }

        #endregion
    }
}