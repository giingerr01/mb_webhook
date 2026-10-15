using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            string art)
        {
            try
            {
                var slackMessage = new SlackMessage
                {
                    attachments = new List<SlackAttachment>
                    {
                        new SlackAttachment
                        {
                            color = "#0d56aa",
                            blocks = new List<SlackBlock>
                            {
                                // Header
                                new SlackBlock
                                {
                                    type = "header",
                                    text = new SlackText
                                    {
                                        type = "plain_text",
                                        text = "🎶 Now Playing",
                                        emoji = true
                                    }
                                },

                                // Title, artist, and album artwork
                                new SlackBlock
                                {
                                    type = "section",
                                    text = new SlackText
                                    {
                                        type = "mrkdwn",
                                        text = $"*{title}*\n_{artist}_"
                                    },
                                    accessory = !string.IsNullOrEmpty(art)
                                    ? new SlackAccessory
                                    {
                                        type = "image",
                                        image_url = art,
                                        alt_text = $"Album artwork for {album}"
                                    }
                                    : null
                                },

                                // Album and bitrate
                                new SlackBlock
                                {
                                    type = "section",
                                    fields = new List<SlackText>
                                    {
                                        new SlackText
                                        {
                                            type = "mrkdwn",
                                            text = $"*Album*\n{album}"
                                        },
                                        new SlackText
                                        {
                                            type = "mrkdwn",
                                            text = $"*Bitrate*\n{bitrate}"
                                        }
                                    }
                                },

                                // Footer
                                new SlackBlock
                                {
                                    type = "context",
                                    elements = new List<SlackText>
                                    {
                                        new SlackText
                                        {
                                            type = "mrkdwn",
                                            text = "🎧 MusicBee"
                                        }
                                    }
                                }
                            }
                        }
                }
                };

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                var jsonString = JsonConvert.SerializeObject(slackMessage, Formatting.None, settings);

                using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                using (var response = await httpClient.PostAsync(SlackWebhookUrl, content))
                {
                    var body = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        Logger.Log("Slack HTTP Error: " + (int)response.StatusCode + ": " + body);
                        return;
                    }

                    Logger.Log("Slack Message Send successfully.");
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