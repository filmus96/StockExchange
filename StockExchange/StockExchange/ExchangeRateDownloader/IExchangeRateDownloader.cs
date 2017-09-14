using System.Threading.Tasks;

namespace StockExchange
{
    public interface IExchangeRateDownloader
    {
        Task<string> DownloadAsync(string stockName);
    }
}
