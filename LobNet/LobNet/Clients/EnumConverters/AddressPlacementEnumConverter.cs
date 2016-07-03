using System;
using LobNet.Clients.Letters;
using LobNet.Models;
using Newtonsoft.Json;

namespace LobNet.Clients.EnumConverters
{
    internal class AddressPlacementEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.Equals(AddressPlacement.InsertBlankPage))
            {
                value = "insert_blank_page";
            }
            else if (value.Equals(AddressPlacement.TopFirstPage))
            {
                value = "top_first_page";
            }

            base.WriteJson(writer, value, serializer);
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;

            switch (value)
            {
                case "top_first_page":
                    return AddressPlacement.TopFirstPage;
                case "insert_blank_page":
                    return AddressPlacement.InsertBlankPage;
                default:
                    return AddressPlacement.TopFirstPage;
            }
        }
    }
}