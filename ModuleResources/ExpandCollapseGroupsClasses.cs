using System.Windows;
using System.Windows.Controls;

namespace GridDemo
{
    public class DXExpanderDecorator : ContentControl
    {
        public static readonly DependencyProperty IsItemVisibleProperty = DependencyProperty.Register("IsItemVisible", typeof(bool), typeof(DXExpanderDecorator),
            new PropertyMetadata(true, new PropertyChangedCallback(OnIsItemVisibleChanged)));
        static void OnIsItemVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DXExpanderDecorator)d).OnIsItemVisibleChangedCore();
        }
        public bool IsItemVisible
        {
            get { return (bool)GetValue(IsItemVisibleProperty); }
            set { SetValue(IsItemVisibleProperty, value); }
        }
        void OnIsItemVisibleChangedCore()
        {
            VisualStateManager.GoToState(this, IsItemVisible ? "VisibleInGroup" : "HiddenInGroup", false);
        }
    }
}
