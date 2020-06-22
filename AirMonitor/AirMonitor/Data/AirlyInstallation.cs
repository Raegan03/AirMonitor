using Newtonsoft.Json;

namespace AirMonitor.Data
{
    public class AirlyInstallation
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }

        [JsonProperty("airly")]
        public bool Airly { get; set; }

        [JsonProperty("sponsor")]
        public Sponsor Sponsor { get; set; }
    }

    public class Location
    {

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

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
