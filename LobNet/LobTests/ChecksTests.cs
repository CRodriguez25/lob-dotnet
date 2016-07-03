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
    public class ChecksTests : BaseTest
    {
        private ChecksClient _checksClient;
        private void Setup()
        {
            _checksClient = new ChecksClient(_key);
        }

        private BankAccount GetAndVerifyBankAccount()
        {
            var bankAccountClient = new BankAccountsClient(_key);
            var bankAccount = bankAccountClient.GetBankAccounts(new GetFilterOptions
            {
                Limit = 1
            }).Entries.First();

            if (!bankAccount.Verified)
            {
                bankAccountClient.Verify(bankAccount.Id, new[] {20, 40});
            }

            return bankAccount;
        }

        private AddressBookEntry GetAddress()
        {
            var addressClient = new AddressClient(_key);
            return addressClient.GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1
            }).Entries.First();
        }

        [TestMethod]
        public void TestGetChecks()
        {
            Setup();
            var responseList = _checksClient.GetChecks().Entries;
            var response = responseList.First();

            response.Tracking.Should().NotBeNull();
            response.Tracking.Events.Should().NotBeNull();
            response.Tracking.Id.Should().NotBeNull();
            response.Tracking.TrackingNumber.Should().BeNull();
            response.Tracking.Carrier.Should().NotBeNull();
        }

        [TestMethod]
        public async Task TestGetChecksAsync()
        {
            Setup();
            var responseList = (await _checksClient.GetChecksAsync()).Entries;
            var response = responseList.First();

            response.Tracking.Should().NotBeNull();
            response.Tracking.Events.Should().NotBeNull();
            response.Tracking.Id.Should().NotBeNull();
            response.Tracking.TrackingNumber.Should().BeNull();
            response.Tracking.Carrier.Should().NotBeNull();
        }

        [TestMethod]
        public void TestListChecksLimit()
        {
            Setup();
            var responseList = _checksClient.GetChecks(new GetFilterOptions
            {
                Limit = 2
            });

            responseList.Entries.Count().Should().Be(2);

            _checksClient.GetChecks(new GetFilterOptions
            {
                Limit = 1,
                Offset = 2
            }).Entries.Count().Should().Be(1);
        }

        [TestMethod]
        public async Task TestListChecksLimitAsync()
        {
            Setup();
            var responseList = await _checksClient.GetChecksAsync(new GetFilterOptions
            {
                Limit = 2
            });

            responseList.Entries.Count().Should().Be(2);

            (await _checksClient.GetChecksAsync(new GetFilterOptions
            {
                Limit = 1,
                Offset = 2
            })).Entries.Count().Should().Be(1);
        }

        [TestMethod]
        public void TestListChecksFail()
        {
            Setup();
            Action action = () => _checksClient.GetChecks(new GetFilterOptions
            {
                Limit = 1000
            });

            action.ShouldThrow<LobException>();
        }

        [TestMethod]
        public void TestCreateCheck()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                { "key0", value0 }, { "key1", value1 }
            };

            var bankAccount = GetAndVerifyBankAccount();
            var address = GetAddress();

            var checkDefinition = new CheckDefinition
            {
                BankAccountId = bankAccount.Id,
                Description = "check",
                ToAddress = new TargetAddress(address.Id),
                FromAddress = new TargetAddress(address.Id),
                Amount = 1000,
                Message = "test message",
                CheckNumber = 100,
                Memo = "Test Check",
                MetaData = metaData
            };

            var check = _checksClient.CreateCheck(checkDefinition);
            check.BankAccount.Id.Should().Be(bankAccount.Id);
            check.ToAddress.Id.Should().Be(address.Id);
            check.FromAddress.Id.Should().Be(address.Id);
            check.Description.Should().Be("check");
            check.MetaData["key0"].Should().Be(value0);
            check.MetaData["key1"].Should().Be(value1);
            check.Message.Should().Be("test message");
            check.Memo.Should().Be("Test Check");
            check.URL.Should().NotBeEmpty();
            check.CheckNumber.Should().BeGreaterThan(0);
            check.ExpectedDeliveryDate.Should().NotBe(DateTime.MinValue);
            check.Amount.Should().Be(1000);
            check.Thumbnails.Should().NotBeEmpty();

            var metaDataResponse = _checksClient.GetChecks(new GetFilterOptions
            {
                Metadata = metaData,
                Limit = 1
            }).Entries.First();

            metaDataResponse.Id.Should().Be(check.Id);
            checkDefinition.Logo = new LobImageFile("https://s3-us-west-2.amazonaws.com/lob-assets/lob_check_logo.png", false);
            var localLogo = _checksClient.CreateCheck(checkDefinition);

            var newestCheck = _checksClient.GetCheck(localLogo.Id);
            newestCheck.Id.Should().Be(localLogo.Id);
        }

        [TestMethod]
        public async Task TestCreateCheckAsync()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                { "key0", value0 }, { "key1", value1 }
            };

            var bankAccount = GetAndVerifyBankAccount();
            var address = GetAddress();

            var checkDefinition = new CheckDefinition
            {
                BankAccountId = bankAccount.Id,
                Description = "check",
                ToAddress = new TargetAddress(address.Id),
                FromAddress = new TargetAddress(address.Id),
                Amount = 1000,
                Message = "test message",
                CheckNumber = 100,
                Memo = "Test Check",
                MetaData = metaData,
                CheckBottom = new LobImageFile("<div>Hi</div>", false),
                MailType = MailType.UPSNextDayAir
            };

            var check = await _checksClient.CreateCheckAsync(checkDefinition);
            check.BankAccount.Id.Should().Be(bankAccount.Id);
            check.ToAddress.Id.Should().Be(address.Id);
            check.FromAddress.Id.Should().Be(address.Id);
            check.Description.Should().Be("check");
            check.MetaData["key0"].Should().Be(value0);
            check.MetaData["key1"].Should().Be(value1);
            check.Message.Should().BeNull();
            check.Memo.Should().Be("Test Check");
            check.URL.Should().NotBeEmpty();
            check.CheckNumber.Should().BeGreaterThan(0);
            check.ExpectedDeliveryDate.Should().NotBe(DateTime.MinValue);
            check.Amount.Should().Be(1000);
            check.Thumbnails.Should().NotBeEmpty();

            var metaDataResponse = (await _checksClient.GetChecksAsync(new GetFilterOptions
            {
                Metadata = metaData,
                Limit = 1
            })).Entries.First();

            metaDataResponse.Id.Should().Be(check.Id);
            checkDefinition.Logo = new LobImageFile("https://s3-us-west-2.amazonaws.com/lob-assets/lob_check_logo.png", false);
            var remoteLogo = await _checksClient.CreateCheckAsync(checkDefinition);
            var newestCheck = await _checksClient.GetCheckAsync(remoteLogo.Id);
            newestCheck.Id.Should().Be(remoteLogo.Id);
        }
    }
}
