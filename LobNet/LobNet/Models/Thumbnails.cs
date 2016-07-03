using Newtonsoft.Json;

namespace LobNet.Models
{
    public class Thumbnails
    {
        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }
    }
}