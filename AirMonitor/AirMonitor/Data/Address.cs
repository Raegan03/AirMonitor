using Newtonsoft.Json;

namespace AirMonitor.Data
{
    public class Address
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("displayAddress1")]
        public string DisplayAddress1 { get; set; }

        [JsonProperty("displayAddress2")]
        public string DisplayAddress2 { get; set; }
    }
}
