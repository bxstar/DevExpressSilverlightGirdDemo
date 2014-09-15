using System;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using System.Windows;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("Controls/Converters.(cs)")]
    public partial class TableView : GridDemoModule
    {
        public TableView()
        {
            InitializeComponent();
        }

        void lbSummary_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (lbSummary.SelectedIndex == 0)
            {
                grid.View.ShowTotalSummary = false;
                grid.View.ShowFixedTotalSummary = true;
            }
            else
            {
                grid.View.ShowTotalSummary = true;
                grid.View.ShowFixedTotalSummary = false;
            }
        }
    }
}
