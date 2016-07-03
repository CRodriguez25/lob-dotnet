using System.Collections.Generic;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class BankAccountVerificationRequestPopulator : IRequestPopulator
    {
        private readonly IEnumerable<int> _amounts;

        public BankAccountVerificationRequestPopulator(IEnumerable<int> amounts)
        {
            _amounts = amounts;
        }

        public void Populate(IRestRequest request)
        {
            foreach (var amount in _amounts)
                request.AddParameter("amounts[]", amount);
        }
    }
}