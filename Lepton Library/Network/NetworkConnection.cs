using Microsoft.Toolkit.Uwp.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Network
{
    public class NetworkConnection
    {
        public static bool IsInternetAvailable()
        {
            // Detect if Internet can be reached
            return NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable;
        }

        public static bool IsInternetOnMeteredConnection()
        {
            // Detect if the connection is metered
            return NetworkHelper.Instance.ConnectionInformation.IsInternetOnMeteredConnection;
        }         

        public static ConnectionType InternetConnectionType()
        {
            return NetworkHelper.Instance.ConnectionInformation.ConnectionType;
            // Example:
            // switch (NetworkHelper.Instance.ConnectionInformation.ConnectionType)
            // {
            //     case ConnectionType.Ethernet:
            //         // Ethernet
            //         break;
            //     case ConnectionType.WiFi:
            //         // WiFi
            //         break;
            //     case ConnectionType.Data:
            //         // Data
            //         break;
            //     case ConnectionType.Unknown:
            //         // Unknown
            //         break;
            // }
        }
    }
}
