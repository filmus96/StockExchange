using System;

namespace StockExchange
{
    // This class checks if the archived exchange rate and the new one are different by means of their values

    class CanValuesBeUpdatedPolicy
    {
        public static bool IsSatisfiedBy(ExchangeRate archivedExchangeRate, ExchangeRate exchangeRate) 
        {
            if (exchangeRate == null)
                throw new Exception("New exchange rate is invalid");

            if (archivedExchangeRate == null)
            {
                return true;
            }

            return (exchangeRate.RateValue != archivedExchangeRate.RateValue);
        }
    }
}
