using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Xpf.Utils;
using System.ComponentModel;
using System.Collections;
using DevExpress.Mvvm;
using DevExpress.Data.Browsing;
using DevExpress.Xpf.Core.WPFCompatibility;

namespace GridDemo
{
    public class CountItem : BindableBase
    {
        int countCore;
        public string DisplayName { get; set; }
        public int Count
        {
            get { return countCore; }
            set { SetProperty(ref countCore, value, "Count"); }
        }
    }
    public class CountItemCollection : List<CountItem> { }
    public class OrderDataListSource : IListSource
    {
        OrderDataGenerator orderDataGenerator;

        public OrderDataListSource(OrderDataGenerator orderDataGenerator)
        {
            this.orderDataGenerator = orderDataGenerator;
        }
        public IList GetList()
        {
            return orderDataGenerator.GetOrders();
        }
    }

}
