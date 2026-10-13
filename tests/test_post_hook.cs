using System;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Dynamic;

namespace MusicBeePlugin
{
    public static class TestPayload
    {
        private static readonly HttpClient httpClient = new HttpClient();


        public class TestPay
        {
            public string Artist { get; set; }
            public string Title { get; set; }
            public string Album { get; set; }
            public string Bitrate { get; set; }
            public string Art { get; set; }
        }

        public static async Task SendAsync(
            string DiscordWebhokUrl,
            string artist,
            string title,
            string bitrate,
            string album,
            string art
        )

        {
            try
            {
                TestPay testPay = new TestPay { Art = art, Bitrate = bitrate, Title = title, Album = album, Artist = artist };

                string jsonString = JsonConvert.SerializeObject(testPay, Newtonsoft.Json.Formatting.Indented);

                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                httpClient.PostAsync(DiscordWebhokUrl, content).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                Logger.Log("Error:" + ex.Message);
            }
        }

    }
}