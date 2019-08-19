using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinmarketCap.Net.Objects
{
    public class CmcResponse<T> where T:class
    {
       public T Data { get; set; }
        public CmCStatus Status { get; set; }
    }
    public class CmCStatus
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("elapsed")]
        public long Elapsed { get; set; }

        [JsonProperty("credit_count")]
        public long CreditCount { get; set; }
    }
}
