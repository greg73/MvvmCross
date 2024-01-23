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
                    break;
                case "yellow":
                    return Color.Yellow;
                    break;
                case "brown":
                    return Color.Brown;
                    break;
                case "orange":
                    return Color.Orange;
                    break;
                default:
                    return Color.Black;
                    break;
            }
        }
    }
}
