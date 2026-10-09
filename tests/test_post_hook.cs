using System;
using System.Net.Http;
using System.Text;
using System.Diagnostics;

namespace MusicBeePlugin
{
    public static class TestPayload
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private string testUrl = "http://localhost:3000/webhook";
        string artist = mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Artist);
        string title = mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Title);
        string album = mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Album);

        string json =
        "{"
        + "\"Event:\":\"track_changed\","
        + "\"Artist:\":\"" + artist + "\","
        + "\"Title:\":\"" + title + "\","
        + "\"Album:\":\"" + album + "\""
        + "}";

        webhookClient.SendAsync(
            "http://localhost:3000/webhook",
            json

        );

    }
}