using RestSharp;

namespace LobNet.Clients.Populators
{
    public class IdPopulator : IRequestPopulator
    {
        private readonly string _id;

        public IdPopulator(string id)
        {
            _id = id;
        }

        public void Populate(IRestRequest request)
        {
            request.AddParameter("id", _id);
        }
    }
}