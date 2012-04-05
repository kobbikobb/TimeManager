using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TimeManagerLib.View.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(String))]
    public class HoursMinutesTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            TimeSpan newValue;
            if (TimeSpan.TryParse(value.ToString(), out newValue))
                return newValue.ToString(@"hh\:mm");

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            TimeSpan newValue;
            if (TimeSpan.TryParseExact(value.ToString(), @"hh\:mm", CultureInfo.CurrentCulture, out newValue))
                return newValue;

            return value;
        }
    }
}
