using System;
using System.Collections.Generic;
using System.Windows.Data;
using GridDemo;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase.NWind;
using System.Collections.ObjectModel;

namespace GridDemo
{
    public partial class NewItemRow : GridDemoModule
    {
        int newRowID = 10000;
        public NewItemRow()
        {
            InitializeComponent();

            grid.ItemsSource = new ObservableCollection<OrderDetails>(((NWindDataLoader)Resources["NWindDataLoader"]).OrderDetailsNew as IEnumerable<OrderDetails>);
        }

        protected override void RaiseModuleAppear()
        {
            base.RaiseModuleAppear();
            view.ScrollIntoView(view.FocusedRowHandle);
        }
        void view_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e)
        {
            grid.SetCellValue(e.RowHandle, colQuantity, 1);
            grid.SetCellValue(e.RowHandle, colUnitPrice, 100);
            grid.SetCellValue(e.RowHandle, colDiscount, 0);
            grid.SetCellValue(e.RowHandle, colOrderID, newRowID++);
        }
        void newItemRowPositionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (view.NewItemRowPosition != NewItemRowPosition.None)
            {
                view.FocusedRowHandle = GridControl.NewItemRowHandle;
                view.ScrollIntoView(view.FocusedRowHandle);
            }
        }
    }
}
