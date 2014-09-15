using System;
using System.Windows.Controls;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.WPFCompatibility;

namespace GridDemo
{
    public class SendEmailButton : Button
    {
        public SendEmailButton()
        {
            this.SetDefaultStyleKey(typeof(SendEmailButton));
        }
    }
}
