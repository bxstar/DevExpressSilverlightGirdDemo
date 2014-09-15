using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors;
using System.Threading;
using System.Windows.Threading;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.Data.Browsing;
using DevExpress.Xpf.DemoBase;
using PropertyDescriptor = DevExpress.Data.Browsing.PropertyDescriptor;

namespace GridDemo
{
    [CodeFile("Controls/OutlookDataGenerator.(cs)")]
    [CodeFile("ModuleResources/LargeDataSetClasses.(cs)")]
    public partial class LargeDataSet : GridDemoModule
    {
        VirtualList vList = new VirtualList();
        public LargeDataSet()
        {
            InitializeComponent();
            AssignDataSource();
        }

        void AssignDataSource()
        {
            setRowColumnCountButton.Cursor = Cursors.Wait;
            vList.RecordCount = ((CountInfo)rowCountListBox.SelectedItem).Value;
            vList.ColumnCount = ((CountInfo)columnCountListBox.SelectedItem).Value;
            grid.ItemsSource = null;
            grid.Columns.Clear();
            grid.Columns.BeginUpdate();
            PropertyDescriptorCollection properties = ((ITypedList)vList).GetItemProperties(null);
            foreach (PropertyDescriptor propertyDescriptor in properties)
            {
                GridColumn column = new GridColumn();
                column.FieldName = propertyDescriptor.Name;
                if (column.FieldName.Contains("Subject"))
                    column.EditSettings = new MemoEditSettings()
                    {
                        PopupWidth = 300,
                        ShowIcon = false,
                        MemoTextWrapping = TextWrapping.Wrap,
                        MemoVerticalScrollBarVisibility = ScrollBarVisibility.Auto
                    };
                if (column.FieldName.Contains("Priority"))
                {
                    column.SortMode = ColumnSortMode.Value;
                }
                if (column.FieldName.Contains("Size"))
                {
                    column.BestFitArea = BestFitArea.Header;
                }
                column.AllowEditing = ((propertyDescriptor.Name == "ID(1)") ? DefaultBoolean.False : DefaultBoolean.Default);
                column.Header = ((propertyDescriptor.Name == "ID(1)") ? "ID(1)" : null);
                column.AllowColumnFiltering = ((propertyDescriptor.Name == "ID(1)") ? DefaultBoolean.False : DefaultBoolean.True);
                BaseEditSettings settings = CreateEditSettings(propertyDescriptor.Name);
                if (settings != null)
                    column.EditSettings = settings;
                grid.Columns.Add(column);
            }
            grid.Columns.EndUpdate();
            grid.ItemsSource = vList;
            Dispatcher.BeginInvoke(
            new Action(ClearCursorProperty));
        }
        void ClearCursorProperty()
        {
            setRowColumnCountButton.ClearValue(FrameworkElement.CursorProperty);
        }
        BaseEditSettings CreateEditSettings(string propertyName)
        {
            if (propertyName == "ID(1)")
                return new TextEditSettings()
                {
                    DisplayFormat = "#,0",
                    HorizontalContentAlignment = EditSettingsHorizontalAlignment.Right
                };
            if (propertyName.StartsWith("Size"))
                return new TextEditSettings()
                {
                    Mask = "## ##0",
                    MaskType = MaskType.Numeric,
                    MaskUseAsDisplayFormat = true,
                    HorizontalContentAlignment = EditSettingsHorizontalAlignment.Right
                };
            if (propertyName.StartsWith("Priority"))
            {
                return new ComboBoxEditSettings()
                {
                    ItemsSource = DevExpress.Data.Mask.EnumHelper.GetValues(typeof(Priority)),
                    IsTextEditable = false
                };
            }
            return null;
        }
        private void setRowColumnCountButton_Click(object sender, RoutedEventArgs e)
        {
            AssignDataSource();
        }

        private void view_CustomBestFit(object sender, CustomBestFitEventArgs e)
        {
            if (e.Column.FieldName != "ID(1)")
                return;
            List<int> largestIDHandles = new List<int>();
            for (int i = vList.Count - 1; i >= 0; i--)
            {
                int rowHanle = grid.GetRowHandleByListIndex(i);
                if (rowHanle != GridControl.InvalidRowHandle)
                    largestIDHandles.Add(rowHanle);
                if (largestIDHandles.Count >= 100)
                    break;
            }
            e.BestFitRows = largestIDHandles;
        }
    }
}
