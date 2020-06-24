using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AirMonitor.Data
{
    [Serializable]
    public class Measurement
    {
        [JsonProperty("current")]
        public MeasurementItem Current { get; set; }

        //[JsonProperty("history")]
        //public List<MeasurementItem> History { get; set; }

        //[JsonProperty("forecast")]
        //public List<MeasurementItem> Forecast { get; set; }

        public Installation Installation { get; set; }
    }
}
