using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using Fahrputt.Logic;
using Fahrputt.Models;
using Fahrputt.ViewElements;

namespace Fahrputt
{
    class StationsMainPage : ContentPage
    {
        //public CollectionView collectionView;

        FahrputtAppManager appManager;

        ScrollView scrollView;

        public VerticalStackLayout stationStack;

        public Dictionary<string, StationDataStack> allStationStacks;

        List<string> favoritesToAdd;

        private bool IsLoaded = false;

        private LoadingStack loadingStack;

        public StationsMainPage()
        {
            this.Behaviors.Add(new StatusBarBehavior
            {
                StatusBarColor = Colors.Black
            });

            loadingStack = new LoadingStack();

            //this.Behaviors.Add(behavi)

            // Code to run on the main thread
            allStationStacks =  new Dictionary<string, StationDataStack>();
            favoritesToAdd = new List<string>();
            scrollView = new ScrollView();
            stationStack = new VerticalStackLayout();
            stationStack.Add(loadingStack);
            scrollView.Content= stationStack;
            Content = stationStack;
            

            Console.WriteLine("++++++++++++++++++++++++++++++INIT STATION PAGE");


            Console.WriteLine(stationStack.IsLoaded);

            appManager = FahrputtAppManager.GetInstance;
            appManager.OnFavoriteRemoved += OnFavoriteRemoved;
            appManager.OnFavoriteAdded += OnFavoriteAdded;

            if (appManager.InitDone)
            {
                OnInitializationDone();
            }
            else
            {
                appManager.OnInitializationDone += OnInitializationDone;
            }
            //FillDataTEST();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!IsLoaded)
                return;
            foreach (KeyValuePair<string,StationData> allData in appManager.StationDatas)
            {
                if (allStationStacks.ContainsKey(allData.Key))
                {
                    if(allStationStacks[allData.Key].ButtonSetFavorite.IsFavorite != allData.Value.IsFavourite)
                    allStationStacks[allData.Key].SetToFavorites(allData.Value.IsFavourite);
                }
            }
        }


        private void OnInitializationDone()
        {
            Console.WriteLine("STARTFILL?");
            //FillData();
        }

        private void FillData()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                foreach (StationData data in appManager.StationDatas.Values)
                {
                    OnNewStationDataAdded(data);
                }
                if(stationStack.Contains(loadingStack))
                    stationStack.Remove(loadingStack);
                scrollView.Content = stationStack;
                Content = scrollView;
                Console.WriteLine("-------fill done");
                IsLoaded = true;
            });
        }

        private void FillDataTEST()
        {


            MainThread.BeginInvokeOnMainThread(() =>
            {
                ElevatorInfo elevator = new ElevatorInfo();
                elevator.IsElevator = true;
                elevator.LocationText = "daaa";
                elevator.ElevatorWarningState = ElevatorWarningState.Unchanged;
                elevator.WarningType = "kaputt";

                StationData datas = new StationData();
                datas.StationName = "STATION";
                datas.Elevators = new List<ElevatorInfo>();
                datas.Elevators.Add(elevator);
                datas.Escalators = new List<ElevatorInfo>();

            OnNewStationDataAdded(datas);

            });
        }

        private void OnNewStationDataAdded(StationData obj)
        {
            Console.WriteLine("NEW STACK--------------------" + obj.StationName);

            StationDataStack stationData = new StationDataStack(obj);
            stationData.ButtonSetFavorite.Clicked += OnStationSetFavorite;
            stationStack.Add(stationData);
            allStationStacks.Add(obj.StationName,stationData);
        }

        private void OnStationSetFavorite(object sender, EventArgs e)
        {
            ButtonSetFavorite favButton = (ButtonSetFavorite)sender;
            appManager.NewFavoriteClicked(favButton.StationName);
        }

        private void OnFavoriteAdded(string obj)
        {
            if(allStationStacks.ContainsKey(obj))
            {
                allStationStacks[obj].SetToFavorites(true);

            }
            else
            {
                favoritesToAdd.Add(obj);
            }
        }

        private void OnFavoriteRemoved(string obj)
        {
            allStationStacks[obj].SetToFavorites(false);
        }
    }
}
