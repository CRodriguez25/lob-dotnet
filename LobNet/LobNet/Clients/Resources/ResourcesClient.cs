using System.Collections.Generic;
using System.Threading.Tasks;
using LobNet.Clients.Client;
using LobNet.Models;

namespace LobNet.Clients.Resources
{
    public interface IResourcesClient
    {
        List<Country> GetAllCountries();
        Task<List<Country>> GetAllCountriesAsync();
        List<State> GetAllStates();
        Task<List<State>> GetAllStatesAsync();
    }

    public class ResourcesClient : LobClient, IResourcesClient
    {
        public ResourcesClient(string apiKey) : base(apiKey)
        {
        }

        public List<Country> GetAllCountries()
        {
            var resource = Router.COUNTRIES;
            return Execute<GetAllResult<Country>>(resource, "GET").Data;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            var resource = Router.COUNTRIES;
            var result = await ExecuteAsync<GetAllResult<Country>>(resource, "GET");
            return result.Data;
        }

        public List<State> GetAllStates()
        {
            var resource = Router.STATES;
            return Execute<GetAllResult<State>>(resource, "GET").Data;
        }

        public async Task<List<State>> GetAllStatesAsync()
        {
            var resource = Router.STATES;
            var result = await ExecuteAsync<GetAllResult<State>>(resource, "GET");
            return result.Data;
        }
    }
}