using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LobNet.Clients.Addresses;
using LobNet.Clients.BankAccounts;
using LobNet.Clients.Checks;
using LobNet.Clients.Client;
using LobNet.Clients.Routes;
using LobNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LobTests
{
    [TestClass]
    public class RoutesTests : BaseTest
    {
        private RoutesClient _client;
        private void Setup()
        {
            _client = new RoutesClient(_key);
        }

        [TestMethod]
        public void TestZipCodeRoute()
        {
            Setup();
            var response = _client.GetRoutesForZipCodes(new List<ZipCodeRoute>
            {
                {new ZipCodeRoute {ZipCode = "94158"}},
                {new ZipCodeRoute {ZipCode = "60031"}}
            }).ToList();

            response.Should().NotBeEmpty();
            response.First().Routes.Should().NotBeEmpty();
            response.First().Routes.First().Residential.Should().NotBe(null);

            var route = _client.GetRoutesForZipCode(new ZipCodeRoute
            {
                ZipCode = "94158",
                Route = "C002"
            });

            route.Routes.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task TestZipCodeRouteAsync()
        {
            Setup();
            var response = (await _client.GetRoutesForZipCodesAsync(new List<ZipCodeRoute>
            {
                {new ZipCodeRoute {ZipCode = "94158"}},
                {new ZipCodeRoute {ZipCode = "60031"}}
            })).ToList();

            response.Should().NotBeEmpty();
            response.First().Routes.Should().NotBeEmpty();
            response.First().Routes.First().Residential.Should().NotBe(null);

            var route = await _client.GetRoutesForZipCodeAsync(new ZipCodeRoute
            {
                ZipCode = "94158",
                Route = "C002"
            });

            route.Routes.Should().NotBeEmpty();
        }
    }
}
