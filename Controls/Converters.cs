using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using System.Windows;
using DevExpress.Xpf.Editors;
using DevExpress.Data.Filtering;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Markup;
using System.Collections;

namespace GridDemo
{
    public class MultiSelectModeConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !object.Equals((MultiSelectMode)value, MultiSelectMode.None);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? MultiSelectMode.Row : MultiSelectMode.None;
        }
    }
    public class PastUnderFocusedRowToSelectedIndexConverter : MarkupExtension, IValueConverter
    {
        public PastUnderFocusedRowToSelectedIndexConverter() { }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool)value) ? 0 : 1;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((int)value == 0) ? true : false;
        }
        #endregion
    }
    public class GeneratingDataToWaitIndicatorTypeConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? WaitIndicatorType.None : WaitIndicatorType.Panel;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("This method shouldn't be called");
        }
    }
    public class IssueStatusImageConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            string name = ((string)value).Replace(" ", "");
            string path = "GridDemo.Images.IssueIcons." + name + ".png";
            return DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromEmbeddedResource(Assembly.GetExecutingAssembly(), path);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IdToUriConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return "http://devexpress.com/Support/Center/p/" + value.ToString() + ".aspx";
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    public class CategoryDataToImageSourceConverter : BytesToImageSourceConverter
    {
        static Dictionary<string, object> cachedImages = new Dictionary<string, object>();

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            CategoryData categoryData = value as CategoryData;
            if (value == null) return null;
            if (cachedImages.ContainsKey(categoryData.Name))
            {
                return cachedImages[categoryData.Name];
            }
            else
            {
                object image = base.Convert(categoryData.Picture, targetType, parameter, culture);
                cachedImages.Add(categoryData.Name, image);
                return image;
            }
        }
    }
    public class BooleanToDefaultBooleanConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NavigationStyleToTextConverter : IValueConverter
    {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return value;
            if (object.Equals((GridViewNavigationStyle)value, GridViewNavigationStyle.Cell))
                return "By Cell";
            if (object.Equals((GridViewNavigationStyle)value, GridViewNavigationStyle.Row))
                return "By Row";
            return value;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class ScrollingAnimationDurationToBooleanConverter : IValueConverter
    {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return System.Convert.ToDouble(value) > 0;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return System.Convert.ToBoolean(value) ? 350 : 0;
        }
        #endregion
    }
    public class ShowSearchPanelModeToTextConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ShowSearchPanelMode? showSearchPanelModeq = (ShowSearchPanelMode?)value;
            if (showSearchPanelModeq == null)
                return value;
            ShowSearchPanelMode showSearchPanelMode = showSearchPanelModeq.Value;
            switch (showSearchPanelMode)
            {
                case ShowSearchPanelMode.Default: return "Default";
                case ShowSearchPanelMode.Always: return "Always";
                case ShowSearchPanelMode.Never: return "Never";
            }
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class FindModeToTextConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FindMode? findModeq = (FindMode?)value;
            if (findModeq == null)
                return value;
            FindMode findMode = findModeq.Value;
            switch (findMode)
            {
                case FindMode.Always: return "Always";
                case FindMode.FindClick: return "Find on Click";
            }
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FilterConditionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FilterCondition? filterConditionq = (FilterCondition?)value;
            if (filterConditionq == null)
                return value;
            FilterCondition filterCondition = filterConditionq.Value;
            switch (filterCondition)
            {
                case FilterCondition.Default: return "Default";
                case FilterCondition.Contains: return "Contains";
                case FilterCondition.StartsWith: return "Starts With";
                case FilterCondition.Like: return "Like";
            }
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SearchPanelModeConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((ListBoxEditItem)value).Content;
        }
    }
    public class GroupNameToImageConverter : IValueConverter
    {
        public static List<string> images = new List<string> { "administration", "inventory", "manufacturing", "quality", "research", "sales" };
        public static string GetImagePathByGroupName(string groupName)
        {
            groupName = groupName.ToLower();
            foreach (string item in images)
            {
                if (groupName.Contains(item))
                {
                    return "/GridDemo;component/Images/MultiView/GroupName/" + item + ".png";
                }
            }
            return groupName;
        }
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            return GetImagePathByGroupName((string)value);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class HeaderToImageConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            return "/GridDemo;component/Images/MultiView/" + ((string)value).Replace(" ", String.Empty) + ".png";
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ColumnHeaderTextConverter : IValueConverter
    {
        public string ColumnName { get; set; }
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            return ((string)value) == ColumnName ? "Bold" : "Normal";
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BirthdayImageVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is DateTime))
                return Visibility.Collapsed;
            DateTime birthDate = (DateTime)value;
            DateTime someDate = DateTime.Now.AddMonths(3);
            int someMonth = someDate.Month < 3 ? someDate.Month + 12 : someDate.Month;
            int birthMonth = birthDate.Month < 3 ? birthDate.Month + 12 : birthDate.Month;
            return (birthMonth >= DateTime.Now.Month && birthMonth <= someMonth && (birthDate.Month == DateTime.Now.Month ? birthDate.Day > DateTime.Now.Day : true)) ? Visibility.Visible : Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ViewToBooleanConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            return value is TableView || value is TreeListView;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CountToVisibilityConverter : IValueConverter
    {
        public bool Invert { get; set; }
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((int)value > 0) ^ Invert ? Visibility.Visible : Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class IntToDoubleConverter : IValueConverter
    {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert.ToDouble(value);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert.ToInt32(value);
        }
        #endregion
    }
    public class RoundValueConverter : MarkupExtension, IValueConverter
    {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Math.Round(Convert.ToDouble(value));
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Math.Round(Convert.ToDouble(value));
        }
        #endregion
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
