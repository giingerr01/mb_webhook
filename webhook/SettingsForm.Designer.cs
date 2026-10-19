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
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

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
            this.components = new System.ComponentModel.Container();
            this.discordUrlTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.slackUrlTextBox = new System.Windows.Forms.TextBox();
            this.genericUrlTextBox = new System.Windows.Forms.TextBox();
            this.Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // discordUrlTextBox
            // 
            this.discordUrlTextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.discordUrlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.discordUrlTextBox.ForeColor = System.Drawing.Color.Black;
            this.discordUrlTextBox.Location = new System.Drawing.Point(119, 56);
            this.discordUrlTextBox.Name = "discordUrlTextBox";
            this.discordUrlTextBox.Size = new System.Drawing.Size(307, 20);
            this.discordUrlTextBox.TabIndex = 0;
            this.discordUrlTextBox.TextChanged += new System.EventHandler(this.discordUrlTextBox_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // slackUrlTextBox
            // 
            this.slackUrlTextBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.slackUrlTextBox.Location = new System.Drawing.Point(119, 110);
            this.slackUrlTextBox.Name = "slackUrlTextBox";
            this.slackUrlTextBox.Size = new System.Drawing.Size(307, 20);
            this.slackUrlTextBox.TabIndex = 2;
            // 
            // genericUrlTextBox
            // 
            this.genericUrlTextBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.genericUrlTextBox.Location = new System.Drawing.Point(119, 168);
            this.genericUrlTextBox.Name = "genericUrlTextBox";
            this.genericUrlTextBox.Size = new System.Drawing.Size(307, 20);
            this.genericUrlTextBox.TabIndex = 3;
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.SystemColors.Menu;
            this.Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Save.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Save.Location = new System.Drawing.Point(355, 194);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(71, 23);
            this.Save.TabIndex = 5;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = false;
            // 
            // SettingsForm
            // 
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(436, 229);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.genericUrlTextBox);
            this.Controls.Add(this.slackUrlTextBox);
            this.Controls.Add(this.discordUrlTextBox);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Save;
    }
}
