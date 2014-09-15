using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Mvvm;

namespace GridDemo
{
    public class GroupSummaryViewModel : BindableBase
    {
        GridSummaryList defaultDisplayModeSummaries;
        public GridSummaryList DefaultDisplayModeSummaries
        {
            get { return defaultDisplayModeSummaries; }
            set
            {
                SetProperty(ref defaultDisplayModeSummaries, value, () => DefaultDisplayModeSummaries);
                UpdateSummarySource();
            }
        }
        GridSummaryList alignByColumnsDisplayModeSummaries;
        public GridSummaryList AlignByColumnsDisplayModeSummaries
        {
            get { return alignByColumnsDisplayModeSummaries; }
            set
            {
                SetProperty(ref alignByColumnsDisplayModeSummaries, value, () => AlignByColumnsDisplayModeSummaries);
                UpdateSummarySource();
            }
        }
        bool alignSummariesByColumns;
        public bool AlignSummariesByColumns
        {
            get { return alignSummariesByColumns; }
            set
            {
                SetProperty(ref alignSummariesByColumns, value, () => AlignSummariesByColumns);
                UpdateSummarySource();
                UpdateCityColumn();
            }
        }
        bool showGroupFooters = true;
        public bool ShowGroupFooters
        {
            get { return showGroupFooters; }
            set
            {
                SetProperty(ref showGroupFooters, value, () => ShowGroupFooters);
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
        GridSummaryList summarySource;
        public GridSummaryList SummarySource
        {
            get { return summarySource; }
            set { SetProperty(ref summarySource, value, () => SummarySource); }
        }
        int cityColumnVisibleIndex;
        public int CityColumnVisibleIndex
        {
            get { return cityColumnVisibleIndex; }
            set { SetProperty(ref cityColumnVisibleIndex, value, () => CityColumnVisibleIndex); }
        }
        int cityColumnGroupIndex;
        public int CityColumnGroupIndex
        {
            get { return cityColumnGroupIndex; }
            set { SetProperty(ref cityColumnGroupIndex, value, () => CityColumnGroupIndex); }
        }
        void UpdateSummarySource()
        {
            SummarySource = AlignSummariesByColumns ? AlignByColumnsDisplayModeSummaries : DefaultDisplayModeSummaries;
        }
        void UpdateCityColumn()
        {
            if (AlignSummariesByColumns)
                CityColumnVisibleIndex = 0;
            CityColumnGroupIndex = 0;
        }
    }
}
