using System;
using System.Windows;
using System.Windows.Data;
using GridDemo;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Data.Mask;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace GridDemo
{
    public partial class BandedView : GridDemoModule
    {
        public BandedView()
        {
            InitializeComponent();
            grid.ItemsSource = CarsData.NewDataSource;
        }
    }
}
