using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using DevExpress.Xpo;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("ModuleResources/XPOInstantFeedbackTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/XPOInstantFeedbackClasses.(cs)")]
    [CodeFile("ModuleResources/HyperLinkAttachedBehavior.(cs)")]
    [CodeFile("Controls/Converters.(cs)")]
    public partial class XPOInstantFeedback : GridDemoModule
    {
        XPInstantFeedbackSource instantDS;
        public XPOInstantFeedback()
        {
            XPOServiceHelper.SetupXpoLayer();
            instantDS = new XPInstantFeedbackSource(typeof(Question));
            instantDS.ResolveSession += new EventHandler<ResolveSessionEventArgs>(instantDS_ResolveSession);
            instantDS.DismissSession += new EventHandler<ResolveSessionEventArgs>(instantDS_DismissSession);
            instantDS.DefaultSorting = "CreatedOn desc";
            InitializeComponent();
            grid.ItemsSource = instantDS;
            waitIndicatorList.EditValueChanged += waitIndicatorList_EditValueChanged;
        }
        protected override void Clear()
        {
            base.Clear();
            instantDS.Dispose();
        }
        void instantDS_DismissSession(object sender, ResolveSessionEventArgs e)
        {
            IDisposable session = e.Session as IDisposable;
            if (session != null)
            {
                session.Dispose();
            }
        }
        void instantDS_ResolveSession(object sender, ResolveSessionEventArgs e)
        {
            Session s = new UnitOfWork();
            e.Session = s;
        }
        private void waitIndicatorList_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (e.NewValue.ToString() == "Custom")
            {
                view.WaitIndicatorType = WaitIndicatorType.Panel;
                view.WaitIndicatorStyle = Resources["CustomWaitIndicatorStyle"] as Style;
            }
            else
            {
                view.ClearValue(GridViewBase.WaitIndicatorStyleProperty);
                view.WaitIndicatorType = (WaitIndicatorType)e.NewValue;
            }
        }
    }
}
