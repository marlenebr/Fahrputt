using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrputt.ViewElements
{
    class LabelInfoText : Label
    {

        public LabelInfoText(string text,FontAttributes fontStyle)
        {
            Text = text;
            FontAttributes= fontStyle;
            FontSize = VisualStyles.FontSizeMedium;
            Padding = VisualStyles.MediumPadding;
            Margin = VisualStyles.BigMargin;
            LineBreakMode = LineBreakMode.WordWrap;
            WidthRequest = 250;
        }
    }
}