using System.Net.Http;

namespace MusicBeePlugin
{
    public class WebhookClient
    {
        private readonly HttpClient _httpClient;

        public WebhookClient()
        {
            _httpClient = new HttpClient();
        }
        public async Task SendAsync(string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            await _httpClient.PostAsync(url, null);
        }
    }
}