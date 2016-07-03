using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LobNet.Clients.Addresses;
using LobNet.Clients.BankAccounts;
using LobNet.Clients.Client;
using LobNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LobTests
{
    [TestClass]
    public class BankAccountTests : BaseTest
    {
        private BankAccountsClient _bankAccountsClient;
        private void Setup()
        {
            _bankAccountsClient = new BankAccountsClient(_key);
        }

        [TestMethod]
        public void TestListBankAccounts()
        {
            Setup();
            var response = _bankAccountsClient.GetBankAccounts();
            response.Entries.Count().Should().BeGreaterThan(0);
        }

        [TestMethod]
        public async Task TestListBankAccountsAsync()
        {
            Setup();
            var response = await _bankAccountsClient.GetBankAccountsAsync();
            response.Entries.Count().Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void TestListBankAccountsLimit()
        {
            Setup();
            var response = _bankAccountsClient.GetBankAccounts(new GetFilterOptions
            {
                Limit = 2
            });

            response.Entries.Count().Should().Be(2);
            _bankAccountsClient.GetBankAccounts(new GetFilterOptions
            {
                Limit = 1,
                Offset = 2
            }).Entries.Count().Should().Be(1);
        }

        [TestMethod]
        public async Task TestListBankAccountsLimitAsync()
        {
            Setup();
            var response = await _bankAccountsClient.GetBankAccountsAsync(new GetFilterOptions
            {
                Limit = 2
            });

            response.Entries.Count().Should().Be(2);
            (await _bankAccountsClient.GetBankAccountsAsync(new GetFilterOptions
            {
                Limit = 1,
                Offset = 2
            })).Entries.Count().Should().Be(1);
        }

        [TestMethod]
        public void TestListBankAccountsFails()
        {
            Setup();
            Action action = () => _bankAccountsClient.GetBankAccounts(new GetFilterOptions
            {
                Limit = 1000
            });

            action.ShouldThrow<LobException>();
        }

        [TestMethod]
        public void TestCreateBankAccounts()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                { "key0", value0 }, { "key1", value1 }
            };

            var bankAccount = _bankAccountsClient.CreateBankAccount(new BankAccountDefinition
            {
                AccountNumber = "123456789",
                RoutingNumber = "122100024",
                AccountType = AccountType.Company,
                Signatory = "John Doe",
                Description = "Bank Account",
                MetaData = metaData
            });

            bankAccount.Description.Should().Be("Bank Account");
            bankAccount.Verified.Should().BeFalse();
            bankAccount.AccountNumber.Should().Be("123456789");
            bankAccount.AccountType.Should().Be(AccountType.Company);
            bankAccount.RoutingNumber.Should().Be("122100024");
            bankAccount.Signatory.Should().Be("John Doe");
            bankAccount.MetaData["key0"].Should().Be(value0);
            bankAccount.MetaData["key1"].Should().Be(value1);

            var metaDataResponse = _bankAccountsClient.GetBankAccounts(new GetFilterOptions
            {
                Metadata = metaData,
                Limit = 1
            });

            metaDataResponse.Entries.Count().Should().Be(1);
            metaDataResponse.Entries.First().Id.Should().Be(bankAccount.Id);

            var verify = _bankAccountsClient.Verify(bankAccount.Id, new[] {20, 40});
            verify.Verified.Should().Be(true);

            var newAccount = _bankAccountsClient.GetBankAccount(bankAccount.Id);
            newAccount.Id.Should().Be(bankAccount.Id);
            var result = _bankAccountsClient.DeleteBankAccount(newAccount.Id);
            result.Deleted.Should().Be(true);
            var test = _bankAccountsClient.GetBankAccount(bankAccount.Id);
            test.Deleted.Should().Be(true);
        }

        [TestMethod]
        public async Task TestCreateBankAccountsAsync()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                { "key0", value0 }, { "key1", value1 }
            };

            var bankAccount = await _bankAccountsClient.CreateBankAccountAsync(new BankAccountDefinition
            {
                AccountNumber = "123456789",
                RoutingNumber = "122100024",
                AccountType = AccountType.Individual,
                Signatory = "John Doe",
                Description = "Bank Account",
                MetaData = metaData
            });

            bankAccount.Description.Should().Be("Bank Account");
            bankAccount.Verified.Should().BeFalse();
            bankAccount.AccountNumber.Should().Be("123456789");
            bankAccount.AccountType.Should().Be(AccountType.Individual);
            bankAccount.RoutingNumber.Should().Be("122100024");
            bankAccount.Signatory.Should().Be("John Doe");
            bankAccount.MetaData["key0"].Should().Be(value0);
            bankAccount.MetaData["key1"].Should().Be(value1);

            var metaDataResponse = await _bankAccountsClient.GetBankAccountsAsync(new GetFilterOptions
            {
                Metadata = metaData,
                Limit = 1
            });

            metaDataResponse.Entries.Count().Should().Be(1);
            metaDataResponse.Entries.First().Id.Should().Be(bankAccount.Id);

            var verify = await _bankAccountsClient.VerifyAsync(bankAccount.Id, new[] { 20, 40 });
            verify.Verified.Should().Be(true);

            var newAccount = await _bankAccountsClient.GetBankAccountAsync(bankAccount.Id);
            newAccount.Id.Should().Be(bankAccount.Id);
            var result = await _bankAccountsClient.DeleteBankAccountAsync(newAccount.Id);
            result.Deleted.Should().Be(true);
            var test = await _bankAccountsClient.GetBankAccountAsync(bankAccount.Id);
            test.Deleted.Should().Be(true);
        }
    }
}
