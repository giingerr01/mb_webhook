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

        public class DiscordPay
        {
            public string content { get; set; }
            // public string art { get; set; }
            public List<DiscordEmbed> embeds { get; set; }

        }

        public class DiscordEmbed
        {
            public string title { get; set; }
            public string description { get; set; }
            public int color { get; set; }
            public List<DiscordField> fields { get; set; }
        }


        public class DiscordField
        {
            public string name { get; set; }
            public string value { get; set; }
            public bool inline { get; set; }
        }
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
                    art = art,
                    content = "Now Playing 🎶",
                    embeds = new List<DiscordEmbed>

                    {
                        new DiscordEmbed
                        {
                            title = title,
                            description = "Artist: " + artist,
                            color = 4384393,
                            fields = new List<DiscordField>
                            {
                                new DiscordField
                                {
                                    name = "Album",
                                    value = string.IsNullOrWhiteSpace(album)
                                    ?
                                    "Unknown" : album,
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