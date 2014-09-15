using System.Windows;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/TreeListViewTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/TreeListViewClasses.(cs)")]
    public partial class TreeListView : GridDemoModule
    {
        public TreeListView()
        {
            InitializeComponent();
            this.view.ExpandAllNodes();
        }
        private void chkEnableContextMenu_Checked(object sender, RoutedEventArgs e)
        {
            view.IsColumnMenuEnabled = true;
            view.IsRowCellMenuEnabled = true;
            view.IsTotalSummaryMenuEnabled = true;
        }
        private void chkEnableContextMenu_Unchecked(object sender, RoutedEventArgs e)
        {
            view.IsColumnMenuEnabled = false;
            view.IsRowCellMenuEnabled = false;
            view.IsTotalSummaryMenuEnabled = false;
        }
    }
}
