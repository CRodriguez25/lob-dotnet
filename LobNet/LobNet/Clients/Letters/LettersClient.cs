using System.Threading.Tasks;
using LobNet.Clients.Addresses;
using LobNet.Clients.Client;
using LobNet.Clients.Populators;
using LobNet.Models;

namespace LobNet.Clients.Letters
{
    public interface ILettersClient
    {
        Letter CreateLetter(LetterDefinition letterDefinition);
        Task<Letter> CreateLetterAsync(LetterDefinition letterDefinition);
        Letter GetLetter(string id);
        Task<Letter> GetLetterAsync(string id);
        GetResult<Letter> GetLetters(GetFilterOptions options);
        Task<GetResult<Letter>> GetLettersAsync(GetFilterOptions options);
    }

    public class LettersClient : LobClient, ILettersClient
    {
        public LettersClient(string apiKey) : base(apiKey)
        {
        }

        public Letter CreateLetter(LetterDefinition letterDefinition)
        {
            var populator = new CreateLetterPopulator(letterDefinition);
            return Execute<Letter>(Router.LETTERS, "POST", populator);
        }

        public Task<Letter> CreateLetterAsync(LetterDefinition letterDefinition)
        {
            var populator = new CreateLetterPopulator(letterDefinition);
            return ExecuteAsync<Letter>(Router.LETTERS, "POST", populator);
        }

        public Letter GetLetter(string id)
        {
            var resource = Router.LETTERS;
            resource = GetResourceUrl(resource, id);
            return Execute<Letter>(resource, "GET");
        }

        public Task<Letter> GetLetterAsync(string id)
        {
            var resource = Router.LETTERS;
            resource = GetResourceUrl(resource, id);
            return ExecuteAsync<Letter>(resource, "GET");
        }

        public GetResult<Letter> GetLetters(GetFilterOptions options)
        {
            var resource = Router.LETTERS;
            resource = ApplyGetOptions(resource, options);
            return Execute<GetResult<Letter>>(resource, "GET");
        }

        public Task<GetResult<Letter>> GetLettersAsync(GetFilterOptions options)
        {
            var resource = Router.LETTERS;
            resource = ApplyGetOptions(resource, options);
            return ExecuteAsync<GetResult<Letter>>(resource, "GET");
        }
    }
}