using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public static string SendMessage(
            string webhookUrl,
            string artist,
            string title,
            string album,
            string bitrate,
            string artForDisco
            // string art
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
                                text = "Source: MusicBee"
                            },
                            thumbnail  = new EmbedThumbnail
                            {
                                url = artForDisco
                            }
                        }
                    },
                };

                string jsonString = JsonConvert.SerializeObject(embed, Newtonsoft.Json.Formatting.Indented);

                var Data = new StringContent(jsonString, Encoding.UTF8, "application/json");

                string url = webhookUrl.Contains("?")
                ? webhookUrl + "&wait=true"
                : webhookUrl + "?wait=true";

                HttpResponseMessage response = httpClient.PostAsync(url, Data).GetAwaiter().GetResult();
                string body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    Logger.Log("Discord Error" + (int)response.StatusCode + ": " + body);
                    return null;
                }

                return (string)JObject.Parse(body)["id"];


            }
            catch (Exception ex)
            {
                Logger.Log("Discord Send failed: " + ex.Message);
                return null;
            }
        }
    }
}