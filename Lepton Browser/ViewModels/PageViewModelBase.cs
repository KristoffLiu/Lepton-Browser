using Lepton_Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Lepton_Browser.ViewModels
{
    public class PageViewModelBase : ViewModelBase
    {
        public static List<PageViewModelBase> All = new List<PageViewModelBase>();
        public PageViewModelBase()
        {
            All.Add(this);
            Theme = Models.Personalization.Theme;
        }

        public static void SwitchTheme(ElementTheme elementTheme)
        {
            foreach (var itemtheme in All)
            {
                itemtheme.Theme = elementTheme;
            }
        }

        /* Inherited From ViewModelBase */
        /* It is used by the ViewModel Of a page */
        /* It contains some personalization property for the UI
         * such as the ThemeBackground */
        #region Theme UI
        protected ElementTheme _theme = ElementTheme.Light;
        public virtual ElementTheme Theme
        {
            get { return _theme; }
            set { Set(ref _theme, value); }
        }
        #endregion

        public static void AdjustAcrylicBrushOpacity(double _AcrylicBrushOpacity)
        {
            foreach (var itemtheme in All)
            {
                itemtheme.AcrylicBrushOpacity = _AcrylicBrushOpacity;
            }
        }


        public async static void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                Priority, () =>
                {
                    action();
                });
        }


        /* Inherited From ViewModelBase */
        /* It is used by the ViewModel Of a page */
        /* It contains some personalization property for the UI
         * such as the ThemeBackground */
        #region Theme UI
        string _ThemeBackground = "#FFFFFFFF";
        public string ThemeBackground
        {
            get { return _ThemeBackground; }
            set { Set(ref _ThemeBackground, value); }
        }

        double _AcrylicBrushOpacity = 0.3;
        public double AcrylicBrushOpacity
        {
            get { return _AcrylicBrushOpacity; }
            set { Set(ref _AcrylicBrushOpacity, value); }
        }
        #endregion


    }
}
