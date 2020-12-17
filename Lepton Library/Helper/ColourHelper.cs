using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Lepton_Library.Helper
{
    public class ColourHelper
    {
        #region [Color：16 Hex To RGB]
        /// <summary>
        /// [Color：16 Hex To RGB]
        /// </summary>
        /// <param name="strColor">[Color：16 Hex To RGB] [Return RGB]</param>
        /// <returns></returns>
        public static SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            return new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
        }
        #endregion
    }
}
