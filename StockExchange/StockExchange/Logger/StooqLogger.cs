using System;
using System.Collections.Generic;

namespace StockExchange
{
    // This clas is responsible for logging information to the console

    class StooqLogger : ILogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void LogMessage(ExchangeRate exchangeRate)
        {
            Console.WriteLine("{0,-15}{1,-15}{2,-15}", exchangeRate.Name, exchangeRate.RateValue.ToString(), 
                exchangeRate.UpdateDate.ToString());
            Console.WriteLine();
        }

        public void LogMessage(IEnumerable<ExchangeRate> exchangeRates)
        {
            foreach(var exchangeRate in exchangeRates)
            {
                LogMessage(exchangeRate);
            }
        }
    }
}
