using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LobNet.Clients.Addresses;
using LobNet.Clients.Client;
using LobNet.Clients.Populators;
using LobNet.Models;

namespace LobNet.Clients.BankAccounts
{
    public interface IBankAccountsClient
    {
        BankAccount CreateBankAccount(BankAccountDefinition bankAccount);
        Task<BankAccount> CreateBankAccountAsync(BankAccountDefinition bankAccount);
        BankAccount GetBankAccount(string id);
        Task<BankAccount> GetBankAccountAsync(string id);
        DeleteResult DeleteBankAccount(string id);
        Task<DeleteResult> DeleteBankAccountAsync(string id);
        BankAccount Verify(string id, IEnumerable<int> amounts);
        Task<BankAccount> VerifyAsync(string id, IEnumerable<int> amounts);
        GetResult<BankAccount> GetBankAccounts(GetFilterOptions options);
        Task<GetResult<BankAccount>> GetBankAccountsAsync(GetFilterOptions options);
    }

    public class BankAccountsClient : LobClient, IBankAccountsClient
    {
        private readonly string _resource;

        public BankAccountsClient(string apiKey) : base(apiKey)
        {
            _resource = Router.BANKACCOUNTS;
        }

        public BankAccount CreateBankAccount(BankAccountDefinition bankAccount)
        {
            var populator = new BankAccountDefinitionPopulator(bankAccount);
            return Execute<BankAccount>(_resource, "POST", populator);
        }

        public Task<BankAccount> CreateBankAccountAsync(BankAccountDefinition bankAccount)
        {
            var populator = new BankAccountDefinitionPopulator(bankAccount);
            return ExecuteAsync<BankAccount>(_resource, "POST", populator);
        }

        public BankAccount GetBankAccount(string id)
        {
            var resource = GetResourceUrl(_resource, id);
            return Execute<BankAccount>(resource, "GET");
        }

        public Task<BankAccount> GetBankAccountAsync(string id)
        {
            var resource = GetResourceUrl(_resource, id);
            return ExecuteAsync<BankAccount>(resource, "GET");
        }

        public DeleteResult DeleteBankAccount(string id)
        {
            var resource = GetResourceUrl(_resource, id);
            return Execute<DeleteResult>(resource, "DELETE");
        }

        public Task<DeleteResult> DeleteBankAccountAsync(string id)
        {
            var resource = GetResourceUrl(_resource, id);
            return ExecuteAsync<DeleteResult>(resource, "DELETE");
        }

        public BankAccount Verify(string id, IEnumerable<int> amounts)
        {
            var resource = GetResourceUrl(_resource, id, "verify");
            var bankAccountVerifyPopulator = new BankAccountVerificationRequestPopulator(amounts);
            return Execute<BankAccount>(resource, "POST", bankAccountVerifyPopulator);
        }

        public Task<BankAccount> VerifyAsync(string id, IEnumerable<int> amounts)
        {
            var resource = GetResourceUrl(_resource, id, "verify");
            var bankAccoutnVerifyPopulator = new BankAccountVerificationRequestPopulator(amounts);
            return ExecuteAsync<BankAccount>(resource, "POST", bankAccoutnVerifyPopulator);
        }

        public GetResult<BankAccount> GetBankAccounts(GetFilterOptions options)
        {
            var path = ApplyGetOptions(_resource, options);
            return Execute<GetResult<BankAccount>>(path, "GET");
        }

        public Task<GetResult<BankAccount>> GetBankAccountsAsync(GetFilterOptions options)
        {
            var path = ApplyGetOptions(_resource, options);
            return ExecuteAsync<GetResult<BankAccount>>(path, "GET");
        }

        public GetResult<BankAccount> GetBankAccounts()
        {
            return GetBankAccounts(new GetFilterOptions());
        }

        public Task<GetResult<BankAccount>> GetBankAccountsAsync()
        {
            return GetBankAccountsAsync(new GetFilterOptions());
        }
    }
}
