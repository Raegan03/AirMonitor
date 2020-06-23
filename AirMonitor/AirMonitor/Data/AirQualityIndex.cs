using Newtonsoft.Json;
using SQLite;
using System;

namespace AirMonitor.Data
{
    [Serializable, Table("air_quality_index")]
    public class AirQualityIndex
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [JsonProperty("name"), Column("name")]
        public string Name { get; set; }

        [JsonProperty("value"), Column("value")]
        public double? Value { get; set; }

        [JsonProperty("level"), Column("level")]
        public string Level { get; set; }

        [JsonProperty("description"), Column("description")]
        public string Description { get; set; }

        [JsonProperty("advice"), Column("advice")]
        public string Advice { get; set; }

        [JsonProperty("color"), Column("color")]
        public string Color { get; set; }
    }
}
