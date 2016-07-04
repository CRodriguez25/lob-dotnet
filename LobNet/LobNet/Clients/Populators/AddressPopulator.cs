using LobNet.Clients.Addresses;
using RestSharp;

namespace LobNet.Clients.Populators
{
    internal class AddressPopulator : IRequestPopulator
    {
        private readonly Address _address;
        private readonly string _parent;

        public AddressPopulator(Address address)
        {
            _address = address;
        }

        public AddressPopulator(Address address, string parent)
        {
            _address = address;
            _parent = parent;
        }

        public void Populate(IRestRequest request)
        {
            if (_address.Name != null) request.AddParameter(Wrap("name"), _address.Name);
            if (_address.Line1 != null) request.AddParameter(Wrap("address_line1"), _address.Line1);
            if (_address.Line2 != null) request.AddParameter(Wrap("address_line2"), _address.Line2);
            if (_address.City != null) request.AddParameter(Wrap("address_city"), _address.City);
            if (_address.State != null) request.AddParameter(Wrap("address_state"), _address.State);
            if (_address.ZipCode != null) request.AddParameter(Wrap("address_zip"), _address.ZipCode);
            if (_address.Country != null) request.AddParameter(Wrap("address_country"), _address.Country);
        }

        private string Wrap(string property)
        {
            return !string.IsNullOrEmpty(_parent) ? string.Format("{0}[{1}]", _parent, property) : property;
        }
    }
}