using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LobNet.Clients.Resources;

namespace LobTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using LobNet.Clients.Addresses;
    using LobNet.Clients.BankAccounts;
    using LobNet.Clients.Checks;
    using LobNet.Clients.Client;
    using LobNet.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    namespace LobTests
    {
        [TestClass]
        public class ResourcesTests : BaseTest
        {
            private ResourcesClient _client;

            private void Setup()
            {
                _client = new ResourcesClient(_key);
            }

            [TestMethod]
            public void TestCountries()
            {
                Setup();
                var result = _client.GetAllCountries();
                result.Count().Should().BeGreaterThan(0);
            }

            [TestMethod]
            public async Task TestCountriesAsync()
            {
                Setup();
                var result = await _client.GetAllCountriesAsync();
                result.Count().Should().BeGreaterThan(0);
            }

            [TestMethod]
            public void TestStates()
            {
                Setup();
                var result = _client.GetAllStates();
                result.Count().Should().BeGreaterThan(0);
            }

            [TestMethod]
            public async Task TestStatesAsync()
            {
                Setup();
                var result = await _client.GetAllStatesAsync();
                result.Count().Should().BeGreaterThan(0);
            }
        }
    }
}
