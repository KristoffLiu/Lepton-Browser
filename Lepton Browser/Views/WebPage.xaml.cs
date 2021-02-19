using Lepton_Browser.Models;
using Lepton_Browser.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Lepton_Browser.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class WebPage : Page
    {
        public WebPageViewModel ViewModel;
        public WebPage()
        {
            this.InitializeComponent();
            ViewModel = new WebPageViewModel(this);
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if ( ((TabPageFrameItemViewModel)e.Parameter).Title is string )//&& !string.IsNullOrWhiteSpace(((TabPageFrameItemViewModel)e.Parameter).Title)
            {
                var info = ((TabPageFrameItemViewModel)e.Parameter);
                ViewModel.Windows_ID = info.Window_ID;
                ViewModel.ID = info.ID;
                ViewModel.SetSource(new Uri(info.Uri));
            }
            else
            {
                //
            }
            base.OnNavigatedTo(e);
        }
    }
}
