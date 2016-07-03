using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace LobNet.Clients.Client.Extensions
{
    public static class RestSharpExtensionMethods
    {
        public static bool IsSuccessful(this IRestResponse response)
        {
            return response.StatusCode.IsScuccessStatusCode()
                && response.ResponseStatus == ResponseStatus.Completed;
        }

        public static bool IsScuccessStatusCode(this HttpStatusCode responseCode)
        {
            var numericResponse = (int)responseCode;
            return numericResponse >= 200
                && numericResponse <= 399;
        }
    }
}
