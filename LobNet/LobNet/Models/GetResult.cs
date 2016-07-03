using System.Collections.Generic;
using Newtonsoft.Json;

namespace LobNet.Models
{
    public class GetResult<T>
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("data")]
        public List<T> Entries { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}