using System;
using System.Net.Http;
using System.Collections;
using System.Text;
using Newtonsoft.Json;
using Microsoft.VisualBasic;
using System.Diagnostics;


namespace MusicBeePlugin
{
    public static class ArtworkFetcher
    {
        private static readonly HttpClient httpClient = new HttpClient();
        public class SearchTerm
        {
            public string albumTerm { get; set; }
            public string artistTerm { get; set; }
            public string titleTerm { get; set; }
        }

        public static void SendAsync(
            string webhookUrl,
            string artist,
            string album,
            string title
        );

        public static string BuildingSearchUrl(SearchTerm searchTerm)
        {
            string term =
            $"{searchTerm.artistTerm}" +
            $"{searchTerm.albumTerm}" +
            $"{searchTerm.titleTerm}";

            return "https://itunes.apple.com/search" +
                   "?term=" + Uri.EscapeDataString(term) +
                   "&entity=song" +
                   "&limits=5" +
                   "&country=IN";

        }

    }
}