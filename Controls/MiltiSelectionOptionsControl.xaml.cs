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
using System.Collections;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Utils;
using PropertyMetadata = DevExpress.Xpf.Core.WPFCompatibility.SLPropertyMetadata;
using DependencyPropertyChangedEventArgs = DevExpress.Xpf.Core.WPFCompatibility.SLDependencyPropertyChangedEventArgs;
using DevExpress.Xpf.Core.WPFCompatibility;

namespace GridDemo
{
    public partial class MultiSelectionOptionsControl : UserControl
    {
        public static readonly DependencyProperty ComboBoxItemsSourceProperty;
        static MultiSelectionOptionsControl()
        {
            ComboBoxItemsSourceProperty = DependencyPropertyManager.Register("ComboBoxItemsSource", typeof(IEnumerable), typeof(MultiSelectionOptionsControl), new PropertyMetadata(null, ComboBoxItemsSourceChanged));
        }
        static void ComboBoxItemsSourceChanged(DependencyObject dObject, DependencyPropertyChangedEventArgs e)
        {
            ((MultiSelectionOptionsControl)dObject).OnComboBoxItemsSourceChanged();
        }
        public MultiSelectionOptionsControl()
        {
            InitializeComponent();
        }
        public IEnumerable ComboBoxItemsSource
        {
            get { return (IEnumerable)GetValue(ComboBoxItemsSourceProperty); }
            set { SetValue(ComboBoxItemsSourceProperty, value); }
        }
        EventHandler selectButtonClickHandler;
        public event EventHandler SelectButtonClick
        {
            add { selectButtonClickHandler += value; }
            remove { selectButtonClickHandler -= value; }
        }
        EventHandler unselectButtonClickHandler;
        public event EventHandler UnselectButtonClick
        {
            add { unselectButtonClickHandler += value; }
            remove { unselectButtonClickHandler -= value; }
        }
        EventHandler reselectButtonClickHandler;
        public event EventHandler ReselectButtonClick
        {
            add { reselectButtonClickHandler += value; }
            remove { reselectButtonClickHandler -= value; }
        }
        void OnComboBoxItemsSourceChanged()
        {
            comboBoxControl.ItemsSource = ComboBoxItemsSource;
        }
        protected void RaiseButtonClick(EventHandler handler)
        {
            EventArgs e = new EventArgs();
            if (handler != null)
                handler(this, e);
        }
        public string Header { get { return Convert.ToString(groupBoxControl.Header); } set { groupBoxControl.Header = value; } }
        public string ComboBoxDisplayMember { get { return comboBoxControl.DisplayMember; } set { comboBoxControl.DisplayMember = value; } }
        public string ComboBoxValueMember { get { return comboBoxControl.ValueMember; } set { comboBoxControl.ValueMember = value; } }
        public ComboBoxEdit ComboBox { get { return comboBoxControl; } }
        private void SelectButtonClickInClass(object sender, RoutedEventArgs e)
        {
            RaiseButtonClick(selectButtonClickHandler);
        }
        private void UnselectButtonClickInClass(object sender, RoutedEventArgs e)
        {
            RaiseButtonClick(unselectButtonClickHandler);
        }
        private void ReselectButtonClickInClass(object sender, RoutedEventArgs e)
        {
            RaiseButtonClick(reselectButtonClickHandler);
        }
    }
}
