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
            string Webhook = Path.Combine(dataPath, "webhook");
            Directory.CreateDirectory(Webhook);
            string urlFile = Path.Combine(Webhook, "DiscordWebhookUrl.txt");
            if (File.Exists(urlFile))
            {
                DiscordWebhookUrl = File.ReadAllText(urlFile).Trim();
            }
            else
            {
                Logger.Log("Discord webhook URL file not found; using the default URL.");
            }

            //slackwebhok url  file saving method and paths
            string slackUrlFile = Path.Combine(Webhook, "SlackWebhookUrl.txt");
            if (File.Exists(slackUrlFile))
            {
                SlackWebhookUrl = File.ReadAllText(slackUrlFile).Trim();
            }
            else
            {
                Logger.Log("Slack Webhook URL file not found; using the default Url.");
            }

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
            about.TargetApplication = "MusicBee";   //  the name of a Plugin Storage device or panel header for a dockable panel
            about.Type = PluginType.General;
            about.VersionMajor = 1;  // your plugin version
            about.VersionMinor = 0;
            about.Revision = 0;
            about.MinInterfaceVersion = MinInterfaceVersion;
            about.MinApiRevision = MinApiRevision;
            about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents | ReceiveNotificationFlags.TagEvents);
            about.ConfigurationPanelHeight = 60;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function
            return about;
        }

        // called by MusicBee when the user clicks Apply or Save in the MusicBee Preferences screen.
        // its up to you to figure out whether anything has changed and needs updating
        public void SaveSettings()
        {
            if (urlBox != null)
            {
                DiscordWebhookUrl = urlBox.Text.Trim();
            }

            if (slackUrlBox != null)
            {
                SlackWebhookUrl = slackUrlBox.Text.Trim();
            }
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            string Webhook = Path.Combine(dataPath, "webhook");
            Directory.CreateDirectory(Webhook);


            string urlFile = Path.Combine(Webhook, "DiscordWebhookUrl.txt");
            File.WriteAllText(urlFile, DiscordWebhookUrl);


            string slackUrlFile = Path.Combine(Webhook, "SlackWebhookUrl.txt");
            File.WriteAllText(slackUrlFile, SlackWebhookUrl);
        }
    }

}