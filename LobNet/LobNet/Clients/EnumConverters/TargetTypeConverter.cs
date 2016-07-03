using System;
using LobNet.Clients.Areas;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LobNet.Clients.EnumConverters
{
    public class TargetTypeConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.Equals(TargetType.All))
            {
                value = "all";
            }
            else if (value.Equals(TargetType.Residential))
            {
                value = "residential";
            }

            base.WriteJson(writer, value, serializer);
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;

            switch (value)
            {
                case "residential":
                    return TargetType.Residential;
                case "all":
                    return TargetType.All;
                default:
                    return TargetType.All;
            }
        }
    }
}