using System;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Core.Native;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Input;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase;
using DevExpress.Mvvm;

namespace GridDemo
{
    [CodeFile("ModuleResources/RoutedEventsHelper.(cs)")]
    [CodeFile("ModuleResources/FixedColumnsTemplates(.SL).xaml")]
    [CodeFile("Controls/Converters.(cs)")]
    public partial class FixedColumns : GridDemoModule
    {
        public FixedColumns()
        {
            ClosePopupCommand = new DelegateCommand<RoutedEventHandlerArgs>(ClosePopup);
            DataContext = this;
            InitializeComponent();
        }
        private void ClosePopup(RoutedEventHandlerArgs obj)
        {
            RadioButtonList_SelectionChanged(obj.Sender, (EditValueChangedEventArgs)obj.Args);
        }
        private void RadioButtonList_SelectionChanged(object sender, EditValueChangedEventArgs e)
        {
            DependencyObject d = LayoutHelper.FindLayoutOrVisualParentObject(((DependencyObject)sender), new Predicate<DependencyObject>(IsOpenedPopup));
            Popup popupRoot = d as Popup;
            if ((popupRoot != null) && (e.OldValue != null))
                popupRoot.IsOpen = false;
        }
        bool IsOpenedPopup(DependencyObject obj)
        {
            Popup p = obj as Popup;
            return p != null && p.IsOpen;
        }
        public ICommand ClosePopupCommand { get; private set; }
    }
}
