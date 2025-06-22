using Microsoft.UI.Xaml.Data;
using System;

namespace Frontend.Converters
{
    public class DateOnlyToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateOnly dateOnly)
                return new DateTimeOffset(dateOnly.Year, dateOnly.Month, dateOnly.Day, 0, 0, 0, TimeSpan.Zero);

            if (value is DateTimeOffset dateTimeOffset)
                return dateTimeOffset;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dateTimeOffset)
                return DateOnly.FromDateTime(dateTimeOffset.DateTime);

            return null;
        }
    }
}
