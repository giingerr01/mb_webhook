using System;
using System.Text;
using System.Collections.Generic;

namespace MusicBeePlugin
{
    public class DiscordPayload
    {
        public string Greet { get; set; }
        public long Position { get; set; }

        public string AlbumArtist { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public long Duration { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Bitrate { get; set; }
        public string Art { get; set; }
        public string Format { get; set; }
        public string SampleRateHz { get; set; }
    }
}