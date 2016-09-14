using System;
using System.Windows;
using System.Windows.Data;

namespace TMTech.Shared.WPFLIB.Converters
{
    /// <summary>
    /// This class defines a value converter to inverse a boolean value.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolInverseConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool b = false;
            bool.TryParse(value.ToString(), out b);
            return !b;
        }



        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool b = false;
            bool.TryParse(value.ToString(), out b);
            return !b;
        }
    }
}
