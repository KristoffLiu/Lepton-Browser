using Lepton_Library.Common;
using Lepton_Browser.Models;
using Lepton_Browser.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Lepton_Browser.Services;

namespace Lepton_Browser.ViewModels
{
    public class ManipulationBarViewModel : PageViewModelBase
    {
        public static ManipulationBarViewModel Current;
        public ManipulationBar View;
        public ManipulationBarViewModel(ManipulationBar manipulationBar)
        {
            Current = this;
            View = manipulationBar;
        }
        public Guid ID;

        bool _IsBackwardEnabled = false;
        bool _IsForwardEnabled = false;
        bool _IsRefreshEnabled = false;

        public bool IsBackwardEnabled
        {
            get { return _IsBackwardEnabled; }
            set { Set(ref _IsBackwardEnabled, value); }
        }

        public void BackwardButtonHandler()
        {
            TabPageViewModel.Current.GoBack();
        }

        public bool IsForwardEnabled
        {
            get { return _IsForwardEnabled; }
            set { Set(ref _IsForwardEnabled, value); }
        }
        public void ForwardButtonHandler()
        {
            TabPageViewModel.Current.GoForward();
        }

        public bool IsRefreshEnabled
        {
            get { return _IsRefreshEnabled; }
            set { Set(ref _IsRefreshEnabled, value); }
        }

        public void RefreshButtonHandler()
        {
            TabPageViewModel.Current.Refresh();
        }

        string _InteliSenseBoxText = "";
        public string InteliSenseBoxText
        {
            get { return _InteliSenseBoxText; }
            set { Set(ref _InteliSenseBoxText, value); }
        }

        int _TabNum = 0;
        public int TabNum
        {
            get { return _TabNum; }
            set { Set(ref _TabNum, value); }
        }
        public void DownloadButton_Click()
        {

        }

        public void GlobalTimeLineButton_Click()
        {
            MainFrameViewModel.OpenTimeLine();
        }

        public void TabsGridButton_Click()
        {

        }
        public void ExpandMoreAppBarButton_Click()
        {
            MainFrame.Current.ViewModel.IsSideBarPaneOpen = true;
            SideBarViewModel.Current.Update(TabsService.Current.Active_Tab_ID);
        }

        public void Update(TabPageInfoExpansion tabPageInfoExpansion)
        {
            IsBackwardEnabled = tabPageInfoExpansion.IsBackwardEnabled;
            IsForwardEnabled = tabPageInfoExpansion.IsForwardEnabled;
            IsRefreshEnabled = tabPageInfoExpansion.IsRefreshEnabled;
            InteliSenseBoxText = tabPageInfoExpansion.Url;
        }

        public void Navigate(string newsource)
        {
            WebPageViewModel.ReturnActiveWebPageViewModel().SetSource(new Uri(newsource));
        }

        public void IndexInteliSenseBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                Navigate(((TextBox)sender).Text);
            }
        }
    }

    public class TabPageInfoExpansion
    {
        public bool IsBackwardEnabled { get; set; } = false;
        public bool IsForwardEnabled { get; set; } = false;
        public bool IsRefreshEnabled { get; set; } = false;
        public bool IsLoading { get; set; } = false;
        public string Url { get; set; } = "";
    }
}
