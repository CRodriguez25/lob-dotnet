using System.Collections.Generic;
using LobNet.Clients.PostCards;
using Newtonsoft.Json;

namespace LobNet.Models
{
    public class Check
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> MetaData { get; set; }

        [JsonProperty("check_number")]
        public int CheckNumber { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("to")]
        public AddressBookEntry ToAddress { get; set; }

        [JsonProperty("from")]
        public AddressBookEntry FromAddress { get; set; }

        [JsonProperty("bank_account")]
        public BankAccount BankAccount { get; set; }

        [JsonProperty("tracking")]
        public TrackingInformation Tracking { get; set; }

        [JsonProperty("thumbnails")]
        public Thumbnails[] Thumbnails { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public string DateModified { get; set; }

        [JsonProperty("expected_delivery_date")]
        public string ExpectedDeliveryDate { get; set; }
    }
}