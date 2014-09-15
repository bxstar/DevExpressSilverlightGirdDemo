using System;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/RiaInstantFeedbackTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/RiaInstantFeedbackViewModel.(cs)")]
    [CodeFile("Controls/Converters.(cs)")]
    public partial class RIAInstantFeedback : GridDemoModule
    {
        public RIAInstantFeedback()
        {
            InitializeComponent();
            RIAInstantFeedbackViewModel viewModel = new RIAInstantFeedbackViewModel();
            DataContext = viewModel;
            viewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(viewModel_PropertyChanged);
        }

        void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!(sender as RIAInstantFeedbackViewModel).IsUseExtendedDataQuery)
            {
                colProductName.GroupIndex = -1;
                grid.FilterCriteria = null;
            }
        }
    }
}
