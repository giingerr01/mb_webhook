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

namespace MusicBeePlugin
{
    public partial class Plugin
    {

        private MusicBeeApiInterface mbApiInterface;
        private PluginInfo about = new PluginInfo();
        private string webhookUrl = "http://localhost:3000/webhook";
        private TextBox urlBox;
        private WebhookClient webhookClient;


        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);

            //logger for musicbee logtrace
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            string urlFile =
                System.IO.Path.Combine(dataPath, "webhookUrl.txt");
            if (System.IO.File.Exists(urlFile))
            {
                webhookUrl = System.IO.File.ReadAllText(urlFile).Trim();
            }

            Logger.FilePath = Path.Combine(dataPath, "webhook_plugin.log");
            Logger.Log("Plugin intialised");

            // our client declaration
            webhookClient = new WebhookClient();


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

        public bool Configure(IntPtr panelHandle)
        {
            if (panelHandle != IntPtr.Zero)
            {
                Panel configPanel = (Panel)Panel.FromHandle(panelHandle);

                System.Windows.Forms.Label prompt = new System.Windows.Forms.Label();
                prompt.AutoSize = true;
                prompt.Location = new Point(0, 4);
                prompt.Text = "Webhook URL";

                urlBox = new TextBox();
                urlBox.Bounds = new Rectangle(70, 0, 300, urlBox.Height);
                urlBox.Text = webhookUrl;

                configPanel.Controls.AddRange(new Control[] { prompt, urlBox });
            }
            return false;
        }

        // called by MusicBee when the user clicks Apply or Save in the MusicBee Preferences screen.
        // its up to you to figure out whether anything has changed and needs updating
        public void SaveSettings()
        {
            if (urlBox != null)
            {
                webhookUrl = urlBox.Text.Trim();
            }
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();

            string urlFile = System.IO.Path.Combine(dataPath, "webhookUrl.txt");

            System.IO.File.WriteAllText(urlFile, webhookUrl);
        }

        // MusicBee is closing the plugin (plugin is being disabled by user or MusicBee is shutting down)
        public void Close(PluginCloseReason reason)
        {
        }

        // uninstall this plugin - clean up any persisted files
        public void Uninstall()
        {
        }

        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // perform some action depending on the notification type
            switch (type)
            {
                case NotificationType.PluginStartup:
                    // perform startup initialisation
                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:
                            break;

                        case PlayState.Paused:
                            // ...
                            break;
                    }
                    break;


                // case NotificationType.PlayStateChanged:
                //     {
                //         PlayState state = mbApiInterface.Player_GetPlayState();

                //         string stateJson =
                //         "{"
                //         + "\"state\":\"" + state + "\""
                //         + "}";
                //         Logger.Log("State: " + stateJson);

                //         webhookClient.SendAsync(
                //             webhookUrl,
                //             stateJson
                //         ).GetAwaiter().GetResult();
                //     }
                //     break;

                case NotificationType.TrackChanged:

                    DiscordNotifier.SendMessage(webhookUrl);
                    break;
                    // string artist =
                    // mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Artist);

                    // string title =
                    // mbApiInterface.NowPlaying_GetFileTag(MetaDataType.TrackTitle);

                    // string album =
                    // mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Album);

                    // // string art = mbApiInterface.NowPlaying_GetArtwork();

                    // // string lyrics =
                    // // mbApiInterface.NowPlaying_GetLyrics();


                    // string json =
                    // "{"
                    // + "\"event\":\"track_changed\","
                    // + "\"artist\":\"" + artist + "\","
                    // + "\"title\":\"" + title + "\","
                    // + "\"album\":\"" + album + "\""
                    // + "}";

                    // webhookClient.SendAsync(
                    //     webhookUrl,
                    //     json
                    // ).GetAwaiter().GetResult();


                    // MessageBox.Show(
                    // json
                    // );
                    break;
            }
        }


        public List<ToolStripItem> GetHeaderMenuItems()
        {
            List<ToolStripItem> list = new List<ToolStripItem>();
            list.Add(new ToolStripMenuItem("A menu item"));
            return list;
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Red);
            TextRenderer.DrawText(e.Graphics, "hello", SystemFonts.CaptionFont, new Point(10, 10), Color.Blue);
        }
    }
}