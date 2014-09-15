using System;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using System.Data;

namespace GridDemo
{
    public class CustomerDetailsConverter : IValueConverter
    {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            RowData rowData = (RowData)value;
            if (rowData == null)
                return null;
            DevExpress.Xpf.DemoBase.NWind.Customers customers = (DevExpress.Xpf.DemoBase.NWind.Customers)rowData.Row;
            return String.Format("{0}, {1}, {2}\r\n{3}, {4}", customers.Country, customers.City, customers.PostalCode, customers.Address, customers.Phone);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
