using System;
using System.Windows.Data;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/FilteringTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/FilteringClasses.(cs)")]
    public partial class Filtering : GridDemoModule
    {
        public Filtering()
        {
            InitializeComponent();
            grid.FilterCriteria = new BinaryOperator("City", "Bergamo", BinaryOperatorType.Equal);
            grid.FilterCriteria = new BinaryOperator("OrderDate", new DateTime(1995, 1, 1), BinaryOperatorType.GreaterOrEqual);
            showFilterPanelModeListBox.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(showFilterPanelModeListBox_SelectionChanged);

        }
        private void showFilterPanelModeListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            UpdateShowFilterPanelMode();
        }

        private void UpdateShowFilterPanelMode()
        {
            if (showFilterPanelModeListBox.SelectedIndex == 0)
                grid.View.ShowFilterPanelMode = ShowFilterPanelMode.Default;
            if (showFilterPanelModeListBox.SelectedIndex == 1)
                grid.View.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways;
            if (showFilterPanelModeListBox.SelectedIndex == 2)
                grid.View.ShowFilterPanelMode = ShowFilterPanelMode.Never;
        }
    }
}
