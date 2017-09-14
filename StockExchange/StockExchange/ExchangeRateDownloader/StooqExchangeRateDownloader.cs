using System.Net.Http;
using System.Threading.Tasks;

namespace StockExchange
{
    //Class responsible for downloading exchange rate information from web page and returning it as a string

    public class StooqExchangeRateDownloader : IExchangeRateDownloader // StooqExchangeRateDownloader
    {
        private readonly string _uri;
        public StooqExchangeRateDownloader(string uri = "http://stooq.pl/q/l/?s={0}&f=sd2t2ohlcv&h&e=csv")
        {
            _uri = uri;
        }
        public async Task<string> DownloadAsync(string stockName)
        {
            using (HttpClient httpClient = new HttpClient())

            using (HttpResponseMessage response = await httpClient.GetAsync(string.Format(_uri, stockName)))
            {
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }
    }
}
