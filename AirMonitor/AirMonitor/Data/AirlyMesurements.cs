using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AirMonitor.Data
{
    [Serializable]
    public class AirlyMesurements
    {
        [JsonProperty("current")]
        public Current Current { get; set; }

        [JsonProperty("history")]
        public IList<History> History { get; set; }

        [JsonProperty("forecast")]
        public IList<Forecast> Forecast { get; set; }

        [JsonProperty("installation")]
        public AirlyInstallation Installation { get; set; }
    }

    [Serializable]
    public class ValueData
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
    
    [Serializable]
    public class Index
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public double? Value { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("advice")]
        public string Advice { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    [Serializable]
    public class Standard
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pollutant")]
        public string Pollutant { get; set; }

        [JsonProperty("limit")]
        public double Limit { get; set; }

        [JsonProperty("percent")]
        public double Percent { get; set; }

        [JsonProperty("averaging")]
        public string Averaging { get; set; }
    }

    [Serializable]
    public class Current
    {

        [JsonProperty("fromDateTime")]
        public DateTime FromDateTime { get; set; }

        [JsonProperty("tillDateTime")]
        public DateTime TillDateTime { get; set; }

        [JsonProperty("values")]
        public IList<ValueData> Values { get; set; }

        [JsonProperty("indexes")]
        public IList<Index> Indexes { get; set; }

        [JsonProperty("standards")]
        public IList<Standard> Standards { get; set; }
    }

    [Serializable]
    public class History
    {

        [JsonProperty("fromDateTime")]
        public DateTime FromDateTime { get; set; }

        [JsonProperty("tillDateTime")]
        public DateTime TillDateTime { get; set; }

        [JsonProperty("values")]
        public IList<ValueData> Values { get; set; }

        [JsonProperty("indexes")]
        public IList<Index> Indexes { get; set; }

        [JsonProperty("standards")]
        public IList<Standard> Standards { get; set; }
    }

    [Serializable]
    public class Forecast
    {

        [JsonProperty("fromDateTime")]
        public DateTime FromDateTime { get; set; }

        [JsonProperty("tillDateTime")]
        public DateTime TillDateTime { get; set; }

        [JsonProperty("values")]
        public IList<object> Values { get; set; }

        [JsonProperty("indexes")]
        public IList<Index> Indexes { get; set; }

        [JsonProperty("standards")]
        public IList<object> Standards { get; set; }
    }
}
