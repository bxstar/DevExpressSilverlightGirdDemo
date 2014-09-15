using System;
using System.Diagnostics;
using DevExpress.Xpf.DemoBase.Helpers;
using System.Windows.Data;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/WcfInstantFeedbackViewModel.(cs)")]
    [CodeFile("ModuleResources/WcfInstantFeedbackTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/HyperLinkAttachedBehavior.(cs)")]
    [CodeFile("Controls/Converters.(cs)")]
    public partial class WCFInstantFeedback : GridDemoModule
    {
        public WCFInstantFeedback()
        {
            InitializeComponent();
            WCFInstantFeedbackViewModel viewModel = new WCFInstantFeedbackViewModel();
            DataContext = viewModel;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
        }

        void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!(sender as WCFInstantFeedbackViewModel).IsUseExtendedDataQuery)
            {
                colProductName.GroupIndex = -1;
                grid.FilterCriteria = null;
            }
        }
    }
}
