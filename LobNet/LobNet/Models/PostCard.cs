using LobNet.Clients.PostCards;
using Newtonsoft.Json;

namespace LobNet.Models
{
    public class PostCard
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public dynamic MetaData { get; set; }

        [JsonProperty("to")]
        public AddressBookEntry ToAddress { get; set; }

        [JsonProperty("from")]
        public AddressBookEntry FromAddress { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("tracking")]
        public TrackingInformation Tracking { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public string DateModified { get; set; }

        [JsonProperty("expected_delivery_date")]
        public string ExpectedDeliveryDate { get; set; }

        [JsonProperty("thumbnails")]
        public Thumbnails[] Thumbnails { get; set; }
    }
}