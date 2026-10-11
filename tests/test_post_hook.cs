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
        }


        public static void SendAsync(
            string webhookUrl,
            string artist,
            string title,
            string album
        )

        {
            try
            {
                TestPay testPay = new TestPay { Title = title, Album = album, Artist = artist };

                string jsonString = JsonConvert.SerializeObject(testPay, Newtonsoft.Json.Formatting.Indented);

                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                httpClient.PostAsync(webhookUrl, content).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Logger.Log("Error:" + ex.Message);
            }
        }

    }
}