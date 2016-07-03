using System;
using System.Collections.Generic;
using LobNet.Clients.Letters;
using LobNet.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class CreateLetterPopulator : IRequestPopulator
    {
        private readonly LetterDefinition _letterDefinition;

        public CreateLetterPopulator(LetterDefinition letterDefinition)
        {
            _letterDefinition = letterDefinition;
        }

        public void Populate(IRestRequest request)
        {
            var toPopulator = new TargetAddressPopulator(_letterDefinition.ToAddress, "to");
            var fromPopulator = new TargetAddressPopulator(_letterDefinition.FromAddress, "from");
            var filePopulator = new LobImageFilePopulator(_letterDefinition.File, "file");
            var dataPopulator = new DataPopulator(_letterDefinition.Data);
            var metadataPopulator = new MetaDataPopulator(_letterDefinition.MetaData);

            toPopulator.Populate(request);
            fromPopulator.Populate(request);
            filePopulator.Populate(request);
            dataPopulator.Populate(request);
            metadataPopulator.Populate(request);

            request.AddParameter("color", _letterDefinition.Color);
            request.AddParameter("description", _letterDefinition.Description);
            request.AddParameter("double_sided", _letterDefinition.DoubleSided);

            PopulateReturnEnvelope(request);
            PopulateAddressPlacement(request);
            PopulateExtraService(request);
        }

        private void PopulateReturnEnvelope(IRestRequest request)
        {
            if (_letterDefinition.ReturnEnvelope == null) return;
            request.AddParameter("return_envelope", true);
            request.AddParameter("perforated_page", _letterDefinition.ReturnEnvelope.PerforatedPage);
        }

        private void PopulateExtraService(IRestRequest request)
        {
            var extraService = "";
            switch (_letterDefinition.ExtraService)
            {
                case ExtraService.None:
                    return;
                case ExtraService.Certified:
                    extraService = "certified";
                    break;
                case ExtraService.Registered:
                    extraService = "registered";
                    break;
            }

            request.AddParameter("extra_service", extraService);
        }

        private void PopulateAddressPlacement(IRestRequest request)
        {
            var value = "";
            switch (_letterDefinition.AddressPlacement)
            {
                case AddressPlacement.InsertBlankPage:
                    value = "insert_blank_page";
                    break;
                case AddressPlacement.TopFirstPage:
                    value = "top_first_page";
                    break;
            }

            request.AddParameter("address_placement", value);
        }
    }

    public class MetaDataPopulator : IRequestPopulator
    {
        private readonly Dictionary<string, string> _metaData;

        public MetaDataPopulator(Dictionary<string, string> metaData)
        {
            _metaData = metaData;
        }

        public void Populate(IRestRequest request)
        {
            if (_metaData == null) return;
            request.AddParameter("metadata", JsonConvert.SerializeObject(_metaData));
        }
    }
}