using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;

namespace MusicBeePlugin
{
    public class GenericPayload
    {
        [JsonProperty("schemaVersion")]
        public int SchemaVersion { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("player")]
        public PlayerPayload Player { get; set; }

        [JsonProperty("track")]
        public TrackPayload Track { get; set; }

        [JsonProperty("audio")]
        public AudioPayload Audio { get; set; }

        [JsonProperty("art")]
        public ArtworkPayload Artwork { get; set; }
    }

    public class PlayerPayload
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class TrackPayload
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        // [JsonProperty("")]

        [JsonProperty("albumArtist")]
        public string AlbumArtist { get; set; }

        [JsonProperty("durationMs")]
        public long DurationMs { get; set; }
    }

    public class AudioPayload
    {
        [JsonProperty("bitrateKbps")]
        public string BitrateKbps { get; set; }

        [JsonProperty("sampleRateHz")]
        public string SampleRateHz { get; set; }

        [JsonProperty("bitDepth")]
        public int BitDepth { get; set; }

        [JsonProperty("channels")]
        public int Channels { get; set; }

        [JsonProperty("codec")]
        public string Codec { get; set; }


    }

    public class ArtworkPayload
    {
        [JsonProperty("url")]
        public string Url { get; set; }

    }
}