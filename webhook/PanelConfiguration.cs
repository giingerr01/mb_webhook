using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

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