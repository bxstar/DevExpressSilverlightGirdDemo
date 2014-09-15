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
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using System.Windows.Markup;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using DevExpress.Xpf.Utils;

using DevExpress.Xpf.Core.WPFCompatibility.Helpers;
using DevExpress.Xpf.Core.WPFCompatibility;

namespace GridDemo
{
    [ContentProperty("Storyboard")]
    public class StoryboardContainer : Control
    {
        public static Storyboard CreateStoryboard(Control resourceHolder, string resourceName)
        {
            ContentControl c = new ContentControl() { Template = (ControlTemplate)resourceHolder.Resources[resourceName] };
            c.ApplyTemplate();
            StoryboardContainer container = (StoryboardContainer)VisualTreeHelper.GetChild(c, 0);
            return container.Storyboard;
        }
        public Storyboard Storyboard { get; set; }
    }
}
