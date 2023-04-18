using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrputt.ViewElements
{
    internal class LoadingStack : VerticalStackLayout
    {
        public LoadingStack()
        {
            VerticalOptions = new LayoutOptions(LayoutAlignment.Center,true);
            Image image = new Image();
            image.Source = new FileImageSource
            {
                File = "aufzug_white_regular.png"
            };
          
            image.HeightRequest = 128;
            image.WidthRequest = 128;


            Label label = new Label();
            label.Text = "Loading...";
            label.TextColor = VisualStyles.TextColorBright;
            label.FontSize = VisualStyles.FontSizeBig;
            label.HeightRequest = 64;


            this.Add(image);
            this.Add(label);
        }
    }
}
