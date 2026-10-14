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

            //logger for musicbee logtrace
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            string Webhook = Path.Combine(dataPath, "webhook");
            Directory.CreateDirectory(Webhook);
            string urlFile = Path.Combine(Webhook, "DiscordWebhookUrl.txt");
            Logger.FilePath = Path.Combine(Webhook, "webhook_plugin_log.log");

            if (File.Exists(urlFile))
            {
                DiscordWebhookUrl = File.ReadAllText(urlFile).Trim();
            }
            else
            {
                Logger.Log("Discord webhook URL file not found; using the default URL.");
            }

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
    }
}