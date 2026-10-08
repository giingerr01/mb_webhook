using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

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
            using var request = new HttpRequestMessage(
                HttpMethod.Post, url
            );
            request.Content = new StringContent("Hello from MusicBee!!");

            await _httpClient.SendAsync(request);
        }
    }
}