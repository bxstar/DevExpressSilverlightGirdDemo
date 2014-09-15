using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Utils.Themes;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core.WPFCompatibility;

namespace GridDemo
{
    public class GridDemoModule : DemoModule
    {
        public static readonly DependencyProperty GridControlProperty;

        static GridDemoModule()
        {
            GridControlProperty = DependencyProperty.Register("GridControl", typeof(GridControl), typeof(GridDemoModule), new PropertyMetadata(null));
        }

        public GridDemoModule()
        {
            ThemeManager.ActualApplicationThemeChanged += ThemeNameChanged;
        }

        protected virtual bool IsGridBorderVisible { get { return false; } }
        public bool UseGridControlWrapperAsDataContext { get; set; }
        protected virtual void ThemeNameChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e) { }
        protected override void Clear()
        {
            base.Clear();
            ThemeManager.ActualApplicationThemeChanged -= ThemeNameChanged;
        }
        public GridControl GridControl
        {
            get { return (GridControl)GetValue(GridControlProperty); }
            set { SetValue(GridControlProperty, value); }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
        protected override object GetModuleDataContext()
        {
            if (GridControl == null)
            {
                GridControl = FindGrid();
                if (GridControl != null)
                    GridControl.ShowBorder = IsGridBorderVisible;
            }
            if (UseGridControlWrapperAsDataContext)
                return new GridControlWrapper(GridControl);
            return GridControl;
        }
        protected virtual GridControl FindGrid()
        {
            return (GridControl)DemoModuleControl.FindDemoContent(typeof(GridControl), (DependencyObject)DemoModuleControl.Content);
        }
        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            base.RaiseIsPopupContentInvisibleChanged(e);
            if (IsPopupContentInvisible && GridControl != null)
                GridControl.View.HideColumnChooser();
        }
        protected override bool CanLeave()
        {
            if (GridControl == null)
                return true;
            GridControl.View.CommitEditing();
            return true;
        }

    }
    public class GridControlWrapper : System.ComponentModel.INotifyPropertyChanged
    {
        GridControl grid;
        public GridControl GridControl
        {
            get
            {
                return grid;
            }
            set
            {
                if (grid == value)
                    return;
                grid = value;
                OnPropertyChanged("GridControl");
            }
        }
        public GridControlWrapper(GridControl gridControl)
        {
            GridControl = gridControl;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class PrintViewGridDemoModule : GridDemoModule
    {
        public static LinkPreviewModel CreateLinkPreviewModel(IPrintableControl printableControl)
        {
            PrintableControlLink link = new PrintableControlLink(printableControl as IPrintableControl);
            link.ExportServiceUri = "../ExportService.svc";
            return new LinkPreviewModel(link);
        }
        protected virtual DXTabControl DXTabControl { get { return null; } }
        protected override GridControl FindGrid()
        {
            return DXTabControl != null ? (GridControl)((DXTabItem)DXTabControl.Items[0]).Content : null;
        }
        public void ShowPrintPreview()
        {
        }
        public void ShowPrintPreviewInNewTab(GridControl grid, DXTabControl tabControl, string tabName)
        {
            DocumentPreview preview = new DocumentPreview();
            LinkPreviewModel model = CreateLinkPreviewModel(grid.View as IPrintableControl);
            preview.Model = model;

            TabHeaderPrintInfoControl tabHeaderPrintInfoControl = new TabHeaderPrintInfoControl() { TabName = tabName, LinkPreviewModel = model };
            DXTabItem tabItem = new DXTabItem() { AllowHide = DefaultBoolean.True, Content = preview, Header = tabHeaderPrintInfoControl };
            tabControl.Items.Add(tabItem);
            tabControl.SelectedItem = tabItem;

            model.Link.CreateDocument(true);
        }
        protected void DisposePrintPreviewTabContent(DXTabItem tabItem)
        {
            DXTabControl.Items.Remove(tabItem);
            TabHeaderPrintInfoControl tabHeaderPrintInfoControl = (TabHeaderPrintInfoControl)tabItem.Header;
            tabHeaderPrintInfoControl.LinkPreviewModel.Link.Dispose();
        }
        protected override void Clear()
        {
            base.Clear();
            for (int i = DXTabControl.Items.Count - 1; i >= 1; i--)
            {
                DisposePrintPreviewTabContent((DXTabItem)DXTabControl.Items[i]);
            }
        }
        protected virtual void ShowPreviewInNewTab() { }
    }
    public class ResourcesStackPanel : StackPanel
    {
        Uri _resourceSource;
        public Uri ResourceSource
        {
            get
            {
                return _resourceSource;
            }
            set
            {
                _resourceSource = value;
                Resources = new ResourceDictionary() { Source = _resourceSource };
            }
        }
    }
    public class CountryToFlagImageConverter : BytesToImageSourceConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            foreach (Country item in CountriesData.DataSource)
            {
                if (item.Name == (string)value)
                    return base.Convert(item.Flag, targetType, parameter, culture);
            }
            return null;
        }
    }
}
namespace CommonDemo
{
    public class CommonDemoModule : GridDemo.GridDemoModule
    {
    }
}
