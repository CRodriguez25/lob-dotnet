using System;
using LobNet.Clients.Areas;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LobNet.Clients.EnumConverters
{
    public class TargetTypeConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;

            switch (value)
            {
                case "residential":
                    return TargetType.Residential;
                default:
                    return TargetType.All;
            }
        }
    }
}