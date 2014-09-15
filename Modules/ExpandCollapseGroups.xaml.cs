using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using System.Windows.Media.Animation;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/ExpandCollapseGroupsTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/ExpandCollapseGroupsClasses.(cs)")]
    public partial class ExpandCollapseGroups : GridDemoModule
    {
        public ExpandCollapseGroups()
        {
            InitializeComponent();
        }
        void animationTypeComboBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (grid == null)
                return;
            if (animationTypeComboBox.SelectedIndex == 0 || animationTypeComboBox.SelectedIndex == 2 || animationTypeComboBox.SelectedIndex == 4)
            {
                grid.View.ClearValue(GridViewBase.CollapseStoryboardProperty);
                grid.View.ClearValue(GridViewBase.ExpandStoryboardProperty);
            }
            if (animationTypeComboBox.SelectedIndex == 1 || animationTypeComboBox.SelectedIndex == 3 || animationTypeComboBox.SelectedIndex == 5)
            {
                view.ExpandStoryboard = GetStoryboard("expandStoryborad");
                view.CollapseStoryboard = GetStoryboard("collapseStoryborad");
            }
            if (animationTypeComboBox.SelectedIndex == 1 || animationTypeComboBox.SelectedIndex == 2)
                view.RowDecorationTemplate = (ControlTemplate)Resources["fadeInTemplate"];
            if (animationTypeComboBox.SelectedIndex == 3 || animationTypeComboBox.SelectedIndex == 4)
                view.RowDecorationTemplate = (ControlTemplate)Resources["blindsTemplate"];
            if (animationTypeComboBox.SelectedIndex == 0 || animationTypeComboBox.SelectedIndex == 5)
                view.ClearValue(DevExpress.Xpf.Grid.TableView.RowDecorationTemplateProperty);
        }
        string GetStoryboard(string resourceName)
        {
            return
                @"
			<Storyboard>
				<DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=""AnimationProgress"">
					<DiscreteDoubleKeyFrame Value=""0"" KeyTime=""0""/>
				</DoubleAnimationUsingKeyFrames>
			</Storyboard>
";
        }
    }
}
