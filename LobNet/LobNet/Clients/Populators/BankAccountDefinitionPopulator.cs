using LobNet.Clients.BankAccounts;
using LobNet.Clients.Letters;
using LobNet.Models;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class BankAccountDefinitionPopulator : IRequestPopulator
    {
        private readonly BankAccountDefinition _bankAccount;

        public BankAccountDefinitionPopulator(BankAccountDefinition bankAccount)
        {
            _bankAccount = bankAccount;
        }

        public void Populate(IRestRequest request)
        {
            if (!string.IsNullOrEmpty(_bankAccount.Description))
                request.AddParameter("description", _bankAccount.Description);

            request.AddParameter("routing_number", _bankAccount.RoutingNumber);
            request.AddParameter("account_number", _bankAccount.AccountNumber);
            request.AddParameter("account_type", _bankAccount.AccountType == AccountType.Company ? "company" : "individual");
            request.AddParameter("signatory", _bankAccount.Signatory);

            if(_bankAccount.MetaData != null) 
                new MetaDataPopulator(_bankAccount.MetaData).Populate(request);
        }
    }
}