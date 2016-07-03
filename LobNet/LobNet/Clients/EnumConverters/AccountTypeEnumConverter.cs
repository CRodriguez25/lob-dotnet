using System;
using LobNet.Clients.BankAccounts;
using LobNet.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LobNet.Clients.EnumConverters
{
    internal class AccountTypeEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var value = (string) reader.Value;

            switch (value)
            {
                case "company":
                    return AccountType.Company;
                default:
                    return AccountType.Individual;
            }
        }
    }
}