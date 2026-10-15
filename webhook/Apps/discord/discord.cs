using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace MusicBeePlugin
{
    public static class DiscordNotifier
    {
        // private static readonly object sync = new object();

        // private static string messageId;
        private static readonly HttpClient httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        public static async Task<string> SendMessage(
            string DiscordWebhookUrl,
            string artist,
            string title,
            string album,
            string bitrate,
            string art
            )

        {
            try
            {
                DiscordEmbeded embed = new DiscordEmbeded
                {
                    username = "giingerr01",
                    content = "Now Playing 🎶",
                    avatar_url = "https://i.imgur.com/AfFp7pu.png",
                    embeds = new List<DiscordEmbed>

                    {
                        new DiscordEmbed
                        {
                            title = title,
                            description = "by " + artist,
                            color = 3447003,
                            fields = new List<DiscordField>
                            {
                                new DiscordField
                                {
                                    name = "Album",
                                    value = string.IsNullOrWhiteSpace(album)
                                    ?
                                    "Unknown" : album,
                                    inline = false
                                },
                                new DiscordField
                                {
                                    name = "Bitrate",
                                    value = bitrate,
                                    inline = true
                                },
                            },
                            footer = new EmbedFooter
                            {
                                text = "Source: MusicBee",
                            },

                            thumbnail  = new EmbedThumbnail
                            {
                                url = art
                            }
                        }
                    },
                };

                string jsonString = JsonConvert.SerializeObject(embed, Newtonsoft.Json.Formatting.Indented);

                var Data = new StringContent(jsonString, Encoding.UTF8, "application/json");

                string url = DiscordWebhookUrl.Contains("?")
                ? DiscordWebhookUrl + "&wait=true"
                : DiscordWebhookUrl + "?wait=true";


                HttpResponseMessage response = httpClient.PostAsync(url, Data).GetAwaiter().GetResult();
                using (response)
                {
                    string body = response.Content.ReadAsStringAsync()
                    .GetAwaiter()
                    .GetResult();

                    if (!response.IsSuccessStatusCode)
                    {
                        Logger.Log(
                            "Discord Error" +
                            (int)response.StatusCode + ": " + body
                            );
                        return null;
                    }
                    JObject result = JObject.Parse(body);
                    JToken idToken = result["id"];

                    if (idToken == null)
                    {
                        Logger.Log("Discord response did not contain a message ID.");
                        return null;
                    }
                    return idToken.Value<string>();
                }
            }

            catch (HttpRequestException ex)
            {
                Logger.Log("Discord HTTP request failed: " + ex);
                return null;
            }
            catch (OperationCanceledException ex)
            {
                Logger.Log("Discord request timed out or was cancelled: " + ex);
                return null;
            }
            catch (JsonException ex)
            {
                Logger.Log("Discord JSON error: " + ex);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Log("Unexpected Error: " + ex.Message);
                return null;
            }
        }
    }
}