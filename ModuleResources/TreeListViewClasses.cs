using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using DevExpress.Xpf.Grid;

namespace GridDemo
{
    public class NavigationStyleList : List<GridViewNavigationStyle> { }
    #region Converters
    public class NavigationStyleToIsEnabledConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return true;
            return !object.Equals(value, GridViewNavigationStyle.Row);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ShowTreeListLinesConverter : IValueConverter
    {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !object.Equals(value, TreeListLineStyle.None);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TreeListLineStyle.Solid : TreeListLineStyle.None;
        }
        #endregion
    }
    #endregion
}
