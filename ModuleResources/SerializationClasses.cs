using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;

namespace GridDemo
{
    public abstract class LayoutSampleBase
    {
        readonly string description;
        public LayoutSampleBase(string description)
        {
            this.description = description;
        }
        public abstract Stream GetStream();
        public override string ToString()
        {
            return description;
        }
    }
    public class ResourceLayoutSample : LayoutSampleBase
    {
        readonly string resourcePath;
        public ResourceLayoutSample(string description, string resourcePath)
            : base(description)
        {
            this.resourcePath = resourcePath;
        }
        public override Stream GetStream()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        }
    }
    public class MemoryStreamLayoutSample : LayoutSampleBase
    {
        readonly MemoryStream stream;
        public MemoryStreamLayoutSample(string description, MemoryStream stream)
            : base(description)
        {
            this.stream = stream;
        }
        public override Stream GetStream()
        {
            return stream;
        }
    }
}
