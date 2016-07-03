using System;
using LobNet.Clients.Letters;
using LobNet.Models;
using Newtonsoft.Json;

namespace LobNet.Clients.EnumConverters
{
    internal class AddressPlacementEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;

            switch (value)
            {
                case "top_first_page":
                    return AddressPlacement.TopFirstPage;
                default:
                    return AddressPlacement.InsertBlankPage;
            }
        }
    }
}