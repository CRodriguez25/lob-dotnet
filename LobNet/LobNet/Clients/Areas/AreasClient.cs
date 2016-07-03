using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LobNet.Clients.Addresses;
using LobNet.Clients.Client;
using LobNet.Clients.EnumConverters;
using LobNet.Clients.Populators;
using LobNet.Clients.PostCards;
using LobNet.Clients.Routes;
using LobNet.Models;
using Newtonsoft.Json;

namespace LobNet.Clients.Areas
{
    public interface IAreasClient
    {
        Area CreateAreaMailing(AreaMailingDefinition definition);
        Task<Area> CreateAreaMailingAsync(AreaMailingDefinition definition);
        Area GetAreaMailing(string id);
        Task<Area> GetAreaMailingAsync(string id);
        GetResult<Area> GetAreaMailings(GetFilterOptions options);
        Task<GetResult<Area>> GetAreaMailingsAsync(GetFilterOptions options);
    }

    public class AreasClient : LobClient, IAreasClient
    {
        public AreasClient(string apiKey) : base(apiKey)
        {
        }

        public Area CreateAreaMailing(AreaMailingDefinition definition)
        {
            var populator = new AreaMailingDefinitionPopulator(definition);
            var resource = Router.AREAS;
            return Execute<Area>(resource, "POST", populator);
        }

        public Task<Area> CreateAreaMailingAsync(AreaMailingDefinition definition)
        {
            var populator = new AreaMailingDefinitionPopulator(definition);
            var resource = Router.AREAS;
            return ExecuteAsync<Area>(resource, "POST", populator);
        }

        public Area GetAreaMailing(string id)
        {
            var resource = GetResourceUrl(Router.AREAS, id);
            return Execute<Area>(resource, "GET");
        }

        public Task<Area> GetAreaMailingAsync(string id)
        {
            var resource = GetResourceUrl(Router.AREAS, id);
            return ExecuteAsync<Area>(resource, "GET");
        }

        public GetResult<Area> GetAreaMailings(GetFilterOptions options)
        {
            var resource = ApplyGetOptions(Router.AREAS, options);
            return Execute<GetResult<Area>>(resource, "GET");
        }

        public GetResult<Area> GetAreaMailings()
        {
            return GetAreaMailings(new GetFilterOptions());
        }

        public Task<GetResult<Area>> GetAreaMailingsAsync(GetFilterOptions options)
        {
            var resource = ApplyGetOptions(Router.AREAS, options);
            return ExecuteAsync<GetResult<Area>>(resource, "GET");
        }

        public Task<GetResult<Area>> GetAreaMailingsAsync()
        {
            return GetAreaMailingsAsync(new GetFilterOptions());
        }
    }

    public class Area
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> MetaData { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("target_type")]
        [JsonConverter(typeof (TargetTypeConverter))]
        public TargetType TargetType { get; set; }

        [JsonProperty("addresses")]
        public int Addresses { get; set; }

        [JsonProperty("zip_codes")]
        public List<ZipCodeRoutes> ZipCodes { get; set; }

        [JsonProperty("thumbnails")]
        public List<Thumbnails> Thumbnails { get; set; }

        [JsonProperty("trackings")]
        public List<TrackingInformation> Trackings { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public DateTime DateModified { get; set; }

        [JsonProperty("expected_delivery_date")]
        public DateTime ExpectedDeliveryDate { get; set; }
    }
}