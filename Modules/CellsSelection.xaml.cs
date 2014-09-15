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
using DevExpress.Xpf.DemoBase.NWind;
using DevExpress.Data.Browsing;

namespace GridDemo
{
    public partial class CellsSelection : GridDemoModule
    {
        public CellsSelection()
        {
            InitializeComponent();
            AssignDataSource();

            SelectCells();
        }

        void SelectCells()
        {
            grid.BeginSelection();
            int[,] selectedCells = new int[,] {
                {0, 1}, {1, 1}, {2, 1}, {3, 1}, {4, 1}, {5, 1}, {6, 1}, {7, 1}, {6, 1},{6, 1}, {7, 1}, {8, 1}, {9, 1}, {10, 1},
                {0, 2}, {1, 2}, {2, 2}, {8, 2}, {9, 2}, {10, 2},
                {2, 3}, {3, 3}, {4, 3}, {5, 3}, {6, 3}, {7, 3}, {8, 3},

                {12, 1}, {13, 1}, {14, 1}, {15, 1}, {19, 1}, {20, 1},  {21, 1},  {22, 1},
                {14, 2}, {15, 2}, {16, 2}, {17, 2}, {18, 2}, {19, 2}, {20, 2},
                {12, 3}, {13, 3}, {14, 3}, {15, 3}, {19, 3}, {20, 3},  {21, 3},  {22, 3}
            };
            for (int i = 0; i < selectedCells.GetLength(0); i++)
                view.SelectCell(selectedCells[i, 0], view.VisibleColumns[selectedCells[i, 1]]);
            grid.EndSelection();
        }

        void AssignDataSource()
        {
            grid.ItemsSource = SalesByYearData.GetSalesByYearData();
            grid.Columns["Date"].Visible = false;
            grid.Columns["Date"].ShowInColumnChooser = false;
            foreach (GridColumn column in view.VisibleColumns)
            {
                grid.TotalSummary.Add(new GridSummaryItem() { FieldName = column.FieldName, SummaryType = DevExpress.Data.SummaryItemType.Custom, DisplayFormat = "${0:N}" });
                column.EditSettings = new SpinEditSettings() { MaskType = MaskType.Numeric, MaskUseAsDisplayFormat = true, Mask = "c", MaskCulture = new CultureInfo("en-US") };
            }
        }

        int sum = 0;
        void grid_CustomSummary(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (object.Equals(e.SummaryProcess, CustomSummaryProcess.Start))
            {
                sum = 0;
            }
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                if (grid.View != null)
                {
                    if (!checkEdit.IsChecked.Value || view.IsCellSelected(e.RowHandle, grid.Columns[((GridSummaryItem)e.Item).FieldName]))
                    {
                        if (e.FieldValue != DBNull.Value && e.FieldValue != null)
                            sum += (int)e.FieldValue;
                    }
                }
            }
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                e.TotalValue = sum;
        }

        void TableView_SelectionChanged(object sender, DevExpress.Xpf.Grid.GridSelectionChangedEventArgs e)
        {
            grid.UpdateTotalSummary();
        }

        void CheckEdit_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            grid.UpdateTotalSummary();
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectCells(true);
        }
        void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SelectCells(false);
        }

        void SelectCells(bool shouldSelectTopValues)
        {
            List<KeyValuePair<GridCell, int>> list = new List<KeyValuePair<GridCell, int>>();
            for (int i = 0; i < grid.VisibleRowCount; i++)
                for (int j = 0; j < view.VisibleColumns.Count; j++)
                    list.Add(new KeyValuePair<GridCell, int>(new GridCell(i, view.VisibleColumns[j]), (int)grid.GetCellValue(i, view.VisibleColumns[j])));
            list.Sort(delegate(KeyValuePair<GridCell, int> x, KeyValuePair<GridCell, int> y)
            {
                return Compare(x, y, shouldSelectTopValues);
            });

            grid.BeginSelection();
            view.DataControl.UnselectAll();
            for (int i = 0; i < Math.Min(20, list.Count); i++)
            {
                view.SelectCell(list[list.Count - i - 1].Key.RowHandle, list[list.Count - i - 1].Key.Column);
            }
            grid.EndSelection();
        }

        private static int Compare(KeyValuePair<GridCell, int> x, KeyValuePair<GridCell, int> y, bool shouldSelectTopValues)
        {
            if (shouldSelectTopValues)
                return Comparer<int>.Default.Compare(x.Value, y.Value);
            else
                return Comparer<int>.Default.Compare(y.Value, x.Value);
        }
    }
}
