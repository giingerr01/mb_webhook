using System;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipes;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace MusicBeePlugin
{
    public class SettingsForm : Form
    {
        public TextBox urlBox { get; private set; }
        public TextBox slackUrlBox { get; private set; }
        public TextBox genericUrlBox { get; private set; }


        public SettingsForm(string DiscordWebhookUrl, string SlackWebhookUrl, string GenericWebhookUrl)
        {
            this.Text = "webhook settings";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Height = 320;
            this.Width = 320;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            System.Windows.Forms.Label prompt = new System.Windows.Forms.Label();
            prompt.AutoSize = true;
            prompt.Location = new Point(15, 20);
            prompt.Text = "Discord URL";

            urlBox = new TextBox();
            urlBox.Bounds = new Rectangle(95, 18, 190, 10);
            urlBox.Text = DiscordWebhookUrl;

            System.Windows.Forms.Label slackprompt = new System.Windows.Forms.Label();
            slackprompt.AutoSize = true;
            slackprompt.Location = new Point(15, 55);
            slackprompt.Text = "Slack URL";

            slackUrlBox = new TextBox();
            slackUrlBox.Bounds = new Rectangle(95, 53, 190, 10);
            slackUrlBox.Text = SlackWebhookUrl;

            System.Windows.Forms.Label genericprompt = new System.Windows.Forms.Label();
            genericprompt.AutoSize = true;
            genericprompt.Location = new Point(15, 90);
            genericprompt.Text = "Custom URL";

            genericUrlBox = new TextBox();
            genericprompt.Bounds = new Rectangle(95, 70, 190, 10);
            genericprompt.Text = GenericWebhookUrl;

            Button saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.DialogResult = DialogResult.OK;
            saveButton.Location = new Point(405, 90);
            saveButton.Width = 80;


            this.Controls.AddRange(new Control[] { prompt, urlBox, slackprompt, slackUrlBox, genericprompt, genericUrlBox, saveButton });
            this.AcceptButton = saveButton;


        }
    }

}