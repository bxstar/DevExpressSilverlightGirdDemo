using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using DevExpress.Xpf.DemoBase.NWind;

namespace GridDemo
{
    public class DemoTableViewHitTestVisitor : TableViewHitTestVisitorBase
    {
        readonly HitTest hitTest;
        public DemoTableViewHitTestVisitor(HitTest hitTest)
        {
            this.hitTest = hitTest;
        }
        public override void VisitRowIndicator(int rowHandle, IndicatorState indicatorState)
        {
            hitTest.AddHitInfo("RowIndicatorState", indicatorState.ToString());
            StopHitTesting();
        }
        public override void VisitTotalSummary(ColumnBase column)
        {
            hitTest.AddTotalSummaryInfo(column);
        }
        public override void VisitFixedTotalSummary(string summaryText)
        {
            hitTest.AddFixedTotalSummaryInfo(summaryText);
        }
        public override void VisitGroupValue(int rowHandle, GridColumnData columnData)
        {
            hitTest.AddGroupValueInfo(columnData);
        }
        public override void VisitGroupSummary(int rowHandle, GridGroupSummaryData summaryData)
        {
            hitTest.AddGroupSummaryInfo(summaryData);
        }
    }
    public class HitTestInfo : DependencyObject
    {
        public string Name { get; private set; }
        public string Text { get; private set; }
        public HitTestInfo(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }
    }
    public class SLNWindDataLoader : NWindDataLoader
    {
        object invoicesNew = new ObservableCollection<Invoices>((IEnumerable<Invoices>)new NWindDataLoader().InvoicesNew);
        public new object InvoicesNew { get { return invoicesNew; } }
    }
}
