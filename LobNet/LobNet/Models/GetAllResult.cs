using System.Collections.Generic;
using Newtonsoft.Json;

namespace LobNet.Models
{
    public class GetAllResult<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}