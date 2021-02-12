using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebViewJSBridge
{
    public interface IWebViewModel
    {
        void ShowContextMenu(String webViewId, float x, float y, String nodeName, String subType, String href, String image, bool selection);
    }
}
