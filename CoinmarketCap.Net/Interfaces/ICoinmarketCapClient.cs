using CoinmarketCap.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinmarketCap.Net.Interfaces
{
    public interface ICoinmarketCapClient
    {
        CmcResponse<List<CryptocurrencyMap>> CryptocurrencyMap(string symbols);
    }
}
