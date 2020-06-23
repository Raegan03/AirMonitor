using Newtonsoft.Json;
using SQLite;
using System;

namespace AirMonitor.Data
{
    [Serializable, Table("measurement_value")]
    public class MeasurementValue
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [JsonProperty("name"), Column("name")]
        public string Name { get; set; }

        [JsonProperty("value"), Column("value")]
        public double Value { get; set; }
    }
}
