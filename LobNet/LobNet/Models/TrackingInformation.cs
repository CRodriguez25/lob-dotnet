using System.Collections.Generic;
using Newtonsoft.Json;

namespace LobNet.Models
{
    public class TrackingInformation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tracking_number")]
        public string TrackingNumber { get; set; }

        [JsonProperty("carrier")]
        public string Carrier { get; set; }

        [JsonProperty("events")]
        public List<string> Events { get; set; }
    }
}