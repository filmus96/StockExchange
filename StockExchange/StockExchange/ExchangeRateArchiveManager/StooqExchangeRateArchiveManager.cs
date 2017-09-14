using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StockExchange
{
    // Class responsible for saving/reading data to/from file

    public class StooqExchangeRateArchiveManager : IExchangeRateArchiveManager
    {
        private readonly string _fileName;

        public StooqExchangeRateArchiveManager(string fileName = "ExchangeRates.txt")
        {
            _fileName = fileName;
        }

        public IEnumerable<ExchangeRate> GetFromFile()
        {
            if (!File.Exists(_fileName))
            {
                return Enumerable.Empty<ExchangeRate>().ToList();
            }
            string json = File.ReadAllText(_fileName);
            return JsonConvert.DeserializeObject<IEnumerable<ExchangeRate>>(json);
        }

        public void SaveToFile(IEnumerable<ExchangeRate> exchangeRates)
        {
            string json = JsonConvert.SerializeObject(exchangeRates);
            File.WriteAllText(_fileName, json);
        }
    }
}