using System;
using System.Windows;
using GridDemo;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("Controls/DemoModuleControl.(cs)")]
    [CodeFile("ModuleResources/PrintOptionsTemplates(.SL).xaml")]
    public partial class PrintOptions : PrintViewGridDemoModule
    {
        protected override DXTabControl DXTabControl { get { return tabControl; } }
        public PrintOptions()
        {
            InitializeComponent();
            printStyleChooser.SelectedIndex = 1;
            ShowPreviewInNewTab();
            printStyleChooser.SelectedIndex = 0;
            ShowPreviewInNewTab();
            tabControl.SelectionChanged += TabControlSelectionChanged;
        }
        void TabControlSelectionChanged(object sender, TabControlSelectionChangedEventArgs e)
        {
            ThemeManager.UpdateApplicationTheme((FrameworkElement)tabControl.SelectedItemContent);
        }
        private void printStyleChooser_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (view == null) return;
            if (printStyleChooser.SelectedIndex == 0)
            {
                view.ClearValue(DevExpress.Xpf.Grid.TableView.PrintColumnHeaderStyleProperty);
                view.ClearValue(DevExpress.Xpf.Grid.TableView.PrintCellStyleProperty);
                view.ClearValue(DevExpress.Xpf.Grid.TableView.PrintGroupRowStyleProperty);
                view.ClearValue(DevExpress.Xpf.Grid.TableView.PrintTotalSummaryStyleProperty);
                view.ClearValue(DevExpress.Xpf.Grid.TableView.PrintFixedTotalSummaryStyleProperty);
            }
            if (printStyleChooser.SelectedIndex == 1)
            {
                view.PrintColumnHeaderStyle = (Style)Resources["customPrintColumnHeaderStyle"];
                view.PrintCellStyle = (Style)Resources["customPrintCellStyle"];
                view.PrintGroupRowStyle = (Style)Resources["customGroupRowStyle"];
                view.PrintTotalSummaryStyle = (Style)Resources["customPrintTotalSummaryStyle"];
                view.PrintFixedTotalSummaryStyle = (Style)Resources["customPrintFixedTotalSummaryStyle"];
            }
        }
        protected override void ShowPreviewInNewTab()
        {
            ShowPrintPreviewInNewTab(grid, tabControl, string.Format("{0} Style Preview", printStyleChooser.SelectedItem));
        }
        protected void newTabButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPreviewInNewTab();
        }
        protected void tabControl_TabHidden(object sender, TabControlTabHiddenEventArgs e)
        {
            DisposePrintPreviewTabContent((DXTabItem)DXTabControl.Items[e.TabIndex]);
        }
    }
}
