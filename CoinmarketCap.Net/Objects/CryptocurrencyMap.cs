using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinmarketCap.Net.Objects
{
    public class CryptocurrencyMap
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("is_active")]
        public long IsActive { get; set; }

        [JsonProperty("first_historical_data")]
        public DateTimeOffset FirstHistoricalData { get; set; }

        [JsonProperty("last_historical_data")]
        public DateTimeOffset LastHistoricalData { get; set; }

        [JsonProperty("platform")]
        public Platform Platform { get; set; }
    }

    public partial class Platform
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("token_address")]
        public string TokenAddress { get; set; }
    }
}
