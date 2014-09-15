using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.NWind;

namespace GridDemo
{
    public class OrderDataGenerator
    {
        static object SyncRoot = new object();
        static List<string> customerNames = new List<string>();
        static List<CategoryData> categoryData = new List<CategoryData>();
        static List<ProductData> productData = new List<ProductData>();
        int count;
        List<OrderData> cachedOrders = new List<OrderData>();

        static List<string> ExtractCustomerNames()
        {
            if (customerNames.Count == 0)
            {
                IList customers = NWindData.Customers;
                customerNames.Capacity = customers.Count;
                foreach (Customers customer in customers)
                {
                    customerNames.Add(customer.ContactName);
                }
            }
            return customerNames;
        }
        public static List<CategoryData> ExtractCategoryDataList()
        {
            if (categoryData.Count == 0)
            {
                IList categories = NWindData.Categories;
                categoryData.Capacity = categories.Count;
                foreach (DevExpress.Xpf.DemoBase.NWind.Categories category in categories)
                {
                    categoryData.Add(new CategoryData()
                    {
                        Name = category.CategoryName,
                        Picture = category.Icon_25
                    });
                }
            }
            return categoryData;
        }
        public static List<ProductData> ExtractProductDataList(List<CategoryData> categoriesList)
        {
            if (productData.Count == 0)
            {
                IList categoryProducts = NWindData.CategoryProducts;
                productData.Capacity = categoryProducts.Count;
                Random rand = new Random();
                foreach (CategoryProducts categoryProduct in categoryProducts)
                {
                    productData.Add(new ProductData()
                    {
                        Category = FindCategory(categoriesList, categoryProduct.CategoryName),
                        Name = categoryProduct.ProductName,
                        Price = (decimal)(rand.Next(20) + rand.Next(99)) / 100m
                    });
                }
            }
            return productData;
        }

        static CategoryData FindCategory(List<CategoryData> categoriesList, string name)
        {
            foreach (CategoryData category in categoriesList)
            {
                if (category.Name == name) return category;
            }
            return null;
        }

        List<OrderData> GenerateOrders(int generateCount, int startFrom)
        {
            List<OrderData> result = new List<OrderData>(generateCount);
            List<string> customerNames = ExtractCustomerNames();
            List<CategoryData> categoriesList = ExtractCategoryDataList();
            List<ProductData> productsList = ExtractProductDataList(categoriesList);

            OnGenerateOrderDataStarted(EventArgs.Empty);
            Random rand = new Random();
            int generateCountPerCent = generateCount / 100;
            for (int i = 0; i < generateCount; i++)
            {
                ProductData randomProduct = productsList[rand.Next(productsList.Count)];
                string randomName = customerNames[rand.Next(customerNames.Count)];
                OrderData data = new OrderData();
                data.OrderId = i + startFrom;
                data.OrderDate = DateTime.Today.Subtract(TimeSpan.FromDays(rand.Next(180)));
                data.CustomerName = randomName; data.Quantity = rand.Next(200) + 1;
                data.ProductCategory = randomProduct.Category;
                data.ProductName = randomProduct.Name;
                data.Price = randomProduct.Price;
                data.IsReady = (rand.Next(2) == 0);
                result.Add(data);
                if (((i + 1) % generateCountPerCent) == 0)
                {
                    OnGenerateOrderDataProgress(new GenerateOrderDataProgressEventArgs(Convert.ToDouble((i + 1) / generateCountPerCent)));
                }
            }
            OnGenerateOrderDataCompleted(EventArgs.Empty);
            return result;
        }

        protected virtual void OnGenerateOrderDataStarted(EventArgs e)
        {
            if (GenerateOrderDataStarted != null)
            {
                GenerateOrderDataStarted(this, e);
            }
        }
        protected virtual void OnGenerateOrderDataCompleted(EventArgs e)
        {
            if (GenerateOrderDataCompleted != null)
            {
                GenerateOrderDataCompleted(this, e);
            }
        }
        protected virtual void OnGenerateOrderDataProgress(GenerateOrderDataProgressEventArgs e)
        {
            if (GenerateOrderDataProgress != null)
            {
                GenerateOrderDataProgress(this, e);
            }
        }

        public OrderDataGenerator(int count)
        {
            this.count = count;
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public List<OrderData> GetOrders()
        {
            List<OrderData> result;
            lock (SyncRoot)
            {
                if (Count > cachedOrders.Count)
                {
                    cachedOrders.AddRange(GenerateOrders(Count - cachedOrders.Count, cachedOrders.Count + 1));
                }
                result = cachedOrders.GetRange(0, Count);
            }
            return result;
        }
        public List<CategoryData> GetCategories()
        {
            return ExtractCategoryDataList();
        }

        public event EventHandler GenerateOrderDataStarted;
        public event EventHandler GenerateOrderDataCompleted;
        public event EventHandler<GenerateOrderDataProgressEventArgs> GenerateOrderDataProgress;
    }

    public class GenerateOrderDataProgressEventArgs : EventArgs
    {
        double progress;

        public GenerateOrderDataProgressEventArgs(double progress)
        {
            this.progress = progress;
        }
        public double Progress
        {
            get { return progress; }
        }
    }

    public class CategoryData : IComparable, IComparable<CategoryData>
    {
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public override string ToString()
        {
            return Name;
        }

        #region IComparable Members
        public int CompareTo(object obj)
        {
            if (obj is CategoryData)
                return CompareTo((CategoryData)obj);
            return -1;
        }
        #endregion
        #region IComparable<CategoryData> Members
        public int CompareTo(CategoryData other)
        {
            return StringComparer.CurrentCulture.Compare(Name, other.Name);
        }
        #endregion
    }
    public class ProductData
    {
        public string Name { get; set; }
        public CategoryData Category { get; set; }
        public decimal Price { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class OrderData
    {
        public int OrderId { get; set; }
        public bool IsReady { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public CategoryData ProductCategory { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
