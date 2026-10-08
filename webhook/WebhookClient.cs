using System.Net.Http;
using System.Text;
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

        public async Task SendAsync(string url, string json)
        {
            MessageBox.Show("Inside SendAsync");

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            MessageBox.Show("About to PostAsync");

            await _httpClient.PostAsync(url, null);

            MessageBox.Show("PostAsync finished");
        }
    }
}