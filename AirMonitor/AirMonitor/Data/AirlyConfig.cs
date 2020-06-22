using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Data
{
    public class AirlyConfig
    {
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("api_uri")]
        public string ApiUri { get; set; }

        [JsonProperty("api_installations_nearest")]
        public string ApiInstallationsNearest { get; set; }

        [JsonProperty("api_installations_by_id")]
        public string ApiInstallationsById { get; set; }

        [JsonProperty("api_measurements_nearest")]
        public string ApiMeasurementsNearest { get; set; }

        [JsonProperty("api_measurements_by_id")]
        public string ApiMeasurementsById { get; set; }
    }
}

