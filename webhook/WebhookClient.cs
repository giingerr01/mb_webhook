using System.Net.Http;
using System.Threading.Tasks;

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
            await _httpClient.PostAsync(url, null);
        }
    }
}