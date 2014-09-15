using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Windows.Markup;
using DevExpress.Xpf.Editors.Settings;
using System.Collections;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase;

namespace GridDemo
{
    [CodeFile("Controls/Converters.(cs)")]
    [CodeFile("ModuleResources/InplaceEditorsTemplates(.SL).xaml")]
    public partial class InplaceEditors : GridDemoModule
    {
        public InplaceEditors()
        {
            InitializeComponent();
            ComboBoxEditSettings settings = new ComboBoxEditSettings() { IsTextEditable = false };
            settings.ItemsSource = DevExpress.Data.Mask.EnumHelper.GetValues(typeof(Priority));
            colPriority.EditSettings = settings;
            grid.ItemsSource = OutlookDataGenerator.CreateOutlookDataTable(1000);
            booleanColumnEditorListBox.EditValueChanged += new EditValueChangedEventHandler(booleanColumnEditorListBox_EditValueChanged);
            editorButtonShowModeListBox.EditValueChanged += new EditValueChangedEventHandler(editorButtonShowModeListBox_EditValueChanged);
            alternativeDisplayTemplateCheckBox.IsChecked = true;
            alternativeEditTemplateCheckBox.IsChecked = true;
            editorShowModeCombobox.EditValueChanged += new EditValueChangedEventHandler(editorShowModeCombobox_EditValueChanged);

            ComboBoxEditSettings comboBoxEditSettings = new ComboBoxEditSettings();
            comboBoxEditSettings.ValueMember = "Id";
            comboBoxEditSettings.DisplayMember = "Name";
            comboBoxEditSettings.ItemsSource = (Resources["DemoDataProvider"] as DemoDataProvider).Users;
            comboBoxEditSettings.SetBinding(ComboBoxEditSettings.IsTextEditableProperty, new Binding("IsChecked") { Source = autoCompleteCheckBox, Mode = BindingMode.TwoWay });
            comboBoxEditSettings.SetBinding(ComboBoxEditSettings.AutoCompleteProperty, new Binding("IsChecked") { Source = autoCompleteCheckBox, Mode = BindingMode.TwoWay });
            comboBoxEditSettings.SetBinding(ComboBoxEditSettings.ImmediatePopupProperty, new Binding("IsChecked") { Source = immediatePopupCheckBox, Mode = BindingMode.TwoWay });
            colUserId.EditSettings = comboBoxEditSettings;
        }
        #region options
        void editorShowModeCombobox_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            UpdateEditorShowMode();
        }
        void booleanColumnEditorListBox_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (booleanColumnEditorListBox.SelectedIndex == 0)
            {
                colHasAttachment.EditSettings = null;
            }
            if (booleanColumnEditorListBox.SelectedIndex == 1)
            {
                colHasAttachment.EditSettings = new TextEditSettings();
            }
            if (booleanColumnEditorListBox.SelectedIndex == 2)
            {
                colHasAttachment.EditSettings = new ComboBoxEditSettings() { ItemsSource = new bool[] { true, false } };
            }
        }
        void editorButtonShowModeListBox_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            UpdateEditorButtonShowMode();
        }
        void UpdateEditorButtonShowMode()
        {
            if (editorButtonShowModeListBox.SelectedIndex == 0)
            {
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowOnlyInEditor;
            }
            if (editorButtonShowModeListBox.SelectedIndex == 1)
            {
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowForFocusedCell;
            }
            if (editorButtonShowModeListBox.SelectedIndex == 2)
            {
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowForFocusedRow;
            }
            if (editorButtonShowModeListBox.SelectedIndex == 3)
            {
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowAlways;
            }
        }
        void UpdateEditorShowMode()
        {
            if (editorShowModeCombobox.SelectedIndex == 0)
            {
                grid.View.EditorShowMode = EditorShowMode.MouseDown;
            }
            if (editorShowModeCombobox.SelectedIndex == 1)
            {
                grid.View.EditorShowMode = EditorShowMode.MouseDownFocused;
            }
            if (editorShowModeCombobox.SelectedIndex == 2)
            {
                grid.View.EditorShowMode = EditorShowMode.MouseUp;
            }
            if (editorShowModeCombobox.SelectedIndex == 3)
            {
                grid.View.EditorShowMode = EditorShowMode.MouseUpFocused;
            }
        }
        private void colHoursActive_Validate(object sender, GridCellValidationEventArgs e)
        {
            double value = Convert.ToDouble(e.Value, e.Culture);
            if (value <= 0 || 1000 < value)
            {
                e.SetError("The Hours Active value must be greater than zero and less than or equal to 1000", ErrorType.Default);
            }
        }
        #endregion
        #region colHoursActive options
        private void alternativeDisplayTemplateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            colHoursActive.DisplayTemplate = (ControlTemplate)Resources["alternativeHoursActiveDisplayTemplate"];
        }
        private void alternativeDisplayTemplateCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            colHoursActive.DisplayTemplate = null;
        }
        private void alternativeEditTemplateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            colHoursActive.EditTemplate = (ControlTemplate)Resources["alternativeHoursActiveEditTemplate"];
        }
        private void alternativeEditTemplateCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            colHoursActive.EditTemplate = null;
        }
        #endregion
        #region custom edit template events
        private void TextEditSettings_GetIsActivatingKey(object sender, GetIsActivatingKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Add:
                case Key.Subtract:
                    e.IsActivatingKey = (e.Modifiers == ModifierKeys.None) || (e.Modifiers == ModifierKeys.Control);
                    break;
            }
        }
        private void TextEditSettings_ProcessActivatingKey(object sender, ProcessActivatingKeyEventArgs e)
        {
        }
        #endregion
    }
}
