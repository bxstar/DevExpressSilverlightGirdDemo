using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GridDemo
{
    public enum ColumnChooserType { Default, Custom }
    public class GridColumnChooserToExpanderVisible : IValueConverter
    {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (ColumnChooserType)value == ColumnChooserType.Custom ? Visibility.Visible : Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
