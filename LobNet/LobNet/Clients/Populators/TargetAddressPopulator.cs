using LobNet.Clients.PostCards;
using LobNet.Models;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class TargetAddressPopulator : IRequestPopulator
    {
        private readonly Location _target;
        private readonly string _type;

        public TargetAddressPopulator(Location target, string type)
        {
            _target = target;
            _type = type;
        }

        public void Populate(IRestRequest request)
        {
            if (_target.AddressId != null)
            {
                request.AddParameter(_type, _target.AddressId);
            }
            else
            {
                var addressPopulator = new AddressPopulator(_target.Address, _type);
                addressPopulator.Populate(request);
            }
        }
    }
}