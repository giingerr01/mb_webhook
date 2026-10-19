using System;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace MusicBeePlugin
{
    public static class GenericWebhook
    {
        private static readonly HttpClient httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };



        public static async Task SendAsync(
            string LocalWebhookUrl,
            long positionMs,
            long durationMs,
            string artist,
            string title,
            string albumArtist,
            int year,
            string genre,
            string bitrate,
            string format,
            string sampleRateHz,
            string bitDepth,
            string album,
            string art
        )

        {
            try
            {
                var genericPayload = new GenericPayload
                {
                    SchemaVersion = 1,
                    Event = "track_changed",
                    TimeStamp = DateTimeOffset.Now.ToString("o"),

                    Player = new PlayerPayload
                    {
                        Name = "MusicBee"
                    },

                    Track = new TrackPayload
                    {
                        Title = title,
                        Artist = artist,
                        Album = album,
                        AlbumArtist = albumArtist,
                        Genre = genre,
                        Year = year,
                        DurationMs = durationMs
                    },

                    Audio = new AudioPayload
                    {
                        BitrateKbps = bitrate,
                        SampleRateHz = sampleRateHz,
                        BitDepth = bitDepth,
                        // channels = null 
                        Codec = format

                    },

                    Artwork = new ArtworkPayload
                    {
                        Url = art,
                    }
                };

                string jsonString = JsonConvert.SerializeObject(genericPayload, Newtonsoft.Json.Formatting.Indented);

                using (var requestContent = new StringContent(
                    jsonString, Encoding.UTF8, "application/json"
                    ))
                {
                    using (HttpResponseMessage response = await httpClient.PostAsync(LocalWebhookUrl, requestContent))
                    {
                        string body = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            Logger.Log("Local HTTP error: " + (int) response.StatusCode + ": " + body);
                            return;
                        }
                        Logger.Log("Generic Payload sent successfully.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.Log("Generic host HTTP request failed: " + ex);
            }
            catch (OperationCanceledException ex)
            {
                Logger.Log("Generic host request timed out or was cancelled: " + ex);
            }
            catch (JsonException ex)
            {
                Logger.Log("Generic host JSON error: " + ex);

            }
            catch (Exception ex)
            {
                Logger.Log("Unexpected Generic webhook error: " + ex);
            }
        }

    }
}