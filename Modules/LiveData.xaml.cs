using System;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;

namespace GridDemo
{
    [CodeFile("ModuleResources/LiveDataClasses.(cs)")]
    [CodeFile("Controls/Converters.(cs)")]
    [CodeFile("ModuleResources/LiveDataTemplates(.SL).xaml")]
    public partial class LiveData : GridDemoModule
    {
        ProcessGenerator generator;
        public LiveData()
        {
            InitializeComponent();
            generator = new ProcessGenerator(this);
            generator.Initialize();
            grid.AllowLiveDataShaping = true;
            grid.ItemsSource = generator.Processes;
            grid.DataContext = generator;
            updateModeList.SelectedIndex = 0;
            chkAllowUpdating.IsChecked = true;
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            generator.Start();
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            generator.Stop();
        }
        private void maxCountSlider_ValueChanged(object sender, EventArgs e)
        {
            if (generator != null)
                generator.ProcessMinCount = Math.Max(5, generator.ProcessMaxCount - 15);
        }

        private void grid_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            if (e.Column != null && e.Column.FieldName == "AnimationElement")
            {
                e.Value = generator.GetAnimationElement(generator.Processes[e.ListSourceRowIndex]);
            }
        }
        protected override void Clear()
        {
            base.Clear();
            generator.Stop();
        }
        private void updateModeList_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            generator.UpdateMode = (ProcessGenerator.ProcessUpdateMode)updateModeList.SelectedIndex;
        }
    }
}
