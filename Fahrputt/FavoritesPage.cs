using Fahrputt.Logic;
using Fahrputt.ViewElements;

namespace Fahrputt
{
    class FavoritesPage : ContentPage
    {
        FahrputtAppManager appManager;

        ScrollView scrollView;

        public VerticalStackLayout stationStack;

        private Dictionary<string, StationDataStack> stationDataStacks;

        private bool IsLoaded = false;


        public FavoritesPage()
        {
            Console.WriteLine("FAV PAGA INIT START");
            stationDataStacks = new Dictionary<string, StationDataStack>();
            appManager = FahrputtAppManager.GetInstance;

            appManager.OnFavoriteRemoved += OnFavoriteRemoved;
            appManager.OnFavoriteAdded += OnFavoriteAdded;

            stationStack = new VerticalStackLayout();
            scrollView = new ScrollView();
            stationStack = new VerticalStackLayout();
            scrollView.Content = stationStack;
            Content = scrollView;

            if(appManager.FavoriteStations.Count> 0) 
            { 
                foreach(string favorite in appManager.FavoriteStations)
                {
                    OnFavoriteAdded(favorite);
                }
            }
            IsLoaded = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!IsLoaded)
                return;
            foreach (StationDataStack fav in stationDataStacks.Values)
            {
                fav.SetToFavorites(true);
            }
        }

        //New Favorite entry
        private void OnFavoriteAdded(string stationName)
        {
            if(!stationDataStacks.ContainsKey(stationName) && appManager.StationDatas.ContainsKey(stationName))
            {
                StationDataStack stationData = new StationDataStack(appManager.StationDatas[stationName]);
                stationData.ButtonSetFavorite.Clicked += OnStationClickFavorite;
                stationStack.Add(stationData);
                stationDataStacks.Add(stationName, stationData);
                stationData.SetToFavorites(true);
            }
        }

        //Removing favorite Entry
        private void OnFavoriteRemoved(string stationName)
        {
            if (stationStack.Contains(stationDataStacks[stationName]))
            {
                stationStack.Remove(stationDataStacks[stationName]);
                stationDataStacks.Remove(stationName);
            }
        }

        private void OnStationClickFavorite(object sender, EventArgs e)
        {
            ButtonSetFavorite favButton = (ButtonSetFavorite)sender;
            appManager.NewFavoriteClicked(favButton.StationName);
        }

    }
}
