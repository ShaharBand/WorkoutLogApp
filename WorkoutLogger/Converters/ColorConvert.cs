using System;
using System.Globalization;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

namespace WorkoutLogger.Converters
{
    class ColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = (string)value;
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Contains("-")) return Color.FromRgb(204, 0, 0);
                else return Color.FromRgb(153, 204, 10);
                
            }
            return Color.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
