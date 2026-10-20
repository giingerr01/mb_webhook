using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        public bool Configure(IntPtr panleHandle)
        {
            using (SettingsForm configForm = new SettingsForm(
                DiscordWebhookUrl,
                SlackWebhookUrl,
                GenericWebhookUrl,
                DiscordEnabled,
                SlackEnabled,
                GenericEnabled
                ))
            {
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    DiscordWebhookUrl = configForm.DiscordWebhookUrl.Trim();
                    SlackWebhookUrl = configForm.SlackWebhookUrl.Trim();
                    GenericWebhookUrl = configForm.GenericWebhookUrl.Trim();

                    DiscordEnabled = configForm.DiscordEnabled;
                    SlackEnabled = configForm.SlackEnabled;
                    GenericEnabled = configForm.GenericEnabled;

                    SaveSettings();
                }
            }
            return true;
        }
    }
}
