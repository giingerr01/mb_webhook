namespace MusicBeePlugin
{
    public class WebhookSettings
    {
        public string DiscordUrl { get; set; } = string.Empty;
        public bool DiscordEnabled { get; set; }

        public string SlackUrl { get; set; } = string.Empty;
        public bool SlackEnabled { get; set; }

        public string GenericUrl { get; set; } = string.Empty;
        public bool GenericEnabled { get; set; }
    }
}