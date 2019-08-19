using CoinmarketCap.Net.Interfaces;
using CoinmarketCap.Net.Objects;
using System;

namespace CoinmarketCap.Net
{
    public class CoinmarketCapClient : BaseRestClient, ICoinmarketCapClient
    {
        public CoinmarketCapClient(string key, bool isProd):base(key,isProd? "https://pro-api.coinmarketcap.com" : null)
        {

        }
        public CmcResponse<CryptocurrencyMap> CryptocurrencyMap(string[] symbols)
        {
            return ExecuteRequest<CryptocurrencyMap>("", new System.Collections.Generic.Dictionary<string, object>() { }).Result;
        }
    }
}
