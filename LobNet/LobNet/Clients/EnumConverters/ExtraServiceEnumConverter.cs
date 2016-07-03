using System;
using LobNet.Clients.Letters;
using LobNet.Models;
using Newtonsoft.Json;

namespace LobNet.Clients.EnumConverters
{
    internal class ExtraServiceEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.Equals(ExtraService.None))
            {
                value = null;
            } else if (value.Equals(ExtraService.Certified))
            {
                value = "certified";
            }
            else if (value.Equals(ExtraService.Registered))
            {
                value = "registered";
            }
            base.WriteJson(writer, value, serializer);
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;

            switch (value)
            {
                case "certified":
                    return ExtraService.Certified;
                case "registered":
                    return ExtraService.Registered;
                default:
                    return ExtraService.None;
            }
        }
    }
}