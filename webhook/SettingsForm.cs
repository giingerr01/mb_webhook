using System.Windows.Forms;

namespace MusicBeePlugin
{
    public partial class SettingsForm : Form
    {
        public SettingsForm(
            string discordWebhookUrl,
            string slackWebhookUrl,
            string genericWebhookUrl)
        {
            InitializeComponent();
            discordUrlTextBox.Text = discordWebhookUrl;
            slackUrlTextBox.Text = slackWebhookUrl;
            genericUrlTextBox.Text = genericWebhookUrl;
        }

        public TextBox urlBox => discordUrlTextBox;
        public TextBox slackUrlBox => slackUrlTextBox;
        public TextBox genericUrlBox => genericUrlTextBox;

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
    }
}
