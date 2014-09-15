using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Mvvm;

namespace GridDemo
{
    public class MasterDetailViewModel : BindableBase
    {

        public EmployeesWithDetails Employees { get; private set; }

        public MasterDetailViewModel()
        {
            Employees = new EmployeesWithDetails();
            ShowOrdersSummaries = true;
            ShowOrdersColumnHeaders = true;
            ShowCustomersColumnHeaders = true;
            ShowCustomersSummaries = true;
            ShowInvoicesColumnHeaders = true;
            ShowInvoicesSummaries = true;
            ShowDetailButtons = true;
        }
        bool showOrdersColumnHeadersCore;
        public bool ShowOrdersColumnHeaders
        {
            get { return showOrdersColumnHeadersCore; }
            set { SetProperty(ref showOrdersColumnHeadersCore, value, () => ShowOrdersColumnHeaders); }
        }
        bool showOrdersSummariesCore;
        public bool ShowOrdersSummaries
        {
            get { return showOrdersSummariesCore; }
            set { SetProperty(ref showOrdersSummariesCore, value, () => ShowOrdersSummaries); }
        }

        bool showCustomersColumnHeadersCore;
        public bool ShowCustomersColumnHeaders
        {
            get { return showCustomersColumnHeadersCore; }
            set { SetProperty(ref showCustomersColumnHeadersCore, value, () => ShowCustomersColumnHeaders); }
        }
        bool showCustomersSummariesCore;
        public bool ShowCustomersSummaries
        {
            get { return showCustomersSummariesCore; }
            set { SetProperty(ref showCustomersSummariesCore, value, () => ShowCustomersSummaries); }
        }

        bool showInvoicesColumnHeadersCore;
        public bool ShowInvoicesColumnHeaders
        {
            get { return showInvoicesColumnHeadersCore; }
            set { SetProperty(ref showInvoicesColumnHeadersCore, value, () => ShowInvoicesColumnHeaders); }
        }
        bool showInvoicesSummariesCore;
        public bool ShowInvoicesSummaries
        {
            get { return showInvoicesSummariesCore; }
            set { SetProperty(ref showInvoicesSummariesCore, value, () => ShowInvoicesSummaries); }
        }

        bool showDetailButtonsCore;
        public bool ShowDetailButtons
        {
            get { return showDetailButtonsCore; }
            set { SetProperty(ref showDetailButtonsCore, value, () => ShowDetailButtons); }
        }
    }
}
