using System;
using System.Collections.Generic;
using System.Windows;
using GridDemo;
using System.Collections;
using System.Globalization;
using DevExpress.Data;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.NWind;
using DevExpress.Data.Browsing;

namespace GridDemo
{
    [CodeFile("Controls/SalesByYearData.(cs)")]
    [CodeFile("ModuleResources/GroupSummariesAlignmentViewModel.(cs)")]
    public partial class GroupSummariesAlignment : GridDemoModule
    {
        GroupSummariesAlignmentViewModel ViewModel { get { return (GroupSummariesAlignmentViewModel)DataContext; } }
        public GroupSummariesAlignment()
        {
            InitializeComponent();
            if (grid.ItemsSource != null)
                PopulateColumnsAndSummaries();
            grid.ExpandGroupRow(-1);
            grid.ItemsSourceChanged += new ItemsSourceChangedEventHandler(grid_ItemsSourceChanged);
        }

        void PopulateColumnsAndSummaries()
        {
            PropertyDescriptorCollection properties = ((ITypedList)grid.ItemsSource).GetItemProperties(null);
            foreach (PropertyDescriptor property in properties)
            {
                if (property.Name.Contains("Date")) continue;
                grid.Columns.Add(new GridColumn()
                {
                    FieldName = property.Name,
                    EditSettings = new SpinEditSettings()
                    {
                        MaskType = MaskType.Numeric,
                        Mask = "c",
                        MaskCulture = new CultureInfo("en-US"),
                        MaskUseAsDisplayFormat = true,
                        DisplayFormat = "${0:N}"
                    }
                });
                grid.TotalSummary.Add(new GridSummaryItem() { FieldName = property.Name, SummaryType = SummaryItemType.Sum, DisplayFormat = "${0:N}" });
                grid.GroupSummary.Add(new GridSummaryItem() { FieldName = property.Name, SummaryType = SummaryItemType.Sum, DisplayFormat = "${0:N}" });
            }
        }
        private void grid_ItemsSourceChanged(object sender, ItemsSourceChangedEventArgs e)
        {
            if (grid.GroupSummary.Count == 0)
                PopulateColumnsAndSummaries();
            bool byMonthReport = ViewModel.ReportTypeIndex == 1;
            grid.Columns["DateMonth"].Visible = byMonthReport;
            if (byMonthReport)
                grid.Columns["DateMonth"].GroupIndex = 1;
            else
                grid.UngroupBy("DateMonth");
            grid.ExpandGroupRow(-1);
        }
    }
}
