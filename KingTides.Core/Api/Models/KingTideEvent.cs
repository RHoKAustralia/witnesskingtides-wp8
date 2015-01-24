using System;
using Newtonsoft.Json;

namespace KingTides.Core.Api.Models
{
    public class KingTideEvent
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "event")]
        public TideEvent Event { get; set; }
    }

    public class TideEvent
    {
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "highTideOccurs")]
        public DateTime HighTideOccurs { get; set; }

        [JsonProperty(PropertyName = "eventStart")]
        public DateTime EventStart { get; set; }

        [JsonProperty(PropertyName = "eventEnd")]
        public DateTime EventEnd { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public decimal Longitude { get; set; }
    }
}
