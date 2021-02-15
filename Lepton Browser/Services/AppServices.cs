using Lepton_Browser.ViewModels;
using Lepton_Browser.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Browser.Services
{
    public class AppServices
    {
        public static AppServices Current;
        public static TabsService TabsService;

        public AppServices()
        {
            TabsService = new TabsService();
            Current = this;
            //AddTabPage();
        }
    }
}
