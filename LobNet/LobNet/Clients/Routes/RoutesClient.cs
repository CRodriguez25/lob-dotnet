using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using LobNet.Clients.Client;
using Newtonsoft.Json;

namespace LobNet.Clients.Routes
{
    public interface IRoutesClient
    {
        ZipCodeRoutes GetRoutesForZipCode(ZipCodeRoute route);
        Task<ZipCodeRoutes> GetRoutesForZipCodeAsync(ZipCodeRoute route);
        IEnumerable<ZipCodeRoutes> GetRoutesForZipCodes(IEnumerable<ZipCodeRoute> filters);
        Task<IEnumerable<ZipCodeRoutes>> GetRoutesForZipCodesAsync(IEnumerable<ZipCodeRoute> filters);
    }

    public class RoutesClient : LobClient, IRoutesClient
    {
        public RoutesClient(string apiKey) : base(apiKey)
        {
        }

        public ZipCodeRoutes GetRoutesForZipCode(ZipCodeRoute route)
        {
            var resource = Router.ROUTES;
            resource = GetResourceUrl(resource, route.ZipCode);
            if (route.Route != null) resource += "-" + route.Route;
            return Execute<ZipCodeRoutes>(resource, "GET");
        }

        public Task<ZipCodeRoutes> GetRoutesForZipCodeAsync(ZipCodeRoute route)
        {
            var resource = Router.ROUTES;
            resource = GetResourceUrl(resource, route.ZipCode);
            if (route.Route != null) resource += "-" + route.Route;
            return ExecuteAsync<ZipCodeRoutes>(resource, "GET");
        }

        public IEnumerable<ZipCodeRoutes> GetRoutesForZipCodes(IEnumerable<ZipCodeRoute> filters)
        {
            var resource = Router.ROUTES;
            var filterList = new NameValueCollection();
            resource = ApplyFilters(filters, filterList, resource);
            return Execute<GetZipCodesResult>(resource, "GET").Data;
        }

        public async Task<IEnumerable<ZipCodeRoutes>> GetRoutesForZipCodesAsync(IEnumerable<ZipCodeRoute> filters)
        {
            var resource = Router.ROUTES;
            var filterList = new NameValueCollection();
            resource = ApplyFilters(filters, filterList, resource);
            var result = await ExecuteAsync<GetZipCodesResult>(resource, "GET");
            return result.Data;
        }

        private static string ApplyFilters(IEnumerable<ZipCodeRoute> filters, NameValueCollection filterList,
            string resource)
        {
            foreach (var filter in filters)
            {
                filterList.Add("zip_codes", filter.ToString());
            }

            resource = resource + ToQueryString(filterList);
            return resource;
        }
    }

    internal class GetZipCodesResult
    {
        [JsonProperty("data")]
        public List<ZipCodeRoutes> Data { get; set; }
    }
}