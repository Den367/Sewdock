using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Mayando.ServiceModel;

namespace Mayando.Client
{
    public class Program
    {
        #region Constants

        // Return value constants.
        private const int ReturnSucceeded = 0;
        private const int ReturnFailed = 1;
        private const int ReturnException = -1;

        // General constants.
        private const string ArgumentNameUrl = "url";
        private const string ArgumentNameKey = "key";
        private const string ArgumentNameAction = "action";

        // PhotoProviderSynchronize constants.
        private const string ArgumentValuePhotoProviderSynchronize = "PhotoProviderSynchronize";
        private const string ArgumentValuePhotoProviderSynchronizeShort = "pps";
        private const string ArgumentNameSyncStartTime = "syncStartTime";
        private const string ArgumentNameSyncStartTimeShort = "sst";
        private const string ArgumentNameTags = "tags";
        private const string ArgumentNameTagsShort = "t";

        #endregion

        #region Main

        public static int Main(string[] args)
        {
            try
            {
                var arguments = ParseArguments(args);
                if (!arguments.ContainsKey(ArgumentNameUrl) || !arguments.ContainsKey(ArgumentNameKey) || !arguments.ContainsKey(ArgumentNameAction))
                {
                    ShowUsage();
                    return ReturnSucceeded;
                }

                var url = arguments[ArgumentNameUrl];
                var key = arguments[ArgumentNameKey];
                var action = arguments[ArgumentNameAction].ToLowerInvariant();

                Console.WriteLine("Connecting to Mayando at URL: {0}", url);
                Console.WriteLine();

                var client = new MayandoClient(url, key);
                switch (action)
                {
                    case ArgumentValuePhotoProviderSynchronize:
                    case ArgumentValuePhotoProviderSynchronizeShort:
                        var succeeded = PhotoProviderSynchronize(client, arguments);
                        return (succeeded ? ReturnSucceeded : ReturnFailed);

                    default:
                        ShowUsage();
                        return ReturnSucceeded;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
                Console.WriteLine();
                return ReturnException;
            }
        }

        #endregion

        #region PhotoProviderSynchronize

        private static bool PhotoProviderSynchronize(MayandoClient client, IDictionary<string, string> arguments)
        {
            string syncStartTimeString = GetArgument(arguments, ArgumentNameSyncStartTime, ArgumentNameSyncStartTimeShort);
            string tagList = GetArgument(arguments, ArgumentNameTags, ArgumentNameTagsShort);

            DateTimeOffset? syncStartTime = null;
            DateTimeOffset syncStartTimeParsed;
            if (!string.IsNullOrEmpty(syncStartTimeString))
            {
                if (DateTimeOffset.TryParse(syncStartTimeString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out syncStartTimeParsed))
                {
                    syncStartTime = syncStartTimeParsed;
                }
                else
                {
                    Console.WriteLine("The sync start time could not be determined from the input \"{0}\".", syncStartTimeString);
                    Console.WriteLine("Make sure it is in in ISO8601 format: {0}", SerializationProvider.Iso8601FormatString);
                    Console.WriteLine();
                    return false;
                }
            }

            Console.WriteLine("Starting a synchronization with the photo provider...");
            if (syncStartTime.HasValue)
            {
                Console.WriteLine("  Sync Start Time : {0}", syncStartTime.Value.ToString());
            }
            if (!string.IsNullOrEmpty(tagList))
            {
                Console.WriteLine("  Tags            : {0}", tagList);
            }
            Console.WriteLine();

            var result = client.PhotoProviderSynchronize(syncStartTime, tagList);
            Console.WriteLine("Synchronization {0}.", result.Succeeded ? "succeeded" : "failed");
            var lastSyncTime = "Not synchronized yet";
            if (result.LastSyncTimeUtc.HasValue)
            {
                lastSyncTime = new DateTimeOffset(result.LastSyncTimeUtc.Value, TimeSpan.Zero).ToString();
            }
            Console.WriteLine("  Last Sync Time  : {0}", lastSyncTime);
            Console.WriteLine("  New Photos      : {0}", result.LastSyncNewPhotos ?? 0);
            Console.WriteLine("  New Comments    : {0}", result.LastSyncNewComments ?? 0);
            Console.WriteLine("  Status (HTML)   : {0}", result.LastSyncStatusHtml);
            Console.WriteLine();

            return result.Succeeded;
        }

        #endregion

        #region Helper Methods

        private static IDictionary<string, string> ParseArguments(string[] args)
        {
            var arguments = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                // Parse arguments in the form "/name:value" or "-name:value".
                if (arg != null && arg.StartsWith("-") || arg.StartsWith("/") & arg.IndexOf(":") >= 2)
                {
                    var separatorIndex = arg.IndexOf(":");
                    var argumentName = arg.Substring(1, separatorIndex - 1);
                    var argumentValue = arg.Substring(separatorIndex + 1);
                    arguments.Add(argumentName.ToLowerInvariant(), argumentValue);
                }
            }
            return arguments;
        }

        private static string GetArgument(IDictionary<string, string> arguments, params string[] argumentNames)
        {
            foreach (var argumentName in argumentNames)
            {
                if (arguments.ContainsKey(argumentName.ToLowerInvariant()))
                {
                    return arguments[argumentName.ToLowerInvariant()];
                }
            }
            return null;
        }

        #endregion

        #region ShowUsage

        private static void ShowUsage()
        {
            var exeName = Assembly.GetExecutingAssembly().GetName().Name;
            var url = "http://mayando.example.org/";
            var apiKey = "14c74598-3b1d-42ce-a2e5-f4b4ae2261e8";
            var syncStartTime = new DateTimeOffset(2010, 6, 27, 19, 0, 0, TimeSpan.Zero);

            Console.WriteLine("Mayando Command-Line Client.");
            Console.WriteLine();
            Console.WriteLine("USAGE: {0} /{1}:url /{2}:apikey /{3}:action [options]", exeName, ArgumentNameUrl, ArgumentNameKey, ArgumentNameAction);
            Console.WriteLine();
            Console.WriteLine("  url     : The root URL of the Mayando website to connect to.");
            Console.WriteLine("  apikey  : The Service API Key as found on the Mayando website.");
            Console.WriteLine("  action  : One of the available actions to be performed.");
            Console.WriteLine("  options : Additional options depending on the selected action.");
            Console.WriteLine();

            Console.WriteLine("AVAILABLE ACTIONS (with abbreviation):");
            Console.WriteLine();
            Console.WriteLine("{0} ({1})", ArgumentValuePhotoProviderSynchronize, ArgumentValuePhotoProviderSynchronizeShort);
            Console.WriteLine("  Starts a synchronization with the photo provider.");
            Console.WriteLine("  Options:");
            Console.WriteLine();
            Console.WriteLine("  [/{0}:value] ({1}) : The UTC start time from which to include", ArgumentNameSyncStartTime, ArgumentNameSyncStartTimeShort);
            Console.WriteLine("                                 changes, in ISO8601 format:");
            Console.WriteLine("                                 {0}", SerializationProvider.Iso8601FormatString);
            Console.WriteLine("                                 If omitted, uses the last sync time.");
            Console.WriteLine("  [/{0}:\"value1,value2\"] ({1})  : The tags that the photos must all have to be", ArgumentNameTags, ArgumentNameTagsShort);
            Console.WriteLine("                                 synchronized, comma separated.");
            Console.WriteLine("                                 If omitted, all tags are included.");
            Console.WriteLine();

            Console.WriteLine("EXAMPLES:");
            Console.WriteLine();
            Console.WriteLine("{0} /{1}:{2} /{3}:{4} /{5}:{6}", exeName, ArgumentNameUrl, url, ArgumentNameKey, apiKey, ArgumentNameAction, ArgumentValuePhotoProviderSynchronizeShort);
            Console.WriteLine();
            Console.WriteLine("  Starts a synchronization with the photo provider, including all changes");
            Console.WriteLine("  since the last synchronization and all tags.");
            Console.WriteLine();
            Console.WriteLine("{0} /{1}:{2} /{3}:{4} /{5}:{6} /{7}:{8} /{9}:\"people,portraits\"", exeName, ArgumentNameUrl, url, ArgumentNameKey, apiKey, ArgumentNameAction, ArgumentValuePhotoProviderSynchronizeShort, ArgumentNameSyncStartTimeShort, SerializationProvider.FormatDateIso8601(syncStartTime), ArgumentNameTagsShort);
            Console.WriteLine();
            Console.WriteLine("  Starts a synchronization with the photo provider, including all changes since");
            Console.WriteLine("  {0} (UTC), and the tags \"people\" and \"portraits\".", syncStartTime.ToString());
            Console.WriteLine();

            Console.WriteLine("RETURN VALUES:");
            Console.WriteLine("  {0,2} : The action succeeded.", ReturnSucceeded);
            Console.WriteLine("  {0,2} : The action failed.", ReturnFailed);
            Console.WriteLine("  {0,2} : An exception occurred and the action could not be completed.", ReturnException);
            Console.WriteLine();
        }

        #endregion
    }
}