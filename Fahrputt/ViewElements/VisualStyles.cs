using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrputt.ViewElements
{
    public static class VisualStyles
    {

        public const double FontSizeBig = 24;
        public const double FontSizeMedium = 12;

        public const int RoundedBorder = 8;


        public static Thickness BigPadding = new Thickness(4);
        public static Thickness MediumPadding = new Thickness(2);
        public static Thickness SmallPadding = new Thickness(1);

        public static Thickness BigMargin = new Thickness(8);
        public static Thickness MediumMargin = new Thickness(6);
        public static Thickness SmallMargin = new Thickness(2);

        //ColorIdea 1: https://www.colourlovers.com/palette/77121/Good_Friends

        public static Color WarningColor = new Color(212, 92, 92); //rötlich
        public static Color RegularColor = new Color(32, 65, 65); //grün grau
        public static Color FixedColor = new Color(67, 160, 71); //grün kräftig
        public static Color SetFavoriteColor = new Color(255, 140, 0); //gelborange

    }
}
