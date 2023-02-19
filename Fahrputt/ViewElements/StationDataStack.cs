using Fahrputt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrputt.ViewElements
{
    class StationDataStack : VerticalStackLayout
    {
        public ButtonSetFavorite ButtonSetFavorite;
        public StationDataStack(StationData stationData)
        {
            //if(stationData.HasChangedOrIsNew) 
            //{ 
            //BackgroundColor = VisualStyles.RegularColor;        
            //}  
            //else
            //{
            //    BackgroundColor = VisualStyles.WarningColor;
            //}
            BackgroundColor = VisualStyles.RegularColor;

            Padding = VisualStyles.SmallPadding;
            Margin = VisualStyles.MediumMargin;

            LabelStationName labelStationName = new LabelStationName(stationData.StationName);
            
            HorizontalStackLayout horizontalstack = new HorizontalStackLayout();
            horizontalstack.Add(labelStationName);

            ButtonSetFavorite = new ButtonSetFavorite(stationData);
            horizontalstack.Add(ButtonSetFavorite);

            this.Add(horizontalstack);

            foreach (ElevatorInfo elevator in stationData.Elevators)
            {
                ElevatorStack warningInfoStack = new ElevatorStack(elevator,true);
                this.Add(warningInfoStack);
            }

            foreach (ElevatorInfo escalator in stationData.Escalators)
            {
                ElevatorStack warningInfoStack = new ElevatorStack(escalator, false);
                this.Add(warningInfoStack);
            }
        }

        public void SetToFavorites(bool isFavorite)
        {
            ButtonSetFavorite.IsFavorite = isFavorite;
        }
    }
}
