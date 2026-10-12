using System;
using System.Net.Http;
using System.Windows.Forms;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Json.Linq;
using System.Text.Json.Nodes;


namespace MusicBeePlugin
{
    public static class ArtworkFetcher
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static string GetArtworkUrl(string artist, string title)
        {
            try
            {
                string url = "https://itunes.apple.com/search"
                + "?term=" + Uri.EscapeDataString(artist + " " + title)
                + "&entity=song&limit=1";

                string json = httpClient.GetStringAsync(url).GetAwaiter().GetResult();

                string art = (string)JsonObject.Parse(json).SelectToken("Results[0].artworkUrl100");

                MessageBox.Show(art);


            }
            catch (Exception ex)
            {
                Logger.Log("Artwork lookup failed: " + ex.Message);
                return null;
            }
        }

    }
}