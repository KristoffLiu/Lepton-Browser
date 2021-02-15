using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Browser.Models
{
    public class TabModel
    {
        public TabModel()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }
        public Guid Windows_ID { get; set; }
        public TabPageCategory Category { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Avastar { get; set; }
    }

    public enum TabPageCategory
    {
        WebPage = 0,
        FormPage = 1,
    }
}
