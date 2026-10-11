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
        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // perform some action depending on the notification type
            switch (type)
            {
                case NotificationType.PluginStartup:
                    // perform startup initialisation
                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:
                            break;

                        case PlayState.Paused:
                            // ...
                            break;
                    }
                    break;

                case NotificationType.TrackChanged:

                    string artist =
                    mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Artist);

                    string title =
                    mbApiInterface.NowPlaying_GetFileTag(MetaDataType.TrackTitle);

                    string album =
                    mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Album);

                    string bitrate =
                    mbApiInterface.NowPlaying_GetFileProperty(FilePropertyType.Bitrate);

                    //a custom url for local post test 
                    TestPayload.SendAsync(
                        "http://localhost:3000/webhook",
                        artist,
                        title,
                        bitrate,
                        album
                    );


                    //post request to discord
                    DiscordNotifier.SendMessage(
                        webhookUrl,
                        artist,
                        title,
                        album,
                        bitrate
                        );

                    break;

                    // string art = mbApiInterface.NowPlaying_GetArtwork();


                    // MessageBox.Show(
                    // json
                    // );
                    // break;
            }
        }
    }
}