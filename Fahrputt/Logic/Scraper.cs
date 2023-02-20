using Abot2.Crawler;
using Fahrputt.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrputt.Logic
{
    class Scraper
    {
        public Crawler crawler;

        public bool ScrapeDone;
        public Scraper()
        {
            //Stationdatas = new List<StationData>();
            crawler = Crawler.GetInstance;
            crawler.politeWebCrawlerElevators.PageCrawlCompleted += ElevatorCrawlCompleted;//Several events available...
            crawler.politeWebCrawlerElevators.PageCrawlDisallowed += CrawlDisallowed;
            Console.WriteLine("LISTENING ");


        }

        private void CrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            throw new NotImplementedException();
        }

        public async Task GetAllElevatorDataAsync()
        {
            await Task.Run(crawler.CrawlBrokenElevatorData);
        }

        public Action<Dictionary<string,StationData>> OnScrapeDone;


        private void ElevatorCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var httpStatus = e.CrawledPage.HttpResponseMessage.StatusCode;
            string rawPageText = e.CrawledPage.Content.Text;
            ScrapeBrokenelEvators(rawPageText);
        }

        private void ScrapeBrokenelEvators(string rawPagetext)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawPagetext);
            List<HtmlNode> stations  = htmlDoc.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("c-accordion__tab"))
        .ToList();

            Dictionary<string,StationData> scrapedstations= new Dictionary<string, StationData>();

            foreach (HtmlNode station in stations)
            {
                //Eine Station
                string stationName = station.FirstChild.InnerText;

                Console.WriteLine("---- " + stationName + " ----");

                List<ElevatorInfo> elevatorInfo = new List<ElevatorInfo>();
                StationData stationData = new StationData {Id= Guid.NewGuid().ToString(), StationName = stationName };
                stationData.Elevators = new List<ElevatorInfo>();
                stationData.Escalators = new List<ElevatorInfo>();

                List<HtmlNode> elevatorsNode = station.Descendants("h4").ToList(); //Alle aufzüge einer Station
                if (elevatorsNode != null && elevatorsNode.Count > 0)
                {
                    List<HtmlNode> elevatorsOfStation = elevatorsNode[0].ParentNode.Descendants("li").ToList();
                    foreach (HtmlNode elevatorOfStation in elevatorsOfStation)
                    {
                        //Elevator

                        List<HtmlNode> elevatorOrStairs = elevatorOfStation.Descendants("span").Where(node => node.GetAttributeValue("data-tooltip", "").Contains("Aufzug")).ToList();

                        foreach (HtmlNode elev in elevatorOrStairs)
                        {
                            //Sollte nur einer sein
                            string elevatorInfoText = elev.ParentNode.Descendants("span").Where(node => node.GetAttributeValue("data-equip-col", "").Contains("description")).ToList()[0].InnerText;
                            string elevatorAnnouncementText = elev.ParentNode.Descendants("span").Where(node => node.GetAttributeValue("data-equip-col", "").Contains("announcement")).ToList()[0].InnerText;
                            stationData.Elevators.Add(new ElevatorInfo { ElevatorWarningState = ElevatorWarningState.Unchanged, WarningType = elevatorAnnouncementText, LocationText = elevatorInfoText, IsElevator = true });
                        }

                        //Escelator

                        List<HtmlNode> stairs = elevatorOfStation.Descendants("span").Where(node => node.GetAttributeValue("data-tooltip", "").Contains("Rolltreppe")).ToList();

                        foreach (HtmlNode stair in stairs)
                        {
                            //Sollte nur einer sein
                            string elevatorInfoText = stair.ParentNode.Descendants("span").Where(node => node.GetAttributeValue("data-equip-col", "").Contains("description")).ToList()[0].InnerText;
                            string elevatorAnnouncementText = stair.ParentNode.Descendants("span").Where(node => node.GetAttributeValue("data-equip-col", "").Contains("announcement")).ToList()[0].InnerText;
                            stationData.Escalators.Add(new ElevatorInfo { ElevatorWarningState = ElevatorWarningState.Unchanged, WarningType = elevatorAnnouncementText, LocationText = elevatorInfoText, IsElevator = false });
                        }
                    }
                }
                scrapedstations.Add(stationData.StationName,stationData);
            }

            ScrapeDone = true;
            OnScrapeDone?.Invoke(scrapedstations);

        }

        private void ScrapeAllStations(string rawPagetext)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawPagetext);
            List<HtmlNode> stations = htmlDoc.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("id", "").Contains("stations-list")).ToList();//[0].ChildNodes.ToList();

            foreach(HtmlNode station in stations)
            {
                Console.WriteLine(station.InnerText);
            }

        }
    }
}
