using CommunityToolkit.Mvvm.Input;
using Fahrputt.Logic;
using Fahrputt.Models;
using Fahrputt.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fahrputt.ViewModels
{
//    public partial class StationItemsViewModel : BaseStationViewModel
//    {

        
//        public ObservableCollection<StationData> StationDatas { get; } = new();
//        StationDataService monkeyService;
//        public StationItemsViewModel(StationDataService monkeyService)
//        {
//            //Title = "Monkey Finder";
//            this.monkeyService = monkeyService;
//            Console.WriteLine("INIT SXERVICEEEE");
//            Scraper.GetInstance.OnNewStationDataAdded += OnNewStationDataAdded;

//            List<StationData> data =  Scraper.GetInstance.Stationdatas;
//            foreach(StationData station in data)
//            {
//                Console.WriteLine("PREADD");

//                StationDatas.Add(station);
//            }

//        }

//        private void OnNewStationDataAdded(StationData obj)
//        {
//            Console.WriteLine("ADD");

//            StationDatas.Add(obj);
//        }

//        [RelayCommand]
//        async Task GetMonkeysAsync()
//        {

//            Console.WriteLine("trytget");

//            //if (IsBusy)
//            //    return;

//            try
//            {
//                //IsBusy = true;
//                var monkeys =  monkeyService.GetStationList();

//                //if (Monkeys.Count != 0)
//                //    Monkeys.Clear();

//                foreach (var monkey in monkeys)
//                {
//                    Console.WriteLine("ddd" + monkey.StationName);
//                    StationDatas.Add(monkey);

//                }

//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
//                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
//            }
//            finally
//            {
//                //IsBusy = false;
//            }

//        }
//    }
}