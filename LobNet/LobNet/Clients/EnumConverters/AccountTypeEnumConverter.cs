using System;
using LobNet.Clients.BankAccounts;
using LobNet.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LobNet.Clients.EnumConverters
{
    internal class AccountTypeEnumConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.Equals(AccountType.Company))
            {
                value = "company";
            }
            else if (value.Equals(AccountType.Individual))
            {
                value = "individual";
            }

            base.WriteJson(writer, value, serializer);
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var value = (string) reader.Value;

            switch (value)
            {
                case "company":
                    return AccountType.Company;
                case "individual":
                    return AccountType.Individual;
                default:
                    return AccountType.Individual;
            }
        }
    }
}