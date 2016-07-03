using RestSharp;

namespace LobNet.Clients.Populators
{
    /// <summary>
    /// A request populator is a class that knows how to take an object 
    /// in the domain (i.e. an "Address", "PostCardDefinition", etc.), 
    /// take the relevant fields,
    /// and populate a REST request to Lob.com correctly.
    /// </summary>
    public interface IRequestPopulator
    {
        void Populate(IRestRequest request);
    }
}