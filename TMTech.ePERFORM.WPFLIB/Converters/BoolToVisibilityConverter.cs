using System;
using System.Windows;
using System.Windows.Data;

namespace TMTech.Shared.WPFLIB.Converters
{
    /// <summary>
    /// This class defines a value converter to inverse a boolean value.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {

        /// <summary>
        /// Inverse the bool value when converting
        /// </summary>
        public bool Inverse { get; set; } = false;


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var visible = false;

            bool.TryParse(value.ToString(), out visible);

            // Inverse the value
            if (Inverse) visible = !visible;

            if (visible)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;

        }



        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
