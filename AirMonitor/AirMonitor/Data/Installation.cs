using Newtonsoft.Json;

namespace AirMonitor.Data
{
    public class Installation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

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

        public Installation() { }

        public Installation(InstallationEntity installationEntity)
        {
            Id = installationEntity.Id;
            Elevation = installationEntity.Elevation;
            Airly = installationEntity.Airly;

            Location = JsonConvert.DeserializeObject<Location>(installationEntity.Location);
            Address = JsonConvert.DeserializeObject<Address>(installationEntity.Address);
            Sponsor = JsonConvert.DeserializeObject<Sponsor>(installationEntity.Sponsor);
        }
    }
}
