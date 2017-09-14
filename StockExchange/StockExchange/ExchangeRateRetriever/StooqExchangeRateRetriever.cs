using System;

namespace StockExchange
{
    // This class splits a string with data and asigns the values to proper fields of exchangeRate

    public class StooqExchangeRateRetriever : IExchangeRateRetriever
    {
        public void RetrieveValuesFromString(ExchangeRate exchangeRate, string exchangeRateValues)
        {
            // Sets time of updating the data
            exchangeRate.UpdateDate = DateTime.Now;
            var splittedData = exchangeRateValues.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (splittedData.Length != 2)
            {
                throw new Exception("Invalid csv data");
            }

            var exchanges = splittedData[1].Split(',');
            if (exchanges.Length != 8)
            {
                throw new Exception("Invalid csv data");
            }

            var exchangeRateAsString = exchanges[6].Replace('.', ',');
            double exchangeRateValue;
            if (!double.TryParse(exchangeRateAsString, out exchangeRateValue))
            {
                throw new Exception("Invalid csv data");
            }

            exchangeRate.RateValue = exchangeRateValue;
        }
    }
}
