using System.Collections.Generic;
using LobNet.Models;

namespace LobNet.Clients.PostCards
{
    public class PostCardDefinition
    {
        public TargetAddress ToAddress { get; set; }
        public TargetAddress FromAddress { get; set; }
        public LobImageFile Front { get; set; }
        public LobImageFile Back { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public Dictionary<string, string> MetaData { get; set; }
        public string Description { get; set; }
        public PostCardSize PostCardSize { get; set; }

        public PostCardDefinition()
        {
            PostCardSize = PostCardSize.FourBySix;
        }
    }

    public enum PostCardSize
    {
        FourBySix,
        SixByEleven
    }
}