using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LobNet.Clients.Addresses;
using LobNet.Clients.Client;
using LobNet.Clients.PostCards;
using LobNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LobTests
{
    [TestClass]
    public class PostCardsTests : BaseTest
    {
        private PostCardClient _client;

        private void Setup()
        {
            _client = new PostCardClient(_key);
        }

        [TestMethod]
        public void TestListPostCards()
        {
            Setup();
            var responseList = _client.GetPostCards().Entries;
            var response = responseList.First();
            response.Should().NotBeNull();
        }

        [TestMethod]
        public async Task TestListPostCardsAsync()
        {
            Setup();
            var responseList = (await _client.GetPostCardsAsync()).Entries;
            var response = responseList.First();
            response.Should().NotBeNull();
        }

        [TestMethod]
        public void TestListPostcardsLimit()
        {
            Setup();
            _client.GetPostCards(new GetFilterOptions
            {
                Limit = 2
            }).Entries.Count().Should().Be(2);

            _client.GetPostCards(new GetFilterOptions
            {
                Limit = 1,
                Offset = 2
            }).Entries.Count().Should().Be(1);
        }

        [TestMethod]
        public async Task TestListPostcardsLimitAsync()
        {
            Setup();
            (await _client.GetPostCardsAsync(new GetFilterOptions
            {
                Limit = 2
            })).Entries.Count().Should().Be(2);

            (await _client.GetPostCardsAsync(new GetFilterOptions
            {
                Limit = 1,
                Offset = 2
            })).Entries.Count().Should().Be(1);
        }

        [TestMethod]
        public void TestListPostCardFail()
        {
            Setup();
            Action action = () => _client.GetPostCards(new GetFilterOptions
            {
                Limit = 1000
            });

            action.ShouldThrow<LobException>();
        }

        [TestMethod]
        public void TestListPostCardFailAsync()
        {
            Setup();
            Action action = () => _client.GetPostCardsAsync(new GetFilterOptions
            {
                Limit = 1000
            }).Wait();

            action.ShouldThrow<LobException>();
        }

        [TestMethod]
        public void TestCreatePostCard()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                {"key0", value0},
                {"key1", value1}
            };

            var address = new AddressClient(_key).GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1
            }).Entries.First();

            var postCard = _client.CreatePostCard(new PostCardDefinition
            {
                Description = "postcard",
                ToAddress = new Location(address.Id),
                FromAddress = new Location(address.Id),
                Front = new LobImageFile("<div>Test</div>", false),
                Back = new LobImageFile("<div>Back</div>", false),
                PostCardSize = PostCardSize.SixByEleven,
                MetaData = metaData
            });

            postCard.ToAddress.Id.Should().Be(address.Id);
            postCard.FromAddress.Id.Should().Be(address.Id);
            postCard.Description.Should().Be("postcard");
            postCard.URL.Should().NotBeNull();
            postCard.ExpectedDeliveryDate.Should().NotBe(DateTime.MinValue);
            postCard.Thumbnails.Count().Should().Be(2);
            postCard.Size.Should().Be("6x11");
            postCard.MetaData["key0"].Should().Be(value0);
            postCard.MetaData["key1"].Should().Be(value1);

            var metaDataResponse = _client.GetPostCards(new GetFilterOptions
            {
                Metadata = metaData,
                Limit = 1
            }).Entries.First();

            metaDataResponse.Id.Should().Be(postCard.Id);
            var getCard = _client.GetPostCard(postCard.Id);
            getCard.Id.Should().Be(postCard.Id);
        }

        [TestMethod]
        public async Task TestCreatePostCardAsync()
        {
            Setup();
            var value0 = Guid.NewGuid().ToString();
            var value1 = Guid.NewGuid().ToString();

            var metaData = new Dictionary<string, string>
            {
                {"key0", value0},
                {"key1", value1}
            };

            var address = new AddressClient(_key).GetAddressBookEntries(new GetFilterOptions
            {
                Limit = 1
            }).Entries.First();

            var postCard = await _client.CreatePostCardAsync(new PostCardDefinition
            {
                Description = "postcard",
                ToAddress = new Location(address.Id),
                FromAddress = new Location(address.Id),
                Front = new LobImageFile("<div>Test</div>", false),
                Back = new LobImageFile("<div>Back</div>", false),
                PostCardSize = PostCardSize.SixByEleven,
                MetaData = metaData
            });

            postCard.ToAddress.Id.Should().Be(address.Id);
            postCard.FromAddress.Id.Should().Be(address.Id);
            postCard.Description.Should().Be("postcard");
            postCard.URL.Should().NotBeNull();
            postCard.ExpectedDeliveryDate.Should().NotBe(DateTime.MinValue);
            postCard.Thumbnails.Count().Should().Be(2);
            postCard.Size.Should().Be("6x11");
            postCard.MetaData["key0"].Should().Be(value0);
            postCard.MetaData["key1"].Should().Be(value1);

            var metaDataResponse = (await _client.GetPostCardsAsync(new GetFilterOptions
            {
                Metadata = metaData,
                Limit = 1
            })).Entries.First();

            metaDataResponse.Id.Should().Be(postCard.Id);
            var getCard = await _client.GetPostCardAsync(postCard.Id);
            getCard.Id.Should().Be(postCard.Id);
        }

        [TestMethod]
        public void TestCreatePostCardsInline()
        {
            Setup();
            var postCard = _client.CreatePostCard(new PostCardDefinition
            {
                ToAddress = new Location(new Address
                {
                    Line1 = "185 Berry Street",
                    Line2 = "Suite 1510",
                    City = "San Francisco",
                    State = "CA",
                    ZipCode = "94107",
                    Country = "US",
                    Name = "Lob0"
                }),
                FromAddress = new Location(new Address
                {
                    Line1 = "185 Berry Street",
                    Line2 = "Suite 1510",
                    City = "San Francisco",
                    State = "CA",
                    ZipCode = "94107",
                    Country = "US",
                    Name = "Lob1"
                }),
                Front = new LobImageFile("<div>Hi</div>", false),
                Back = new LobImageFile("<div>Hi</div>", false)
            });

            postCard.ToAddress.Name.Should().Be("Lob0");
            postCard.FromAddress.Name.Should().Be("Lob1");
        }

        [TestMethod]
        public async Task TestCreatePostCardsInlineAsync()
        {
            Setup();
            var postCard = await _client.CreatePostCardAsync(new PostCardDefinition
            {
                ToAddress = new Location(new Address
                {
                    Line1 = "185 Berry Street",
                    Line2 = "Suite 1510",
                    City = "San Francisco",
                    State = "CA",
                    ZipCode = "94107",
                    Country = "US",
                    Name = "Lob0"
                }),
                FromAddress = new Location(new Address
                {
                    Line1 = "185 Berry Street",
                    Line2 = "Suite 1510",
                    City = "San Francisco",
                    State = "CA",
                    ZipCode = "94107",
                    Country = "US",
                    Name = "Lob1"
                }),
                Front = new LobImageFile("<div>Hi</div>", false),
                Back = new LobImageFile("Resources/postcardback.png", true)
            });

            postCard.ToAddress.Name.Should().Be("Lob0");
            postCard.FromAddress.Name.Should().Be("Lob1");
        }

        [TestMethod]
        public void TestCreatePostCardsNoFrom()
        {
            Setup();
            var postCard = _client.CreatePostCard(new PostCardDefinition
            {
                ToAddress = new Location(new Address
                {
                    Line1 = "185 Berry Street",
                    Line2 = "Suite 1510",
                    City = "San Francisco",
                    State = "CA",
                    ZipCode = "94107",
                    Country = "US",
                    Name = "Lob0"
                }),
                Front = new LobImageFile("<div>Hi</div>", false),
                Message = "TestMessage",
                Data = new Dictionary<string, string> { { "key1", "val1" } }
            });

            postCard.ToAddress.Name.Should().Be("Lob0");
        }

        [TestMethod]
        public async Task TestCreatePostCardsNoFromAsync()
        {
            Setup();
            var postCard = await _client.CreatePostCardAsync(new PostCardDefinition
            {
                ToAddress = new Location(new Address
                {
                    Line1 = "185 Berry Street",
                    Line2 = "Suite 1510",
                    City = "San Francisco",
                    State = "CA",
                    ZipCode = "94107",
                    Country = "US",
                    Name = "Lob0"
                }),
                Front = new LobImageFile("<div>Hi</div>", false),
                Back = new LobImageFile("Resources/postcardback.png", true)
            });

            postCard.ToAddress.Name.Should().Be("Lob0");
        }
    }
}