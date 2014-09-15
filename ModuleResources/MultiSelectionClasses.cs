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
using System.Collections.Generic;
using DevExpress.Xpf.DemoBase.NWind;
using DevExpress.Xpf.DemoBase;
using System.Linq;
using System.Data;

namespace GridDemo
{
    public struct Range
    {
        public string Text { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
    public class ProductIdToProductNameConverter : IValueConverter
    {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FillProductsDictionary();
            if (productsDictionary.ContainsKey((int)value))
                return productsDictionary[(int)value];
            return string.Empty;
        }
        static Dictionary<int, string> productsDictionary = new Dictionary<int, string>();
        void FillProductsDictionary()
        {
            if (productsDictionary.Count == 0)
            {
                List<Products> list = NWindData.Products as List<Products>;
                foreach (Products product in list)
                    productsDictionary[product.ProductID] = product.ProductName;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
