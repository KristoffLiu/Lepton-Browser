using Lepton_Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Lepton_Browser.ViewModels
{
    public class SideBarViewModel : PageViewModelBase
    {
        public static SideBarViewModel Current;
        public SideBarViewModel()
        {
            Current = this;
        }

        public WebPageViewModel CurrentWebPageViewModel;

        string _MenuPageDocumentTitleText = "";
        string _MenuPageUriTextBlock = "";
        BitmapSource _screenShot;

        public string MenuPageDocumentTitleText
        {
            get { return _MenuPageDocumentTitleText; }
            set { Set(ref _MenuPageDocumentTitleText, value); }
        }
        public string MenuPageUriText
        {
            get { return _MenuPageUriTextBlock; }
            set { Set(ref _MenuPageUriTextBlock, value); }
        }

        public BitmapSource ScreenShot
        {
            get { return _screenShot; }
            set { Set(ref _screenShot, value); }
        }


        public async void Update(Guid tab_id)
        {
            CurrentWebPageViewModel = TabPageViewModel.Current.ActiveWebPageViewModel(tab_id);
            MenuPageDocumentTitleText = CurrentWebPageViewModel.WebTitle();
            MenuPageUriText = CurrentWebPageViewModel.Source.ToString();
            ScreenShot = await CurrentWebPageViewModel.CaptureScreenShot();
        }

        
    }
}
