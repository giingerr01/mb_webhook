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
        public bool Configure(IntPtr panelHandle)
        {
            using (SettingsForm configForm = new SettingsForm(DiscordWebhookUrl, SlackWebhookUrl))
            {
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    DiscordWebhookUrl = configForm.urlBox.Text.Trim();
                    SlackWebhookUrl = configForm.slackUrlBox.Text.Trim();

                    SaveSettings();
                }
            }
            return false;
        }
    }
}