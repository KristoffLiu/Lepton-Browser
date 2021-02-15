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
    public sealed partial class OverView : Page
    {
        OverViewViewModel ViewModel;
        public OverView()
        {
            this.InitializeComponent();
            ViewModel = new OverViewViewModel(this);
            DataContext = ViewModel;
        }

        public TabGridViewItemViewModel SelectedModel
        {
            get { return (TabGridViewItemViewModel)GetValue(SelectedModelProperty); }
            set { SetValue(SelectedModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedModelProperty =
            DependencyProperty.Register("SelectedModel", typeof(TabGridViewItemViewModel), typeof(OverView), new PropertyMetadata(null));

        private void VerticalThumb_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            
        }

        private void VerticalThumb_PointerReleased(object sender, PointerRoutedEventArgs e)
        {

        }

        private void TimeLineScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (!MainFrameViewModel.Current.isAnimating)
            //{
            //    ViewModel.UpdateLayout();
            //}
        }
    }
}
