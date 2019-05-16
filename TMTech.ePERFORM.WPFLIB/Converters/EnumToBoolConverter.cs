using System;
using System.Windows.Data;

namespace TMTech.Shared.WPFLIB.Converters
{
    public class EnumToBoolConverter : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter.Equals(value)) return true; else return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(bool))
            {
                bool b = (bool)value;
                if (b) return parameter;
            }
            return null;
        }
        #endregion
    }
}
