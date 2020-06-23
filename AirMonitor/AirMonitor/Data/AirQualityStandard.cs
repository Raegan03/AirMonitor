using Newtonsoft.Json;
using SQLite;
using System;

namespace AirMonitor.Data
{
    [Serializable, Table("air_quality_standard")]
    public class AirQualityStandard
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [JsonProperty("name"), Column("name")]
        public string Name { get; set; }

        [JsonProperty("pollutant"), Column("pollutant")]
        public string Pollutant { get; set; }

        [JsonProperty("limit"), Column("limit")]
        public double Limit { get; set; }

        [JsonProperty("percent"), Column("percent")]
        public double Percent { get; set; }

        [JsonProperty("averaging"), Column("averaging")]
        public string Averaging { get; set; }
    }
}
