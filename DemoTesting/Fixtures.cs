using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Grid.Native;
using System.Linq;

namespace GridDemo.Tests
{
    public class GridCheckAllDemosFixture : CheckAllDemosFixture
    {
        Type[] skipMemoryLeaksCheckModules = new Type[] {
            typeof(HitTest), typeof(InplaceLookUpEdit)
        };
        Type[] skipRunModules = new Type[] {
            typeof(PagedCollectionView),
            typeof(RIAServices),
            typeof(MultiEditors)
        };
        protected override bool CheckMemoryLeaks(Type moduleType)
        {
            return !skipMemoryLeaksCheckModules.Contains(moduleType);
        }
        protected override bool CanRunModule(Type moduleType)
        {
            return !skipRunModules.Contains(moduleType);
        }
    }
}
