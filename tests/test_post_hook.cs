using System;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MusicBeePlugin
{
    public static class TestPayload
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task SendAsync(
            string url,
            string artist,
            string title,
            string album
        )

        {
            string json =
            "{"
            + "\"event:\":\"track_changed\","
            + "\"artist:\":\"" + artist + "\","
            + "\"title:\":\"" + title + "\","
            + "\"album:\":\"" + album + "\""
            + "}";

            using (var content = new StringContent(
                json, Encoding.UTF8, "application/json"
            ))
            {
                await httpClient.PostAsync(url, content);
            }
        }
    }
}