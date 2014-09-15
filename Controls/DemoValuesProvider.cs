using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;
using DevExpress.DemoData.Helpers;


namespace GridDemo
{
    public class DemoValuesProvider
    {
        public IEnumerable<GridViewNavigationStyle> NavigationStyles { get { return DevExpress.Data.Mask.EnumHelper.GetValues(typeof(GridViewNavigationStyle)).Cast<GridViewNavigationStyle>(); } }
    }
}
