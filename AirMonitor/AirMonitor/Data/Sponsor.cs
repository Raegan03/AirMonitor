using Newtonsoft.Json;

namespace AirMonitor.Data
{
    public class Sponsor
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }
}
