using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TMTech.Shared.WPFLIB.Converters
{

    [ValueConversion(typeof(object), typeof(Visibility))]
    public class GeneralVisibilityConverter : IValueConverter
    {

        /// <summary>
        /// Inverse the bool value when converting
        /// </summary>
        public bool Inverse { get; set; } = false;

        /// <summary>
        /// Hide the element if invisible
        /// </summary>
        public bool Hidden { get; set; } = false;


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var visible = false;

            if (value != null)
            {
                if (value.GetType() == typeof(bool))
                {
                    visible = (bool)value;
                }
                else if (value.GetType() == typeof(string)) {
                    if (!string.IsNullOrEmpty((string)value)) visible = true;
                }
                else if (value.GetType() == typeof(int))
                {
                    if ((int)value > 0) visible = true;
                }
                else if (value.GetType() == typeof(double))
                {
                    if ((double)value > 0) visible = true;
                }
                else if (value.GetType() == typeof(decimal))
                {
                    if ((decimal)value > 0) visible = true;
                }
                else
                {
                    visible = true;
                }


            }


            // Inverse the value
            if (Inverse) visible = !visible;

            if (visible)
                return Visibility.Visible;
            else if (Hidden)
                return Visibility.Hidden;
            else
                return Visibility.Collapsed;

        }



        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
