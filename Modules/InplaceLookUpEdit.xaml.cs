using System;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using System.Collections;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/InplaceLookUpEditResources(.SL).xaml")]
    [CodeFile("ModuleResources/InplaceLookUpEditClasses.(cs)")]
    public partial class InplaceLookUpEdit : GridDemoModule
    {
        public InplaceLookUpEdit()
        {
            InitializeComponent();
            grid.Columns["CustomerID"].EditSettings = CreateLookUpEditSettings("CustomerID", "CompanyName", (IEnumerable)((Resources["NWindDataLoader"] as NWindDataLoader).Customers), Resources["customerGridTemplate"] as ControlTemplate);
            grid.Columns["EmployeeID"].EditSettings = CreateLookUpEditSettings("EmployeeID", "LastName", (IEnumerable)((Resources["NWindDataLoader"] as NWindDataLoader).EmployeesNew), Resources["employeeGridTemplate"] as ControlTemplate);
        }
        LookUpEditSettings CreateLookUpEditSettings(string valueMember, string displayMember, IEnumerable itemsSource, ControlTemplate contentTemplate)
        {
            LookUpEditSettings settings = new LookUpEditSettings() { AutoPopulateColumns = false, IsPopupAutoWidth = false, DisplayMember = displayMember, ValueMember = valueMember, ItemsSource = itemsSource, PopupContentTemplate = contentTemplate };
            settings.SetBinding(LookUpEditSettings.AutoCompleteProperty, new Binding("IsChecked") { Source = chkAllowAutoComplete, Mode = BindingMode.TwoWay });
            settings.SetBinding(LookUpEditSettings.ImmediatePopupProperty, new Binding("IsChecked") { Source = chkImmediatePopup, Mode = BindingMode.TwoWay });
            return settings;
        }
    }
}
