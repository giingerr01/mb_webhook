using System.Collections.Generic;

namespace MusicBeePlugin
{
    public class SlackMessage
    {
        public string text { get; set; }
        public List<SlackBlock> blocks { get; set; }
    }

    public class SlackText
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class SlackBlock
    {
        public string type { get; set; }
        public SlackText text { get; set; }
        public SlackAccessory accessory { get; set; }
        public List<SlackText> elements { get; set; }
    }
    public class SlackAccessory
    {
        public string type { get; set; }
        public string image_url { get; set; }
        public string alt_text { get; set; }
    }
}