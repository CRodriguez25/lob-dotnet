using System.IO;
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
                request.AlwaysMultipartFormData = true;
                using (var fs = File.Open(_file.File, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var fileName = Path.GetFileName(_file.File);
                    var buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    request.AddFileBytes(_name, buffer, "logo", fileName);
                }
            }
            else
            {
                request.AddParameter(_name, _file.File);
            }
        }
    }
}