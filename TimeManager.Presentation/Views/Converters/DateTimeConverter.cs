using System;
using System.Globalization;
using System.Windows.Data;

namespace TimeManager.Presentation.Views.Converters
{
    [ValueConversion(typeof(DateTime), typeof(String))]
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            DateTime newDateTime;
            if (DateTime.TryParse(value.ToString(), out newDateTime))
                return newDateTime.ToString(@"dd.MM.yy HH:mm");

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            DateTime newDateTime;
            if (DateTime.TryParseExact(value.ToString(), @"dd.MM.yy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None, out newDateTime))
                return newDateTime;

            TimeSpan newTimeStamp;
            if (TimeSpan.TryParseExact(value.ToString(), @"HH:mm", CultureInfo.CurrentCulture, out newTimeStamp))
                return DateTime.Today.Add(newTimeStamp);

            return value;
        }
    }
}
