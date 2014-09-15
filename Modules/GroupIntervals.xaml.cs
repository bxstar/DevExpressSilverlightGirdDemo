using System;
using System.Collections.Generic;
using DevExpress.Xpf.Grid;
using DevExpress.XtraGrid;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Bars;
using DevExpress.Mvvm;

namespace GridDemo
{
    [CodeFile("ModuleResources/GroupIntervalsClasses.(cs)")]
    public partial class GroupIntervals : GridDemoModule
    {
        public GroupIntervals()
        {
            InitializeComponent();
            groupModeList.SelectedIndex = 0;
        }
        private void groupModeList_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            SetGroupInterval(groupModeList.SelectedIndex);
        }
        void SetGroupInterval(int index)
        {
            grid.SortInfo.Clear();
            grid.GroupCount = 0;
            foreach (GridColumn column in grid.Columns)
            {
                column.GroupInterval = ColumnGroupInterval.Default;
                column.SortMode = ColumnSortMode.Default;
            }
            switch (index)
            {
                case 0:
                    SetInterval("Country", ColumnGroupInterval.Alphabetical);
                    break;
                case 1:
                    SetInterval("OrderDate", ColumnGroupInterval.DateMonth);
                    break;
                case 2:
                    SetInterval("OrderDate", ColumnGroupInterval.DateYear);
                    break;
                case 3:
                    SetInterval("OrderDate", ColumnGroupInterval.DateRange);
                    break;
                case 4:
                    SetSortMode("UnitPrice", ColumnSortMode.Custom);
                    break;
            }
        }
        void SetInterval(string fieldName, ColumnGroupInterval interval)
        {
            grid.Columns[fieldName].GroupInterval = interval;
            grid.GroupBy(fieldName);
        }
        void SetSortMode(string fieldName, ColumnSortMode sortMode)
        {
            grid.Columns[fieldName].SortMode = sortMode;
            grid.GroupBy(fieldName);
        }
        void grid_CustomColumnGroup(object sender, CustomColumnSortEventArgs e)
        {
            double x = Math.Floor(Convert.ToDouble(e.Value1) / 10);
            double y = Math.Floor(Convert.ToDouble(e.Value2) / 10);
            int res = Comparer<double>.Default.Compare(x, y);
            if (x > 19 && y > 19) res = 0;
            e.Result = res;
            e.Handled = true;
        }
        void view_CustomGroupDisplayText(object sender, CustomGroupDisplayTextEventArgs e)
        {
            if (e.Column.SortMode == ColumnSortMode.Custom)
            {
                double d = Math.Floor(Convert.ToDouble(e.Value) / 10);
                string ret = string.Format("{0:$0.00} - {1:$0.00} ", d * 10, (d + 1) * 10);
                if (d > 19) ret = string.Format(">= {0:$0.00} ", d * 10);
                e.DisplayText = ret;
            }
        }

        void view_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column && e.MenuInfo.Column.FieldName == "OrderDate")
            {
                e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultColumnMenuItemNames.MenuColumnGroupIntervalNone });
                e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultColumnMenuItemNames.MenuColumnGroupIntervalDay });
                SetContextMenuItemCommand(e, DefaultColumnMenuItemNames.MenuColumnGroupIntervalMonth, 1);
                SetContextMenuItemCommand(e, DefaultColumnMenuItemNames.MenuColumnGroupIntervalYear, 2);
                SetContextMenuItemCommand(e, DefaultColumnMenuItemNames.MenuColumnGroupIntervalSmart, 3);
            }
        }
        void SetContextMenuItemCommand(GridMenuEventArgs e, string itemName, int index)
        {
            BarItem item = e.MenuInfo.Menu.GetBarItemByName(itemName);
            if (item != null)
                item.Command = new DelegateCommand<object>(delegate(object obj) { groupModeList.SelectedIndex = index; });
        }
    }
}
