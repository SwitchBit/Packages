using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwitchBit.Xaml.Xamarin.Controls
{
    public class GridHeaderCell : ViewCell
    {
        public GridHeaderCell()
        {
            this.Height = 25;
            var title = new Label
            {
                //Font = Font.SystemFontOfSize(NamedSize.Small, FontAttributes.Bold),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center
            };
            title.FontFamily = "Arial";
            title.SetBinding(Label.TextProperty, "Key");

            View = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 25,
                BackgroundColor = Color.FromRgb(52, 152, 218), //TODO: Lame
                Padding = 5,
                Orientation = StackOrientation.Horizontal,
                Children = { title }
            };
        }
    }
}
