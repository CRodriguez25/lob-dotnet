using Newtonsoft.Json;

namespace LobNet.Clients.Addresses
{
    public class Address
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address_line1")]
        public string Line1 { get; set; }

        [JsonProperty("address_line2")]
        public string Line2 { get; set; }

        [JsonProperty("address_city")]
        public string City { get; set; }

        [JsonProperty("address_state")]
        public string State { get; set; }

        [JsonProperty("address_zip")]
        public string ZipCode { get; set; }

        [JsonProperty("address_country")]
        public string Country { get; set; }
    }
}