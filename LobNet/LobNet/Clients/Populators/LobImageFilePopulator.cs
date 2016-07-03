using LobNet.Clients.PostCards;
using LobNet.Models;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class LobImageFilePopulator : IRequestPopulator
    {
        private readonly LobImageFile _file;
        private readonly string _name;

        public LobImageFilePopulator(LobImageFile file, string name)
        {
            _file = file;
            _name = name;
        }

        public void Populate(IRestRequest request)
        {
            if (_file.IsLocalPath)
            {
                request.AddFile(_name, _file.File);
            }
            else
            {
                request.AddParameter(_name, _file.File);
            }
        }
    }
}