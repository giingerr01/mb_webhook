using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;

namespace MusicBeePlugin
{
    public static class SlackNotifier
    {
        public static readonly HttpClient httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        public static async Task SendAsync(
            string SlackWebhookUrl,
            string artist,
            string title,
            string album,
            string bitrate,
            string art

        )
        {
            try
            {
                var slackMessage = new SlackMessage
                {
                    text = $"Now Playing: {title} by {artist}",
                    blocks = new List<SlackBlock>
                    {
                        new SlackBlock
                        {
                            type = "section",
                            text = new SlackText
                            {
                                type = "mrkdwn",
                                text = $"*NowPlaying on MusicBee*\n*Artist:* {artist}\n*{album}\n*Bitrate:* {bitrate}"
                            },
                            accessory = !string.IsNullOrEmpty(art) ?
                            new SlackAccessory
                            {
                                type = "image",
                                image_url = art,
                                alt_text = "Album Art"
                            } : null
                        }
                    }
                };

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                string jsonString = JsonConvert.SerializeObject(slackMessage, Newtonsoft.Json.Formatting.None, settings);

                using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                {
                    using (HttpResponseMessage response = await httpClient.PostAsync(SlackWebhookUrl, content))
                    {
                        string body = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            Logger.Log(
                                "Slack HTTP Error: " + (int)response.StatusCode + ": " + body
                            );
                            return;
                        }
                        Logger.Log("Slack Message Send successfully.");
                    }
                }

            }
            catch (HttpRequestException ex)
            {
                Logger.Log("Slack HTTP request Failed: " + ex);
            }
            catch (OperationCanceledException ex)
            {
                Logger.Log("Slack request Timed out or was cancelled: " + ex);
            }
            catch (JsonException ex)
            {
                Logger.Log("Slack JSON error: " + ex);
            }
            catch (Exception ex)
            {
                Logger.Log("Unexpected Slack Error: " + ex.Message);
            }
        }
    }
}