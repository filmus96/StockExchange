using System.Collections.Generic;

namespace StockExchange
{
    interface IExchangeRateArchiveManager
    {
        void SaveToFile(IEnumerable<ExchangeRate> exchangeRates);

        IEnumerable<ExchangeRate> GetFromFile();
    }
}
