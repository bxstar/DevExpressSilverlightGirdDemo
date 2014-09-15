using System.Windows.Controls;
using DevExpress.Data.Filtering;

namespace GridDemo
{
    public partial class FilterControl : GridDemoModule
    {
        public FilterControl()
        {
            InitializeComponent();
            filterGrid.FilterCriteria = new BinaryOperator("OrderID", 10248, BinaryOperatorType.Equal);
        }

        protected override bool IsGridBorderVisible { get { return true; } }

        private void ApplyFilterButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            filterEditor.ApplyFilter();
        }

        private void TableView_FilterEditorCreated(object sender, DevExpress.Xpf.Grid.FilterEditorEventArgs e)
        {
            e.FilterControl.ShowGroupCommandsIcon = filterEditor.ShowGroupCommandsIcon;
            e.FilterControl.ShowOperandTypeIcon = filterEditor.ShowOperandTypeIcon;
            e.FilterControl.ShowToolTips = filterEditor.ShowToolTips;
        }
    }
}
