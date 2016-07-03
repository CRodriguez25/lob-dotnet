using System.Collections.Generic;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class DataPopulator : IRequestPopulator
    {
        private readonly Dictionary<string, string> _data;

        public DataPopulator(Dictionary<string, string> data)
        {
            _data = data;
        }

        public void Populate(IRestRequest request)
        {
            if (_data == null) return;
            foreach (var kvp in _data)
            {
                request.AddParameter(string.Format("data[{0}]", kvp.Key), kvp.Value);
            }
        }
    }
}