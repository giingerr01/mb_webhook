using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Diagnostics;
using System.Security;
using System.Reflection.Emit;
using Microsoft.VisualBasic;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        public bool Configure(IntPtr panelHandle)
        {
            if (panelHandle != IntPtr.Zero)
            {
                Panel configPanel = (Panel)Panel.FromHandle(panelHandle);

                System.Windows.Forms.Label prompt = new System.Windows.Forms.Label();
                prompt.AutoSize = true;
                prompt.Location = new Point(0, 4);
                prompt.Text = "Webhook URL";

                urlBox = new TextBox();
                urlBox.Bounds = new Rectangle(70, 0, 300, urlBox.Height);
                urlBox.Text = webhookUrl;

                configPanel.Controls.AddRange(new Control[] { prompt, urlBox });
            }
            return false;
        }
    }
}