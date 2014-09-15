using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.DemoBase.Helpers;
using System.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Data.Browsing;

namespace GridDemo
{
    public class AsyncLookUpEditViewModel : BindableBase
    {
        CountItemCollection countItems;
        CountItem selectedCountItem;
        OrderDataGenerator orderDataGenerator;
        OrderDataListSource orderDataListSource;
        bool isDesignTime = true;

        public OrderDataListSource ListSource
        {
            get { return orderDataListSource; }
            set { SetProperty(ref orderDataListSource, value, () => orderDataListSource); }
        }
        public CountItemCollection CountItems
        {
            get { return countItems; }
            set
            {
                if (SetProperty(ref countItems, value, () => CountItems))
                {
                    if ((CountItems != null) && (CountItems.Count > 0))
                    {
                        SelectedCountItem = CountItems[CountItems.Count / 2];
                    }
                    else
                    {
                        SelectedCountItem = null;
                    }
                }
            }
        }
        public CountItem SelectedCountItem
        {
            get { return selectedCountItem; }
            set { SetProperty(ref selectedCountItem, value, () => SelectedCountItem, RecreateItemsSource); }
        }

        public AsyncLookUpEditViewModel()
        {
            orderDataGenerator = new OrderDataGenerator(0);
        }

        void RecreateItemsSource()
        {
            if (SelectedCountItem == null || isDesignTime)
            {
                orderDataGenerator.Count = 0;
            }
            else
            {
                orderDataGenerator.Count = SelectedCountItem.Count;
            }
            ListSource = new OrderDataListSource(orderDataGenerator);
        }
        public void SetIsDesignTime(bool value)
        {
            isDesignTime = value;
            RecreateItemsSource();
        }
    }
}
