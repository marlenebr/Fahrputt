using System;
using System.Threading.Tasks;
using Abot2.Core;
using Abot2.Crawler;
using Abot2.Poco;
using Serilog;

namespace Fahrputt.Logic
{
    public sealed class Crawler
    {
        private static Crawler instance = null;
        public static Crawler GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Crawler();
                return instance;
            }
        }

        public PoliteWebCrawler politeWebCrawlerElevators;

        public PoliteWebCrawler politeWebCrawlerStations;


        private Crawler()
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 1, //Only crawl 10 pages
                MinCrawlDelayPerDomainMilliSeconds = 3000 //Wait this many millisecs between requests
            };
            politeWebCrawlerElevators = new PoliteWebCrawler(config);
            politeWebCrawlerStations = new PoliteWebCrawler(config);
            Console.WriteLine("Created polite ");
        }

        public async Task CrawlAsync()
        {
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Information()
            //    .WriteTo.Console()
            //    .CreateLogger();

            //Log.Logger.Information("Demo starting up!");


           // await CrawlAllstations();
            await CrawlBrokenElevatorData();
            
        }

        //private async Task CrawlAllstations()
        //{

        //    var crawlResult = await politeWebCrawlerStations.CrawlAsync(new Uri("https://sbahn.berlin/fahren/bahnhofsuebersicht/"));
        //}

        private async Task CrawlBrokenElevatorData()
        {

            //politeWebCrawler.PageCrawlCompleted += PageCrawlCompleted;//Several events available...

            var crawlResult = await politeWebCrawlerElevators.CrawlAsync(new Uri("https://sbahn.berlin/fahren/bahnhofsuebersicht/barrierefrei-unterwegs/aufzugs-fahrtreppenstoerung"));
        }

    }
}
