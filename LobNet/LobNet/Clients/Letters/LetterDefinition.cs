using System.Collections.Generic;
using LobNet.Clients.PostCards;
using LobNet.Models;

namespace LobNet.Clients.Letters
{
    public class LetterDefinition
    {
        public TargetAddress ToAddress { get; set; }

        public TargetAddress FromAddress { get; set; }
        public string Description { get; set; }
        public bool Color { get; set; }
        public LobImageFile File { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public Dictionary<string, string> MetaData { get; set; }
        public bool DoubleSided { get; set; }
        public AddressPlacement AddressPlacement { get; set; }
        public ExtraService ExtraService { get; set; }
        public ReturnEnvelope ReturnEnvelope { get; set; }

        public LetterDefinition()
        {
            DoubleSided = true;
            AddressPlacement = AddressPlacement.TopFirstPage;
        }
    }

    public class ReturnEnvelope
    {
        public int PerforatedPage { get; set; }

        public ReturnEnvelope(int perforatedPage)
        {
            PerforatedPage = perforatedPage;
        }
    }
}