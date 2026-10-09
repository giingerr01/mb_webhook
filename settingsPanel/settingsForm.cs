using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Windows.Forms;
using static MusicBeePlugin.Plugin;

namespace MusicBeePlugin
{
    public partial class SettingsForm : Form
    {
        private TextBox _WebUrlBox;

        public string _webUrl => _WebUrlBox.Text.Trim();

        public SettingsForm(string _WebUrl)
        {
            Text = "Webhook Settings";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(420, 90);
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            System.Windows.Forms.Label urlLabel = new System.Windows.Forms.Label();
            urlLabel.Text = "Webhook URL:";
            urlLabel.AutoSize = true;
            urlLabel.Location = new Point(12, 18);

            _WebUrlBox = new TextBox();
            _WebUrlBox.Location = new Point(105, 14);
            _WebUrlBox.Size = new Size(300, 23);
            _WebUrlBox.Anchor = AnchorStyles.Top
                              | AnchorStyles.Left
                              | AnchorStyles.Right;

            _WebUrlBox.Text = _webUrl;


            Controls.Add(urlLabel);
            Controls.Add(_WebUrlBox);
        }


    }
}