using System.Collections.Generic;
using LobNet.Clients.PostCards;
using LobNet.Models;

namespace LobNet.Clients.Checks
{
    public class CheckDefinition
    {
        public string Description { get; set; }
        public Location ToAddress { get; set; }
        public Location FromAddress { get; set; }
        public string BankAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public int? CheckNumber { get; set; }
        public LobImageFile Logo { get; set; }
        public string Message { get; set; }
        public LobImageFile CheckBottom { get; set; }
        public LobImageFile Attachment { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public MailType MailType { get; set; }
        public Dictionary<string, string> MetaData { get; set; }
    }

    public enum MailType
    {
        USPSFirstClass,
        UPSNextDayAir
    }
}