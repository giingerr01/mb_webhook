using System.Collections.Generic;

namespace MusicBeePlugin
{
    public class DiscordEmbeded
    {
        public string username { get; set; }
        public string avatar_url { get; set; }
        public string content { get; set; }
        // public string art { get; set; }
        public List<DiscordEmbed> embeds { get; set; }

    }

    public class DiscordEmbed
    {
        public string title { get; set; }
        public int color { get; set; }
        // public EmbedAuthor author { get; set; }
        public EmbedFooter footer { get; set; }
        public EmbedThumbnail thumbnail { get; set; }
        public string description { get; set; }
        public List<DiscordField> fields { get; set; }
        public List<DiscordLine> lines { get; set; }
    }

    public class DiscordLine
    {
        public string name { get; set; }
        public string value { get; set; }
        public string inline { get; set; }
    }

    public class EmbedThumbnail
    {
        public string url { get; set; }
    }

    public class EmbedFooter
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    // public class EmbedAuthor
    // {
    //     public string name { get; set; }
    //     public string description { get; set; }
    // }

    public class DiscordField
    {
        public string name { get; set; }
        public string value { get; set; }
        public bool inline { get; set; }

    }
}