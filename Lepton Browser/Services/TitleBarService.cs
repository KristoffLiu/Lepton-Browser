using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace Lepton_Browser.Models
{
    public class TitleBarService
    {
        static ApplicationView view; //来进行以下一系列设置标题栏组件颜色的操作
        public static void SetTransparentColor()
        {
            view = ApplicationView.GetForCurrentView();
            view.TitleBar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            view.TitleBar.ButtonForegroundColor = Colors.White;
            view.TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(38, 0, 0, 0);
            view.TitleBar.ButtonHoverForegroundColor = Colors.White;
            view.TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(70, 0, 0, 0);
            view.TitleBar.ButtonPressedForegroundColor = Colors.White;
            view.TitleBar.ButtonInactiveBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            view.TitleBar.ButtonInactiveForegroundColor = Colors.Gray;
        }
        public static void SetContrastColor()
         {
            view = ApplicationView.GetForCurrentView();
            if(PersonalizationService.IsNightMode == true)
            {
                view.TitleBar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
                view.TitleBar.ButtonForegroundColor = Colors.White;
                view.TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(38, 0, 0, 0);
                view.TitleBar.ButtonHoverForegroundColor = Colors.White;
                view.TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(70, 0, 0, 0);
                view.TitleBar.ButtonPressedForegroundColor = Colors.White;
                view.TitleBar.ButtonInactiveBackgroundColor = Color.FromArgb(0, 0, 0, 0);
                view.TitleBar.ButtonInactiveForegroundColor = Colors.Gray;
            }
            else
            {
                view.TitleBar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
                view.TitleBar.ButtonForegroundColor = Colors.Black;
                view.TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(38, 0, 0, 0);
                view.TitleBar.ButtonHoverForegroundColor = Colors.Black;
                view.TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(70, 0, 0, 0);
                view.TitleBar.ButtonPressedForegroundColor = Colors.Black;
                view.TitleBar.ButtonInactiveBackgroundColor = Color.FromArgb(0, 0, 0, 0);
                view.TitleBar.ButtonInactiveForegroundColor = Colors.Gray;
            }
        }
    }
}
