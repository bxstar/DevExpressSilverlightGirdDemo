using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Web;
using DevExpress.Xpf.DemoBase.Web.Services;
using DevExpress.Xpf.Grid;

namespace GridDemo
{
    public class MasterDetailWithRIAViewModel : DemoViewModelBase
    {
        NWindDomainContext domainContext;
        public MasterDetailWithRIAViewModel()
        {
            domainContext = new NWindDomainContext();

            OrdersLoadingConverter = new LazyLoadingConverter<NWindDomainContext, Customers, Orders>(domainContext,
                (dc, c) => { return dc.GetCustomerOrdersQuery(c.CustomerID); });
            OrderDetailsLoadingConverter = new LazyLoadingConverter<NWindDomainContext, Orders, Order_Details_Extended>(domainContext,
                (dc, o) => { return dc.GetOrderOrderDetailsExtendedQuery(o.OrderID); });
            domainContext.PropertyChanged += domainContext_PropertyChanged;

        }

        void domainContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLoading")
                IsLoading = domainContext.IsLoading;
        }
        public IEnumerable<Customers> ItemsSource
        {
            get
            {
                IsLoading = true;
                LoadOperation<Customers> loadOperation = domainContext.Load<Customers>(domainContext.GetCustomersQuery(), new Action<LoadOperation<Customers>>(RaiseSourceLoaded), null);
                return loadOperation.Entities;
            }
        }

        void RaiseSourceLoaded(LoadOperation loadOperation)
        {
            if (!loadOperation.HasError)
            {
                if (SourceLoaded != null)
                    SourceLoaded(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Connection could not be established." + Environment.NewLine + loadOperation.Error.Message, "Connection Error", MessageBoxButton.OK);
                loadOperation.MarkErrorAsHandled();
            }
        }

        public event EventHandler SourceLoaded;

        public LazyLoadingConverter<NWindDomainContext, Customers, Orders> OrdersLoadingConverter { get; set; }

        public LazyLoadingConverter<NWindDomainContext, Orders, Order_Details_Extended> OrderDetailsLoadingConverter { get; set; }

        bool isLoadingCore;
        public bool IsLoading
        {
            get { return isLoadingCore; }
            set
            {
                if (isLoadingCore != value)
                {
                    isLoadingCore = value;
                    OnPropertyChanged("IsLoading");
                }
            }
        }
    }
}
