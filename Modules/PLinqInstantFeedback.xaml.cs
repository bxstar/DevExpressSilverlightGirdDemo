using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/PLinqInstantFeedbackViewModel.(cs)")]
    [CodeFile("ModuleResources/PLinqInstantFeedbackClasses.(cs)")]
    [CodeFile("ModuleResources/PLinqInstantFeedbackTemplates(.SL).xaml")]
    [CodeFile("Controls/OrderDataGenerator.(cs)")]
    public partial class PLinqInstantFeedback : GridDemoModule
    {
        public PLinqInstantFeedback()
        {
            InitializeComponent();
            PLinqInstantFeedbackDemoViewModel viewModel = (PLinqInstantFeedbackDemoViewModel)Resources["PLinqInstantFeedbackDemoViewModel"];
            viewModel.SetIsDesignTime(false);
        }
    }
}
