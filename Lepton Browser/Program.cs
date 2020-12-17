using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace Lepton_Browser
{
    public static class Program
    {
        //private static IList<AppInstance> instances;

        static InstanceManager InstanceManager;

        // For demo purposes, we're tracking live instances with a simple integer count.


        #region Main
        static void Main(string[] args)
        {
            InstanceManager = new InstanceManager();
        }

        #endregion


    }

}
