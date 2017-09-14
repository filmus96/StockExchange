using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchange
{
    class StooqExchangeProcessor
    {
        private readonly List<string> _exchangeRateNames;
        private readonly List<ExchangeRate> _exchangeRates;
        private IExchangeRateArchiveManager _archiveManager;
        private IExchangeRateDownloader _exchangeDownloader;
        private IExchangeRateRetriever _exchangeRateRetriever;
        private ILogger _logger;

        public StooqExchangeProcessor(IExchangeRateArchiveManager archiveManager, 
                                        IExchangeRateDownloader exchangeDownloader,
                                        IExchangeRateRetriever exchangeRateRetriever,
                                        ILogger logger)
        {
            _exchangeRateRetriever = exchangeRateRetriever;
            _exchangeDownloader = exchangeDownloader;
            _archiveManager = archiveManager;
            _logger = logger;
            _exchangeRateNames = new List<string>() { "wig", "wig20", "fw20", "mwig40", "swig80" };
            _exchangeRates = new List<ExchangeRate>();

            foreach (var name in _exchangeRateNames) //initialization of ExchangeRate objects
            {
                _exchangeRates.Add(new ExchangeRate { Name = name });
            }
        }

        public async Task RunProcessAsync()
        {
            {
                bool saveFlag = false;
                var exchangeRatesToBeUpdated = new List<ExchangeRate>();
                _logger.LogMessage("Checking current exchange rate values...");
                foreach (var exchangeRate in _exchangeRates)
                {
                    //gets proper exchange rate information from web page
                    string exchangeRateValues = await _exchangeDownloader.DownloadAsync(exchangeRate.Name);
                    //asigns proper values to exchangeRate
                    _exchangeRateRetriever.RetrieveValuesFromString(exchangeRate, exchangeRateValues);
                    exchangeRatesToBeUpdated.Add(exchangeRate);
                    //loads proper exchange rate from file
                    var archivedExchangeRate = _archiveManager.GetFromFile().FirstOrDefault(ex => ex.Name == exchangeRate.Name);
                    if(CanValuesBeUpdatedPolicy.IsSatisfiedBy(archivedExchangeRate, exchangeRate))
                    {
                        saveFlag = true;
                    }
                }
                UpdateArchive(saveFlag, exchangeRatesToBeUpdated);
            }
        }

        private void UpdateArchive(bool saveFlag, List<ExchangeRate> exchangeRatesToBeUpdated)
        {
            if (saveFlag)
            {
                _archiveManager.SaveToFile(exchangeRatesToBeUpdated);
                _logger.LogMessage("Exchange rates has been just updated. Current values:");
                _logger.LogMessage(exchangeRatesToBeUpdated);
            }
            else
            {
                _logger.LogMessage("The values have not changed yet");
            }
        }
    }
}
