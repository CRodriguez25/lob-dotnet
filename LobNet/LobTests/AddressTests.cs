using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LobNet.Clients.Addresses;
using LobNet.Clients.Client;
using LobNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LobTests
{
    [TestClass]
    public class AddressTests : BaseTest
    {
        private AddressClient _addressClient;
        private void Setup()
        {
            _addressClient = new AddressClient(_key);
        }

        [TestMethod]
        public void TestListAddresses()
        {
            Setup();
            var addresses = _addressClient.GetAddressBookEntries(new GetFilterOptions());
            addresses.Entries.Should().NotBeEmpty();
            addresses.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public async Task TestListAddressesAsync()
        {
            Setup();
            var addresses = await _addressClient.GetAddressBookEntriesAsync(new GetFilterOptions());
            addresses.Entries.Should().NotBeEmpty();
            addresses.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void TestListAddressesLimit()
        {
            Setup();
            var addresses = _addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 2
            });

            addresses.Entries.Count.Should().Be(2);

            _addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1,
                Offset = 2
            }).Entries.Count.Should().Be(1);
        }

        [TestMethod]
        public void TestListAddressesFailsWhenLimitOver100()
        {
            Setup();
            Action action = () => _addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1000
            });

            action.ShouldThrow<LobException>();
        }

        [TestMethod]
        public void TestCreateAddress()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                { "key0", value0 }, { "key1", value1 }
            };
            var address = _addressClient.CreateAddressBookEntry(new Address
            {
                Address1 = "185 Berry Street",
                Address2 = "Suite 1510",
                City = "San Francisco",
                State = "CA",
                ZipCode = "94107",
                Country = "US"
            }, new AddressInfo
            {
                Company = "Lob",
                Description = "address response",
                Name = "Lob",
                Email = "support@lob.com",
                Phone = "555-555-5555",
                MetaData = metaData
            });

            address.Name.Should().Be("Lob");
            address.Description.Should().Be("address response");
            address.Company.Should().Be("Lob");
            address.Email.Should().Be("support@lob.com");
            address.Phone.Should().Be("555-555-5555");
            address.DateCreated.Should().NotBe(DateTime.MinValue);
            address.DateModified.Should().NotBe(DateTime.MinValue);
            address.MetaData["key0"].Should().Be(value0);
            address.MetaData["key1"].Should().Be(value1);

            var metaDataResponse = _addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Metadata = metaData,
                Limit = 1
            }).Entries.First();

            metaDataResponse.Id.Should().Be(address.Id);
        }

        [TestMethod]
        public void TestDeleteAddress()
        {
            Setup();
            var id = _addressClient.GetAddressBookEntries(new GetFilterOptions()).Entries.First().Id;
            var response = _addressClient.DeleteAddress(id);
            response.Deleted.Should().BeTrue();
        }
    }
}
