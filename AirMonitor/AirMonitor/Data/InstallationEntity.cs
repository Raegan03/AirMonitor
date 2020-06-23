using Newtonsoft.Json;
using SQLite;
using System;

namespace AirMonitor.Data
{
    [Serializable, Table("installation_entity")]
    public class InstallationEntity
    {
        [Column("id")]
        public string Id { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("elevation")]
        public double Elevation { get; set; }

        [Column("airly")]
        public bool Airly { get; set; }

        [Column("sponsor")]
        public string Sponsor { get; set; }

        public InstallationEntity()
        {

        }

        public InstallationEntity(Installation installation)
        {
            Id = installation.Id.ToString();
            Location = JsonConvert.SerializeObject(installation.Location);
            Elevation = installation.Elevation;
            Airly = installation.Airly;
            Address = JsonConvert.SerializeObject(installation.Address);
            Sponsor = JsonConvert.SerializeObject(installation.Sponsor);
        }
    }
}
