using Lepton_Browser.ViewModels;
using Lepton_Library.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace Lepton_Browser.Models
{
    public class PersonalizationService
    {

        public static bool IsNightMode
        {
            get {
                    switch (Theme)
                    {
                        case ElementTheme.Light:
                            return false;
                        case ElementTheme.Dark:
                            return true;
                        default:
                            return false;
                    }
                }
            set
            {
                if (value)
                {
                    Theme = ElementTheme.Dark;
                    PageViewModelBase.SwitchTheme(ElementTheme.Dark);
                }
                else
                {
                    Theme = ElementTheme.Light;
                    PageViewModelBase.SwitchTheme(ElementTheme.Light);
                }
            }
        }

        public static ElementTheme Theme
        {
            get { return LocalSetting.GetValueOrDefault("ElementTheme", ElementTheme.Light); }
            set { LocalSetting.AddOrUpdateValue("ElementTheme", value); }
        }
        public static double AcrylicBrushOpacity
        {
            get { return LocalSetting.GetValueOrDefault("AcrylicBrushOpacity", 0.6); }
            set { LocalSetting.AddOrUpdateValue("AcrylicBrushOpacity", value);
                PageViewModelBase.AdjustAcrylicBrushOpacity(value);
            }
        }
        
        public static int BackgroundImageIndex
        {
            get { return LocalSetting.GetValueOrDefault("BackgroundImageIndex", 0); }
            set { LocalSetting.AddOrUpdateValue("BackgroundImageIndex", value); }
        }

        public static string BackgroundImageFolderPath()
        {
            switch (BackgroundImageIndex)
            {
                case 0:
                    return "ms-appx:///Assets/Art/Girl_June/";
                case 1:
                    return "ms-appx:///Assets/Art/CrouchingCat_Fanshu/";
                default:
                    return "ms-appx:///Assets/Art/Girl_June/";
            }
        }

        public static int PlaceHolderTextIndexChosen
        {
            get { return LocalSetting.GetValueOrDefault("PlaceHolderTextIndexChosen", 0); }
            set { LocalSetting.AddOrUpdateValue("PlaceHolderTextIndexChosen", value); }
        }

        public static String CustomizedPlaceHolderTextStored
        {
            get { return LocalSetting.GetValueOrDefault("CustomizedPlaceHolderTextStored", ""); }
            set { LocalSetting.AddOrUpdateValue("CustomizedPlaceHolderTextStored", value); }
        }

        public static string PlaceHolderTextPreStored
        {
            get { return ""; }
        }


    }
}
