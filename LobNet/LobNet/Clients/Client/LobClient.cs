using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LobNet.Clients.Addresses;
using LobNet.Clients.Client.Exception;
using LobNet.Clients.Client.Extensions;
using LobNet.Clients.Populators;
using LobNet.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions.MonoHttp;

namespace LobNet.Clients.Client
{
    public abstract class LobClient
    {
        protected readonly string _apiKey;
        protected readonly IRestClient _restClient;
        protected LobClient(string apiKey): this(apiKey, Lob.BASE_URL)
        {
        }

        protected LobClient(string apiKey, string baseUrl)
        {
            _apiKey = apiKey;
            _restClient = new RestClient(baseUrl);
        }

        protected Task<T> ExecuteAsync<T>(string resource, string httpMethod, IRequestPopulator populator) where T: new()
        {
            var tcs = new TaskCompletionSource<T>();
            var webRequest = InitializeWebRequest(resource, httpMethod);
            if (populator != null) populator.Populate(webRequest);
            _restClient.ExecuteAsync(webRequest, r =>
            {
                CheckForErrors(r);
                tcs.SetResult(JsonConvert.DeserializeObject<T>(r.Content));
            });

            return tcs.Task;
        }

        private void CheckForErrors(IRestResponse restResponse)
        {
            if (restResponse.IsSuccessful()) return;
            var error = JsonConvert.DeserializeObject<ErrorResponse>(restResponse.Content);
            throw new LobException(error.Error.Message);
        }

        private RestRequest InitializeWebRequest(string resource, string httpMethod)
        {
            var webRequest = new RestRequest(resource, GetMethod(httpMethod))
            {
                Credentials = new NetworkCredential(_apiKey, "")
            };
            return webRequest;
        }

        protected Task<T> ExecuteAsync<T>(string resource, string httpMethod) where T : new()
        {
            return ExecuteAsync<T>(resource, httpMethod, null);
        }

        protected T Execute<T>(string resource, string httpMethod, IRequestPopulator populator) where T : new()
        {
            var webRequest = InitializeWebRequest(resource, httpMethod);
            if (populator != null) populator.Populate(webRequest);
            var result = _restClient.Execute<T>(webRequest);
            CheckForErrors(result);
            return JsonConvert.DeserializeObject<T>(result.Content);
        }

        protected T Execute<T>(string resource, string httpMethod) where T : new()
        {
            return Execute<T>(resource, httpMethod, null);
        }

        protected string GetResourceUrl(string resource, string id, params string[] subpath)
        {
            var result = string.Format("{0}/{1}", resource, id);
            return subpath.Aggregate(result, 
                (current, sub) => current + ("/" + sub));
        }

        protected static string ToQueryString(Dictionary<string, string> nvc)
        {
            var array = (from key in nvc.Keys
                         let value = nvc[key]
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }

        protected static string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }

        protected static string ApplyGetOptions(string resource, GetFilterOptions getOptions)
        {
            var options = new Dictionary<string, string>
            {
                {"limit", getOptions.Limit.ToString()},
                {"offset", getOptions.Offset.ToString()}
            };

            if (getOptions.IncludeTotalCount) options.Add("include[]", "total_count");
            if (getOptions.Metadata != null)
            {
                foreach (var kvp in getOptions.Metadata)
                    options.Add(string.Format("metadata[{0}]", kvp.Key), kvp.Value);
            }

            dynamic dateFilter = new System.Dynamic.ExpandoObject();
            if (getOptions.GreaterThanDate.HasValue) dateFilter.gt = getOptions.GreaterThanDate.Value.ToString("yyyy-MM-dd");
            if (getOptions.LessThanDate.HasValue) dateFilter.lt = getOptions.LessThanDate.Value.ToString("yyyy-MM-dd");
            if (getOptions.GreaterThanOrEqualToDate.HasValue)
                dateFilter.gte = getOptions.GreaterThanOrEqualToDate.Value.ToString("yyyy-MM-dd");
            if (getOptions.LessThanOrEqualToDate.HasValue)
                dateFilter.lte = getOptions.LessThanOrEqualToDate.Value.ToString("yyyy-MM-dd");

            var dateFilterSerialized = JsonConvert.SerializeObject(dateFilter);
            options.Add("date_created", dateFilterSerialized);
            return resource + ToQueryString(options);
        }

        private static Method GetMethod(string httpMethod)
        {
            switch (httpMethod.ToUpper())
            {
                case "POST": 
                    return Method.POST;
                case "GET": 
                    return Method.GET;
                case "PUT": 
                    return Method.PUT;
                case "PATCH":
                    return Method.PATCH;
                case "DELETE":
                    return Method.DELETE;
                default:
                    throw new ArgumentException("Invalid HTTP Method");
            }
        }
    }
}
