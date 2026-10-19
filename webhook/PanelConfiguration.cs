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
                GenericWebhookUrl))
            {
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    DiscordWebhookUrl = configForm.urlBox?.Text?.Trim() ?? string.Empty;
                    SlackWebhookUrl  = configForm.slackUrlBox?.Text?.Trim() ?? string.Empty;
                    GenericWebhookUrl = configForm.genericUrlBox?.Text?.Trim() ?? string.Empty;

                    SaveSettings();
                }
            }
            return true;
        }
    }
}
