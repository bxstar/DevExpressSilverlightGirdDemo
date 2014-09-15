using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/SharedResources(.SL).xaml")]
    [CodeFile("ModuleResources/MasterDetailViewModel.(cs)")]
    [CodeFile("Controls/ControlStyles/MasterDetailTemplates(.SL).xaml")]
    [CodeFile("Controls/Converters.(cs)")]
    public partial class MasterDetailView : GridDemoModule
    {
        public MasterDetailView()
        {
            InitializeComponent();
        }
    }
}
