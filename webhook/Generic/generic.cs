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
    public static class TestPayload
    {
        private static readonly HttpClient httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };



        public static async Task SendAsync(
            string LocalWebhookUrl,
            string greet,
            long positionMs,
            long durationMs,
            string artist,
            string title,
            string albumArtist,
            int year,
            string genre,
            string bitrate,
            string album,
            string art
        )

        {
            try
            {
                DiscordPayload discordPayload = new DiscordPayload
                {

                };

                string jsonString = JsonConvert.SerializeObject(discordPayload, Newtonsoft.Json.Formatting.Indented);

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
                        Logger.Log("Local message send successfully.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.Log("Local host HTTP request failed: " + ex);
            }
            catch (OperationCanceledException ex)
            {
                Logger.Log("Local host request timed out or was cancelled: " + ex);
            }
            catch (JsonException ex)
            {
                Logger.Log("Local host JSON error: " + ex);

            }
            catch (Exception ex)
            {
                Logger.Log("Unexpected Local webhook error: " + ex);
            }
        }

    }
}