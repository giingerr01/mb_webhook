using System;
using System.Threading.Tasks;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        public static string firstMessageId;

        public async Task ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            switch (type)
            {
                case NotificationType.PluginStartup:
                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:

                            break;

                        case PlayState.Paused:
                            break;
                    }
                    break;

                case NotificationType.TrackChanged:
                    long positionMs = mbApiInterface.Player_GetPosition();
                    long durationMs = mbApiInterface.NowPlaying_GetDuration();

                    string album = mbApiInterface.NowPlaying_GetFileTag(
                        MetaDataType.Album
                        );
                    string genre = mbApiInterface.NowPlaying_GetFileTag(
                        MetaDataType.Genre
                        );
                    string artist = mbApiInterface.NowPlaying_GetFileTag(
                        MetaDataType.Artist
                        );
                    string title = mbApiInterface.NowPlaying_GetFileTag(
                        MetaDataType.TrackTitle
                        );
                    string format = mbApiInterface.NowPlaying_GetFileProperty(
                        FilePropertyType.Kind
                        );
                    string albumArtist = mbApiInterface.NowPlaying_GetFileTag(
                        MetaDataType.AlbumArtist
                        );
                    string bitrate = mbApiInterface.NowPlaying_GetFileProperty(
                        FilePropertyType.Bitrate
                        );
                    string sampleRateHz = mbApiInterface.NowPlaying_GetFileProperty(
                        FilePropertyType.SampleRate
                        );

                    // fetching raw date first 
                    string rawYear = mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Year);

                    // parse it into year 
                    int year = 0;
                    if (DateTime.TryParse(rawYear, out var parsedDate))
                    {
                        year = parsedDate.Year;
                    }

                    string art = null;
                    try
                    {
                        art = await Task.Run(() => ArtworkFetcher.GetArtworkUrl(artist, title, album));
                        Logger.Log("ArtworkUrl: " + art);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Artwork Lookup failed: " + ex);
                    }

                    Task GenericTask = SafeSendAsync(
                        "Local",
                        () => GenericWebhook.SendAsync(
                            GenericWebhookUrl,
                            positionMs,
                            durationMs,
                            artist,
                            title,
                            albumArtist,
                            year,
                            genre,
                            bitrate,
                            format,
                            sampleRateHz,
                            album,
                            art));

                    Task<string> discordTask = SafeSendDiscordAsync(
                        artist,
                        title,
                        album,
                        bitrate,
                        art);

                    Task slackTask = SafeSendAsync(
                        "Slack",
                        () => SlackNotifier.SendAsync(
                            SlackWebhookUrl,
                            artist,
                            title,
                            album,
                            bitrate,
                            art));

                    await Task.WhenAll(GenericTask, discordTask, slackTask);

                    string id = await discordTask;
                    if (id != null)
                    {
                        Logger.Log("Discord Message Id: " + id);

                        if (firstMessageId == null)
                        {
                            firstMessageId = id;
                        }
                    }
                    break;
            }
        }

        private async Task SafeSendAsync(string name, Func<Task> send)
        {
            try
            {
                await send();
            }
            catch (Exception ex)
            {
                Logger.Log(name + " webhook exception: " + ex);
            }
        }

        private async Task<string> SafeSendDiscordAsync(
            string artist,
            string title,
            string album,
            string bitrate,
            string art)
        {
            try
            {
                return await DiscordNotifier.SendMessage(
                    DiscordWebhookUrl,
                    artist,
                    title,
                    album,
                    bitrate,
                    art);
            }
            catch (Exception ex)
            {
                Logger.Log("Discord webhook exception: " + ex);
                return null;
            }
        }
    }
}
