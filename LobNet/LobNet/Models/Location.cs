using LobNet.Clients.Addresses;

namespace LobNet.Models
{
    public class Location
    {
        public string AddressId { get; private set; }
        public Address Address { get; private set; }

        /// <summary>
        /// Constructor that takes in the actual address to send to/from. Will be created for you and
        /// added to address book.
        /// </summary>
        /// <param name="address">The address</param>
        public Location(Address address)
        {
            Address = address;
        }

        /// <summary>
        /// Constructor that takes in ID of existing entry in address book. 
        /// </summary>
        /// <param name="addressId">ID of existing address book entry</param>
        public Location(string addressId)
        {
            AddressId = addressId;
        }
    }
}