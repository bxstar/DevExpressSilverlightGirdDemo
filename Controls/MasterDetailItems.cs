using System;
using System.Windows;
using System.Windows.Data;
using GridDemo;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Data.Mask;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Grid;
using System.Windows.Controls;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DevExpress.Xpf.DemoBase.NWind;
using System.Windows.Input;
using DevExpress.Xpf.Core.WPFCompatibility;
using DevExpress.Xpf.Collections;
using Control = DevExpress.Xpf.Core.WPFCompatibility.SLControl;
using PropertyDescriptor = DevExpress.Data.Browsing.PropertyDescriptor;
using PropertyMetadata = DevExpress.Xpf.Core.WPFCompatibility.SLPropertyMetadata;
using DependencyPropertyChangedEventArgs = DevExpress.Xpf.Core.WPFCompatibility.SLDependencyPropertyChangedEventArgs;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{

    public class OrdersWithDetail : Orders
    {
        public IList<Invoices> Invoices { get; private set; }

        public static IList<OrdersWithDetail> CreateOrdersForMasterDetailView(string customerID, int employeeID)
        {
            IList<Orders> orders = (IList<Orders>)NWindData.Orders;
            IList<OrdersWithDetail> res = new List<OrdersWithDetail>();
            Dictionary<int, int> dict = EmployeesWithPhotoData.OrdersRelationsDictionary;
            foreach (Orders order in orders)
            {
                if ((customerID == "" || order.CustomerID == customerID) && (dict[order.OrderID]) == employeeID)
                    res.Add(new OrdersWithDetail(order));
            }
            return res;
        }
        public static IList<OrdersWithDetail> CreateOrders(string customerID, int employeeID)
        {
            IList<Orders> orders = (IList<Orders>)NWindData.Orders;
            IList<OrdersWithDetail> res = new List<OrdersWithDetail>();
            int i = 0;
            foreach (Orders order in orders)
            {
                if (i > 10) break;
                if ((customerID == "" || order.CustomerID == customerID) && order.EmployeeID == employeeID)
                {
                    res.Add(new OrdersWithDetail(order));
                    i++;
                }
            }
            return res;
        }
        IList<Invoices> CreateInvoices(int orderID, string customerID)
        {
            IList<Invoices> invoices = (IList<Invoices>)NWindData.Invoices;
            IList<Invoices> res = new List<Invoices>();
            foreach (Invoices invoice in invoices)
            {
                if ((customerID == "" || invoice.CustomerID == customerID) && invoice.OrderID == orderID)
                    res.Add(invoice);
            }
            return res;
        }
        public OrdersWithDetail(Orders o)
        {
            this.CustomerID = o.CustomerID;
            this.EmployeeID = o.EmployeeID;
            this.Freight = o.Freight;
            this.OrderDate = o.OrderDate;
            this.OrderID = o.OrderID;
            this.RequiredDate = o.RequiredDate;
            this.ShipAddress = o.ShipAddress;
            this.ShipCity = o.ShipCity;
            this.ShipCountry = CountryNameResolver.Resolve(o.ShipCountry);
            this.ShipName = o.ShipName;
            this.ShippedDate = o.ShippedDate;
            this.ShipPostalCode = o.ShipPostalCode;
            this.ShipRegion = o.ShipRegion;
            this.ShipVia = o.ShipVia;
            Invoices = CreateInvoices(OrderID, CustomerID);
        }
    }
    public class CustomersWithDetail : Customers
    {
        public IList<OrdersWithDetail> Orders { get; private set; }

        public static IList<CustomersWithDetail> CreateCustomersForMaterDetailView(int employeeID)
        {
            IList<Orders> orders = (IList<Orders>)NWindData.Orders;
            IList<Customers> customers = (IList<Customers>)NWindData.Customers;
            Dictionary<int, int> dict = EmployeesWithPhotoData.OrdersRelationsDictionary;
            IList<CustomersWithDetail> res = new List<CustomersWithDetail>();
            foreach (Customers c in customers)
            {
                Customers customer = c;
                if (orders.Where((order) => order.CustomerID == customer.CustomerID && dict[order.OrderID] == employeeID).Count() > 0)
                    res.Add(new CustomersWithDetail(c, employeeID, true));
            }
            return res;
        }
        public static IList<CustomersWithDetail> CreateCustomers(int employeeID)
        {
            IList<Orders> orders = (IList<Orders>)NWindData.Orders;
            IList<Customers> customers = (IList<Customers>)NWindData.Customers;
            IList<CustomersWithDetail> res = new List<CustomersWithDetail>();
            foreach (Customers c in customers)
            {
                Customers customer = c;
                if (orders.Where((order) => order.CustomerID == customer.CustomerID && order.EmployeeID == employeeID).Count() > 0)
                    res.Add(new CustomersWithDetail(c, employeeID));
            }
            return res;
        }

        public CustomersWithDetail(Customers c, int employeeID, bool newOrders = false)
        {
            this.Address = c.Address;
            this.City = c.City;
            this.CompanyName = c.CompanyName;
            this.ContactName = c.ContactName;
            this.ContactTitle = c.ContactTitle;
            this.Country = CountryNameResolver.Resolve(c.Country);
            this.CustomerID = c.CustomerID;
            this.Fax = c.Fax;
            this.Phone = c.Phone;
            this.PostalCode = c.PostalCode;
            this.Region = c.Region;
            Orders = newOrders ? OrdersWithDetail.CreateOrdersForMasterDetailView(CustomerID, employeeID) : OrdersWithDetail.CreateOrders(CustomerID, employeeID);
        }

    }
    public class EmployeesWithDetails : List<EmployeeWithDetails>
    {
        public EmployeesWithDetails()
        {
            AddRange(EmployeeWithDetails.CreateMasterDetailSource());
        }
    }

    public class EmployeesWithDetailsForEmbeddedView : List<EmployeeWithDetails>
    {
        public EmployeesWithDetailsForEmbeddedView()
        {
            AddRange(EmployeeWithDetails.CreateEmbeddedViewSource());
        }
    }
    public class EmployeeWithDetails : Employees
    {
        IList<CustomersWithDetail> customersCore;
        public IList<CustomersWithDetail> Customers
        {
            get
            {
                if (customersCore == null)
                    customersCore = CustomersWithDetail.CreateCustomers(EmployeeID);
                return customersCore;
            }
        }
        IList<OrdersWithDetail> ordersCore;
        public IList<OrdersWithDetail> Orders
        {
            get
            {
                if (ordersCore == null)
                    ordersCore = OrdersWithDetail.CreateOrders("", EmployeeID);
                return ordersCore;
            }
        }

        IList<CustomersWithDetail> mdcustomersCore;
        public IList<CustomersWithDetail> MDCustomers
        {
            get
            {
                if (mdcustomersCore == null)
                    mdcustomersCore = CustomersWithDetail.CreateCustomersForMaterDetailView(EmployeeID);
                return mdcustomersCore;
            }
        }
        IList<OrdersWithDetail> mdordersCore;
        public IList<OrdersWithDetail> MDOrders
        {
            get
            {
                if (mdordersCore == null)
                    mdordersCore = OrdersWithDetail.CreateOrdersForMasterDetailView("", EmployeeID);
                return mdordersCore;
            }
        }

        IEnumerable<ChartPoint> chartSourceCore;
        public IEnumerable<ChartPoint> ChartSource
        {
            get
            {
                if (chartSourceCore == null)
                    chartSourceCore = CreateChartSource();
                return chartSourceCore;
            }
        }

        IEnumerable<ChartPoint> CreateChartSource()
        {
            IList<ChartPoint> list = (from o in MDOrders
                                      group o by o.OrderDate into cp
                                      select new ChartPoint()
                                      {
                                          ArgumentMember = cp.Key,
                                          Orders = cp.ToList()
                                      }).ToList();
            foreach (ChartPoint cp in list)
            {
                decimal value = 0;
                foreach (OrdersWithDetail order in cp.Orders)
                    foreach (Invoices inv in order.Invoices)
                        value += inv.Quantity * inv.UnitPrice;
                cp.ValueMember = (int)value;
            }
            return list;
        }
        public string EMail { get; set; }

        public static IList<EmployeeWithDetails> CreateMasterDetailSource()
        {
            List<Employee> empls = EmployeesWithPhotoData.DataSource;
            List<EmployeeWithDetails> res = new List<EmployeeWithDetails>();

            foreach (Employee employee in empls)
                res.Add(new EmployeeWithDetails(employee));
            return res;
        }
        public static IList<EmployeeWithDetails> CreateEmbeddedViewSource()
        {
            IList<Employees> employees = (IList<Employees>)NWindData.Employees;
            List<EmployeeWithDetails> res = new List<EmployeeWithDetails>();
            foreach (Employees employee in employees)
            {
                res.Add(new EmployeeWithDetails(employee));
            }
            return res;
        }
        public EmployeeWithDetails(Employees e)
        {
            Address = e.Address;
            BirthDate = e.BirthDate;
            City = e.City;
            Country = CountryNameResolver.Resolve(e.Country);
            EmployeeID = e.EmployeeID;
            Extension = e.Extension;
            FirstName = e.FirstName;
            HireDate = e.HireDate;
            HomePhone = e.HomePhone;
            LastName = e.LastName;
            Notes = e.Notes;
            Photo = e.Photo;
            PostalCode = e.PostalCode;
            Region = e.Region;
            ReportsTo = e.ReportsTo;
            Title = e.Title;
            TitleOfCourtesy = e.TitleOfCourtesy;
        }
        public EmployeeWithDetails(Employee e)
        {
            Address = e.AddressLine1;
            BirthDate = e.BirthDate;
            City = e.City;
            Country = CountryNameResolver.Resolve(e.CountryRegionName);
            EmployeeID = e.Id;
            FirstName = e.FirstName;
            HireDate = e.HireDate;
            HomePhone = e.Phone;
            LastName = e.LastName;
            Photo = e.ImageData;
            PostalCode = e.PostalCode;
            Region = e.CountryRegionName;
            EMail = e.EmailAddress;
            Title = e.JobTitle;
            this.ParentId = e.ParentId;
        }
        internal int ParentId { get; private set; }
    }

    public class ChartPoint
    {
        public DateTime ArgumentMember { get; internal set; }
        public int ValueMember { get; set; }
        internal IList<OrdersWithDetail> Orders { get; set; }
    }
    internal static class CountryNameResolver
    {
        internal static string Resolve(string countryName)
        {
            switch (countryName)
            {
                case "USA":
                    return "United States";
                case "UK":
                    return "United Kingdom";
                default: return countryName;
            }
        }
    }

    public class EmployeeToOrdersConverter : IValueConverter
    {
        Dictionary<Employee, IEnumerable<Orders>> employeeOrders = new Dictionary<Employee, IEnumerable<Orders>>();

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Employee empl = value as Employee;
            if (empl == null) return null;
            IEnumerable<Orders> orders = null;
            if (!employeeOrders.TryGetValue(empl, out orders))
            {
                orders = OrdersWithDetail.CreateOrdersForMasterDetailView("", empl.Id);
                employeeOrders.Add(empl, orders);
            }
            return orders;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
