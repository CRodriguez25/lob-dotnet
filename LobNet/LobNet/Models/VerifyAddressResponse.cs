using LobNet.Clients.Addresses;
using Newtonsoft.Json;

namespace LobNet.Models
{
    public class VerifyAddressResponse
    {
        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("message")]
        public string Message { get; set;  }
    }
}