using System.Collections;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Mvvm;

namespace GridDemo
{
    public class GroupSummariesAlignmentViewModel : BindableBase
    {
        public IList SalesByYearReport { get; private set; }
        public IList SalesByMonthReport { get; private set; }
        public string[] ReportTypes { get { return new string[] { "by Year", "by Month" }; } }
        IList actualSalesReport;
        public IList ActualSalesReport
        {
            get { return actualSalesReport; }
            set { SetProperty(ref actualSalesReport, value, () => ActualSalesReport); }
        }
        int reportTypeIndex;
        public int ReportTypeIndex
        {
            get { return reportTypeIndex; }
            set
            {
                SetProperty(ref reportTypeIndex, value, () => ReportTypeIndex);
                UpdateActualSalesReport();
            }
        }
        bool allowCascadeUpdate;
        public bool AllowCascadeUpdate
        {
            get { return allowCascadeUpdate; }
            set
            {
                SetProperty(ref allowCascadeUpdate, value, () => AllowCascadeUpdate);
            }
        }
        public GroupSummariesAlignmentViewModel()
        {
            SalesByYearReport = SalesByYearData.GetSalesByYearData(false);
            SalesByMonthReport = SalesByYearData.GetSalesByYearData(true);
            UpdateActualSalesReport();
            allowCascadeUpdate = true;
        }
        void UpdateActualSalesReport()
        {
            ActualSalesReport = ReportTypeIndex == 0 ? SalesByYearReport : SalesByMonthReport;
        }
    }
}
