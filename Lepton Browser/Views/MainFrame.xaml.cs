using Lepton_Browser.Models;
using Lepton_Browser.Services;
using Lepton_Browser.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
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
    public sealed partial class MainFrame : Page
    {
        public MainFrameViewModel ViewModel;
        public static MainFrame Current;

        public MainFrame()
        {
            Current = this;
            ViewModel = new MainFrameViewModel(this);
            this.InitializeComponent();
            Models.TitleBarService.SetContrastColor();
            DataContext = ViewModel;
            ViewModel.InputUserControl(taskview, MainFrame_Grid);
            #region 标题栏设置 Title Bar Setting
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true; //扩展标题栏
            Window.Current.SetTitleBar(TitleBar); //设置某个容器空间（我一般就用的Grid）为标题栏，即可拖拽来位移窗体
            //注意：该控件必须要有颜色，哪怕是Transparent透明也行。如果不设置background，它就是空的没法进行拖拽。
            #endregion
        }

        public void TaskView()
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TabsService.Current.Add("百度", "https://www.baidu.com");
        }
    }
}
