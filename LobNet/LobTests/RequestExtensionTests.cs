using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using LobNet.Clients.Addresses;
using LobNet.Clients.BankAccounts;
using LobNet.Clients.Checks;
using LobNet.Clients.Client;
using LobNet.Clients.Client.Extensions;
using LobNet.Clients.Letters;
using LobNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LobTests
{
    [TestClass]
    public class RequestExtensionTests : BaseTest
    {
        [TestMethod]
        public void ResponseCodeTests()
        {
            HttpStatusCode.Continue.IsSuccessStatusCode().Should().BeFalse();
            HttpStatusCode.OK.IsSuccessStatusCode().Should().BeTrue();
            HttpStatusCode.BadRequest.IsSuccessStatusCode().Should().BeFalse();
        }
    }
}
