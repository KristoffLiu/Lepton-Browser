using Lepton_Library.Common;
using Lepton_Browser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Input;
using Lepton_Browser.Views;
using WebViewJSBridge;

namespace Lepton_Browser.ViewModels
{
    public class WebPageViewModel : ViewModelBase
    {
        BridgeDemo _bridge = new BridgeDemo();
        public static List<WebPageViewModel> All = new List<WebPageViewModel>();
        public WebPageViewModel(WebPage webPage)
        {
            All.Add(this);

            WebView = webPage.Webview;
            WebviewFlyout = webPage.WebviewFlyout;

            #region Event Initialization 加载事件
            //WebView = new WebView(WebViewExecutionMode.SeparateThread);
            WebView.FrameContentLoading += webView_FrameContentLoading;
            WebView.FrameNavigationStarting += WebView_FrameNavigationStarting;
            WebView.NavigationStarting += WebView_NavigationStarting;
            //WebView.ContentLoading += webView_ContentLoading;
            WebView.NavigationCompleted += WebView_NavigationCompleted;
            WebView.FrameNavigationCompleted += webView_FrameNavigationCompleted;
            WebView.NewWindowRequested += WebView_NewWindowRequested;
            WebView.ContainsFullScreenElementChanged += WebView_ContainsFullScreenElementChanged;
            WebView.RightTapped += Webview_RightTapped;
            WebView.DOMContentLoaded += Webview_DOMContentLoaded;
            #endregion
        }

        public Guid Windows_ID;
        public Guid ID;

        public WebView WebView;
        public MenuFlyout WebviewFlyout;

        public static WebView ReturnActiveWebview()
        {
            WebView result = null;
            Guid active_id;
            active_id = AppManager.Current.Active_Tab_ID;

            foreach (var item in All)
            {
                if (item.ID == active_id)
                {
                    result = item.WebView;
                }
            }
            return result;
        }

        public static WebPageViewModel ReturnActiveWebPageViewModel()
        {
            WebPageViewModel result = null;
            Guid active_id;
            active_id = AppManager.Current.Active_Tab_ID;

            foreach (var item in All)
            {
                if (item.ID == active_id)
                {
                    result = item;
                }
            }
            return result;
        }



        #region 外部调取方法

        public string WebTitle()
        {
            return WebView.DocumentTitle;
        }
        public Uri Source
        {
            get { return WebView.Source; }
        }
        public void SetSource(Uri uri)
        {
            WebView.Source = uri;
        }
        public async Task<string> DOM()
        {
            return await WebView.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });
        }

        public bool CanGoBack
        {
            get { return WebView.CanGoBack; }
        }

        public bool CanGoForward
        {
            get { return WebView.CanGoForward; }
        }

        public void GoBack()
        {
            WebView.GoBack();
        }

        public void GoForward()
        {
            WebView.GoForward();
        }

        public void Refresh()
        {
            WebView.Refresh();
        }


        /// <summary>
        /// Zoom
        /// </summary>
        /// <param name="value"></param>
        public async void ChangeWebViewZoom(double value)
        {
            await WebView.InvokeScriptAsync("eval", new string[] { "document.getElementsByTagName('body')[0].style.zoom = " + value / 100 + " ;" });
            //MainGridScrollviewer.ChangeView(0, 0, ZoomToFactor(float.Parse(value / 100)));
            //MainPage.CurrentMainPage.TabModels[TabIndex].Zoom = value;
        }

        /// <summary>
        /// NightMode
        /// </summary>
        /// <param name="value"></param>
        public async void ChangeNightModeUI(bool value)
        {
            if (value)
            {
                await WebView.InvokeScriptAsync("eval", new string[] { "document.body.style.backgroundColor = 'black' ;" });
                await WebView.InvokeScriptAsync("eval", new string[] { "document.body.style.color = 'white' ;" });
            }
            else
            {
                await WebView.InvokeScriptAsync("eval", new string[] { "document.body.style.background = 'White' ;" });
                await WebView.InvokeScriptAsync("eval", new string[] { "document.body.style.color = 'Black' ;" });
            }
        }


        public void UpdateTabInfo()
        {

        }

        #endregion

        #region 页面加载逻辑

        
        private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            //OURBRIDGEOBJ这个是我们的对象插入到页面之后对象的变量名，这是一个全局变量，也就是window.OURBRIDGEOBJ
            this.WebView.AddWebAllowedObject("BridgeObject", _bridge);
            IsLoading = true;
            var info = new TabPageInfo();
            info.ID = ID;
            info.Title = "正在加载";
            info.Avastar = WebView.Source.ToString();
            AppManager.Current.UpdateTabPage(info);
            Update();
        }

        public async Task testAsync(WebView sender)
        {

        }
        private void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            IsLoading = false;
            var info = new TabPageInfo();
            info.ID = ID;
            info.Title = WebView.DocumentTitle;
            info.Avastar = WebView.Source.ToString();
            AppManager.Current.UpdateTabPage(info);
            Update();
            UpdateCaptureScreenShot();
        }

        private async void Webview_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            string js = @"window.onscroll = function(){
                            // 首先判断我们对象是否正确插入
                            if (window.BridgeObject) {
                                //调用的我们消息函数
                                window.BridgeObject.showMessage(""呵呵呵，我是个message"");
                            }
                        }";
            await sender.InvokeScriptAsync("eval", new[] { js });
        }


        //private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        //{
        //    IsLoading = true;
        //    var info = new TabPageInfo();
        //    foreach (var manager in AppManager.All)
        //    {
        //        info.ID = ID;
        //        info.Title = "正在加载";
        //        info.Avastar = WebView.Source.ToString();
        //        manager.UpdateTabPage(info);
        //    }
        //    Update();
        //}
        //private void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        //{
        //    IsLoading = false;
        //    var info = new TabPageInfo();
        //    foreach (var manager in AppManager.All)
        //    {
        //        info.ID = ID;
        //        info.Title = WebView.DocumentTitle;
        //        info.Avastar = WebView.Source.ToString();
        //        manager.UpdateTabPage(info);
        //    }
        //    Update();
        //    UpdateCaptureScreenShot();
        //}

        private async void OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            //WebView_NavigationCompleted
            //OnPropertyChanged(nameof(IsBackEnabled));
            //OnPropertyChanged(nameof(IsForwardEnabled));
            ////string js = @"window.onscroll = function() {var scrollTop = document.documentElement.scrollTop;}";// || document.body.scrollTop
            ////MouseWheelDelta = Convert.ToInt32(await sender.InvokeScriptAsync("eval", new[] { js }));
        }


        #endregion

        #region Web事件

        public bool isCompleted { get; set; } = false;


        public double _Zoom = 100;
        public double Zoom
        {
            get
            {
                return _Zoom;
            }
            set
            {
                Set(ref _Zoom, value);
                ChangeWebViewZoom(value);
            }
        }
        bool _IsNightModeOn = false;
        public bool IsNightModeOn
        {
            get { return _IsNightModeOn; }
            set
            {
                Set(ref _IsNightModeOn, value);
                if (value)
                {
                    //RequestedTheme = ElementTheme.Dark;
                    ChangeNightModeUI(value);
                }
                else
                {
                    //RequestedTheme = ElementTheme.Light;
                    ChangeNightModeUI(value);
                }
            }
        }

        //加载中
        private void webView_Loading(FrameworkElement sender, object args)
        {

        }

        private void WebView_FrameNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {

        }

        private void webView_FrameContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {


        }

        //private void webView_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        //{
        //    LoadIncompleted(sender);
        //    CheckCompletion(sender);
        //}


        private void webView_FrameNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {

        }

        private void webView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //thunderView.UpdateInformation(string.Empty);

        }

        private void webView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
        }

        private void webView_FrameDOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
        }



        private void webView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {

        }
        //
        private void webView_Unloaded(object sender, RoutedEventArgs e)
        {
            //IsLoading = false;
            //TitleBarText = "正在加载";
        }

        //是否正在加载
        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (value)
                {
                    IsShowingFailedMessage = false;
                }
                Set(ref _isLoading, value);
                IsLoadingVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _isLoadingVisibility;

        public Visibility IsLoadingVisibility
        {
            get { return _isLoadingVisibility; }
            set { Set(ref _isLoadingVisibility, value); }
        }

        private bool _isShowingFailedMessage;

        public bool IsShowingFailedMessage
        {
            get
            {
                return _isShowingFailedMessage;
            }

            set
            {
                if (value)
                {
                    IsLoading = false;
                }
                Set(ref _isShowingFailedMessage, value);
                FailedMesageVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _failedMesageVisibility;

        public Visibility FailedMesageVisibility
        {
            get { return _failedMesageVisibility; }
            set { Set(ref _failedMesageVisibility, value); }
        }



        private void OnNavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            // Use `e.WebErrorStatus` to vary the displayed message based on the error reason
            IsShowingFailedMessage = true;

        }

        private void OnRetry(object sender, RoutedEventArgs e)
        {
            //IsShowingFailedMessage = false;
            //IsLoading = true;
            //((WebView)MainGrid.Children[TabIndex]).Refresh();
        }

        //开新窗口
        //private void Open_NewWindow(object sender, WebViewNewWindowRequestedEventArgs e)
        //{
        //    e.Handled = true; //这个一定要
        //    if (e.Uri != null && e.Uri.ToString() != null)
        //    {
        //        //可以添加让当前浏览停止的代码
        //        ((WebView)MainGrid.Children[TabIndex]).Navigate(e.Uri); //主窗口转向新页面
        //                                                                //跳转后需要处理的代码
        //    }
        //}

        // 在尝试用新开窗口打开 uri 时触发的事件
        private void WebView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            // 交由我处理吧（否则的话系统会用浏览器打开）
            args.Handled = true;
            // 需要新开窗口的 uri（本例中此值为 https://www.baidu.com/）
            Uri uri = args.Uri;
            // uri 的 referrer（本例中此值为 https://www.baidu.com/ 并不是 uri 的 referrer，为啥？）
            Uri referrer = args.Referrer;
            //MainPage.CurrentMainPage.WebView_NewWindowRequested(uri);
            //await new MessageDialog(uri.ToString(), "需要新开窗口的 uri").ShowAsync();
        }



        private void WebView_ContainsFullScreenElementChanged(WebView sender, object args)
        {

        }


        private async void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {


        }

        public async Task<BitmapSource> CaptureScreenShot()
        {
            InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
            await WebView.CapturePreviewToStreamAsync(ms);

            // 显示原始截图
            //BitmapImage bitmapImage = new BitmapImage();
            //bitmapImage.SetSource(ms);

            //裁剪
            int longlength = 1000, width = 0, height = 0;
            double srcwidth = WebView.ActualWidth, srcheight = WebView.ActualHeight;
            double factor = srcwidth / srcheight;
            if (factor < 1)
            {
                height = longlength;
                width = (int)(longlength * factor);
            }
            else
            {
                width = longlength;
                height = (int)(longlength / factor);
            }

            BitmapSource thumbnail = await Resize(width, height, ms);
            return thumbnail;
        }

        // 将指定的图片修改为指定的大小，并返回修改后的图片
        public async Task<BitmapSource> Resize(int width, int height, IRandomAccessStream source)
        {
            WriteableBitmap thumbnail = new WriteableBitmap(width, height);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(source);
            BitmapTransform transform = new BitmapTransform();
            transform.ScaledHeight = (uint)height;
            transform.ScaledWidth = (uint)width;
            PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Straight,
                transform,
                ExifOrientationMode.RespectExifOrientation,
                ColorManagementMode.DoNotColorManage);
            pixelData.DetachPixelData().CopyTo(thumbnail.PixelBuffer);

            return thumbnail;
        }

        public async Task UpdateCaptureScreenShot()
        {
            BitmapSource bitmapImage = await CaptureScreenShot();
            TabsSetViewModel.Current.UpdateCaptureScreenShot(ID, bitmapImage);
        }

        public void Update()
        {
            ManipulationBarViewModel.Current.Update(ReturnExpansionInfo());
        }

        public async void ClearCache()
        {
            await WebView.ClearTemporaryWebDataAsync();
        }
        #endregion

        #region 右键
        private void Webview_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            WebviewFlyout.ShowAt(WebView, e.GetPosition(sender as UIElement));
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        public TabPageInfoExpansion ReturnExpansionInfo()
        {
            TabPageInfoExpansion tabPageInfoExpansion = new TabPageInfoExpansion();
            tabPageInfoExpansion.Url = WebView.Source.ToString();
            tabPageInfoExpansion.IsBackwardEnabled = WebView.CanGoBack ? true : false;
            tabPageInfoExpansion.IsForwardEnabled = WebView.CanGoForward ? true : false;
            tabPageInfoExpansion.IsRefreshEnabled = true;
            return tabPageInfoExpansion;
        }

        public void Test()
        {

        }
    }
}
