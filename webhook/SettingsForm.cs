using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicBeePlugin
{
   public partial class SettingsForm : Form
    {
        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;
        private TextBox slackUrlTextBox;
        private TextBox genericUrlTextBox;
        private TextBox discordUrlTextBox;

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
        public TextBox genericUrlTextBox => genericUrlTextBox;


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.discordUrlTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.slackUrlTextBox = new System.Windows.Forms.TextBox();
            this.genericUrlTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // discordUrlTextBox
            // 
            this.discordUrlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.discordUrlTextBox.ForeColor = System.Drawing.Color.Black;
            this.discordUrlTextBox.Location = new System.Drawing.Point(119, 21);
            this.discordUrlTextBox.Name = "discordUrlTextBox";
            this.discordUrlTextBox.Size = new System.Drawing.Size(307, 20);
            this.discordUrlTextBox.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // slackUrlTextBox
            // 
            this.slackUrlTextBox.Location = new System.Drawing.Point(119, 64);
            this.slackUrlTextBox.Name = "slackUrlTextBox";
            this.slackUrlTextBox.Size = new System.Drawing.Size(307, 20);
            this.slackUrlTextBox.TabIndex = 2;
            // 
            // genericUrlTextBox
            // 
            this.genericUrlTextBox.Location = new System.Drawing.Point(119, 108);
            this.genericUrlTextBox.Name = "genericUrlTextBox";
            this.genericUrlTextBox.Size = new System.Drawing.Size(307, 20);
            this.genericUrlTextBox.TabIndex = 3;
            // 
            // SettingsForm
            // 
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(447, 166);
            this.Controls.Add(this.genericUrlTextBox);
            this.Controls.Add(this.slackUrlTextBox);
            this.Controls.Add(this.discordUrlTextBox);
            this.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.Name = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
    
}
