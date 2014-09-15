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
using System.Collections;
using DevExpress.Data.Browsing;
using System.Collections.Generic;
using System.ComponentModel;
using PropertyDescriptor = DevExpress.Data.Browsing.PropertyDescriptor;

namespace GridDemo
{
    public class CountInfo
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }
    public class VirtualPropertyDescriptor : PropertyDescriptor
    {
        string propertyName;
        Type propertyType;
        bool isReadOnly;
        VirtualList list;
        int index;
        public VirtualPropertyDescriptor(VirtualList list, int index, string propertyName, Type propertyType, bool isReadOnly)
            : base(propertyName, null)
        {
            this.propertyName = propertyName;
            this.propertyType = propertyType;
            this.isReadOnly = isReadOnly;
            this.list = list;
            this.index = index;
        }
        public override bool CanResetValue(object component)
        {
            return false;
        }
        public override object GetValue(object component)
        {
            return list.GetPropertyValue((int)component, index);
        }
        public override void SetValue(object component, object val)
        {
            list.SetPropertyValue((int)component, index, val);
        }
        public override bool IsReadOnly { get { return isReadOnly; } }
        public override string Name { get { return propertyName; } }
        public override Type ComponentType { get { return typeof(VirtualList); } }
        public override Type PropertyType { get { return propertyType; } }
        public override void ResetValue(object component)
        {
        }
        public override bool ShouldSerializeValue(object component) { return true; }
    }
    public struct Location
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public override bool Equals(object obj)
        {
            Location l = (Location)obj;
            return l.Row == Row && l.Column == Column;
        }
        public override int GetHashCode()
        {
            return Row ^ Column;
        }
    }
    public class VirtualList : IList, ITypedList
    {
        const int BaseColumnCount = 7;
        int recordCount;
        int columnCount;
        Dictionary<Location, object> fValues = new Dictionary<Location, object>();
        PropertyDescriptorCollection columnCollection;
        public VirtualList()
        {
            recordCount = 1000;
            columnCount = 1000;
            CreateColumnCollection();
        }
        public void SetPropertyValue(int rowIndex, int columnIndex, object value)
        {
            fValues[new Location() { Column = columnIndex, Row = rowIndex }] = value;
        }
        public object GetPropertyValue(int rowIndex, int columnIndex)
        {
            object value = null;
            if (fValues.TryGetValue(new Location() { Column = columnIndex, Row = rowIndex }, out value))
            {
                return value;
            }
            if (columnIndex == 0)
                return rowIndex + 1;
            switch ((columnIndex - 1) % BaseColumnCount)
            {
                case 0: //From
                    return OutlookData.Users[GetPseudoRandomValue(rowIndex, columnIndex, OutlookData.Users.Length)].Name;
                case 1: //To
                    return OutlookData.Users[GetPseudoRandomValue(rowIndex, columnIndex, OutlookData.Users.Length)].Name;
                case 2: //Sent
                    return DateTime.Today.AddDays(GetPseudoRandomValue(rowIndex, columnIndex, 30));
                case 3: //HasAttachment
                    return GetPseudoRandomValue(rowIndex, columnIndex, 2) == 0 ? true : false;
                case 4: //Size
                    return GetPseudoRandomValue(rowIndex, columnIndex, 10000);
                case 5: //Priority
                    return (Priority)GetPseudoRandomValue(rowIndex, columnIndex, DevExpress.Data.Mask.EnumHelper.GetValues(typeof(Priority)).Length);
                case 6: //Subject
                    return OutlookDataGenerator.Subjects[GetPseudoRandomValue(rowIndex, columnIndex, OutlookDataGenerator.Subjects.Length)];
            }
            throw new NotImplementedException();
        }
        public string GetPropertyName(int columnIndex)
        {
            if (columnIndex == 0)
                return "ID(1)";
            switch ((columnIndex - 1) % BaseColumnCount)
            {
                case 0: //From
                    return GetFullPropertyName("From", columnIndex);
                case 1: //To
                    return GetFullPropertyName("To", columnIndex);
                case 2: //Sent
                    return GetFullPropertyName("Sent", columnIndex);
                case 3: //HasAttachment
                    return GetFullPropertyName("HasAttachment", columnIndex);
                case 4: //Size
                    return GetFullPropertyName("Size", columnIndex);
                case 5: //Priority
                    return GetFullPropertyName("Priority", columnIndex);
                case 6: //Subject
                    return GetFullPropertyName("Subject", columnIndex);
            }
            throw new NotImplementedException();
        }
        string GetFullPropertyName(string name, int columnIndex)
        {
            return name + "(" + (columnIndex + 1) + ")";
        }
        public Type GetPropertyType(int columnIndex)
        {
            if (columnIndex == 0)
                return typeof(int);
            switch ((columnIndex - 1) % BaseColumnCount)
            {
                case 0: //From
                    return typeof(string);
                case 1: //To
                    return typeof(string);
                case 2: //Sent
                    return typeof(DateTime);
                case 3: //HasAttachment
                    return typeof(bool);
                case 4: //Size
                    return typeof(int);
                case 5: //Priority
                    return typeof(Priority);
                case 6: //Subject
                    return typeof(string);
            }
            throw new NotImplementedException();
        }
        int GetPseudoRandomValue(int rowIndex, int columnIndex, int maxValue)
        {
            return (rowIndex + columnIndex) % maxValue;
        }
        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                if (value < 1) value = 0;
                if (RecordCount == value) return;
                recordCount = value;
            }
        }
        public int ColumnCount
        {
            get { return columnCount; }
            set
            {
                if (value < 1) value = 0;
                if (ColumnCount == value) return;
                columnCount = value;
                CreateColumnCollection();
            }
        }
        protected virtual void CreateColumnCollection()
        {
            VirtualPropertyDescriptor[] pds = new VirtualPropertyDescriptor[ColumnCount];
            for (int n = 0; n < ColumnCount; n++)
            {
                pds[n] = new VirtualPropertyDescriptor(this, n, GetPropertyName(n), GetPropertyType(n), false);
            }
            columnCollection = new PropertyDescriptorCollection(pds);
        }

        #region ITypedList Interface
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] descs) { return columnCollection; }
        string ITypedList.GetListName(PropertyDescriptor[] descs) { return ""; }
        #endregion
        #region IList Interface
        public virtual int Count
        {
            get { return RecordCount; }
        }
        public virtual bool IsSynchronized
        {
            get { return true; }
        }
        public virtual object SyncRoot
        {
            get { return true; }
        }
        public virtual bool IsReadOnly
        {
            get { return false; }
        }
        public virtual bool IsFixedSize
        {
            get { return true; }
        }
        public virtual IEnumerator GetEnumerator()
        {
            return null;
        }
        public virtual void CopyTo(System.Array array, int fIndex)
        {
        }
        public virtual int Add(object val)
        {
            throw new NotImplementedException();
        }
        public virtual void Clear()
        {
            throw new NotImplementedException();
        }
        public virtual bool Contains(object val)
        {
            throw new NotImplementedException();
        }
        public virtual int IndexOf(object val)
        {
            throw new NotImplementedException();
        }
        public virtual void Insert(int fIndex, object val)
        {
            throw new NotImplementedException();
        }
        public virtual void Remove(object val)
        {
            throw new NotImplementedException();
        }
        public virtual void RemoveAt(int fIndex)
        {
            throw new NotImplementedException();
        }
        object IList.this[int fIndex]
        {
            get { return fIndex; }
            set { }
        }
        #endregion
    }
}
