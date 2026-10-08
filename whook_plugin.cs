using System;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        private MusicBeeApiInterface mbApiInterface;
        private PluginInfo about = new PluginInfo();
        private static readonly HttpClient httpClient = new HttpClient();

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);
            about.PluginInfoVersion = PluginInfoVersion;
            about.Name = "Now Playing Webhook.";
            about.Description = "Sends a post request to a webhook on current track change.";
            about.Author = "giingerr01";
            about.TargetApplication = "MusicBee";   //  the name of a Plugin Storage device or panel header for a dockable panel
            about.Type = PluginType.General;
            about.VersionMajor = 1;  // your plugin version
            about.VersionMinor = 0;
            about.Revision = 1;
            about.MinInterfaceVersion = MinInterfaceVersion;
            about.MinApiRevision = MinApiRevision;
            about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents | ReceiveNotificationFlags.TagEvents);
            about.ConfigurationPanelHeight = 0;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function
            return about;
        }

        public void ReciveNotifiction(string sourceFileUrl, NotificationType type)
        {
            if (type == NotificationType.TrackChanged)
            {
                SendWebhookPayload();
            }
        }

    }
}