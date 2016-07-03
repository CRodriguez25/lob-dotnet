using LobNet.Clients.PostCards;
using LobNet.Models;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class TargetAddressPopulator : IRequestPopulator
    {
        private readonly TargetAddress _target;
        private readonly string _type;

        public TargetAddressPopulator(TargetAddress target, string type)
        {
            _target = target;
            _type = type;
        }

        public void Populate(IRestRequest request)
        {
            if (_target.Id != null)
            {
                request.AddParameter(_type, _target.Id);
            }
            else
            {
                var addressPopulator = new AddressPopulator(_target.Address, _type);
                addressPopulator.Populate(request);
            }
        }
    }
}