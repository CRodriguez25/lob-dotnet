using Newtonsoft.Json;

namespace LobNet.Models
{
    public class DeleteResult
    {
        [JsonProperty("deleted")]
        public bool Deleted { get; set; }
    }
}