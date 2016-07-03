using System;
using LobNet.Clients.Areas;
using LobNet.Clients.Letters;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class AreaMailingDefinitionPopulator : IRequestPopulator
    {
        private readonly AreaMailingDefinition _definition;

        public AreaMailingDefinitionPopulator(AreaMailingDefinition definition)
        {
            _definition = definition;
        }

        public void Populate(IRestRequest request)
        {
            if (!string.IsNullOrEmpty(_definition.Description))
                request.AddParameter("description", _definition.Description);
            PopulateRoutes(request);
            PopulateSubEntities(request);
            PopulateTargetType(request);
        }

        private void PopulateTargetType(IRestRequest request)
        {
            string value;
            switch (_definition.TargetType)
            {
                case TargetType.All:
                    value = "all";
                    break;
                case TargetType.Residential:
                    value = "residential";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            request.AddParameter("target_type", value);
        }

        private void PopulateSubEntities(IRestRequest request)
        {
            new LobImageFilePopulator(_definition.Front, "front").Populate(request);
            new LobImageFilePopulator(_definition.Back, "back").Populate(request);
            if (_definition.Data != null) new DataPopulator(_definition.Data).Populate(request);
            if (_definition.MetaData != null) new MetaDataPopulator(_definition.MetaData).Populate(request);
        }

        private void PopulateRoutes(IRestRequest request)
        {
            foreach (var route in _definition.Routes)
            {
                request.AddParameter("routes", route.ToString());
            }
        }
    }
}