using System.Collections.Generic;
using LobNet.Models;
using Newtonsoft.Json;

namespace LobNet.Clients.Routes
{
    public class ZipCodeRoutes
    {
        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }
    }
}