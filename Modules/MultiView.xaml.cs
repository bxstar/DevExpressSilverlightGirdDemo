using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DevExpress.Xpf.Core.Commands;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Grid;

namespace GridDemo
{
    [CodeFile("ModuleResources/MultiViewTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/MultiViewViewTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/SharedResources(.SL).xaml")]
    [CodeFile("ModuleResources/MultiViewClasses.(cs)")]
    [CodeFile("ModuleResources/MultiViewViewModel.(cs)")]
    [CodeFile("Controls/Converters.(cs)")]
    public partial class MultiView : GridDemoModule
    {
        public MultiView()
        {
            InitializeComponent();
            BeforeModuleDisappear += new RoutedEventHandler(MultiView_BeforeModuleDisappear);
        }

        void MultiView_BeforeModuleDisappear(object sender, RoutedEventArgs e)
        {
            multiViewListBox.Items.Clear();
        }
    }
}
