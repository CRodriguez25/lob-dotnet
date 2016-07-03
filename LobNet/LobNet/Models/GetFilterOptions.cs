using System;
using System.Collections.Generic;

namespace LobNet.Models
{
    public class GetFilterOptions
    {
        public int Limit = 10;
        public int Offset = 0;
        public bool IncludeTotalCount { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public DateTime? GreaterThanDate { get; set; }
        public DateTime? LessThanDate { get; set; }
        public DateTime? GreaterThanOrEqualToDate { get; set; }
        public DateTime? LessThanOrEqualToDate { get; set; }
    }
}