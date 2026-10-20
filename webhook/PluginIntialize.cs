using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        // Dictionary to store service names and their given  Urls

        public WebhookSettings Settings { get; set; }
        = new WebhookSettings();

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);

            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            string webhookFolder = Path.Combine(dataPath, "webhook");
            Directory.CreateDirectory(webhookFolder);

            // Path for the single JSON configuration file
            string jsonFilePath = Path.Combine(webhookFolder, "webhooks.json");

            if (File.Exists(jsonFilePath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    JObject settingsJson = JObject.Parse(jsonContent);
                    if (settingsJson.Property("DiscordUrl") != null
                        || settingsJson.Property("SlackUrl") != null
                        || settingsJson.Property("GenericUrl") != null)
                    {
                        Settings = settingsJson.ToObject<WebhookSettings>()
                            ?? new WebhookSettings();
                    }
                    else
                    {
                        Dictionary<string, string> legacyUrls =
                            settingsJson.ToObject<Dictionary<string, string>>();
                        Settings = new WebhookSettings
                        {
                            DiscordUrl = GetLegacyUrl(legacyUrls, "discord"),
                            SlackUrl = GetLegacyUrl(legacyUrls, "slack"),
                            GenericUrl = GetLegacyUrl(legacyUrls, "Generic")
                        };
                    }
                    Logger.Log("Webhook settings loaded successfully from JSON.");
                }
                catch (Exception ex)
                {
                    Logger.Log("Failed to parse webhooks.json: " + ex.Message);
                    Settings = new WebhookSettings();
                }
            }
            else
            {
                Logger.Log("webhooks.json not found; initializing with empty settings.");
                // Fallback: migrate old text files if they exist
            }

            // Assign individual variables or access directly from the dictionary as needed
            DiscordWebhookUrl = Settings.DiscordUrl;
            SlackWebhookUrl = Settings.SlackUrl;
            GenericWebhookUrl = Settings.GenericUrl;

            DiscordEnabled = Settings.DiscordEnabled;
            SlackEnabled = Settings.SlackEnabled;
            GenericEnabled = Settings.GenericEnabled;


            // Logger for musicbee logtrace
            Logger.FilePath = Path.Combine(webhookFolder, "webhook_plugin_log.log");
            Logger.Log("Plugin initialised");

            // Our client declaration
            webhookClient = new WebhookClient();

            // Webhook plugin metadata
            about.PluginInfoVersion = PluginInfoVersion;
            about.Name = "Webhook Plugin";
            about.Description = "A plugin that posts track now playing status";
            about.Author = "giingerr01";
            about.TargetApplication = "MusicBee";
            about.Type = PluginType.General;
            about.VersionMajor = 2;
            about.VersionMinor = 1;
            about.Revision = 0;
            about.MinInterfaceVersion = MinInterfaceVersion;
            about.MinApiRevision = MinApiRevision;
            about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents | ReceiveNotificationFlags.TagEvents);
            about.ConfigurationPanelHeight = 0;
            return about;
        }

        // Called by MusicBee when the user clicks Apply or Save.
        public void SaveSettings()
        {
            Settings.DiscordUrl = DiscordWebhookUrl;
            Settings.SlackUrl = SlackWebhookUrl;
            Settings.GenericUrl = GenericWebhookUrl;
            Settings.DiscordEnabled = DiscordEnabled;
            Settings.SlackEnabled = SlackEnabled;
            Settings.GenericEnabled = GenericEnabled;

            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            string webhookFolder = Path.Combine(dataPath, "webhook");
            Directory.CreateDirectory(webhookFolder);

            string jsonFilePath = Path.Combine(webhookFolder, "webhooks.json");
            string jsonContent = JsonConvert.SerializeObject(Settings, Formatting.Indented);

            File.WriteAllText(jsonFilePath, jsonContent);
            Logger.Log("Webhook settings saved to webhooks.json.");
        }

        private static string GetLegacyUrl(
            Dictionary<string, string> legacyUrls,
            string key)
        {
            return legacyUrls != null
                && legacyUrls.TryGetValue(key, out string url)
                ? url
                : string.Empty;
        }
    }
}