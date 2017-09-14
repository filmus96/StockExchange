using System.Collections.Generic;

namespace StockExchange
{
    interface ILogger
    {
        void LogMessage(string message);

        void LogMessage(ExchangeRate exchangeRate);

        void LogMessage(IEnumerable<ExchangeRate> exchangeRates);
    }
}
