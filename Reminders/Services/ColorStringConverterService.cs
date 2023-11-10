using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics.Converters;

namespace Reminders.Services;

public class StringToColorConverterService : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ColorTypeConverter converter = new();
        if ((string)value == string.Empty)
        {
            value = "#000000";
        }
        Color color = (Color) converter.ConvertFromInvariantString((string)value);
        return color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
