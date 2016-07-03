using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LobNet.Models
{
    public class AddressBookEntry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> MetaData { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public DateTime DateModified { get; set; }

        [JsonProperty("address_line1")]
        public string Address1 { get; set; }

        [JsonProperty("address_line2")]
        public string Address2 { get; set; }

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