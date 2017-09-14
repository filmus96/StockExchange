using Nito.AsyncEx;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace StockExchange
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }

        static async Task MainAsync(string[] args)
        {
            var archiveManager = new StooqExchangeRateArchiveManager();
            var exchangeDownloader = new StooqExchangeRateDownloader();
            var exchangeRateRetriever = new StooqExchangeRateRetriever();
            var logger = new StooqLogger();
            var stockExchangeProcessor = new StooqExchangeProcessor(archiveManager, exchangeDownloader, exchangeRateRetriever, logger);
            var conditionFlag = true;

            // The safest way to use timer events is to manage them right there from where they've been called
            // That's why this code below hasn't been extracted to a seperate class or function

            Timer timer = new Timer(10000);
            timer.Elapsed += async (sender, e) => await stockExchangeProcessor.RunProcessAsync();
            timer.Start();
            while (conditionFlag)
            {  
                logger.LogMessage("Press Enter to exit");
                logger.LogMessage("Press T to change interval");
                var keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.Enter)
                {
                    timer.Stop();
                    conditionFlag = false;
                }

                if(keyPressed.Key == ConsoleKey.T)
                {
                    timer.Stop();
                    Console.WriteLine();
                    timer.Interval = SetTimerInterval(logger);
                    timer.Start();
                }
            }

        }

        private static int SetTimerInterval(StooqLogger logger)
        {
            logger.LogMessage("Set the timer interval in miliseconds");
            int interval;
            var isInteger = int.TryParse(Console.ReadLine().ToString(), out interval);
            while (!isInteger)
            {
                logger.LogMessage("Type in an integer value");
                isInteger = int.TryParse(Console.ReadLine().ToString(), out interval);
            }

            return interval;
        }
    }
}
