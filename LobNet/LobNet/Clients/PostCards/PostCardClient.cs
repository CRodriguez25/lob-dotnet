using System.Threading.Tasks;
using LobNet.Clients.Addresses;
using LobNet.Clients.Client;
using LobNet.Clients.Populators;
using LobNet.Models;

namespace LobNet.Clients.PostCards
{
    public interface IPostCardClient
    {
        PostCard CreatePostCard(PostCardDefinition postCard);
        Task<PostCard> CreatePostCardAsync(PostCardDefinition postCard);
        PostCard GetPostCard(string id);
        Task<PostCard> GetPostCardAsync(string id);
        GetResult<PostCard> GetPostCards(GetFilterOptions options);
    }

    public class PostCardClient : LobClient, IPostCardClient
    {
        public PostCardClient(string apiKey) : base(apiKey)
        {
        }

        public PostCard CreatePostCard(PostCardDefinition postCard)
        {
            var populator = new PostCardDefinitionPopulator(postCard);
            var resource = Router.POSTCARDS;
            return Execute<PostCard>(resource, "POST", populator);
        }

        public Task<PostCard> CreatePostCardAsync(PostCardDefinition postCard)
        {
            var populator = new PostCardDefinitionPopulator(postCard);
            var resource = Router.POSTCARDS;
            return ExecuteAsync<PostCard>(resource, "POST", populator);
        }

        public PostCard GetPostCard(string id)
        {
            var resource = Router.POSTCARDS;
            resource = GetResourceUrl(resource, id);
            return Execute<PostCard>(resource, "GET");
        }

        public Task<PostCard> GetPostCardAsync(string id)
        {
            var resource = Router.POSTCARDS;
            resource = GetResourceUrl(resource, id);
            return ExecuteAsync<PostCard>(resource, "GET");
        }

        public GetResult<PostCard> GetPostCards(GetFilterOptions options)
        {
            var resource = Router.POSTCARDS;
            resource = ApplyGetOptions(resource, options);
            return Execute<GetResult<PostCard>>(resource, "GET");
        }

        public GetResult<PostCard> GetPostCards()
        {
            return GetPostCards(new GetFilterOptions());
        }

        public Task<GetResult<PostCard>> GetPostCardsAsync(GetFilterOptions options)
        {
            var resource = Router.POSTCARDS;
            resource = ApplyGetOptions(resource, options);
            return ExecuteAsync<GetResult<PostCard>>(resource, "GET");
        }

        public Task<GetResult<PostCard>> GetPostCardsAsync()
        {
            return GetPostCardsAsync(new GetFilterOptions());
        }
    }
}