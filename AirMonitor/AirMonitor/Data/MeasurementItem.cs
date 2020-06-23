using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AirMonitor.Data
{
    [Serializable]
    public class MeasurementItem
    {
        [JsonProperty("fromDateTime")]
        public DateTime FromDateTime { get; set; }

        [JsonProperty("tillDateTime")]
        public DateTime TillDateTime { get; set; }

        [JsonProperty("values")]
        public List<MeasurementValue> Values { get; set; }

        [JsonProperty("indexes")]
        public List<AirQualityIndex> Indexes { get; set; }

        [JsonProperty("standards")]
        public List<AirQualityStandard> Standards { get; set; }
    }
}
