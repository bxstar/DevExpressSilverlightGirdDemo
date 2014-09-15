using System;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/CurrentDataRowTemplates(.SL).xaml")]
    public partial class CurrentDataRow : GridDemoModule
    {
        public CurrentDataRow()
        {
            InitializeComponent();
        }
        protected override bool IsGridBorderVisible { get { return true; } }
    }
}
