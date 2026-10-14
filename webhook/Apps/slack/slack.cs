using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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
                SlackMessage slackMessage = new SlackMessage
                {
                    text = $"Now playing: {title} by {artist} ",
                };

                string jsonString = JsonConvert.SerializeObject(slackMessage, Newtonsoft.Json.Formatting.Indented);

                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                try
                {
                    await httpClient.PostAsync(SlackWebhookUrl, content);
                }
                catch (Exception ex)
                {
                    Logger.Log("Error at postAsync: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error:" + ex.Message);
            }
        }
    }
}