using System;
using System.Collections.Generic;
using DevExpress.Xpf.DemoBase.NWind;
using NWindData = DevExpress.Xpf.DemoBase.NWindData;

namespace GridDemo
{
    #region GroupIntervalData

    public class GroupIntervalData
    {
        public static Random rnd = new Random();
        public object Invoices { get { return CreateInvoicesDataTable(); } }
        public object Products { get { return CreateProductsDataTable(); } }
        static DateTime GetDate(bool range)
        {
            DateTime ret = DateTime.Now;
            int r = rnd.Next(20);
            if (range)
            {
                if (r > 1) ret = ret.AddDays(rnd.Next(80) - 40);
                if (r > 18) ret = ret.AddMonths(rnd.Next(18));
            }
            else
            {
                ret = ret.AddDays(rnd.Next(r * 30) - r * 15);
            }
            return ret;
        }
        static decimal GetCount()
        {
            return rnd.Next(50) + 10;
        }
        static object CreateInvoicesDataTable()
        {
            List<object> list = new List<object>();
            foreach (Invoices invoice in NWindData.Invoices)
            {
                Invoice row = new Invoice();
                row.Country = invoice.Country;
                row.City = invoice.City;
                row.OrderDate = GetDate(true);
                row.UnitPrice = (decimal)invoice.UnitPrice;
                row.Region = invoice.Region;
                list.Add(row);
            }
            return list;
        }
        static object CreateProductsDataTable()
        {
            List<Product> list = new List<Product>();
            foreach (Products product in NWindData.Products)
            {
                Product row = new Product();
                row.ProductName = product.ProductName;
                row.UnitPrice = product.UnitPrice;
                row.Count = GetCount();
                row.OrderSum = row.UnitPrice * row.Count;
                row.OrderDate = GetDate(false);
                list.Add(row);
            }
            return list;
        }
    }

    public class Invoice
    {
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal UnitPrice { get; set; }
        public string Region { get; set; }
    }
    public sealed class Product
    {
        public string ProductName { get; set; }
        public decimal Count { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OrderSum { get; set; }
        public string QuantityPerUnit { get; set; }

    }

    #endregion
}
