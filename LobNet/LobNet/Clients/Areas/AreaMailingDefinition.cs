using System.Collections.Generic;
using LobNet.Clients.PostCards;
using LobNet.Clients.Routes;
using LobNet.Models;

namespace LobNet.Clients.Areas
{
    public class AreaMailingDefinition
    {
        public string Description { get; set; }
        public IEnumerable<ZipCodeRoute> Routes { get; set; }
        public TargetType TargetType { get; set; }
        public LobImageFile Front { get; set; }
        public LobImageFile Back { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public Dictionary<string, string> MetaData { get; set; }

    }

    public enum TargetType
    {
        All,
        Residential
    }
}