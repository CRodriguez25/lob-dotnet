using Newtonsoft.Json;

namespace LobNet.Models
{
    public class Route
    {
        [JsonProperty("route")]
        public string RouteId { get; set; }

        [JsonProperty("residential")]
        public int? Residential { get; set; }

        [JsonProperty("business")]
        public int? Business { get; set; }

        [JsonProperty("median_income")]
        public int? MedianIncome { get; set; }

        [JsonProperty("age_20_24")]
        public int? Age20To24 { get; set; }

        [JsonProperty("age_25_34")]
        public int? Age25To34 { get; set; }

        [JsonProperty("age_35_44")]
        public int? Age35To44 { get; set; }

        [JsonProperty("age_45_54")]
        public int? Age45To54 { get; set; }

        [JsonProperty("age_55_64")]
        public int? Age55To64 { get; set; }

        [JsonProperty("age_65_74")]
        public int? Age65To74 { get; set; }

        [JsonProperty("age_75_84")]
        public int? Age75To84 { get; set; }

        [JsonProperty("age_lt_19")]
        public int? AgeLessThan19 { get; set; }

        [JsonProperty("age_gt_85")]
        public int? AgeGreaterThan85 { get; set; }

        [JsonProperty("median_age")]
        public int? MedianAge { get; set; }

        [JsonProperty("avg_household_size")]
        public decimal? AverageHouseholdSize { get; set; }
    }
}