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
                case "I am green!":
                    return Color.Green;
                case "I am yellow!":
                    return Color.Yellow;
                case "I am brown!":
                    return Color.Brown;
                case "I am orange!":
                    return Color.Orange;
                default:
                    return Color.Black;
            }
        }
    }
}
