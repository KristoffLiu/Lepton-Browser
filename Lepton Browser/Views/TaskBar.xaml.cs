using Lepton_Browser.Models;
using Lepton_Browser.Services;
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

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Lepton_Browser.Views
{
    public sealed partial class TaskBar : UserControl
    {
        public TaskBarViewModel ViewModel;
        public static TaskBar Current;

        public TaskBar()
        {
            Current = this;
            DataContext = ViewModel;
            this.InitializeComponent();
            ViewModel = new TaskBarViewModel(this);
        }

        private void FocusDeleteButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.FocusDeleteButton_Tapped(sender, e);
        }

        Grid se;

        private void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            se = sender as Grid;
            ISource.Source = ((TaskBarItemViewModel)se.DataContext).CapturedImage;
            Text.Text = ((TaskBarItemViewModel)se.DataContext).Title;
            ShowMenu(false,se);
        }

        private void ShowMenu(bool isTransient,FrameworkElement fe)
        {
            FlyoutShowOptions myOption = new FlyoutShowOptions();
            myOption.ShowMode = isTransient ? FlyoutShowMode.Transient : FlyoutShowMode.Standard;
            CommandBarFlyout1.ShowAt(fe, myOption);
        }

        private void OnElementClicked(object sender, RoutedEventArgs e)
        {
            
        }

        private void CloseButton_Clicked(object sender, RoutedEventArgs e)
        {
            TabsService.Current.Delete(((TaskBarItemViewModel)se.DataContext).Id);
        }
    }
}
