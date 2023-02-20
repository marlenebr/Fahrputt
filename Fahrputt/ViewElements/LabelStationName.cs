using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrputt.ViewElements
{
    class LabelStationName : Label
    {

        public LabelStationName(string text) 
        { 
            Text= text;
            FontSize = VisualStyles.FontSizeBig;
            Padding = VisualStyles.BigPadding;
            Margin = VisualStyles.MediumMargin;
            FontAttributes = FontAttributes.Bold;
            //Style = new Style(typeof(Label));

        }   

    }
}
