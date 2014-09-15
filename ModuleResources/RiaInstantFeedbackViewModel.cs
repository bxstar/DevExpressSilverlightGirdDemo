using DevExpress.Xpf.DemoBase.Helpers;

namespace GridDemo
{
    public class RIAInstantFeedbackViewModel : DemoViewModelBase
    {
        bool isUseExtendedDataQuery;

        public bool IsUseExtendedDataQuery
        {
            get { return this.isUseExtendedDataQuery; }
            set { ChangeProperty(ref this.isUseExtendedDataQuery, value, "IsUseExtendedDataQuery"); }
        }

        public RIAInstantFeedbackViewModel()
        {
            IsUseExtendedDataQuery = true;
        }
    }
}
