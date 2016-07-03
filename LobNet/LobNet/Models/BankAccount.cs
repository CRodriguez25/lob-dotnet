using System.Collections.Generic;
using LobNet.Clients.EnumConverters;
using Newtonsoft.Json;

namespace LobNet.Models
{
    public class BankAccount
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> MetaData { get; set; }

        [JsonProperty("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("signatory")]
        public string Signatory { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("account_type")]
        [JsonConverter(typeof(AccountTypeEnumConverter))]
        public AccountType AccountType { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public string DateModified { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }
    }
    public enum AccountType
    {
        Individual,
        Company
    }
}