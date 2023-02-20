using Fahrputt.Models;

namespace Fahrputt.ViewElements
{
    class ButtonSetFavorite : Button
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

        private Color defaultColor;

        private ImageSource FavoriteImage;

        private ImageSource UnFavoriteImage;

        public ButtonSetFavorite(StationData stationData)
        {
            this.Style = VisualStyles.ButtonstyleDark;

            defaultColor = BackgroundColor;
            StationName = stationData.StationName;
            //Text = "(o)";
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
            ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Top, 6);
            IsFavorite = stationData.IsFavourite;
        }

        public void SetButtonColor()
        {
            if (isFavorite)
            {
                BackgroundColor = VisualStyles.SetFavoriteColor;
                ImageSource = FavoriteImage;
            }
            else
            {
                BackgroundColor = defaultColor;
                ImageSource= UnFavoriteImage;
            }
        }
    }
}
