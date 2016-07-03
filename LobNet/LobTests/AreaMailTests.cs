using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LobNet.Clients.Areas;
using LobNet.Clients.Client;
using LobNet.Clients.Routes;
using LobNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LobTests
{
    [TestClass]
    public class AreaMailTests : BaseTest
    {
        private AreasClient _areasClient;
        private void Setup()
        {
            _areasClient = new AreasClient(_key);
        }

        [TestMethod]
        public void TestCreateArea()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                { "key0", value0 }, { "key1", value1 }
            };

            var response = _areasClient.CreateAreaMailing(new AreaMailingDefinition
            {
                Description = "area mail",
                Front = new LobImageFile("https://s3-us-west-2.amazonaws.com/lob-assets/areafront.pdf", false),
                Back = new LobImageFile("https://s3-us-west-2.amazonaws.com/lob-assets/areaback.pdf", false),
                Routes = new List<ZipCodeRoute>()
                {
                    new ZipCodeRoute
                    {
                        ZipCode = "94158"
                    },
                    new ZipCodeRoute
                    {
                        ZipCode = "60031"
                    }
                },
                TargetType = TargetType.All,
                MetaData = metaData
            });

            response.Description.Should().Be("area mail");
            response.URL.Should().NotBeEmpty();
            response.ExpectedDeliveryDate.Should().NotBe(DateTime.MinValue);
            response.DateCreated.Should().NotBe(DateTime.MinValue);
            response.DateModified.Should().NotBe(DateTime.MinValue);
            response.Addresses.Should().BeGreaterThan(0);
            response.Price.Should().BeGreaterThan(0);
            response.TargetType.Should().Be(TargetType.All);
            response.Thumbnails.Should().NotBeEmpty();
            response.ZipCodes.Should().NotBeEmpty();
            response.MetaData["key0"].Should().Be(value0);
            response.MetaData["key1"].Should().Be(value1);

            var metaDataResponse = _areasClient.GetAreaMailings(new GetFilterOptions
            {
                Metadata = metaData,
                Limit = 1
            }).Entries.First();

            metaDataResponse.Id.Should().Be(response.Id);
            var getResponse = _areasClient.GetAreaMailing(metaDataResponse.Id);
            getResponse.Id.Should().Be(metaDataResponse.Id);
        }

        [TestMethod]
        public async Task TestCreateAreaAsync()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                { "key0", value0 }, { "key1", value1 }
            };

            var response = await _areasClient.CreateAreaMailingAsync(new AreaMailingDefinition
            {
                Description = "area mail",
                Front = new LobImageFile("https://s3-us-west-2.amazonaws.com/lob-assets/areafront.pdf", false),
                Back = new LobImageFile("https://s3-us-west-2.amazonaws.com/lob-assets/areaback.pdf", false),
                Routes = new List<ZipCodeRoute>()
                {
                    new ZipCodeRoute
                    {
                        ZipCode = "94158"
                    },
                    new ZipCodeRoute
                    {
                        ZipCode = "60031"
                    }
                },
                TargetType = TargetType.All,
                MetaData = metaData
            });

            response.Description.Should().Be("area mail");
            response.URL.Should().NotBeEmpty();
            response.ExpectedDeliveryDate.Should().NotBe(DateTime.MinValue);
            response.DateCreated.Should().NotBe(DateTime.MinValue);
            response.DateModified.Should().NotBe(DateTime.MinValue);
            response.Addresses.Should().BeGreaterThan(0);
            response.Price.Should().BeGreaterThan(0);
            response.TargetType.Should().Be(TargetType.All);
            response.Thumbnails.Should().NotBeEmpty();
            response.ZipCodes.Should().NotBeEmpty();
            response.MetaData["key0"].Should().Be(value0);
            response.MetaData["key1"].Should().Be(value1);

            var metaDataResponseTask = await _areasClient.GetAreaMailingsAsync(new GetFilterOptions
            {
                Metadata = metaData,
                Limit = 1
            });
            
            var metaDataResponse = metaDataResponseTask.Entries.First();
            metaDataResponse.Id.Should().Be(response.Id);

            var getResponse =  await _areasClient.GetAreaMailingAsync(metaDataResponse.Id);
            getResponse.Id.Should().Be(metaDataResponse.Id);
        }

        [TestMethod]
        public void TestListAreas()
        {
            Setup();
            var response = _areasClient.GetAreaMailings();
            response.Entries.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public async Task TestListAreasAsync()
        {
            Setup();
            var response = await _areasClient.GetAreaMailingsAsync();
            response.Entries.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void TestListAreasLimit()
        {
            Setup();
            var response = _areasClient.GetAreaMailings(new GetFilterOptions
            {
                Limit = 2
            });

            response.Entries.Count().Should().Be(2);
            _areasClient.GetAreaMailings(new GetFilterOptions
            {
                Limit = 1,
                Offset = 2
            }).Entries.Count().Should().Be(1);
        }

        [TestMethod]
        public void TestListAreasFail()
        {
            Setup();
            Action action = () => _areasClient.GetAreaMailings(new GetFilterOptions
            {
                Limit = 1000
            });
         
            action.ShouldThrow<LobException>();
        }
    }
}
