using System;
using LobNet.Clients.Checks;
using LobNet.Clients.Letters;
using LobNet.Clients.PostCards;
using LobNet.Models;
using RestSharp;

namespace LobNet.Clients.Populators
{
    public class CheckDefinitionPopulator : IRequestPopulator
    {
        private readonly CheckDefinition _checkDefinition;

        public CheckDefinitionPopulator(CheckDefinition checkDefinition)
        {
            _checkDefinition = checkDefinition;
        }

        public void Populate(IRestRequest request)
        {
            PopulateOptionalArguments(request);
            PopulateSubEntities(request);
            request.AddParameter("bank_account", _checkDefinition.BankAccountId);
            request.AddParameter("amount", _checkDefinition.Amount);
            PopulateMailType(request);
        }

        private void PopulateSubEntities(IRestRequest request)
        {
            new TargetAddressPopulator(_checkDefinition.ToAddress, "to").Populate(request);
            new TargetAddressPopulator(_checkDefinition.FromAddress, "from").Populate(request);
            new DataPopulator(_checkDefinition.Data).Populate(request);
            new MetaDataPopulator(_checkDefinition.MetaData).Populate(request);
        }

        private void PopulateOptionalArguments(IRestRequest request)
        {
            if (!string.IsNullOrEmpty(_checkDefinition.Description))
                request.AddParameter("description", _checkDefinition.Description);

            if (!string.IsNullOrEmpty(_checkDefinition.Memo))
                request.AddParameter("memo", _checkDefinition.Memo);

            if (_checkDefinition.CheckNumber.HasValue)
                request.AddParameter("check_number", _checkDefinition.CheckNumber.Value);

            PopulateCheckBottom(request);
            PopulateImage(_checkDefinition.Attachment, request, "attachment");
            PopulateImage(_checkDefinition.Logo, request, "logo");
        }

        private void PopulateMailType(IRestRequest request)
        {
            var mailTypeVal = "";
            switch (_checkDefinition.MailType)
            {
                case MailType.USPSFirstClass:
                    mailTypeVal = "usps_first_class";
                    break;
                case MailType.UPSNextDayAir:
                    mailTypeVal = "ups_next_day_air";
                    break;
            }

            request.AddParameter("mail_type", mailTypeVal);
        }

        private static void PopulateImage(LobImageFile image, IRestRequest request, string name)
        {
            if (image != null)
                new LobImageFilePopulator(image, name).Populate(request);
        }

        private void PopulateCheckBottom(IRestRequest request)
        {
            if (_checkDefinition.CheckBottom != null)
            {
                PopulateImage(_checkDefinition.CheckBottom, request, "check_bottom");
            }
            else if (_checkDefinition.Message != null)
            {
                request.AddParameter("message", _checkDefinition.Message);
            }
        }
    }
}