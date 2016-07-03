using System.Collections.Generic;
using LobNet.Models;

namespace LobNet.Clients.BankAccounts
{
    public class BankAccountDefinition
    {
        public string Description { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public string Signatory { get; set; }
        public Dictionary<string, string> MetaData { get; set; }
    }
}