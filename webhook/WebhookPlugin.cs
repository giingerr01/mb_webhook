using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;

namespace MusicBeePlugin
{
    public partial class Plugin
    {

        private MusicBeeApiInterface mbApiInterface;
        private PluginInfo about = new PluginInfo();
        private string GenericWebhookUrl = "http://localhost:3000/webhook";
        private string DiscordWebhookUrl = "http://localhost:3000/webhook";
        private string SlackWebhookUrl = "http://localhost:4000/webhook";
        private bool DiscordEnabled;
        private bool SlackEnabled;
        private bool GenericEnabled;
        private WebhookClient webhookClient;


        // MusicBee is closing the plugin (plugin is being disabled by user or MusicBee is shutting down)
        public void Close(PluginCloseReason reason)
        {
        }

        // uninstall this plugin - clean up any persisted files
        public void Uninstall()
        {
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