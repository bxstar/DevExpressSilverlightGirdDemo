using System;
using System.Windows;
using System.Windows.Data;
using GridDemo;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Data.Mask;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/AutoFilterRowTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/AutoFilterRowClasses.(cs)")]
    public partial class AutoFilterRow : GridDemoModule
    {
        public AutoFilterRow()
        {
            InitializeComponent();
            grid.ItemsSource = OutlookDataGenerator.CreateOutlookDataTable(1000);
            ComboBoxEditSettings settings = new ComboBoxEditSettings() { IsTextEditable = false };
            settings.ItemsSource = EnumHelper.GetValues(typeof(Priority));
            colPriority.EditSettings = settings;
        }
    }
}
