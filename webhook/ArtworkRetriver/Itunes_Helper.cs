using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace MusicBeePlugin
{
    public static class ArtworkFetcher
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static string GetArtworkUrl(string artist, string title, string album)
        {
            try
            {
                string url = "https://itunes.apple.com/search"
                    + "?term=" + Uri.EscapeDataString(artist + " " + title + " " + album)
                    + "&entity=song&limit=1";

                string json = httpClient.GetStringAsync(url).GetAwaiter().GetResult();

                string art = (string)JObject.Parse(json).SelectToken("results[0].artworkUrl100");

                return art;
            }
            catch (Exception ex)
            {
                Logger.Log("Artwork lookup failed: " + ex.Message);
                return null;
            }
        }
    }
}