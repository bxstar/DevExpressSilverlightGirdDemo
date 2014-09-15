using System;


namespace GridDemo
{
    public partial class Grouping : GridDemoModule
    {
        public Grouping()
        {
            InitializeComponent();
            GroupByCountryThenCity();
        }
        void viewsListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
        }
        void GroupByCountryThenCity()
        {
            grid.ClearGrouping();
            grid.GroupBy("Country");
            grid.GroupBy("City");
        }

        private void GroupByCountryThenCityThenOrderDate()
        {
            grid.ClearGrouping();
            grid.GroupBy("Country");
            grid.GroupBy("City");
            grid.GroupBy("OrderDate");
        }

        private void GroupByCityThenOrderDate()
        {
            grid.ClearGrouping();
            grid.GroupBy("City");
            grid.GroupBy("OrderDate");
        }
        private void ClearGrouping()
        {
            grid.ClearGrouping();
        }
        private void groupList_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (grid == null) return;
            switch (groupList.SelectedIndex)
            {
                case 0: GroupByCountryThenCity(); break;
                case 1: GroupByCountryThenCityThenOrderDate(); break;
                case 2: GroupByCityThenOrderDate(); break;
                case 3: ClearGrouping(); break;
            }
        }
    }
}
