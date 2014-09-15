using System;
using System.Collections.Generic;
using System.Windows;
using GridDemo;
using System.Collections;
using System.Globalization;
using DevExpress.Data;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using DevExpress.Xpf.DemoBase.NWind;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Data.Browsing;

namespace GridDemo
{
    public class SalesByEmployeeData
    {
        IList data;
        public IList Data
        {
            get
            {
                if (data == null)
                {
                    data = GetSalesByEmployee();
                }
                return data;
            }
        }
        public static IList GetSalesByEmployee()
        {
            List<ColumnDescription> columns = new List<ColumnDescription>();
            columns.Add(new ColumnDescription() { PropertyName = "Employee", PropertyType = typeof(string), Attributes = new Attribute[] { new DisplayAttribute() { GroupName = "Employee" } } });

            for (int yearIndex = 10; yearIndex > 0; yearIndex--)
            {
                int year = DateTime.Now.Year - yearIndex;
                for (int month = 1; month <= 12; month++)
                {
                    columns.Add(new ColumnDescription() { PropertyName = year + "-" + month, PropertyType = typeof(int), DisplayName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month), Attributes = new Attribute[] { new DisplayAttribute() { GroupName = year + "/Q" + Math.Floor((double)(month + 2) / 3) } } });
                }
            }

            Random random = new Random();
            CellSelectionList source = new CellSelectionList(columns, true);
            foreach (Employee employee in EmployeesWithPhotoData.DataSource)
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                row["Employee"] = employee.FirstName + " " + employee.LastName;
                for (int columnIndex = 1; columnIndex < columns.Count; columnIndex++)
                    row[columns[columnIndex].PropertyName] = random.Next(30000);
                source.Add(row);
            }

            return source;
        }
    }
    public static class SalesByYearData
    {
        public static IList GetSalesByYearData(bool byMonthReport = false)
        {
            List<ColumnDescription> columns = new List<ColumnDescription>();
            columns.Add(new ColumnDescription() { PropertyName = "Date", PropertyType = typeof(DateTime) });
            if (byMonthReport)
                columns.Add(new ColumnDescription() { PropertyName = "DateMonth", PropertyType = typeof(DateTime) });
            foreach (Employees employee in NWindData.Employees)
            {
                string name = employee.FirstName + " " + employee.LastName;
                columns.Add(new ColumnDescription() { PropertyName = name, PropertyType = typeof(int) });
            }

            CellSelectionList source = new CellSelectionList(columns, false);

            Random random = new Random();
            for (int yearIndex = 10; yearIndex > 0; yearIndex--)
            {
                int year = DateTime.Now.Year - yearIndex;
                for (int month = 1; month <= 12; month++)
                {
                    int daysCount = byMonthReport ? DateTime.DaysInMonth(year, month) : 1;
                    for (int day = 1; day <= daysCount; day++)
                    {
                        Dictionary<string, object> row = new Dictionary<string, object>();
                        row["Date"] = new DateTime(year, month, day);
                        int startColumnIndex = 1;
                        if (byMonthReport)
                        {
                            row["DateMonth"] = row["Date"];
                            startColumnIndex++;
                        }
                        for (int columnIndex = startColumnIndex; columnIndex < columns.Count; columnIndex++)
                            row[columns[columnIndex].PropertyName] = random.Next(30000 / daysCount);
                        source.Add(row);
                    }
                }
            }
            return source;
        }
    }
    public class ColumnDescription
    {
        public string PropertyName { get; set; }
        public string DisplayName { get; set; }
        public Type PropertyType { get; set; }
        public Attribute[] Attributes { get; set; }
    }
    public class CellSelectionList : IList, ITypedList
    {
        List<Dictionary<string, object>> list;
        PropertyDescriptorCollection columns;
        public CellSelectionList(List<ColumnDescription> columnNames, bool assignAttribute)
        {
            list = new List<Dictionary<string, object>>();

            columns = CreateColumnCollection(columnNames);
        }
        PropertyDescriptorCollection CreateColumnCollection(List<ColumnDescription> columnDescriptions)
        {
            CellSelectionPropertyDescriptor[] pds = new CellSelectionPropertyDescriptor[columnDescriptions.Count];
            for (int i = 0; i < columnDescriptions.Count; i++)
                pds[i] = new CellSelectionPropertyDescriptor(this, columnDescriptions[i]);
            return new PropertyDescriptorCollection(pds);
        }

        #region ITypedList Members

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return columns;
        }

        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return string.Empty;
        }

        #endregion

        public void SetPropertyValue(int rowIndex, string column, object value)
        {
            list[rowIndex][column] = value;
        }
        public object GetPropertyValue(int rowIndex, string column)
        {
            return list[rowIndex][column];
        }

        #region IList Members

        public int Add(object value)
        {
            list.Add((Dictionary<string, object>)value);
            return -1;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { return true; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public object this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public object SyncRoot
        {
            get { return true; }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return null;
        }

        #endregion
    }
    public class CellSelectionPropertyDescriptor : PropertyDescriptor
    {
        readonly ColumnDescription columnDescription;
        readonly CellSelectionList list;
        public CellSelectionPropertyDescriptor(CellSelectionList list, ColumnDescription columnDescription)
            : base(columnDescription.PropertyName, columnDescription.Attributes)
        {
            this.columnDescription = columnDescription;
            this.list = list;
        }
        public override object GetValue(object component)
        {
            return ((Dictionary<string, object>)component)[columnDescription.PropertyName];
        }
        public override void SetValue(object component, object val)
        {
            ((Dictionary<string, object>)component)[columnDescription.PropertyName] = val;
        }
        public override bool CanResetValue(object component)
        {
            return false;
        }
        public override bool IsReadOnly { get { return false; } }
        public override Type ComponentType { get { return typeof(MultiEditorsList); } }
        public override Type PropertyType { get { return columnDescription.PropertyType; } }
        public override string DisplayName { get { return columnDescription.DisplayName; } }
        public override void ResetValue(object component)
        {
        }
        public override bool ShouldSerializeValue(object component) { return true; }
    }
    public class SalesByYearDataColumnTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ColumnGeneratorItemContext context = (ColumnGeneratorItemContext)item;
            GridControl grid = (GridControl)container;
            return (DataTemplate)grid.Resources[context.PropertyDescriptor.Name == "Date" ? "DateColumnTemplate" : "EmployeeColumnTemplate"];
        }
    }
}
