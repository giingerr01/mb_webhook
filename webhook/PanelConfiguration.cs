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
            if (panelHandle != IntPtr.Zero)
            {
                Panel configPanel = (Panel)Panel.FromHandle(panelHandle);

                System.Windows.Forms.Label prompt = new System.Windows.Forms.Label();
                prompt.AutoSize = true;
                prompt.Location = new Point(0, 2);
                prompt.Text = "Discord URL";

                urlBox = new TextBox();
                urlBox.Bounds = new Rectangle(80, 0, 100, urlBox.Height);
                urlBox.Text = DiscordWebhookUrl;

                configPanel.Controls.AddRange(new Control[] { prompt, urlBox });

                System.Windows.Forms.Label slackprompt = new System.Windows.Forms.Label();
                slackprompt.AutoSize = true;
                slackprompt.Location = new Point(0, 25);
                slackprompt.Text = "Slack URL:";

                slackUrlBox = new TextBox();
                slackUrlBox.Bounds = new Rectangle(80, 0, 200, slackUrlBox.Height);
                slackUrlBox.Text = SlackWebhookUrl;

                configPanel.Controls.AddRange(new Control[] { slackprompt, slackUrlBox });
            }
            return false;
        }
    }
}