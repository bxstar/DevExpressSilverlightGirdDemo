using System;
using System.Windows;
using DevExpress.DemoData;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.DemoData.Helpers;

namespace GridDemo
{
    public class App : Application
    {
        public App()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-Hans");

            StartupBase.Run<Startup>(this);
        }
    }
    public class Startup : DemoStartup
    {
        public static void InitDemo()
        {
#if !EXTMAP
            Loader.DemoDataAssembly = typeof(DevExpress.DemoData.AssemblyMarker).Assembly;
#endif
        }
        protected override bool GetDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
        protected override Type GetFixtureTypeForXBAPOrSLTesting()
        {
            return null;
        }
    }
}
