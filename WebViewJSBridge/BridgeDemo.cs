using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;

namespace WebViewJSBridge
{
    [AllowForWeb]
    public sealed class BridgeDemo
    {
        public void showMessage(string msg)
        {
            new MessageDialog(msg).ShowAsync();
        }
    }
}
