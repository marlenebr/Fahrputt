using Fahrputt.Logic;
using Fahrputt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fahrputt.Services
{
    public class StationDataService
    {

        readonly List<StationData> StationList;



        //public StationDataService()
        //{
        //    Scraper.GetInstance.OnNewStationDataAdded += OnNewStationData;
        //    //HIER!!!
        //    // items = new List<StationData>();
        //    StationList = Scraper.GetInstance.Stationdatas;
        //    Console.WriteLine("added items to mackDataStore: " + StationList.Count);

        //    //items = new List<StationData>()
        //    //{
        //    //    new StationData { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
        //    //    new StationData { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
        //    //    new StationData { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
        //    //    new StationData { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
        //    //    new StationData { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
        //    //    new StationData { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
        //    //};

        //    //Task.WaitAll(GetTheDataAsync());

        //}

        //public List<StationData> GetStationList()
        //{
        //    return StationList;
        //}


        //private void OnNewStationData(StationData obj)
        //{
        //    StationList.Add(obj);
        //}

        //public async Task<bool> AddItemAsync(StationData item)
        //{
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> UpdateItemAsync(StationData item)
        //{
        //    var oldItem = items.Where((StationData arg) => arg.Id == item.Id).FirstOrDefault();
        //    items.Remove(oldItem);
        //    items.Add(item);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> DeleteItemAsync(string id)
        //{
        //    var oldItem = items.Where((StationData arg) => arg.Id == id).FirstOrDefault();
        //    items.Remove(oldItem);

        //    return await Task.FromResult(true);
        //}

        //public async Task<StationData> GetItemAsync(string id)
        //{
        //    return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        //}

        //public async Task<IEnumerable<StationData>> GetItemsAsync(bool forceRefresh = false)
        //{
        //    return await Task.FromResult(items);
        //}
    }
}

