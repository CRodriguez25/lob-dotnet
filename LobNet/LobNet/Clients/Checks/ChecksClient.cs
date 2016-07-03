using System.Threading.Tasks;
using LobNet.Clients.Addresses;
using LobNet.Clients.Client;
using LobNet.Clients.Populators;
using LobNet.Models;

namespace LobNet.Clients.Checks
{
    public class ChecksClient : LobClient
    {
        public ChecksClient(string apiKey) : base(apiKey)
        {
        }

        public Check CreateCheck(CheckDefinition checkDefinition)
        {
            var populator = new CheckDefinitionPopulator(checkDefinition);
            var resource = Router.CHECKS;
            return Execute<Check>(resource, "POST", populator);
        }

        public Task<Check> CreateCheckAsync(CheckDefinition checkDefinition)
        {
            var populator = new CheckDefinitionPopulator(checkDefinition);
            var resource = Router.CHECKS;
            return ExecuteAsync<Check>(resource, "POST", populator);
        }

        public Check GetCheck(string id)
        {
            var resource = GetResourceUrl(Router.CHECKS, id);
            return Execute<Check>(resource, "GET");
        }

        public Task<Check> GetCheckAsync(string id)
        {
            var resource = GetResourceUrl(Router.CHECKS, id);
            return ExecuteAsync<Check>(resource, "GET");
        }

        public GetResult<Check> GetChecks(GetFilterOptions options)
        {
            var resource = Router.CHECKS;
            resource = ApplyGetOptions(resource, options);
            return Execute<GetResult<Check>>(resource, "GET");
        }

        public Task<GetResult<Check>> GetChecksAsync(GetFilterOptions options)
        {
            var resource = Router.CHECKS;
            resource = ApplyGetOptions(resource, options);
            return ExecuteAsync<GetResult<Check>>(resource, "GET");
        }

        public GetResult<Check> GetChecks()
        {
            return GetChecks(new GetFilterOptions());
        }

        public Task<GetResult<Check>> GetChecksAsync()
        {
            return GetChecksAsync(new GetFilterOptions());
        }
    }
}