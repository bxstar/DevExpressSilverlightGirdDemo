using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Utils;
using System.Collections;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core.WPFCompatibility;
using PropertyMetadata = DevExpress.Xpf.Core.WPFCompatibility.SLPropertyMetadata;
using DependencyPropertyChangedEventArgs = DevExpress.Xpf.Core.WPFCompatibility.SLDependencyPropertyChangedEventArgs;

namespace GridDemo
{
    public class MultiViewViewItem
    {
        public object Content { get; set; }
        public string DisplayName { get; set; }
    }

    public abstract class MultiViewViewModelBase : BindableBase
    {
        IList employees = EmployeesWithPhotoData.DataSource;
        public IList Employees { get { return employees; } }

        string columnInfoFieldName = "Phone";
        public string ColumnInfoFieldName
        {
            get { return columnInfoFieldName; }
            set { SetProperty(ref columnInfoFieldName, value, () => ColumnInfoFieldName); }
        }

        public MultiViewViewModelBase()
        {
            ChangeFieldNameCommand = new DelegateCommand<object>(OnChangeFieldName);
        }
        public DelegateCommand<object> ChangeFieldNameCommand { get; private set; }
        bool CanExecuteCommand
        {
            get { return true; }
        }
        void OnChangeFieldName(object param)
        {
            ColumnInfoFieldName = (string)param;
        }
    }
    public class MultiViewTableViewViewModel : MultiViewViewModelBase
    {
    }
    public class MultiViewTreeListViewViewModel : MultiViewViewModelBase
    {
    }
    public class MultiViewBandedTableViewViewModel : MultiViewViewModelBase
    {
    }
    public class MultiViewBandedTreeListViewViewModel : MultiViewViewModelBase
    {
    }
}
