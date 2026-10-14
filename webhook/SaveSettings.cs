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

        // called by MusicBee when the user clicks Apply or Save in the MusicBee Preferences screen.
        // its up to you to figure out whether anything has changed and needs updating
        public void SaveSettings()
        {
            if (urlBox != null)
            {
                DiscordWebhookUrl = urlBox.Text.Trim();
            }
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            string Webhook = Path.Combine(dataPath, "webhook");
            Directory.CreateDirectory(Webhook);
            string urlFile = System.IO.Path.Combine(Webhook, "webhookUrl.txt");
            System.IO.File.WriteAllText(urlFile, DiscordWebhookUrl);
        }

    }
}