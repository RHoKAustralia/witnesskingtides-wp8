using System;
using Newtonsoft.Json;

namespace KingTides.Core.Api.Models
{
    public class KingTideEvent
    {
        public string id { get; set; }

        [JsonProperty(PropertyName = "event")]
        public TideEvent Event { get; set; }
    }

    public class TideEvent
    {
        public string location { get; set; }
        public string state { get; set; }
        public string description { get; set; }
        public DateTime highTideOccurs { get; set; }
        public DateTime eventStart { get; set; }
        public DateTime evetEnd { get; set; }
        public decimal latitude { get; set; }
        public string longitude { get; set; }
    }
}
