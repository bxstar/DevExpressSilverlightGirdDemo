using System;
using System.Collections;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using DevExpress.Xpf.DemoBase.Web;
using DevExpress.Xpf.DemoBase.Web.Services;

namespace GridDemo
{
    public partial class RIAServices : GridDemoModule
    {
        NWindDomainContext domainContext = new NWindDomainContext();
        public RIAServices()
        {
            InitializeComponent();

            DataContext = domainContext;
            LoadDataSource();
        }
        void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadDataSource();
        }

        void LoadDataSource()
        {
            grid.Columns.Clear();
            grid.ItemsSource = SelectSource(listBoxEdit.EditValue as string);
        }
        IEnumerable SelectSource(string tableName)
        {
            switch (tableName)
            {
                case "Invoices":
                    return GetSource<Invoices>(() => domainContext.GetInvoicesQuery());
                case "Customers":
                    return GetSource<Customers>(() => domainContext.GetCustomersQuery());
                case "Employees":
                    return GetSource<Employees>(() => domainContext.GetEmployeesQuery());
                case "Products":
                    return GetSource<Products>(() => domainContext.GetProductsQuery());
                default:
                    throw new NotImplementedException();
            }
        }
        IEnumerable GetSource<T>(Func<EntityQuery<T>> getQuery) where T : Entity
        {
            LoadOperation<T> loadOperation = domainContext.Load<T>(getQuery(), new Action<LoadOperation<T>>(OnCompleted), null);
            return loadOperation.Entities;
        }
        void OnCompleted(LoadOperation op)
        {
            if (op.HasError)
            {
                MessageBox.Show("Connection could not be established." + Environment.NewLine + op.Error.Message, "Connection Error", MessageBoxButton.OK);
                op.MarkErrorAsHandled();
            }
        }
    }
}
