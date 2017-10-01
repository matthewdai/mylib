using System;
using System.Globalization;
using System.Windows.Data;

namespace TMTech.Shared.WPFLIB.Converters
{


    /// <summary>
    /// Convert the excel column header to specified name
    /// </summary>
    public class ExcelHeaderIndexToNameConverter : IValueConverter
    {
        /// <summary>
        /// Defines the names 
        /// </summary>
        public string[] Names { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)value;

            return Names != null && index >=0 && index < Names.Length ? Names[index] : null;
        }

        //public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (values != null && values.Length == 2)
        //    {
        //        var headings = values[1] as String[];
        //        int colIndex = (int)(values[0]);

        //        if (headings != null && colIndex >= 0 && colIndex < headings.Length)
        //            return headings[colIndex];
        //        else
        //            return colIndex;
        //    }

        //    return null;

        //}

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }



    /// <summary>
    /// Convert column index to letter
    /// </summary>
    [ValueConversion(typeof(int), typeof(string))]
    public class ExcelHeaderLetterConverter : IValueConverter
    {
        public int Offset { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int colIndex = (int)value + 1 + Offset;

            if (colIndex > 0)
                return GetExcelColumnName(colIndex);
            else
                return null;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        public static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = System.Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
    }


    /// <summary>
    /// Convert column index to letter
    /// </summary>
    [ValueConversion(typeof(int), typeof(string))]
    public class ExcelHeaderIndexShiftConverter : IValueConverter
    {
        public int Offset { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int colIndex = (int)value + 1 + Offset;

            if (colIndex > 0)
                return colIndex;
            else
                return null;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
