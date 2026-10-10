using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

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
            )

        {
            try
            {

                var content = new StringContent(trackState, System.Text.Encoding.UTF8, "application/json");

                httpClient.PostAsync(webhookUrl, content).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Logger.Log("Error:" + ex.Message);
            }
        }
    }
}