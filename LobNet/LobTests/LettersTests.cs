using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LobNet.Clients.Addresses;
using LobNet.Clients.BankAccounts;
using LobNet.Clients.Checks;
using LobNet.Clients.Client;
using LobNet.Clients.Letters;
using LobNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LobTests
{
    [TestClass]
    public class LettersTests : BaseTest
    {
        private LettersClient _client;

        private void Setup()
        {
            _client = new LettersClient(_key);
        }

        [TestMethod]
        public void TestListLetters()
        {
            Setup();
            var responseList = _client.GetLetters().Entries;
            var response = responseList.First();
            _client.GetLetter(response.Id).Id.Should().Be(response.Id);
        }

        [TestMethod]
        public async Task TestListLettersAsync()
        {
            Setup();
            var responseList = (await _client.GetLettersAsync()).Entries;
            var response = responseList.First();
            (await _client.GetLetterAsync(response.Id)).Id.Should().Be(response.Id);
        }

        [TestMethod]
        public void TestListLettersLimit()
        {
            Setup();
            _client.GetLetters(new GetFilterOptions
            {
                Limit = 2
            }).Entries.Count().Should().Be(2);

            _client.GetLetters(new GetFilterOptions
            {
                Limit = 1,
                Offset = 2
            }).Entries.Count().Should().Be(1);
        }

        [TestMethod]
        public async Task TestListLettersLimitAsync()
        {
            Setup();
            (await _client.GetLettersAsync(new GetFilterOptions
            {
                Limit = 2
            })).Entries.Count().Should().Be(2);

            (await _client.GetLettersAsync(new GetFilterOptions
            {
                Limit = 1,
                Offset = 2
            })).Entries.Count().Should().Be(1);
        }

        [TestMethod]
        public void TestCreateLetter()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                { "key0", value0 }, { "key1", value1 }
            };

            var data = new Dictionary<string, string> {{"name", "carlos"}};
            var addressClient = new AddressClient(_key);
            var address = addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1
            }).Entries.First();
            const string file = "<div>Test HTML</div>";
            var letter = _client.CreateLetter(new LetterDefinition
            {
                ToAddress = new TargetAddress(address.Id),
                FromAddress = new TargetAddress(address.Id),
                Description = "letter",
                File = new LobImageFile(file, false),
                Data = data,
                MetaData = metaData,
                Color = true,
                AddressPlacement = AddressPlacement.TopFirstPage,
                DoubleSided = false
            });

            letter.ToAddress.Id.Should().Be(address.Id);
            letter.FromAddress.Id.Should().Be(address.Id);
            letter.Description.Should().Be("letter");
            letter.ExpectedDeliveryDate.Should().NotBe(DateTime.MinValue);
            letter.URL.Should().NotBeNull();
            letter.Color.Should().BeTrue();
            letter.DoubleSided.Should().BeFalse();
            letter.AddressPlacement.Should().Be(AddressPlacement.TopFirstPage);
            letter.ExtraService.Should().Be(ExtraService.None);
            letter.ReturnEnvelope.Should().BeFalse();
            letter.PerforatedPage.Should().Be(null);
            letter.Thumbnails.Count().Should().Be(1);
            letter.Tracking.Should().NotBeNull();
            letter.MetaData["key0"].Should().Be(value0);
            letter.MetaData["key1"].Should().Be(value1);

            var metaDataResponse = _client.GetLetters(new GetFilterOptions
            {
                Metadata = metaData
            }).Entries.First();

            metaDataResponse.Id.Should().Be(letter.Id);
        }

        [TestMethod]
        public async Task TestCreateLetterAsync()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                { "key0", value0 }, { "key1", value1 }
            };

            var data = new Dictionary<string, string> { { "name", "carlos" } };
            var addressClient = new AddressClient(_key);
            var address = addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1
            }).Entries.First();
            const string file = "<div>Test HTML</div>";
            var letter = await _client.CreateLetterAsync(new LetterDefinition
            {
                ToAddress = new TargetAddress(address.Id),
                FromAddress = new TargetAddress(address.Id),
                Description = "letter",
                File = new LobImageFile(file, false),
                Data = data,
                MetaData = metaData,
                Color = true,
                AddressPlacement = AddressPlacement.InsertBlankPage,
                DoubleSided = false,
                ExtraService = ExtraService.Registered
            });

            letter.ToAddress.Id.Should().Be(address.Id);
            letter.FromAddress.Id.Should().Be(address.Id);
            letter.Description.Should().Be("letter");
            letter.ExpectedDeliveryDate.Should().NotBe(DateTime.MinValue);
            letter.URL.Should().NotBeNull();
            letter.Color.Should().BeTrue();
            letter.DoubleSided.Should().BeFalse();
            letter.AddressPlacement.Should().Be(AddressPlacement.InsertBlankPage);
            letter.ExtraService.Should().Be(ExtraService.Registered);
            letter.ReturnEnvelope.Should().BeFalse();
            letter.PerforatedPage.Should().Be(null);
            letter.Thumbnails.Count().Should().Be(1);
            letter.Tracking.Should().NotBeNull();
            letter.MetaData["key0"].Should().Be(value0);
            letter.MetaData["key1"].Should().Be(value1);

            var metaDataResponse = (await _client.GetLettersAsync(new GetFilterOptions
            {
                Metadata = metaData
            })).Entries.First();

            metaDataResponse.Id.Should().Be(letter.Id);
        }

        [TestMethod]
        public void TestCreateCertifiedLetter()
        {
            Setup();
            const string file = "<div>Test html</div>";
            const int perforatedPage = 1;
            var addressClient = new AddressClient(_key);
            var address = addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1
            }).Entries.First();

            var letter = _client.CreateLetter(new LetterDefinition
            {
                AddressPlacement = AddressPlacement.TopFirstPage,
                DoubleSided = false,
                ReturnEnvelope = new ReturnEnvelope(perforatedPage),
                Description = "letter",
                ToAddress = new TargetAddress(address.Id),
                FromAddress = new TargetAddress(address.Id),
                File = new LobImageFile(file, false),
                Color = true,
                ExtraService = ExtraService.Certified
            });

            letter.ExtraService.Should().Be(ExtraService.Certified);
        }

        [TestMethod]
        public async Task TestCreateCertifiedLetterAsync()
        {
            Setup();
            const string file = "<div>Test html</div>";
            const int perforatedPage = 1;
            var addressClient = new AddressClient(_key);
            var address = addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1
            }).Entries.First();

            var letter = await _client.CreateLetterAsync(new LetterDefinition
            {
                AddressPlacement = AddressPlacement.TopFirstPage,
                DoubleSided = false,
                ReturnEnvelope = new ReturnEnvelope(perforatedPage),
                Description = "letter",
                ToAddress = new TargetAddress(address.Id),
                FromAddress = new TargetAddress(address.Id),
                File = new LobImageFile(file, false),
                Color = true,
                ExtraService = ExtraService.Certified
            });

            letter.ExtraService.Should().Be(ExtraService.Certified);
        }


        [TestMethod]
        public void TestCreateReturnEnvelopeLetter()
        {
            Setup();
            const string file = "<div>Test html</div>";
            const int perforatedPage = 1;
            var addressClient = new AddressClient(_key);
            var address = addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1
            }).Entries.First();

            var letter = _client.CreateLetter(new LetterDefinition
            {
                AddressPlacement = AddressPlacement.TopFirstPage,
                DoubleSided = false,
                ReturnEnvelope = new ReturnEnvelope(perforatedPage),
                Description = "letter",
                ToAddress = new TargetAddress(address.Id),
                FromAddress = new TargetAddress(address.Id),
                File = new LobImageFile(file, false),
                Color = true
            });

            letter.ReturnEnvelope.Should().BeTrue();
            letter.PerforatedPage.Should().Be(1);
        }

        [TestMethod]
        public async Task TestCreateReturnEnvelopeLetterAsync()
        {
            Setup();
            const string file = "<div>Test html</div>";
            const int perforatedPage = 1;
            var addressClient = new AddressClient(_key);
            var address = addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1
            }).Entries.First();

            var letter = await _client.CreateLetterAsync(new LetterDefinition
            {
                AddressPlacement = AddressPlacement.TopFirstPage,
                DoubleSided = false,
                ReturnEnvelope = new ReturnEnvelope(perforatedPage),
                Description = "letter",
                ToAddress = new TargetAddress(address.Id),
                FromAddress = new TargetAddress(address.Id),
                File = new LobImageFile(file, false),
                Color = true
            });

            letter.ReturnEnvelope.Should().BeTrue();
            letter.PerforatedPage.Should().Be(1);
        }
    }
}
