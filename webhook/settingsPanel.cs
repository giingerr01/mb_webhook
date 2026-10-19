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
        private TextBox textBox1;
        private TextBox textBox2;
        private CheckBox checkBox2;
        private Button button1;
        private CheckBox checkBox1;

        public TextBox urlBox { get; private set; }
        public TextBox slackUrlBox { get; private set; }
        public TextBox genericUrlBox { get; private set; }


        public SettingsForm(string DiscordWebhookUrl, string SlackWebhookUrl, string GenericWebhookUrl)
        {
            this.Text = "webhook settings";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Height = 200;
            this.Width = 320;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            System.Windows.Forms.Label prompt = new System.Windows.Forms.Label();
            prompt.AutoSize = true;
            prompt.Location = new Point(15, 20);
            prompt.Text = "Discord URL";

            urlBox = new TextBox();
            urlBox.Bounds = new Rectangle(95, 15, 190, 10);
            urlBox.Text = DiscordWebhookUrl;

            System.Windows.Forms.Label slackprompt = new System.Windows.Forms.Label();
            slackprompt.AutoSize = true;
            slackprompt.Location = new Point(15, 60);
            slackprompt.Text = "Slack URL";

            slackUrlBox = new TextBox();
            slackUrlBox.Bounds = new Rectangle(95, 55, 190, 10);
            slackUrlBox.Text = SlackWebhookUrl;

            System.Windows.Forms.Label genericprompt = new System.Windows.Forms.Label();
            genericprompt.AutoSize = true;
            genericprompt.Location = new Point(15, 100);
            genericprompt.Text = "Custom URL";

            genericUrlBox = new TextBox();
            genericUrlBox.Bounds = new Rectangle(95, 95, 190, 10);
            genericUrlBox.Text = GenericWebhookUrl;

            Button saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.DialogResult = DialogResult.OK;
            saveButton.Location = new Point(205, 120);
            saveButton.Width = 80;


            this.Controls.AddRange(new Control[] { prompt, urlBox, slackprompt, slackUrlBox, genericprompt, genericUrlBox, saveButton });
            this.AcceptButton = saveButton;


        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(137, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(382, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Info;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(137, 77);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(382, 20);
            this.textBox2.TabIndex = 2;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(41, 35);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Discord URL:";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(41, 80);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(81, 17);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "Slack URL:";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(408, 117);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(531, 259);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
