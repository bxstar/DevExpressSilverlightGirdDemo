using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using System.Dynamic;
using System.Collections.ObjectModel;
using DevExpress.Data;
using System.ComponentModel;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/BindingToDynamicObjectClasses.(cs)")]
    public partial class BindingToDynamicObject : GridDemoModule
    {
        DynamicBindingList list = new DynamicBindingList();
        public BindingToDynamicObject()
        {
            InitializeComponent();
            optionsPanel.DataContext = new NewColumnModel();
            for (int i = 0; i < 50; i++)
            {
                DynamicDictionary dictionary = new DynamicDictionary();
                dictionary.SetValue("Id", i);
                dictionary.SetValue("FirstName", DataGenerator.GetFirstName());
                dictionary.SetValue("LastName", DataGenerator.GetLastName());
                list.Add(dictionary);
            }
            grid.ItemsSource = list;
            grid.CustomUnboundColumnData += grid_CustomUnboundColumnData;
        }
        void grid_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                e.Value = list[e.ListSourceRowIndex].GetValue(e.Column.FieldName);
            }
            else
            {
                list[e.ListSourceRowIndex].SetValue(e.Column.FieldName, e.Value);
            }

        }
        private void CreateNewColBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewColumn();
            FieldNameBox.Text = "";
        }

        void CreateNewColumn()
        {
            string fieldName = FieldNameBox.Text;
            for (int i = 0; i < list.Count; i++)
            {
                ((DynamicDictionary)list[i]).SetValue(fieldName, GetDefaultValue());
            }
            GridColumn column = new GridColumn();
            column.UnboundType = GetNewColunUnboundType();
            column.FieldName = fieldName;
            column.Header = fieldName;
            grid.Columns.Add(column);
        }

        private object GetDefaultValue()
        {
            switch (TypeBox.SelectedIndex)
            {
                case 0: return ((int)DefaultValueBoxSpin.Value);
                case 1: return DefaultValueBoxText.Text;
                case 2: return DefaultValueBoxDate.DateTime;
                case 3: return DefaultValueBoxCheck.IsChecked;
            }
            return null;
        }

        private UnboundColumnType GetNewColunUnboundType()
        {
            switch (TypeBox.SelectedIndex)
            {
                case 0: return UnboundColumnType.Integer;
                case 1: return UnboundColumnType.String;
                case 2: return UnboundColumnType.DateTime;
                case 3: return UnboundColumnType.Boolean;
            }
            return UnboundColumnType.Object;
        }

        void FieldNameBox_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            object value = e.Value;
            if (value != null)
            {
                if (!IsValidFieldName(value.ToString()))
                {
                    e.ErrorContent = "A column bound to the same field in the data source already exists.";
                    e.IsValid = false;
                    return;
                }
            }
        }
        bool IsValidFieldName(string name)
        {
            if (grid == null)
                return false;
            foreach (GridColumn column in grid.Columns)
            {
                string path = column.FieldName;
                if (path == name)
                    return false;
            }
            return true;
        }
    }
}
