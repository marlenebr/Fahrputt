using CommunityToolkit.Maui.Markup;
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



//Dark
        public static Color WarningColorDark = new Color(131, 53, 0); //redorange, dark
        public static Color ConfirmationColorDark = new Color(0, 171, 74); //grass green

        public static Color RegularIntensiveColorDark = new Color(0, 61, 51); //green strong grey, dark
        public static Color RegularColorDark = new Color(27, 36, 34); //green grey, dark
        public static Color TextColorDark = new Color(255, 255, 255); //white


        //Bright
        public static Color WarningColorBright = new Color(207, 102, 31); //redorange, normal
        public static Color ConfirmationColorBright = new Color(46, 247, 133); //grass green

        public static Color RegularIntensiveColorBright = new Color(152, 173, 169); //light green
        public static Color RegularColorBright = new Color(154, 168, 166); //light green
        public static Color TextColorBright = new Color(0, 0, 0); //black


        public static Color SetFavoriteColor = new Color(243, 219, 137); //gelborange



        public static Style ButtonstyleDark = new Style<Entry>(
            (Entry.TextColorProperty, TextColorDark),
            (Entry.BackgroundColorProperty, RegularIntensiveColorDark),
            (Entry.FontAttributesProperty, FontAttributes.Bold));


    }
}
