using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Browser.Models
{
    public class TabModel
    {
        string _avastar;
        public TabModel()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }
        public Guid Windows_ID { get; set; }
        public TabPageCategory Category { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Avastar { 
            get { return _avastar; } 
            set { _avastar = AvastarUri(value); } 
        }

        public string AvastarUri(string _uri)
        {
            var __uri = new Uri(_uri);
            return __uri.Scheme + "://" + __uri.Host.ToString() + "/favicon.ico";
        }
    }

    public enum TabPageCategory
    {
        WebPage = 0,
        FormPage = 1,
    }
}
