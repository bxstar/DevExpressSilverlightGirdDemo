using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.Mvvm;
using DevExpress.Utils;

namespace GridDemo
{
    public class GridSearchPanelViewModel : BindableBase
    {
        ICommand selectAllItemsCommand;
        IList<ListColumn> columns;
        DataViewBase view;
        public DataViewBase View
        {
            get { return this.view; }
            set { SetProperty(ref view, value, () => View); }
        }
        public IList<ListColumn> Columns
        {
            get { return this.columns; }
            set { SetProperty(ref columns, value, () => Columns); }
        }
        public ICommand SelectAllItemsCommand
        {
            get { return this.selectAllItemsCommand; }
            set { SetProperty(ref selectAllItemsCommand, value, () => SelectAllItemsCommand); }
        }
        public ICommand ChangeAllowSearchPanelCommand { get; private set; }
        public ICommand ChangeSearchPanelVisibilityCommand { get; private set; }
        public ICommand PopulateColumnsCommand { get; private set; }
        public ChangeSelectionAction SelectAllAction { get { return ChangeSelectionAction.SelectAll; } }
        public GridSearchPanelViewModel()
        {
            ChangeAllowSearchPanelCommand = new DelegateCommand<ObservableCollection<object>>(UpdateAllowSearchPanel, CanUpdateAllowSearchPanel);
            ChangeSearchPanelVisibilityCommand = new DelegateCommand<bool>(ChangeSearchPanelVisibility);
            PopulateColumnsCommand = new DelegateCommand<GridColumnCollection>(PopulateColumns);
            Columns = new List<ListColumn>();
        }
        bool CanUpdateAllowSearchPanel(ObservableCollection<object> selection)
        {
            return selection != null;
        }
        void PopulateColumns(GridColumnCollection gridColumns)
        {
            Columns = ListColumn.CreateCollection(gridColumns);
        }
        void ChangeSearchPanelVisibility(bool args)
        {
            if ((bool)args)
                View.Commands.ShowSearchPanel.Execute(false);
            else
                View.Commands.HideSearchPanel.Execute(null);
        }
        void UpdateAllowSearchPanel(ObservableCollection<object> selection)
        {
            foreach (var listColumn in Columns)
                listColumn.Column.AllowSearchPanel = selection.Contains(listColumn) ? DefaultBoolean.True : DefaultBoolean.False;
        }
    }
}
