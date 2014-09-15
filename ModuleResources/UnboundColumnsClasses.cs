using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;

namespace GridDemo
{
    public class TotalCellTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            GridCellData cellData = item as GridCellData;
            if (cellData != null)
            {
                try
                {
                    decimal value = Convert.ToDecimal(cellData.Value);
                    if (value >= 500) return TotalCellTemplate;
                }
                catch (Exception) { }
            }
            return base.SelectTemplate(item, container);
        }
        public DataTemplate TotalCellTemplate { get; set; }
    }
}
