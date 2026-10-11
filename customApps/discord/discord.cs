using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Net.Http;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace MusicBeePlugin
{
    public static class DiscordNotifier
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static void SendMessage(
            string webhookUrl,
            string artist,
            string title,
            string album
            // string art
            )

        {
            try
            {
                DiscordPay payload = new DiscordPay
                {
                    username = "giingerr01",
                    avatar_url = "https://i.imgur.com/AfFp7pu.png",
                    embeds = new List<DiscordEmbed>

                    {
                        new DiscordEmbed
                        {
                            author = new EmbedAuthor
                            {
                                name = "Now Playing from **MusicBee**",
                            },

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
                                    name = "BITRATE",
                                    value = "Playing",
                                    inline = true
                                }
                            }
                        }
                    },
                };

                string jsonString = JsonConvert.SerializeObject(payload, Newtonsoft.Json.Formatting.Indented);

                var Data = new StringContent(jsonString, Encoding.UTF8, "application/json");

                httpClient.PostAsync(webhookUrl, Data).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Logger.Log("Error:" + ex.Message);
            }
        }
    }
}