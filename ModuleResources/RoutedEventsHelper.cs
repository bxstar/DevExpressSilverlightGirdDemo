using System;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Utils;
using System.Windows.Markup;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core.WPFCompatibility;
using PropertyMetadata = DevExpress.Xpf.Core.WPFCompatibility.SLPropertyMetadata;
using RoutedEvent = DevExpress.Xpf.Core.WPFCompatibility.SLRoutedEvent;
using RoutedEventArgs = DevExpress.Xpf.Core.WPFCompatibility.SLRoutedEventArgs;

namespace GridDemo
{
    public class RoutedEventCommandHelper : Behavior<FrameworkElement>
    {
        delegate void RoutedEventHandler(object sender, SLRoutedEventArgs e);
        public static readonly DependencyProperty CommandProperty;
        static RoutedEventCommandHelper()
        {
            CommandProperty = DependencyPropertyManager.Register("Command", typeof(ICommand), typeof(RoutedEventCommandHelper), new PropertyMetadata(null));
        }
        readonly RoutedEventHandler handler;
        private RoutedEvent routedEvent;
        public RoutedEventCommandHelper()
        {
            this.handler = new RoutedEventHandler(OnRaiseEvent);
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            if (Event == null) return;
            UpdateSubsrcribtion();
        }
        private void UpdateSubsrcribtion()
        {
            AssociatedObject.RemoveHandler(Event, handler);
            AssociatedObject.AddHandler(Event, this.handler);
        }
        private void OnRaiseEvent(object sender, RoutedEventArgs e)
        {
            if (Command == null) return;
            Command.Execute(new RoutedEventHandlerArgs(sender, e));
        }
        public RoutedEvent Event
        {
            get { return this.routedEvent; }
            set
            {
                this.routedEvent = value;
                if (AssociatedObject == null) return;
                UpdateSubsrcribtion();
            }
        }
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
    public class RoutedEventHandlerArgs
    {
        public object Sender { get; private set; }
        public RoutedEventArgs Args { get; private set; }
        public RoutedEventHandlerArgs(object sender, RoutedEventArgs e)
        {
            Sender = sender;
            Args = e;
        }
    }
    public class EditValueChangedEventExtension : MarkupExtension
    {
        public EditValueChangedEventExtension() { }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return BaseEdit.EditValueChangedEvent;
        }
    }
}
