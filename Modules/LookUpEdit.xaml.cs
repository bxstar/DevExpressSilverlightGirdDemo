using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;
using System.Collections;
using DevExpress.Xpf.Core;
using DevExpress.Utils;


namespace CommonDemo
{
    [CodeFile("ModuleResources/LookUpEditTemplates(.SL).xaml")]
    public partial class LookUpEdit : CommonDemoModule
    {
        NWindDataLoader NWind { get; set; }
        string GenericXamlName { get { return "Generic.SL.xaml"; } }
        IList Categories { get { return (IList)NWind.Categories; } }
        IList Products { get { return (IList)lookUpEdit.DataContext; } }
        int NewItemRowID { get { return Products.Count + 1; } }
        public LookUpEdit()
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(string.Format("/{0};component/Themes/{1}", AssemblyHelper.GetPartialName(typeof(LookUpEdit).Assembly), GenericXamlName), UriKind.Relative) });
            InitializeComponent();
            NWind = Resources["NWindDataLoader"] as NWindDataLoader;
        }
        Control control;
        private void lookUpEdit_ProcessNewValue(DependencyObject sender, DevExpress.Xpf.Editors.ProcessNewValueEventArgs e)
        {
            if (!(bool)chProcessNewValue.IsChecked)
                return;

            control = new ContentControl { Template = (ControlTemplate)Resources["addNewRecordTemplate"], Tag = e };
            DevExpress.Xpf.DemoBase.NWind.Products row = new DevExpress.Xpf.DemoBase.NWind.Products();
            row.ProductName = e.DisplayText;
            row.ProductID = NewItemRowID;

            control.DataContext = row;
            FrameworkElement owner = sender as FrameworkElement;
            DialogClosedDelegate closeHandler = CloseAddNewRecordHandler;

            FloatingContainer.ShowDialogContent(control, owner, Size.Empty, new FloatingContainerParameters()
            {
                Title = "Add New Record",
                AllowSizing = false,
                ClosedDelegate = closeHandler
            });
            e.PostponedValidation = true;
            e.Handled = true;
        }

        void CloseAddNewRecordHandler(bool? close)
        {
            if (close != null && (bool)close)
                Products.Add(control.DataContext);
            control = null;
        }
        protected override object GetModuleDataContext()
        {
            return lookUpEdit;
        }
    }
}
