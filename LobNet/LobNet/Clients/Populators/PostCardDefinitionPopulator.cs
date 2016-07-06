using System;
using LobNet.Clients.Letters;
using LobNet.Clients.PostCards;
using LobNet.Models;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class PostCardDefinitionPopulator : IRequestPopulator
    {
        private readonly PostCardDefinition _postCard;

        public PostCardDefinitionPopulator(PostCardDefinition postCard)
        {
            _postCard = postCard;
        }

        public void Populate(IRestRequest request)
        {
            if(_postCard.FromAddress != null) PopulateAddress(_postCard.FromAddress, request, "from");
            PopulateAddress(_postCard.ToAddress, request, "to");
            PopulateFileParameter(request, _postCard.Front, "front");
            PopulateBackParameter(request);
            if (_postCard.Data != null) PopulateData(request);
            if (_postCard.MetaData != null) PopulateMetaData(request);
            PopulateSize(request);
            if (!string.IsNullOrEmpty(_postCard.Description))
                request.AddParameter("description", _postCard.Description);
        }

        private void PopulateMetaData(IRestRequest request)
        {
            var populator = new MetaDataPopulator(_postCard.MetaData);
            populator.Populate(request);
        }

        private void PopulateSize(IRestRequest request)
        {
            switch (_postCard.PostCardSize)
            {
                case PostCardSize.FourBySix:
                    request.AddParameter("size", "4x6");
                    break;
                case PostCardSize.SixByEleven:
                    request.AddParameter("size", "6x11");
                    break;
            }
        }

        private void PopulateData(IRestRequest request)
        {
            var data = _postCard.Data;
            var populator = new DataPopulator(data);
            populator.Populate(request);
        }

        private void PopulateBackParameter(IRestRequest request)
        {
            if (!string.IsNullOrEmpty(_postCard.Message))
            {
                request.AddParameter("message", _postCard.Message);
                return;
            }
            PopulateFileParameter(request, _postCard.Back, "back");
        }

        private void PopulateFileParameter(IRestRequest request, LobImageFile file, string name)
        {
            var frontPopulator = new LobImageFilePopulator(file, name);
            frontPopulator.Populate(request);
        }

        private static void PopulateAddress(Location address, IRestRequest request, string type)
        {
            var populator = new TargetAddressPopulator(address, type);
            populator.Populate(request);
        }
    }
}