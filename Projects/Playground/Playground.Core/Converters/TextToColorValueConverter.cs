using System.Drawing;
using System.Globalization;
using MvvmCross.Plugin.Color;

namespace Playground.Core.Converters
{
    public class TextToColorValueConverter : MvxColorValueConverter
    {
        protected override Color Convert(object value, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "green":
                    return Color.Green;
                case "yellow":
                    return Color.Yellow;
                case "brown":
                    return Color.Brown;
                case "orange":
                    return Color.Orange;
                default:
                    return Color.Black;
            }
        }
    }
}
