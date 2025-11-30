using Microsoft.UI.Xaml.Data;
using System;

namespace Frontend.Converters;

public class DoubleToDecimalConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is decimal decimalValue)
        {
            return System.Convert.ToDouble(decimalValue);
        }

        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is double doubleValue)
        {
            return System.Convert.ToDecimal(Math.Round(doubleValue, 2));
        }

        return 0m;
    }
}
