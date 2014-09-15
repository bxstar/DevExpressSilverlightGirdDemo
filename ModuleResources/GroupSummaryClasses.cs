using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;

namespace GridDemo
{
    public class ConditionalGroupSummaryItemTemplateSelector : DataTemplateSelector
    {
        readonly DataTemplate smallValueTemplate;
        readonly DataTemplate largeValueTemplate;
        public ConditionalGroupSummaryItemTemplateSelector(DataTemplate smallValueTemplate, DataTemplate largeValueTemplate)
        {
            this.smallValueTemplate = smallValueTemplate;
            this.largeValueTemplate = largeValueTemplate;
        }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            GridGroupSummaryData data = (GridGroupSummaryData)item;
            if (data.SummaryItem.SummaryType == DevExpress.Data.SummaryItemType.Sum)
            {
                double value = Convert.ToDouble(data.Value);
                if (value < 5000)
                    return smallValueTemplate;
                if (value >= 10000)
                    return largeValueTemplate;
            }
            return null;
        }
    }
}
