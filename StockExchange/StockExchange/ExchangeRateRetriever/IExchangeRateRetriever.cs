namespace StockExchange
{
    public interface IExchangeRateRetriever
    {
        void RetrieveValuesFromString(ExchangeRate exchangeRate, string exchangeRateValues);
    }
}
