using System;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;

namespace MusicBeePlugin
{
    public static class SlackNotifier
    {
        public static readonly HttpClient httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(5)
        };
        public class SlackLocal
        {
            public string Artist { get; set; }
            public string Title { get; set; }
            public string Album { get; set; }
            public string Bitrate { get; set; }
            public string Art { get; set; }
        }
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
                SlackLocal slackLocal = new SlackLocal { Title = title, Album = album, Artist = artist, Bitrate = bitrate, Art = art };
                string jsonString = JsonConvert.SerializeObject(slackLocal, Newtonsoft.Json.Formatting.Indented);

                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                await httpClient.PostAsync(SlackWebhookUrl, content);
            }
            catch (Exception ex)
            {
                Logger.Log("Error:" + ex.Message);

            }
        }
    }
}