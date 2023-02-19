﻿using Fahrputt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrputt.ViewElements
{
    class WarningInfoVerticalStack : VerticalStackLayout
    {
        public WarningInfoVerticalStack(ElevatorInfo elevator) 
        {
            Padding = VisualStyles.SmallPadding;
            Margin = VisualStyles.BigMargin;

            if(elevator.ElevatorWarningState == ElevatorWarningState.Unchanged)
            {
                BackgroundColor = VisualStyles.RegularColor;
            }
            else if( elevator.ElevatorWarningState == ElevatorWarningState.WarningChanged || elevator.ElevatorWarningState == ElevatorWarningState.New)
                BackgroundColor = VisualStyles.WarningColor;

            else if (elevator.ElevatorWarningState == ElevatorWarningState.Fixed)
            {
                BackgroundColor = VisualStyles.FixedColor;
            }

            LabelInfoText warningInfoLabel = new LabelInfoText(elevator.WarningType, FontAttributes.Bold);
            LabelInfoText locacionInfoLabel = new LabelInfoText(elevator.LocationText, FontAttributes.None);

            this.Add(warningInfoLabel);
            this.Add(locacionInfoLabel);

        }
    }
}
