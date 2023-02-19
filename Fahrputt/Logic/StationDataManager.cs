using Fahrputt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fahrputt.Logic
{
    public class StationDataManager
    {
        string directoryName;

        string FileNameStationdata = "StationData.json";

        string FileNameFavoritestations = "FavoriteStations.json";


        string DataResourceText;

        public Action<StationData[]> OnStationdataFileReadDone;

        public Action<bool> OnStationdataFileWriteDone;


        public Action<string[]> OnFavoritesFileReadDone;


        FahrputtAppManager appManager;

        public StationDataManager(FahrputtAppManager manager) 
        {
            appManager = manager;
        }

        //public async void Loaddata()
        //{
        //    using var stream = await FileSystem.OpenAppPackageFileAsync(FileNameStationdata);
        //    using var reader = new StreamReader(stream);

        //    var contents = reader.ReadToEnd();
        //}

        public async Task ReadFavoritesFileAsync()
        {
            try
            {
                string path = Path.Combine(FileSystem.Current.CacheDirectory, FileNameFavoritestations);
                if (!File.Exists(path))
                {
                    Console.WriteLine("--------------------File Not existing:" + path);
                    OnFavoritesFileReadDone?.Invoke(null);

                }
                else
                {
                    using (StreamReader sr = File.OpenText(path))
                    {
                        DataResourceText = await sr.ReadToEndAsync();
                        if (DataResourceText == null || DataResourceText == "")
                        {
                            Console.WriteLine("---------DataResourceText of Favs is empty");
                            OnFavoritesFileReadDone?.Invoke(null);
                        }
                        else
                        {
                            string[] favoriteStations = JsonConvert.DeserializeObject<string[]>(DataResourceText);

                            foreach (string sstation in favoriteStations)
                            {

                                Console.WriteLine("FAV READED!!!!!!!!!!!!!!!!: " + sstation);
                            }
                            OnFavoritesFileReadDone?.Invoke(favoriteStations);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                DataResourceText = ex.Message;
                OnFavoritesFileReadDone?.Invoke(null);

            }
        }

        public async Task ReadFile()
        {

                string path = Path.Combine(FileSystem.Current.CacheDirectory, FileNameStationdata);
                if (!File.Exists(path))
                {
                    Console.WriteLine("--------------------File Not existing:" + path);
                    OnStationdataFileReadDone?.Invoke(null);

                }
                else
                {
                    using (StreamReader sr = File.OpenText(path))
                    {
                        DataResourceText = await sr.ReadToEndAsync();
                        if (DataResourceText == null || DataResourceText == "")
                        {
                            Console.WriteLine("------------DataResourceText of Stations is empty");
                            OnStationdataFileReadDone?.Invoke(null);
                        }
                        else
                        {
                        try
                        {
                            StationData[] stations = JsonConvert.DeserializeObject<StationData[]>(DataResourceText);
                            foreach (StationData sstation in stations)
                            {

                                Console.WriteLine("SERIALIZED!!!!!!!!!!!!!!!!!: " + sstation.StationName + "Is Fav: " + sstation.IsFavourite);
                            }
                            OnStationdataFileReadDone?.Invoke(stations);
                        }
                        catch (FileNotFoundException ex)
                        {
                            DataResourceText = ex.Message;
                            OnStationdataFileReadDone?.Invoke(null);

                        }
                        
                    }
                }
            }


            //OnStationdataFileReadDone?.Invoke(null);

        }

        //[RelayCommand]
        public async Task WriteFile()
        {
            StationData[] stationData = appManager.StationDatas.Values.ToArray();
            try
            {
                string path = Path.Combine(FileSystem.Current.CacheDirectory, FileNameStationdata);

                using (StreamWriter sw = new StreamWriter(path))
                {
                     string stationString = JsonConvert.SerializeObject(stationData);
                     await sw.WriteAsync(stationString);
                }
                //OnStationdataFileWriteDone?.Invoke(true);
            }
            catch (FileNotFoundException ex)
            {
               Console.WriteLine(ex.Message);
                //OnStationdataFileWriteDone?.Invoke(false);

            }
        }

        public async Task WriteFavoritesFile()
        {
            string[] favoritestations = appManager.FavoriteStations.ToArray();
            try
            {
                string path = Path.Combine(FileSystem.Current.CacheDirectory, FileNameFavoritestations);

                using (StreamWriter sw = new StreamWriter(path))
                {
                    string favString = JsonConvert.SerializeObject(favoritestations);
                    await sw.WriteAsync(favString);

                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}