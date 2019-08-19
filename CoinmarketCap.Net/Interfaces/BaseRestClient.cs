﻿using CoinmarketCap.Net.Objects;
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
        public BaseRestClient(string key, string baseUrl=null, string version="v1")
        {
            _key = key;
            _baseUrl = baseUrl ?? "https://sandbox.coinmarketcap.com";
            _httClient = new HttpClient() { BaseAddress = new Uri(_baseUrl)};
            _httClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _httClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _key);
        }

        public async Task<CmcResponse<T>> ExecuteRequest<T>(string endpoint,Dictionary<string,object> parameters) where T:class
        {
            var request = await _httClient.GetAsync($"/{_version}/{endpoint}{CreateParamString(parameters,false)}");
            var response =await request.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CmcResponse<T>>(response);
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
    }
}