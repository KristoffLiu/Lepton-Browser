using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WebViewJSBridge
{
    [AllowForWeb]
    public sealed class WebViewBridge
    {
        WebView WebView;

        public void showMessage(string msg)
        {
            new MessageDialog(msg).ShowAsync();
        }

        /// <summary>
        /// Showing the context menu of the content you currently right clicking on.
        /// </summary>
        /// <param name="webViewId"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="nodeName"></param>
        /// <param name="subType"></param>
        /// <param name="href"></param>
        /// <param name="image"></param>
        /// <param name="selection"></param>
        public void showContextMenu(String webViewId, float x, float y,
                                    String nodeName, String subType,
                                    String href, String image, bool selection)
        {
            Guid.Parse(webViewId)
        }
    }
}
