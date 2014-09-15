using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/UnboundColumnsTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/UnboundColumnsClasses.(cs)")]
    public partial class UnboundColumns : GridDemoModule
    {
        public UnboundColumns()
        {
            InitializeComponent();
            grid.ExpandAllGroups();
        }
        private void showExpressionEditorButton_Click(object sender, RoutedEventArgs e)
        {
            grid.View.ShowUnboundExpressionEditor(grid.Columns[(string)((ListBoxItem)columnsList.SelectedItem).Tag]);
        }
    }
}
