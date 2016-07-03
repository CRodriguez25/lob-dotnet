using System.Threading.Tasks;
using LobNet.Clients.Client;
using LobNet.Clients.Populators;
using LobNet.Models;

namespace LobNet.Clients.Addresses
{
    public interface IAddressClient
    {
        Task<AddressBookEntry> CreateAddressBookEntryAsync(Address address, AddressInfo addressInfo);
        AddressBookEntry CreateAddressBookEntry(Address address, AddressInfo addressInfo);
        Task<AddressBookEntry> RetrieveAddressBookEntryAsync(string id);
        AddressBookEntry RetrieveAddressBookEntry(string id);
        GetResult<AddressBookEntry> GetAddressBookEntries(GetFilterOptions getOptions);
        Task<GetResult<AddressBookEntry>> GetAddressBookEntriesAsync(GetFilterOptions getOptions);
        Task<DeleteResult> DeleteAddressAsync(string id);
        DeleteResult DeleteAddress(string id);
        Task<VerifyAddressResponse> VerifyAddressAsync(Address address);
        VerifyAddressResponse VerifyAddress(Address address);
    }

    public class AddressClient : LobClient, IAddressClient
    {
        public AddressClient(string apiKey) : base(apiKey)
        {
        }

        #region Create Address Book Entry

        public Task<AddressBookEntry> CreateAddressBookEntryAsync(Address address, AddressInfo addressInfo)
        {
            var populator = new CreateAddressPopulator(address, addressInfo);
            return ExecuteAsync<AddressBookEntry>(Router.ADDRESSES, "POST", populator);
        }

        public AddressBookEntry CreateAddressBookEntry(Address address, AddressInfo addressInfo)
        {
            var populator = new CreateAddressPopulator(address, addressInfo);
            return Execute<AddressBookEntry>(Router.ADDRESSES, "POST", populator);
        }

        #endregion

        #region Retrieve Address Book Entry

        public Task<AddressBookEntry> RetrieveAddressBookEntryAsync(string id)
        {
            var resource = GetResourceUrl(Router.ADDRESSES, id);
            return ExecuteAsync<AddressBookEntry>(resource, "GET");
        }

        public AddressBookEntry RetrieveAddressBookEntry(string id)
        {
            var resource = GetResourceUrl(Router.ADDRESSES, id);
            return Execute<AddressBookEntry>(resource, "GET");
        }

        #endregion

        #region Get Address Book Entries

        public GetResult<AddressBookEntry> GetAddressBookEntries(GetFilterOptions getOptions)
        {
            var resource = Router.ADDRESSES;
            resource = ApplyGetOptions(resource, getOptions);
            var result = Execute<GetResult<AddressBookEntry>>(resource, "GET");
            return result;
        }

        public Task<GetResult<AddressBookEntry>> GetAddressBookEntriesAsync(GetFilterOptions getOptions)
        {
            var resource = Router.ADDRESSES;
            resource = ApplyGetOptions(resource, getOptions);
            return ExecuteAsync<GetResult<AddressBookEntry>>(resource, "GET");
        }


        public GetResult<AddressBookEntry> GetAddressBookEntries()
        {
            return GetAddressBookEntries(new GetFilterOptions());
        }

        public Task<GetResult<AddressBookEntry>> GetAddressBookEntriesAsync()
        {
            return GetAddressBookEntriesAsync(new GetFilterOptions());
        }

        #endregion

        #region Delete Address

        public DeleteResult DeleteAddress(string id)
        {
            var resource = GetResourceUrl(Router.ADDRESSES, id);
            return Execute<DeleteResult>(resource, "DELETE");
        }

        public Task<DeleteResult> DeleteAddressAsync(string id)
        {
            var resource = GetResourceUrl(Router.ADDRESSES, id);
            var result = ExecuteAsync<DeleteResult>(resource, "DELETE");
            return result;
        }

        #endregion

        #region Verify Address

        public Task<VerifyAddressResponse> VerifyAddressAsync(Address address)
        {
            var populator = new AddressPopulator(address);
            return ExecuteAsync<VerifyAddressResponse>(Router.VERIFY, "POST", populator);
        }

        public VerifyAddressResponse VerifyAddress(Address address)
        {
            var populator = new AddressPopulator(address);
            return Execute<VerifyAddressResponse>(Router.VERIFY, "POST", populator);
        }

        #endregion


    }
}