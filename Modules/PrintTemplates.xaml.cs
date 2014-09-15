using System;
using System.Windows;
using GridDemo;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/PrintTemplatesResources(.SL).xaml")]
    [CodeFile("Controls/DemoModuleControl.(cs)")]
    public partial class PrintTemplates : PrintViewGridDemoModule
    {
        protected override DXTabControl DXTabControl { get { return tabControl; } }
        public PrintTemplates()
        {
            InitializeComponent();
            templateNamesListBox.SelectedIndex = 2;
            ShowPreviewInNewTab();
            templateNamesListBox.SelectedIndex = 1;
            ShowPreviewInNewTab();
            templateNamesListBox.SelectedIndex = 0;
            ShowPreviewInNewTab();
            tabControl.SelectionChanged += TabControlSelectionChanged;
        }
        void TabControlSelectionChanged(object sender, TabControlSelectionChangedEventArgs e)
        {
            ThemeManager.UpdateApplicationTheme((FrameworkElement)tabControl.SelectedItemContent);
        }
        private void templateNamesListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (view == null) return;
            if (templateNamesListBox.SelectedIndex == 0)
            {
                view.PrintHeaderTemplate = (DataTemplate)Resources["detailPrintHeaderTemplate"];
                view.PrintRowTemplate = (DataTemplate)Resources["detailPrintRowTemplate"];
                view.PrintColumnHeaderStyle = (Style)Resources["mailMergePrintHeaderStyle"];
            }
            if (templateNamesListBox.SelectedIndex == 1)
            {
                view.PrintHeaderTemplate = null;
                view.PrintRowTemplate = (DataTemplate)Resources["mailMergePrintRowTemplate"];
                view.PrintColumnHeaderStyle = (Style)Resources["mailMergePrintHeaderStyle"];
            }
            if (templateNamesListBox.SelectedIndex == 2)
            {
                view.ClearValue(DevExpress.Xpf.Grid.TableView.PrintHeaderTemplateProperty);
                view.ClearValue(DevExpress.Xpf.Grid.TableView.PrintRowTemplateProperty);
                view.ClearValue(DevExpress.Xpf.Grid.TableView.PrintColumnHeaderStyleProperty);
            }
        }
        protected override void ShowPreviewInNewTab()
        {
            ShowPrintPreviewInNewTab(grid, tabControl, string.Format("{0} Preview", templateNamesListBox.SelectedItem));
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
