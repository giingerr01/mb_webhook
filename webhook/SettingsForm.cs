using System;
using System.Windows.Forms;

namespace MusicBeePlugin
{
    public partial class SettingsForm : Form
    {
        public SettingsForm(
            string discordWebhookUrl,
            string slackWebhookUrl,
            string genericWebhookUrl,
            bool discordEnabled,
            bool slackEnabled,
            bool genericEnabled)
        {
            InitializeComponent();
            discordUrlTextBox.Text = discordWebhookUrl;
            slackUrlTextBox.Text = slackWebhookUrl;
            genericUrlTextBox.Text = genericWebhookUrl;
            discordEnabledCheckBox.Checked = discordEnabled;
            slackEnabledCheckBox.Checked = slackEnabled;
            genericEnabledCheckBox.Checked = genericEnabled;
        }
        public string DiscordWebhookUrl => discordUrlTextBox.Text;
        public string SlackWebhookUrl => slackUrlTextBox.Text;
        public string GenericWebhookUrl => genericUrlTextBox.Text;
        public bool DiscordEnabled => discordEnabledCheckBox.Checked;
        public bool SlackEnabled => slackEnabledCheckBox.Checked;
        public bool GenericEnabled => genericEnabledCheckBox.Checked;

        private bool IsValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false; // Allow empty URL
            }

            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp
                ||
                uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private void discordUrlTextBox_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void label1_Click_1(object sender, System.EventArgs e)
        {

        }

        private void genericUrlLabel_Click(object sender, System.EventArgs e)
        {

        }

        private void SettingsForm_Load(object sender, System.EventArgs e)
        {

        }

        private void forumLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(
                "https://getmusicbee.com/forum/index.php?topic=44914.0"
                );
        }

        private void Save_Click(object sender, System.EventArgs e)
        {
            if (discordEnabledCheckBox.Checked
                &&
                !IsValidUrl(discordUrlTextBox.Text))
            {
                MessageBox.Show(
                    "Enter a valid Discord webhook URL.",
                    "Invalid URL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                discordUrlTextBox.Focus();
                return;
            }

            if (slackEnabledCheckBox.Checked
                &&
                !IsValidUrl(slackUrlTextBox.Text))
            {
                MessageBox.Show(
                    "Enter a valid Slack webhook URL.",
                    "Invalid URL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                slackUrlTextBox.Focus();
                return;
            }
            if (genericEnabledCheckBox.Checked
                &&
                !IsValidUrl(genericUrlTextBox.Text))
            {
                MessageBox.Show(
                    "Enter a valid URL.",
                    "Invalid URL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                genericUrlTextBox.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();


        }
    }
}
