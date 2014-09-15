using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;
using DevExpress.Data;
using DevExpress.Xpf.DemoBase.Helpers;
using System.Collections.Generic;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/GroupSummaryViewModel.(cs)")]
    [CodeFile("ModuleResources/GroupSummaryClasses.(cs)")]
    [CodeFile("ModuleResources/GroupSummaryTemplates(.SL).xaml")]
    public partial class GroupSummary : GridDemoModule
    {

        public GroupSummary()
        {
            InitializeComponent();
        }

        private void summaryItemTemplateComboBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (grid == null)
                return;
            if (summaryItemTemplateComboBox.SelectedIndex == 0)
            {
                view.GroupSummaryItemTemplate = (DataTemplate)this.FindResource("customTemplateWithSummaryCustomization");
                view.ClearValue(GridViewBase.GroupSummaryItemTemplateSelectorProperty);
            }
            if (summaryItemTemplateComboBox.SelectedIndex == 1)
            {
                view.GroupSummaryItemTemplateSelector = new ConditionalGroupSummaryItemTemplateSelector((DataTemplate)this.FindResource("smallValueTemplate"),
                    (DataTemplate)this.FindResource("largeValueTemplate"));
                view.ClearValue(GridViewBase.GroupSummaryItemTemplateProperty);
            }
            if (summaryItemTemplateComboBox.SelectedIndex == 2)
            {
                view.GroupSummaryItemTemplate = (DataTemplate)this.FindResource("customTemplate");
                view.ClearValue(GridViewBase.GroupSummaryItemTemplateSelectorProperty);
            }
            if (summaryItemTemplateComboBox.SelectedIndex == 3)
            {
                grid.View.ClearValue(GridViewBase.GroupSummaryItemTemplateProperty);
                grid.View.ClearValue(GridViewBase.GroupSummaryItemTemplateSelectorProperty);
            }
        }
    }
}
