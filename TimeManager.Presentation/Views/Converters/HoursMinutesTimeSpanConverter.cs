using System;
using System.Globalization;
using System.Windows.Data;

namespace TimeManager.Presentation.Views.Converters
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
