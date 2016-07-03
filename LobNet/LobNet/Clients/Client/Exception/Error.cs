using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LobNet.Clients.Client.Exception
{
    class ErrorResponse
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }

    class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status_code")]
        public int StatusCode { get; set; }
    }
}
