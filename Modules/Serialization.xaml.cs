using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Reflection;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;

namespace GridDemo
{
    [CodeFile("ModuleResources/SerializationClasses.(cs)")]
    public partial class Serialization : GridDemoModule
    {
        MemoryStream currentLayoutStream;
        public Serialization()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Serialization_Loaded);
        }
        void Serialization_Loaded(object sender, RoutedEventArgs e)
        {
            List<LayoutSampleBase> layoutSamples = new List<LayoutSampleBase>();
            layoutSamples.Add(new MemoryStreamLayoutSample("Original", SaveLayout()));
            Assembly assembly = typeof(Serialization).Assembly;
            layoutSamples.Add(new ResourceLayoutSample("Brief view", DemoHelper.GetPath("GridDemo.Data.LayoutSamples.", assembly) + "BriefView.xml"));
            layoutSamples.Add(new ResourceLayoutSample("Full view", DemoHelper.GetPath("GridDemo.Data.LayoutSamples.", assembly) + "FullView.xml"));
            layoutSamples.Add(new ResourceLayoutSample("Banded layout", DemoHelper.GetPath("GridDemo.Data.LayoutSamples.", assembly) + "BandedLayout.xml"));
            layoutSamplesComboBox.ItemsSource = layoutSamples;
            layoutSamplesComboBox.SelectedIndex = 0;
        }

        MemoryStream SaveLayout()
        {
            MemoryStream stream = new MemoryStream();
            grid.SaveLayoutToStream(stream);
            return stream;
        }
        void RestoreLayout(Stream stream)
        {
            if (stream == null)
                return;
            stream.Position = 0;
            grid.RestoreLayoutFromStream(stream);
        }
        private void saveLayoutButton_Click(object sender, RoutedEventArgs e)
        {
            currentLayoutStream = SaveLayout();
            restoreLayoutButton.IsEnabled = true;
        }

        private void restoreLayoutButton_Click(object sender, RoutedEventArgs e)
        {
            RestoreLayout(currentLayoutStream);
        }
        private void loadSampleLayoutButton_Click(object sender, RoutedEventArgs e)
        {
            RestoreLayout(((LayoutSampleBase)layoutSamplesComboBox.SelectedItem).GetStream());
        }
    }
}
