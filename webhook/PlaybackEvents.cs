using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        public static string firstMessageId;
        public async Task ReceiveNotification(string sourceFileUrl, NotificationType type)
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

                    string art = null;
                    try
                    {
                        art = await Task.Run(() =>
                        ArtworkFetcher.GetArtworkUrl(
                            artist,
                            title,
                            album
                        ));
                        Logger.Log("ArtworkUrl: " + art);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Artwork Lookup failed: " + ex);
                    }

                    art = await Task.Run(() =>
                        ArtworkFetcher.GetArtworkUrl(artist, title, album)
                    );

                    Logger.Log("ArtworkUrl: " + art);
                    await TestPayload.SendAsync(
                        LocalWebhookUrl,
                        artist,
                        title,
                        bitrate,
                        album,
                        art
                    );

                    string id = DiscordNotifier.SendMessage(
                        DiscordWebhookUrl,
                        artist,
                        title,
                        album,
                        bitrate,
                        art
                    );
                    Logger.Log("Discord Message Id: " + id);
                    if (firstMessageId == null && id != null)
                    {
                        firstMessageId = id;
                    }

                    await SlackNotifier.SendAsync(
                        SlackWebhookUrl,
                        artist,
                        title,
                        album,
                        bitrate,
                        art
                    );
                    break;
            }
        }
    }
}