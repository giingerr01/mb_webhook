using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Diagnostics;
using System.Security;
using System.Reflection.Emit;
using Microsoft.VisualBasic;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);

            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();

            // a sperate folder names webhook to store log traces and users url entered by them
            string webhookFolder = Path.Combine(dataPath, "webhook");
            Directory.CreateDirectory(webhookFolder);
            string jsonFilePath = Path.Combine(webhookFolder, "DiscordWebhookUrl.txt");
            if (File.Exists(jsonFilePath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    WebhookUrls = JsonConvert
                    .DeserializeObject<Dictionary
                    <string, string>>
                    (jsonContent)
                    ?? new Dictionary<string, string>();
                    Logger.Log("Webhook settings loaded successfully fro JSON.");
                }
                catch (Exception ex)
                {
                    Logger.Log("failed to parse webhooks.json: " + ex.Message);
                    WebhookUrls = new Dictionary<string, string>();
                }
            }
            else
            {
                Logger.Log("Webhook.json not found; intitalizign with empty settings");
                MigratedOldTextFiles(webhookFolder);
            }

            DiscordWebhookUrl = WebhookUrls.ContainsKey("discord") ?
            WebhookUrls["discord"] : string.Empty;
            SlackWebhookUrl = WebhookUrls.ContainsKey("slack") ?
            WebhookUrls["slack"] : string.Empty;

            //logger for musicbee logtrace
            Logger.FilePath = Path.Combine(Webhook, "webhook_plugin_log.log");
            Logger.Log("Plugin intialised");

            // our client declaration
            webhookClient = new WebhookClient();

            //webhook plgin metadata
            about.PluginInfoVersion = PluginInfoVersion;
            about.Name = "Webhook Plugin";
            about.Description = "A plugin that post track now playing status";
            about.Author = "giingerr01";
            about.TargetApplication = "MusicBee";
            about.Type = PluginType.General;
            about.VersionMajor = 1;  // your plugin version
            about.VersionMinor = 0;
            about.Revision = 0;
            about.MinInterfaceVersion = MinInterfaceVersion;
            about.MinApiRevision = MinApiRevision;
            about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents | ReceiveNotificationFlags.TagEvents);
            about.ConfigurationPanelHeight = 60;
            return about;
        }

        public void SaveSettings()
        {
            if (urlBox != null)
            {
                DiscordWebhookUrl = urlBox.Text.Trim();
            }

            // save url
            if (slackUrlBox != null)
            {
                SlackWebhookUrl = slackUrlBox.Text.Trim();
            }
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            string Webhook = Path.Combine(dataPath, "webhook");
            Directory.CreateDirectory(Webhook);

            //url saving path for discordwebhook url from users input
            string urlFile = Path.Combine(Webhook, "DiscordWebhookUrl.txt");
            File.WriteAllText(urlFile, DiscordWebhookUrl);


            // saving url path for slackwebhokk url for from user input
            string slackUrlFile = Path.Combine(Webhook, "SlackWebhookUrl.txt");
            File.WriteAllText(slackUrlFile, SlackWebhookUrl);
        }
    }

}