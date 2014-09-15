using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Core;

namespace GridDemo
{
    public partial class TabHeaderPrintInfoControl : UserControl
    {
        public LinkPreviewModel LinkPreviewModel
        {
            get { return (LinkPreviewModel)GetValue(LinkPreviewModelProperty); }
            set { SetValue(LinkPreviewModelProperty, value); }
        }
        public static readonly DependencyProperty LinkPreviewModelProperty =
            DependencyProperty.Register("LinkPreviewModel", typeof(LinkPreviewModel), typeof(TabHeaderPrintInfoControl), new PropertyMetadata(null, new PropertyChangedCallback(OnLinkPreviewModelChanged)));

        public string TabName
        {
            get { return (string)GetValue(TabNameProperty); }
            set { SetValue(TabNameProperty, value); }
        }
        public static readonly DependencyProperty TabNameProperty =
            DependencyProperty.Register("TabName", typeof(string), typeof(TabHeaderPrintInfoControl), new PropertyMetadata(null, new PropertyChangedCallback(OnTabNameChanged)));
        static void OnLinkPreviewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TabHeaderPrintInfoControl)d).OnLinkPreviewModelChanged();
        }
        static void OnTabNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TabHeaderPrintInfoControl)d).OnTabNameChanged();
        }
        public TabHeaderPrintInfoControl()
        {
            InitializeComponent();
        }

        void OnLinkPreviewModelChanged()
        {
            progress.SetBinding(FrameworkElement.VisibilityProperty, new Binding("ProgressVisibility") { Source = LinkPreviewModel, Converter = new BoolToVisibilityConverter() });
            progress.SetBinding(ProgressBar.MaximumProperty, new Binding("ProgressMaximum") { Source = LinkPreviewModel });
            progress.SetBinding(ProgressBar.ValueProperty, new Binding("ProgressValue") { Source = LinkPreviewModel, Mode = BindingMode.OneWay });
        }
        void OnTabNameChanged()
        {
            tabNameTextBlock.Text = TabName;
        }
    }
}
