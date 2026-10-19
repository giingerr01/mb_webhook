using System;
using System.Windows.Forms;

namespace MusicBeePlugin
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox discordUrlTextBox;
        private System.Windows.Forms.TextBox slackUrlTextBox;
        private System.Windows.Forms.TextBox genericUrlTextBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.discordUrlTextBox = new System.Windows.Forms.TextBox();
            this.slackUrlTextBox = new System.Windows.Forms.TextBox();
            this.genericUrlTextBox = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.discordUrlLabel = new System.Windows.Forms.Label();
            this.slackUrlLabel = new System.Windows.Forms.Label();
            this.genericUrlLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // discordUrlTextBox
            // 
            this.discordUrlTextBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.discordUrlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.discordUrlTextBox.ForeColor = System.Drawing.Color.Black;
            this.discordUrlTextBox.Location = new System.Drawing.Point(100, 36);
            this.discordUrlTextBox.Name = "discordUrlTextBox";
            this.discordUrlTextBox.Size = new System.Drawing.Size(307, 20);
            this.discordUrlTextBox.TabIndex = 0;
            this.discordUrlTextBox.TextChanged += new System.EventHandler(this.discordUrlTextBox_TextChanged);
            // 
            // slackUrlTextBox
            // 
            this.slackUrlTextBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.slackUrlTextBox.Location = new System.Drawing.Point(100, 80);
            this.slackUrlTextBox.Name = "slackUrlTextBox";
            this.slackUrlTextBox.Size = new System.Drawing.Size(307, 20);
            this.slackUrlTextBox.TabIndex = 2;
            // 
            // genericUrlTextBox
            // 
            this.genericUrlTextBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.genericUrlTextBox.Location = new System.Drawing.Point(100, 118);
            this.genericUrlTextBox.Name = "genericUrlTextBox";
            this.genericUrlTextBox.Size = new System.Drawing.Size(307, 20);
            this.genericUrlTextBox.TabIndex = 3;
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.SystemColors.Menu;
            this.Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Save.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Save.Location = new System.Drawing.Point(336, 159);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(71, 23);
            this.Save.TabIndex = 5;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = false;
            // 
            // discordUrlLabel
            // 
            this.discordUrlLabel.AutoSize = true;
            this.discordUrlLabel.Location = new System.Drawing.Point(13, 38);
            this.discordUrlLabel.Name = "discordUrlLabel";
            this.discordUrlLabel.Size = new System.Drawing.Size(68, 13);
            this.discordUrlLabel.TabIndex = 7;
            this.discordUrlLabel.Text = "Discord URL";
            // 
            // slackUrlLabel
            // 
            this.slackUrlLabel.AutoSize = true;
            this.slackUrlLabel.Location = new System.Drawing.Point(14, 83);
            this.slackUrlLabel.Name = "slackUrlLabel";
            this.slackUrlLabel.Size = new System.Drawing.Size(59, 13);
            this.slackUrlLabel.TabIndex = 8;
            this.slackUrlLabel.Text = "Slack URL";
            // 
            // genericUrlLabel
            // 
            this.genericUrlLabel.AutoSize = true;
            this.genericUrlLabel.Location = new System.Drawing.Point(14, 121);
            this.genericUrlLabel.Name = "genericUrlLabel";
            this.genericUrlLabel.Size = new System.Drawing.Size(67, 13);
            this.genericUrlLabel.TabIndex = 11;
            this.genericUrlLabel.Text = "Custom URL";
            this.genericUrlLabel.Click += new System.EventHandler(this.genericUrlLabel_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.versionLabel.Location = new System.Drawing.Point(372, 9);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(40, 13);
            this.versionLabel.TabIndex = 12;
            this.versionLabel.Text = "v.2.1.0";
            // 
            // SettingsForm
            // 
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(419, 196);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.genericUrlLabel);
            this.Controls.Add(this.slackUrlLabel);
            this.Controls.Add(this.discordUrlLabel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.genericUrlTextBox);
            this.Controls.Add(this.slackUrlTextBox);
            this.Controls.Add(this.discordUrlTextBox);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Save;
        private Label discordUrlLabel;
        private Label slackUrlLabel;
        private Label genericUrlLabel;
        private Label versionLabel;
    }
}
