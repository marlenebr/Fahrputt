using Fahrputt.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Fahrputt.ViewElements
{
    class ButtonSetFavorite : ImageButton
    {
        public string StationName;

        private bool isFavorite;
        public bool IsFavorite
        {
            get
            {
                return isFavorite;
            }
            set
            {
                isFavorite = value;
                SetButtonColor();
            }
        }

        private ImageSource FavoriteImage;

        private ImageSource UnFavoriteImage;

        public ImageSource TEST;

        public ButtonSetFavorite(StationData stationData)
        {
            StationName = stationData.StationName;
            CornerRadius = VisualStyles.RoundedBorder;
            FavoriteImage = new FileImageSource
            {
                File = "iconmonstr_star_filled_32.png"
            };

            UnFavoriteImage = new FileImageSource
            {
                File = "iconmonstr_star_lined_32.png"
            };
            VerticalOptions = LayoutOptions.Center;
            HorizontalOptions = LayoutOptions.Center;
            IsFavorite = stationData.IsFavourite;
        }

        private void SetButtonColor()
        {
            if (isFavorite)
            { 
                Source = FavoriteImage;
            }
            else
            {
                Source= UnFavoriteImage;
            }
        }
    }
}
