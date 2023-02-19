using Fahrputt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrputt.ViewElements
{
    public class ElevatorStack : HorizontalStackLayout
    {
        public ElevatorStack(ElevatorInfo elevator, bool isElevator)
        {
            Image image = new Image();

            if(isElevator)
                image.Source = new FileImageSource
                {
                    File = "aufzug.png"
                };
            else
                image.Source = new FileImageSource
                {
                    File = "treppe.png"
                };

            image.HeightRequest = 32;
            image.WidthRequest = 32;


            this.Add(image);
            WarningInfoVerticalStack warningInfoStack = new WarningInfoVerticalStack(elevator);
            this.Add(warningInfoStack);
        }
    }
}
