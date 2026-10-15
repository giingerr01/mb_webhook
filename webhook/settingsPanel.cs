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


        public SettingsForm()
        {
            Text = "webhook settings";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(420, 300);

            var Discord = new System.Windows.Forms.Label
            {
                Left = 12,
                Top = 12,
                Width = 396,
                Height = 78,
                Text = "Discord"
            };

        }
    }

}