using Abot2.Crawler;
using Abot2.Poco;

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
                MaxPagesToCrawl = 1,
                MinCrawlDelayPerDomainMilliSeconds = 3000
            };
            politeWebCrawlerElevators = new PoliteWebCrawler(config);
            politeWebCrawlerStations = new PoliteWebCrawler(config);
        }

        public async Task CrawlBrokenElevatorData()
        {
            var crawlResult = await politeWebCrawlerElevators.CrawlAsync(new Uri("https://sbahn.berlin/fahren/bahnhofsuebersicht/barrierefrei-unterwegs/aufzugs-fahrtreppenstoerung"));
        }

    }
}
