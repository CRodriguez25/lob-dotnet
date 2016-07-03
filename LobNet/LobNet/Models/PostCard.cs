using System;
using System.Collections.Generic;
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
        public Dictionary<string, string> MetaData { get; set; }

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
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public DateTime DateModified { get; set; }

        [JsonProperty("expected_delivery_date")]
        public DateTime ExpectedDeliveryDate { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnails> Thumbnails { get; set; }
    }
}