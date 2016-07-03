using LobNet.Clients.Addresses;

namespace LobNet.Models
{
    public class TargetAddress
    {
        public string Id { get; private set; }
        public Address Address { get; private set; }

        /// <summary>
        /// Constructor that takes in the actual address to send to/from. Will be created for you and
        /// added to address book.
        /// </summary>
        /// <param name="address">The address</param>
        public TargetAddress(Address address)
        {
            Address = address;
        }

        /// <summary>
        /// Constructor that takes in ID of existing entry in address book. 
        /// </summary>
        /// <param name="id">ID of existing address book entry</param>
        public TargetAddress(string id)
        {
            Id = id;
        }
    }
}