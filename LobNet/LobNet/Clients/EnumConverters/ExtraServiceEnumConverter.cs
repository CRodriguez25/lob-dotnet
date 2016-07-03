using System;
using LobNet.Clients.Letters;
using LobNet.Models;
using Newtonsoft.Json;

namespace LobNet.Clients.EnumConverters
{
    internal class ExtraServiceEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
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