using System;
using System.Windows;
using DevExpress.Xpf.Grid;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System.Globalization;
using System.Linq;
using DevExpress.Xpf.Core.WPFCompatibility;
using DevExpress.Data.Browsing;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.NWind;
using System.Collections.Generic;

namespace GridDemo
{
    [CodeFile("Controls/ControlStyles/NameTextControl(.SL).xaml")]
    [CodeFile("ModuleResources/HitTestTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/HitTestClasses.(cs)")]
    public partial class HitTest : GridDemoModule
    {
        DevExpress.Xpf.Grid.TableView TableView { get { return (DevExpress.Xpf.Grid.TableView)grid.View; } }
        ObservableCollection<HitTestInfo> hitInfoList = new ObservableCollection<HitTestInfo>();
        Point startPosition;


        public bool AllowShowHitInfo
        {
            get { return (bool)GetValue(AllowShowHitInfoProperty); }
            set { SetValue(AllowShowHitInfoProperty, value); }
        }
        public override bool AllowRtl { get { return false; } }

        public static readonly DependencyProperty AllowShowHitInfoProperty =
            DependencyProperty.Register("AllowShowHitInfo", typeof(bool), typeof(HitTest), new UIPropertyMetadata(true));


        public HitTest()
        {
            InitializeComponent();

            grid.Loaded += grid_Loaded;
            grid.MouseEnter += grid_MouseEnterMouseLeave;
            grid.MouseLeave += grid_MouseEnterMouseLeave;
            DependencyPropertyChangeListener isCheckedListener = new DependencyPropertyChangeListener("IsChecked", showHitInfoCheckEdit, DependencyPropertyChangeHandler);
            DependencyPropertyChangeListener allowShowHitInfoListener = new DependencyPropertyChangeListener("AllowShowHitInfo", this, DependencyPropertyChangeHandler);


            hitIfoItemsControl.ItemsSource = hitInfoList;

        }
        void grid_Loaded(object sender, RoutedEventArgs e)
        {
            SetPopupIsOpen();
        }
        void grid_MouseEnterMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SetPopupIsOpen();
        }
        void DependencyPropertyChangeHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetPopupIsOpen();
        }
        void viewsListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
        }

        private void grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point location = e.GetPosition(grid);
            Rect gridRect = DevExpress.Xpf.Core.Native.LayoutHelper.GetRelativeElementRect(grid, Application.Current.RootVisual);
            PointHelper.Offset(ref location, gridRect.X, gridRect.Y);
            double hOffset = location.X - startPosition.X;
            if (FlowDirection == System.Windows.FlowDirection.RightToLeft)
                hOffset = -hOffset;

            hitInfoPopup.HorizontalOffset = hOffset;
            hitInfoPopup.VerticalOffset = location.Y - startPosition.Y;

            GridViewHitInfoBase info = GetHitInfo(e);

            hitInfoList.Clear();

            AddHitInfo("HitTest", TypeDescriptor.GetProperties(info)["HitTest"].GetValue(info).ToString());

            AddHitInfo("Column", info.Column != null ? info.Column.HeaderCaption as string : "No column");
            AddHitInfo("RowHandle", GetRowHandleDescription(info.RowHandle));
            AddHitInfo("CellValue", info.Column != null ? grid.GetCellDisplayText(info.RowHandle, info.Column) : null);
            info.Accept(CreateDemoHitTestVisitor());
        }
        GridViewHitTestVisitorBase CreateDemoHitTestVisitor()
        {
            return new DemoTableViewHitTestVisitor(this);
        }
        GridViewHitInfoBase GetHitInfo(RoutedEventArgs e)
        {
            if (grid.View is DevExpress.Xpf.Grid.TableView)
                return (GridViewHitInfoBase)TableView.CalcHitInfo(e.OriginalSource as DependencyObject);
            return null;

        }
        string GetRowHandleDescription(int rowHanle)
        {
            if (rowHanle == GridControl.InvalidRowHandle)
                return "No row";
            if (rowHanle == GridControl.NewItemRowHandle)
                return "New Item Row";
            if (rowHanle == GridControl.AutoFilterRowHandle)
                return "Auto Filter Row";
            return string.Format("{0} ({1})", rowHanle, grid.IsGroupRowHandle(rowHanle) ? "group row" : "data row");
        }
        internal void AddHitInfo(string name, string text)
        {
            hitInfoList.Add(new HitTestInfo(name, text));
        }
        internal void RemoveHitInfo(string name)
        {
            HitTestInfo infoToRemove = hitInfoList.Where(info => info.Name == name).FirstOrDefault();
            if (infoToRemove != null)
                hitInfoList.Remove(infoToRemove);
        }
        internal void AddTotalSummaryInfo(ColumnBase column)
        {
            AddHitInfo("TotalSummary", column.TotalSummaryText);
        }
        internal void AddFixedTotalSummaryInfo(string summaryText)
        {
            RemoveHitInfo("CellValue");
            AddHitInfo("FixedTotalSummary", summaryText);
        }
        internal void AddGroupValueInfo(GridColumnData columnData)
        {
            AddHitInfo("GroupValue", string.Format("{0}: {1}", columnData.Column.FieldName, columnData.Value));
        }
        internal void AddGroupSummaryInfo(GridGroupSummaryData summaryData)
        {
            AddHitInfo("GroupSummary", summaryData.Text);
        }
        void hitInfoPopup_Opened(object sender, EventArgs e)
        {
            startPosition = new Point(-10, -10);
        }

        void SetPopupIsOpen()
        {
            hitInfoPopup.IsOpen = grid.IsMouseOver && showHitInfoCheckEdit.IsChecked.Value && AllowShowHitInfo;
        }
    }
}
