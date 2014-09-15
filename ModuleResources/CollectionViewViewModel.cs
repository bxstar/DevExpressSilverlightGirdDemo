using System.Collections;
using System.ComponentModel;
using System.Windows.Data;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Mvvm;

namespace GridDemo
{
    public class CollectionViewViewModel : BindableBase
    {
        IList employeesCore = EmployeesWithPhotoData.DataSource;
        public IList Employees { get { return employeesCore; } }
        public ICollectionView CollectionView { get; private set; }

        public CollectionViewViewModel()
        {
            CollectionView = new CollectionViewSource() { Source = Employees }.View;
            CollectionView.GroupDescriptions.Add(new PropertyGroupDescription("JobTitle"));
            CollectionView.SortDescriptions.Add(new SortDescription("JobTitle", ListSortDirection.Ascending));
            CollectionView.MoveCurrentToFirst();
        }
    }
}
