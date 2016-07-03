using Newtonsoft.Json;

namespace LobNet.Models
{
    public class Country
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }
    }
}