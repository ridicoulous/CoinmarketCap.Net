using CoinmarketCap.Net;
using System;

namespace Testiq
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmc = new CoinmarketCapClient("91f86884-f5f5-47a9-9945-20b3b1871830", true);
            var result = cmc.CryptocurrencyQuotesLatest(new string[] { "PLA","IGG"});
        }
    }
}
