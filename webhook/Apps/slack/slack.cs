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
            public string NowPlaying { get; set; }
            public string Hello { get; set; }
        }
        public static async Task SendAsync(
            string SlackWebhookUrl
        )
        {
            try
            {
                SlackLocal slackLocal = new SlackLocal { NowPlaying = "Hii", Hello = "4000 port" };
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