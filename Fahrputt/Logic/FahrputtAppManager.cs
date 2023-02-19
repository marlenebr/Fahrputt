using Fahrputt.Models;
using System.Collections.Generic;
using System.Windows.Markup;


namespace Fahrputt.Logic
{
    public class FahrputtAppManager
    {
        private Dictionary<string, StationData> ScrapedStationDatas;

        private Dictionary<string, StationData> LoadedStationData;



        public Dictionary<string, StationData> StationDatas;

        public List<string> FavoriteStations;


        private static FahrputtAppManager instance = null;

        public bool ScrapingDone = false;

        public bool FileReadDone = false;

        public bool FavoritesReadedDone = false;


        public bool InitDone = false;

        bool writeFileOnLoad = true;


        public static FahrputtAppManager GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new FahrputtAppManager();
                return instance;
            }
        }


        public Action OnInitializationDone;

        public Action<string> OnFavoriteAdded;

        public Action<string> OnFavoriteRemoved;


        Scraper scraper;
        public StationDataManager stationDataManger;

        public FahrputtAppManager()
        {

            Console.WriteLine("+++++++++++++++++++++++++++INIT MAIJN FAHRPUT");

            FavoriteStations = new List<string>();
            stationDataManger = new StationDataManager(this);
            //if (scraper.ScrapeDone)
            //{
            //    OnScrapeDone(StationDatas);
            //}
            //else
            //{
            //    scraper.OnScrapeDone += OnScrapeDone;
            //}
            //TODO: COMPARE
            Task.Run(DoAllAsyncInit);


        }

        private async Task DoAllAsyncInit()
        {
            scraper = new Scraper();
            scraper.OnScrapeDone += OnScrapeDone;
            await Task.Run(scraper.GetAllElevatorDataAsync);

            stationDataManger.OnStationdataFileReadDone += OnStationDataFilereadDone;
            await Task.Run(stationDataManger.ReadFile);

            stationDataManger.OnFavoritesFileReadDone += OnFavoritesFilereaddone;
            await Task.Run(stationDataManger.ReadFavoritesFileAsync);

            //FinishInitializazion();
            //Console.WriteLine("DOOONNEEEEE");
        }

        //private void OnStationdataFileWriteDone(bool succes)
        //{
        //    Console.WriteLine("AWAIT WRITE STATION DATA");
        //    OnInitializationDone?.Invoke();
        //}

        private void OnScrapeDone(Dictionary<string, StationData> stationData)
        {
            ScrapingDone = true;
            ScrapedStationDatas = stationData;
            //object value = Task.Run(stationDataManger.WriteFileAsync);
            if (FileReadDone)
            {
                FinishInitializazion();
            }
        }

        //private void OnFavoritesFilereaddone(string[] obj)
        //{
        //    if(obj != null)
        //    {
        //        foreach(string stationName in obj)
        //        {
        //            NewFavoriteSet(stationName, true);
        //        }
        //    }
        //}
        private void OnFavoritesFilereaddone(string[] favoriteStationName)
        {
            if(favoriteStationName ==null)
            {
                return;
            }
            foreach (string stationName in favoriteStationName)
            {
                SetDataFavorites(stationName);
            }
            FavoritesReadedDone = true;

        }

        private void OnStationDataFilereadDone(StationData[] stationData)
        {
            FileReadDone = true;
            LoadedStationData = new Dictionary<string, StationData>();
            if(stationData != null)
            {
                foreach (StationData data in stationData)
                {
                    LoadedStationData.Add(data.StationName, data);
                }
            }

            if (ScrapingDone)
            {
                FinishInitializazion();
            }
        }

        private Dictionary<string, StationData> FinishInitializazion()
        {
            Dictionary<string, StationData> finalStationList = new Dictionary<string, StationData>();
            //Compare Loaded Data and Scraped Data

            if (LoadedStationData != null && LoadedStationData.Count > 0)
            {
                foreach (StationData scrapedStationData in ScrapedStationDatas.Values)
                {
                    StationData changedData = new StationData();
                    changedData.StationName = scrapedStationData.StationName;
                    changedData.Elevators = new List<ElevatorInfo>();
                    changedData.Escalators = new List<ElevatorInfo>();

                    if (LoadedStationData.ContainsKey(scrapedStationData.StationName))
                    {
                        changedData.Elevators = CompareAndCreateElevatorInfo(scrapedStationData.Elevators, LoadedStationData[scrapedStationData.StationName].Elevators);
                        changedData.Escalators = CompareAndCreateElevatorInfo(scrapedStationData.Escalators, LoadedStationData[scrapedStationData.StationName].Escalators);
                    }

                    finalStationList.Add(changedData.StationName, changedData);
                }

            }
            else
            {
                finalStationList = ScrapedStationDatas;
            }

            if (FavoritesReadedDone)
            {
                foreach(StationData data in finalStationList.Values)
                {
                    if (FavoriteStations.Contains(data.StationName))
                    {
                        data.IsFavourite = true;
                    }
                }
            }
            StationDatas = finalStationList;
            InitDone = true;
            //________________________DEBUG FILE WRITE_________________________________________________
            Task.Run(stationDataManger.WriteFile);

            OnInitializationDone?.Invoke();


            return finalStationList;

        }

        private List<ElevatorInfo> CompareAndCreateElevatorInfo(List<ElevatorInfo> scrapedElevators, List<ElevatorInfo> loadedElevators)
        {
            List<ElevatorInfo> newElevators = new List<ElevatorInfo>();
            //Gleiche Haltestelle vorhanden.
            foreach (ElevatorInfo Scrapedelevator in scrapedElevators)
            {
                bool isAlreadyExisting = false;
                //rehenfolge Beachten?
                foreach (ElevatorInfo LoadedElevator in loadedElevators)
                {
                    if (Scrapedelevator.LocationText.Equals(LoadedElevator.LocationText))
                    {
                        //Both Data have info about the same elevator

                        if (Scrapedelevator.WarningType.Equals(LoadedElevator.WarningType))
                        {
                            //Unchanged
                            ElevatorInfo changedElevator = new ElevatorInfo();
                            changedElevator.WarningType = Scrapedelevator.WarningType;
                            changedElevator.LocationText = Scrapedelevator.LocationText;
                            changedElevator.IsElevator = Scrapedelevator.IsElevator;
                            //changedElevator.HasChanged = false;
                            changedElevator.ElevatorWarningState = ElevatorWarningState.Unchanged;
                            newElevators.Add(changedElevator);
                            isAlreadyExisting = true;
                            continue;
                        }

                        else
                        {
                            //Same Elevator But Changed
                            ElevatorInfo changedElevator = new ElevatorInfo();
                            changedElevator.WarningType = Scrapedelevator.WarningType;
                            changedElevator.LocationText = Scrapedelevator.LocationText; //Same as from Loaded file
                            changedElevator.IsElevator = Scrapedelevator.IsElevator;
                            changedElevator.ElevatorWarningState = ElevatorWarningState.WarningChanged;
                            newElevators.Add(changedElevator);
                            isAlreadyExisting = true;
                            continue;
                        }

                    }

                    //are we at the very end of the loop? and it was not already existing?
                }
                if (!isAlreadyExisting)
                {
                    //There is a new elevator from fresh scraped data. 
                    ElevatorInfo changedElevator = new ElevatorInfo();
                    changedElevator.WarningType = Scrapedelevator.WarningType;
                    changedElevator.LocationText = Scrapedelevator.LocationText; //Same as from Loaded file
                    changedElevator.IsElevator = Scrapedelevator.IsElevator;
                    changedElevator.ElevatorWarningState = ElevatorWarningState.New;
                    newElevators.Add(changedElevator);
                    //newStationdata.HasChangedOrIsNew = true;
                    continue;

                }
            }

            //Check for Fixed One 
            foreach (ElevatorInfo LoadedElevator in loadedElevators)
            {
                bool isAlreadyExisting = false;

                foreach (ElevatorInfo newComparedElevator in scrapedElevators)
                {
                    if (LoadedElevator.LocationText.Equals(newComparedElevator.LocationText))
                    {
                        isAlreadyExisting = true;
                        continue;
                    }
                }

                if (!isAlreadyExisting)
                {
                    //The Elevator loaded from file is not existing in the new Scraped Data List  - Seems to be Fixed
                    ElevatorInfo changedElevator = new ElevatorInfo();
                    changedElevator.WarningType = "It Seems to Be Fixed";
                    changedElevator.LocationText = LoadedElevator.LocationText; //Same as from Loaded file
                    changedElevator.IsElevator = LoadedElevator.IsElevator;
                    changedElevator.ElevatorWarningState = ElevatorWarningState.Fixed;
                    newElevators.Add(changedElevator);
                    //newStationdata.HasChangedOrIsNew = true;

                }
            }
            return newElevators;

        }

        private void SetDataFavorites(string station)
        {
            if (FavoriteStations.Contains(station))
            {
                FavoriteStations.Remove(station);
                if (StationDatas != null)
                    StationDatas[station].IsFavourite = false;
                OnFavoriteRemoved?.Invoke(station);
            }
            else
            {
                FavoriteStations.Add(station);
                OnFavoriteAdded?.Invoke(station);
                if (StationDatas != null)
                    StationDatas[station].IsFavourite = true;
            }
        }

        public void NewFavoriteClicked(string station)
        {
            SetDataFavorites(station);
            Task.Run(stationDataManger.WriteFavoritesFile);
            
            //TODO: NOT WRITE THAT OFTEN
            //Task.Run(stationDataManger.WriteFileAsync);

        }
    }
}
