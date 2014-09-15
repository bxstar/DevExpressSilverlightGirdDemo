using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.ServiceModel;

namespace GridDemo
{

    public static class XPOServiceHelper
    {

        public static void SetupXpoLayer()
        {
            EndpointAddress address = new EndpointAddress("http://demos.devexpress.com/Services/WcfXpoSC/XPOService.svc");
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            DataStoreClient dataStore = new DataStoreClient(binding, address);
            XpoDefault.DataLayer = new SimpleDataLayer(dataStore);
            XpoDefault.Session = null;

        }
    }
}
