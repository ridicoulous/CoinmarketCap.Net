using CoinmarketCap.Net.Interfaces;
using CoinmarketCap.Net.Objects;
using System;
using System.Collections.Generic;

namespace CoinmarketCap.Net
{
    public class CoinmarketCapClient : BaseRestClient, ICoinmarketCapClient
    {
        private const string MapCmcCurrencyEndpoint = "cryptocurrency/map";
        private const string CmcQuotesEndpoint = "cryptocurrency/quotes/latest";
        public CoinmarketCapClient(string key, bool isProd) : base(key, isProd ? "https://pro-api.coinmarketcap.com" : null)
        {

        }
        public CmcResponse<List<CryptocurrencyMap>> CryptocurrencyMap(string[] symbols)
        {
            return ExecuteRequest<List<CryptocurrencyMap>>(MapCmcCurrencyEndpoint, new System.Collections.Generic.Dictionary<string, object>() { { "symbol", symbols } }).Result;
        }

        public CmcResponse<Dictionary<string, CryptocurrencyQuotesLatest>> CryptocurrencyQuotesLatest(string[] symbols)
        {
            var param = new System.Collections.Generic.Dictionary<string, object>() { { "symbol", symbols } };
            return ExecuteRequest<Dictionary<string, CryptocurrencyQuotesLatest>>(CmcQuotesEndpoint, param).Result;
        }
        public CmcResponse<Dictionary<string, CryptocurrencyQuotesLatest>> CryptocurrencyQuotesLatest(int[] ids)
        {
            var param = new System.Collections.Generic.Dictionary<string, object>() { { "id", ids } };
            return ExecuteRequest<Dictionary<string, CryptocurrencyQuotesLatest>>(CmcQuotesEndpoint, param).Result;
        }
    }
}
