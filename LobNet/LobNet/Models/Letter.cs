using System;
using System.Collections.Generic;
using LobNet.Clients.EnumConverters;
using LobNet.Clients.PostCards;
using Newtonsoft.Json;

namespace LobNet.Models
{
    public class Letter
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("to")]
        public AddressBookEntry ToAddress { get; set; }

        [JsonProperty("from")]
        public AddressBookEntry FromAddress { get; set; }

        [JsonProperty("color")]
        public bool Color { get; set; }

        [JsonProperty("double_sided")]
        public bool DoubleSided { get; set; }

        [JsonProperty("address_placement")]
        [JsonConverter(typeof(AddressPlacementEnumConverter))]

        public AddressPlacement AddressPlacement { get; set; }

        [JsonProperty("return_envelope")]
        public bool ReturnEnvelope { get; set; }

        [JsonProperty("perforated_page")]
        public int? PerforatedPage { get; set; }

        [JsonProperty("extra_service")]
        [JsonConverter(typeof(ExtraServiceEnumConverter))]
        public ExtraService ExtraService { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("tracking")]
        public TrackingInformation Tracking { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnails> Thumbnails { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public DateTime DateModified { get; set; }

        [JsonProperty("expected_delivery_date")]
        public DateTime ExpectedDeliveryDate { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> MetaData { get; set; }

    }
    public enum ExtraService
    {
        None,
        Certified,
        Registered
    }

    public enum AddressPlacement
    {
        InsertBlankPage,
        TopFirstPage
    }
}