using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Web;

namespace Mayando.ServiceModel
{
    /// <summary>
    /// Provides access to the Mayando Service API.
    /// </summary>
    public sealed class MayandoClient
    {
        #region Properties

        /// <summary>
        /// Gets the Service API Key used for authentication.
        /// </summary>
        public string ApiKey { get; private set; }

        /// <summary>
        /// Gets the root URL of the Mayando web site to connect to.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Gets the URL of the Service API.
        /// </summary>
        public string ServiceApiUrl { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MayandoClient"/> class.
        /// </summary>
        /// <param name="url">The URL of the Mayando web site to connect to.</param>
        /// <param name="apiKey">The Service API Key used for authentication.</param>
        public MayandoClient(string url, string apiKey)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException("apiKey");
            }
            this.Url = url;
            this.ServiceApiUrl = this.Url;
            if (!this.ServiceApiUrl.EndsWith("/", StringComparison.Ordinal))
            {
                this.ServiceApiUrl += "/";
            }
            this.ServiceApiUrl += "Services/";
            this.ApiKey = apiKey;
        }

        #endregion

        #region PhotoProviderSynchronize

        /// <summary>
        /// Starts a synchronization with the photo provider.
        /// </summary>
        /// <param name="syncStartTime">The optional start time from which to include all changes.</param>
        /// <param name="tags">The optional tags that the photos must all have to be synchronized.</param>
        /// <returns>The result of the synchronization.</returns>
        public PhotoProviderSynchronizationResult PhotoProviderSynchronize(DateTimeOffset? syncStartTime, IList<string> tags)
        {
            string tagList = null;
            if (tags != null && tags.Count > 0)
            {
                tagList = string.Join(", ", tags.ToArray());
            }
            return PhotoProviderSynchronize(syncStartTime, tagList);
        }

        /// <summary>
        /// Starts a synchronization with the photo provider.
        /// </summary>
        /// <param name="syncStartTime">The optional start time from which to include all changes.</param>
        /// <param name="tagList">The optional tags that the photos must all have to be synchronized.</param>
        /// <returns>The result of the synchronization.</returns>
        public PhotoProviderSynchronizationResult PhotoProviderSynchronize(DateTimeOffset? syncStartTime, string tagList)
        {
            var parameters = GetAuthenticationParameters();
            if (syncStartTime.HasValue)
            {
                parameters.Add("syncStartTime", SerializationProvider.FormatDateIso8601(syncStartTime.Value));
            }
            if (!string.IsNullOrEmpty(tagList))
            {
                parameters.Add("tagList", tagList);
            }

            var result = PerformHttpPost(this.ServiceApiUrl + "PhotoProviderSynchronize", parameters);
            return SerializationProvider.Deserialize<PhotoProviderSynchronizationResult>(result);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the required authentication parameters.
        /// </summary>
        /// <returns>A dictionary containing the required authentication parameters.</returns>
        private IDictionary<string, string> GetAuthenticationParameters()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var auth = new ApiAuthentication(DateTimeOffset.Now, this.ApiKey);
            parameters.Add("timestamp", SerializationProvider.FormatDateIso8601(auth.Timestamp));
            parameters.Add("hash", auth.Hash);
            return parameters;
        }

        /// <summary>
        /// Performs an HTTP POST with a set of parameters.
        /// </summary>
        /// <param name="url">The url to post to.</param>
        /// <param name="parameters">The parameters dictionary to post.</param>
        /// <returns>The return message.</returns>
        private static string PerformHttpPost(string url, IDictionary<string, string> parameters)
        {
            HttpWebResponse response = null;
            return PerformHttpPost(url, parameters, out response);
        }

        /// <summary>
        /// Performs an HTTP POST with a set of parameters.
        /// </summary>
        /// <param name="url">The url to post to.</param>
        /// <param name="parameters">The parameters dictionary to post.</param>
        /// <param name="response">The response from the server.</param>
        /// <returns>The return message.</returns>
        private static string PerformHttpPost(string url, IDictionary<string, string> parameters, out HttpWebResponse response)
        {
            try
            {
                // Build the parameters string.
                StringBuilder parameterString = new StringBuilder();
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    if (parameterString.Length > 0)
                    {
                        parameterString.Append("&");
                    }
                    string key = HttpUtility.UrlEncode(parameter.Key);
                    string value = HttpUtility.UrlEncode(parameter.Value);
                    parameterString.Append(key).Append("=").Append(value);
                }

                // Create a web request.
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                // Set the proper headers to perform the post.
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";

                // Get the length of the parameters.
                byte[] bytes = Encoding.ASCII.GetBytes(parameterString.ToString());
                request.ContentLength = bytes.Length;

                // Write the parameter bytes.
                using (Stream outputStream = request.GetRequestStream())
                {
                    outputStream.Write(bytes, 0, bytes.Length);
                }

                // Get the response and read from the stream.
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd().Trim();
                }
            }
            catch (WebException exc)
            {
                var httpResponse = exc.Response as HttpWebResponse;
                if (httpResponse != null)
                {
                    if (httpResponse.StatusCode == HttpStatusCode.Forbidden)
                    {
                        throw new SecurityException("Service authentication failed or the Service API is not enabled.", exc);
                    }
                }
                throw;
            }
        }

        #endregion
    }
}