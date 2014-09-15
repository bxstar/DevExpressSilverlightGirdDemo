using System;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/DragDropTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/DragDropViewModel.(cs)")]
    public partial class DragDrop : GridDemoModule
    {
        public DragDrop()
        {
            InitializeComponent();
        }
        public override bool AllowRtl { get { return false; } }
    }
}
