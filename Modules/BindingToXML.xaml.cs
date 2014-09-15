using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.DemoBase;
using System.Xml;

namespace GridDemo
{
    [CodeFile("ModuleResources/BindingToXMLClasses.(cs)")]
    public partial class BindingToXML : GridDemoModule
    {
        public BindingToXML()
        {
            InitializeComponent();
            Assembly assembly = typeof(BindingToXML).Assembly;
            Stream stream = EmployeesWithPhotoData.GetDataStream();
            XDocument doc = XDocument.Load(stream);
            var employees = new List<Dictionary<string, object>>();
            foreach (XElement element in doc.Descendants("Employee"))
            {
                var employee = new Dictionary<string, object>();
                employee.Add("Id", element.Element("Id"));
                employee.Add("FirstName", element.Element("FirstName"));
                employee.Add("LastName", element.Element("LastName"));
                employee.Add("JobTitle", element.Element("JobTitle"));
                employee.Add("EmailAddress", element.Element("EmailAddress"));
                employee.Add("BirthDate", element.Element("BirthDate"));
                employees.Add(employee);
            }
            grid.ItemsSource = employees;
        }
    }
}
