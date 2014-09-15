using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Xpf.Grid;

namespace GridDemo
{
    public class ListColumn
    {
        public ListColumn(ColumnBase column)
        {
            this.Column = column;
            HeaderCaption = column.HeaderCaption.ToString();
        }
        public ColumnBase Column { get; private set; }
        public string HeaderCaption { get; private set; }
        public static IList<ListColumn> CreateCollection(GridColumnCollection gridColumns)
        {
            var collection = new ObservableCollection<ListColumn>(gridColumns.Select(col => new ListColumn(col)));
            return new ReadOnlyObservableCollection<ListColumn>(collection);
        }
    }
}
