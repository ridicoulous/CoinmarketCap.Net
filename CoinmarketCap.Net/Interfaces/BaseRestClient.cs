using CoinmarketCap.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoinmarketCap.Net.Interfaces
{
    public abstract class BaseRestClient
    {
        private readonly string _key;
        private readonly string _baseUrl;
        private readonly string _version;

        private readonly HttpClient _httClient;
        public BaseRestClient(string apiKey, string baseUrl=null, string version="v1")
        {
            if (String.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException("apiKey");
            }
            _version = version;
            _key = apiKey;
            _baseUrl = baseUrl ?? "https://sandbox.coinmarketcap.com";
            _httClient = new HttpClient() { BaseAddress = new Uri(_baseUrl)};
            _httClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _httClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _key);
        }
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
        protected async Task<CmcResponse<T>> ExecuteRequest<T>(string endpoint,Dictionary<string,object> parameters) where T:class
        {
            var url = $"{_baseUrl}/{_version}/{endpoint}?{CreateParamString(parameters)}";
            var request = await _httClient.GetAsync(url);
            var response =await request.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CmcResponse<T>>(response,settings);
        }
        /// <summary>
        /// Create a query string of the specified parameters
        /// </summary>
        /// <param name="parameters">The parameters to use</param>
        /// <param name="urlEncodeValues">Whether or not the values should be url encoded</param>
        /// <returns></returns>
        private  string CreateParamString(Dictionary<string, object> parameters, bool urlEncodeValues)
        {
            var uriString = "";
            var arraysParameters = parameters.Where(p => p.Value.GetType().IsArray).ToList();
            foreach (var arrayEntry in arraysParameters)
            {
                uriString += $"{string.Join("&", ((object[])(urlEncodeValues ? WebUtility.UrlEncode(arrayEntry.Value.ToString()) : arrayEntry.Value)).Select(v => $"{arrayEntry.Key}[]={v}"))}&";
            }
            uriString += $"{string.Join("&", parameters.Where(p => !p.Value.GetType().IsArray).Select(s => $"{s.Key}={(urlEncodeValues ? WebUtility.UrlEncode(s.Value.ToString()) : s.Value)}"))}";
            uriString = uriString.TrimEnd('&');
            return uriString;
        }
        private string CreateParamString(Dictionary<string, object> parameters)
        {
            var uriString = "";
            var arraysParameters = parameters.Where(p => p.Value.GetType().IsArray).ToList();
            foreach (var arrayEntry in arraysParameters)
            {
                uriString += $"{arrayEntry.Key}={String.Join(",", (object[])arrayEntry.Value)}"; //$"{string.Join("&",$"{arrayEntry.Key}={String.Join(",",(object[])arrayEntry.Value))}&";
            }
            uriString += $"{string.Join("&", parameters.Where(p => !p.Value.GetType().IsArray).Select(s => $"{s.Key}={s.Value}"))}";
            uriString = uriString.TrimEnd('&');
            return uriString;
        }
    }
}
