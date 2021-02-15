using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Lepton_Browser
{
    public class InstanceManager
    {
        public static int InstanceNumber { get; set; }

        public static IList<AppInstance> Instances;

        public InstanceManager()
        {
            ID = new Guid();
            CreateNewInstance();
            
        }

        public Guid ID;
        public AppInstance Instance;

        public void CreateNewInstance()
        {
            Instance = AppInstance.FindOrRegisterInstanceForKey(ID.ToString());
            global::Windows.UI.Xaml.Application.Start((p) => new App());
        }

        public static void RedirectActivationTo()
        {

        }

        private static void UpdateSharedInstanceNumber()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            object data = localSettings.Values["InstanceCount"];
            if (data == null)
            {
                localSettings.Values["InstanceCount"] = 0;
            }
            if (Instances.Count == 0)
            {
                // Write data to settings, using this app instance's ID.
                // If there are no other instances, we reset the InstanceCount.
                localSettings.Values["InstanceCount"] = 1;
                InstanceNumber = 1;
            }
            else
            {
                // Read the settings data, and increment it.
                InstanceNumber = (int)localSettings.Values["InstanceCount"] + 1;
                localSettings.Values["InstanceCount"] = InstanceNumber;
            }
        }
    }

}
