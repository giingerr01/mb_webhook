using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MusicBeePlugin
{
    public class SettingsForm : Form
    {
        private TextBox _webUrlBox;

        public string _webUrl => _webUrlBox.Text.Trim();

        public SettingsForm(string _webUrl)
        {
            Text = "Enter your desired url";
            FormBorderStyle = FormBorderStyle.FixedDialog;

        }


    }
}