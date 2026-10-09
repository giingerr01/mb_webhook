using System;
using System.Net.Http;
using System.Text;

namespace MusicBeePlugin
{
    public static class DiscordNotifier
    {
        private static readonly HttpClient httpClient = new HttpClient();
        public static void SendMessage(string webhookUrl)
        {
            try
            {
                string payload = @"{""content"": ""hii from musicbee""}";
                var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

                httpClient.PostAsync(webhookUrl, content).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Logger.Log("Error:" + ex.Message);
            }
        }
    }
}