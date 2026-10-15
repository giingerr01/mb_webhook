using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        // Dictionary to store service names and their corresponding URLs
        public Dictionary<string, string> WebhookUrls { get; set; } = new Dictionary<string, string>();

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
                    WebhookUrls = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent) ?? new Dictionary<string, string>();
                    Logger.Log("Webhook settings loaded successfully from JSON.");
                }
                catch (Exception ex)
                {
                    Logger.Log("Failed to parse webhooks.json: " + ex.Message);
                    WebhookUrls = new Dictionary<string, string>();
                }
            }
            else
            {
                Logger.Log("webhooks.json not found; initializing with empty settings.");
                // Fallback: migrate old text files if they exist
            }

            // Assign individual variables or access directly from the dictionary as needed
            DiscordWebhookUrl = WebhookUrls.ContainsKey("discord") ? WebhookUrls["discord"] : string.Empty;
            SlackWebhookUrl = WebhookUrls.ContainsKey("slack") ? WebhookUrls["slack"] : string.Empty;

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
            about.VersionMajor = 1;
            about.VersionMinor = 0;
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
            WebhookUrls["discord"] = DiscordWebhookUrl;
            WebhookUrls["slack"] = SlackWebhookUrl;

            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            string webhookFolder = Path.Combine(dataPath, "webhook");
            Directory.CreateDirectory(webhookFolder);

            string jsonFilePath = Path.Combine(webhookFolder, "webhooks.json");
            string jsonContent = JsonConvert.SerializeObject(WebhookUrls, Formatting.Indented);

            File.WriteAllText(jsonFilePath, jsonContent);
            Logger.Log("Webhook settings saved to webhooks.json.");
        }
    }
}