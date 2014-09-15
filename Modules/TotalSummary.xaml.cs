using System;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using System.Windows.Controls;

namespace GridDemo
{
    public partial class TotalSummary : GridDemoModule
    {
        public TotalSummary()
        {
            InitializeComponent();
        }

        void btnTotalSummaryEditor_Click(object sender, RoutedEventArgs e)
        {
            grid.View.ShowTotalSummaryEditor(grid.Columns[2]);
        }

        void btnFixedTotalSummaryEditor_Click(object sender, RoutedEventArgs e)
        {
            grid.View.ShowFixedTotalSummaryEditor();
        }
    }
}
