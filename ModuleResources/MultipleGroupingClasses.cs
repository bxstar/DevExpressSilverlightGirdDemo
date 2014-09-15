using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using DevExpress.Xpf.Core;
using System.ComponentModel;
using System.Windows.Markup;

namespace GridDemo
{
    public class PercentCompleteToFontWeightConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double percentComplete = Convert.ToDouble(value);
            return 0 < percentComplete && percentComplete < 100 ? FontWeights.Bold : FontWeights.Normal;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class StyleFinder : FrameworkElement
    {
        public static readonly DependencyProperty KeyProperty;
        public static readonly DependencyProperty ValueProperty;

        static StyleFinder()
        {
            KeyProperty = DependencyProperty.Register("Key", typeof(object), typeof(StyleFinder), new PropertyMetadata(null, OnKeyChanged));
            ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(StyleFinder), new PropertyMetadata(null));
        }

        static void OnKeyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((StyleFinder)obj).UpdateValue();
        }

        public object Key
        {
            get { return GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public StyleFinder()
        {
            ThemeManager.ApplicationThemeChanged += ThemeManager_ApplicationThemeChanged;
        }

        void ThemeManager_ApplicationThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e)
        {
            UpdateValue();
        }

        void UpdateValue()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
                Value = ThemeManager.ApplicationTheme.Styles[Key];
        }
    }
}
