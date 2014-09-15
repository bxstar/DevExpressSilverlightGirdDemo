using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace GridDemo
{
    public class DragDropViewModel
    {
        public ICommand ClearRecycleBin { get; private set; }
        public DragDropViewModel()
        {
            DataSource = new ObservableCollection<OutlookData>(OutlookDataGenerator.CreateOutlookArrayList(200).Cast<OutlookData>());
            RecycleBinSource = new ObservableCollection<OutlookData>(OutlookDataGenerator.CreateOutlookArrayList(5).Cast<OutlookData>());
            ClearRecycleBin = new DelegateCommand<Object>(OnClearRecycleBin);
        }
        public ObservableCollection<OutlookData> DataSource { get; private set; }
        public ObservableCollection<OutlookData> RecycleBinSource { get; private set; }
        void OnClearRecycleBin(object parameter)
        {
            RecycleBinSource.Clear();
        }
    }
}
