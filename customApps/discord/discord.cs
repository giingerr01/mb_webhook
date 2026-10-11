using System;
using System.Dynamic;
using System.Globalization;
using System.Net.Http;
using System.Net.Mime;
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
            public string Title { get; set; }
            public string Album { get; set; }

        }
        public static void SendMessage(
            string webhookUrl,
            string artist,
            string title,
            string album
            )

        {
            try
            {
                DiscordPay payload = new DiscordPay { Title = title, Album = album };

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