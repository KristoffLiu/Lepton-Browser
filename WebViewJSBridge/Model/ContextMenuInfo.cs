using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebViewJSBridge.Model
{
    public sealed class ContextMenuInfo
    {
        public int WebViewID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public String NodeName { get; set; }
        public String SubType { get; set; }
        public String Href { get; set; }
        public bool Selection { get; set; }
    }
}
