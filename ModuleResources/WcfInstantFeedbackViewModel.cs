using System;
using DevExpress.Xpf.DemoBase.Helpers;
using System.Windows.Input;
using DevExpress.Xpf.Core.Commands;
using System.Diagnostics;
using DevExpress.Mvvm;

namespace GridDemo
{
    public class WCFInstantFeedbackViewModel : BindableBase
    {
        bool isUseExtendedDataQuery;
        WcfSCService.SCEntities wcfSCService;

        public bool IsUseExtendedDataQuery
        {
            get { return isUseExtendedDataQuery; }
            set { SetProperty(ref isUseExtendedDataQuery, value, () => IsUseExtendedDataQuery); }
        }
        public WcfSCService.SCEntities WcfSCService
        {
            get { return wcfSCService; }
            set { SetProperty(ref wcfSCService, value, () => WcfSCService); }
        }
        public WCFInstantFeedbackViewModel()
        {
            IsUseExtendedDataQuery = true;
            WcfSCService = new WcfSCService.SCEntities(new Uri("http://demos.devexpress.com/Services/WcfLinqSC/WcfSCService.svc/"));
        }
    }
}
