using System;
using System.Globalization;
using System.Windows.Data;

namespace GridDemo
{
    public class XmlIntegerConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CultureInfo InvariantCulture = new CultureInfo("");
            int i = int.Parse((string)value, InvariantCulture);
            return i;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value).ToString();
        }
    }
    public class XmlDateTimeConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value))
                return null;
            DateTime dt = DateTime.Parse((string)value, CultureInfo.InvariantCulture);
            return dt;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? ((DateTime)value).ToString(CultureInfo.InvariantCulture) : null;
        }
    }
}
