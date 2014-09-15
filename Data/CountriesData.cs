using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using DevExpress.Xpf.DemoBase;
using System.ComponentModel;
using DevExpress.Xpf.DemoBase.Helpers;
using System.Reflection;

namespace GridDemo
{
    [XmlRoot("Countries")]
    public class CountriesData : List<Country>
    {
        static IList dataSource = null;
        public static IList DataSource
        {
            get
            {
                if (DesignerProperties.IsInDesignTool)
                    return null;
                if (dataSource != null)
                    return dataSource;
                XmlSerializer s = new XmlSerializer(typeof(CountriesData));
                Assembly assembly = typeof(MultiView).Assembly;
                dataSource = (IList)s.Deserialize(assembly.GetManifestResourceStream(DemoHelper.GetPath("GridDemo.Data.", assembly) + "Countries.xml"));
                return dataSource;
            }
        }
    }

    public class Country
    {
        public string Name { get; set; }
        public byte[] Flag { get; set; }
    }
}
