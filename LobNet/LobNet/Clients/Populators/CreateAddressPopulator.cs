using LobNet.Clients.Addresses;
using LobNet.Clients.Letters;
using LobNet.Models;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class CreateAddressPopulator : IRequestPopulator
    {
        private readonly Address _address;
        private readonly AddressInfo _addressInfo;

        public CreateAddressPopulator(Address address, AddressInfo addressInfo)
        {
            _address = address;
            _addressInfo = addressInfo;
        }

        public void Populate(IRestRequest request)
        {
            var addressPopulator = new AddressPopulator(_address);
            addressPopulator.Populate(request);
            if (_addressInfo.Description != null) request.AddParameter("description", _addressInfo.Description);
            if (_addressInfo.MetaData != null)
            {
                var populator = new MetaDataPopulator(_addressInfo.MetaData);
                populator.Populate(request);
            }

            if (_addressInfo.Name != null) request.AddParameter("name", _addressInfo.Name);
            if (_addressInfo.Phone != null) request.AddParameter("phone", _addressInfo.Phone);
            if (_addressInfo.Email != null) request.AddParameter("email", _addressInfo.Email);
            if (_addressInfo.Company != null) request.AddParameter("company", _addressInfo.Company);
        }
    }
}