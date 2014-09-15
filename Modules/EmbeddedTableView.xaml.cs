using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GridDemo
{
    [CodeFile("ModuleResources/SharedResources(.SL).xaml")]
    [CodeFile("ModuleResources/EmbeddedTableViewTemplates(.SL).xaml")]
    [CodeFile("Controls/MasterDetailItems.(cs)")]
    public partial class EmbeddedTableView : GridDemoModule
    {
        public static readonly DependencyProperty SelectedTabIndexProperty = DependencyProperty.RegisterAttached("SelectedTabIndex", typeof(int), typeof(EmbeddedTableView), new PropertyMetadata(0));
        public static void SetSelectedTabIndex(DependencyObject element, int value)
        {
            element.SetValue(SelectedTabIndexProperty, value);
        }
        public static int GetSelectedTabIndex(DependencyObject element)
        {
            return (int)element.GetValue(SelectedTabIndexProperty);
        }
        public EmbeddedTableView()
        {
            InitializeComponent();
        }
    }
}
