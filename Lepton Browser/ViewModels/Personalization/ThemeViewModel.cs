using Lepton_Browser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Browser.ViewModels.Personalization
{
    public class ThemeViewModel : PageViewModelBase
    {
        public ThemeViewModel()
        {

        }
        int _CarouselIndex = 0;
        public int CarouselIndex
        {
            get { return _CarouselIndex; }
            set
            {
                Set(ref _CarouselIndex, value);
            }
        }

        int _BackgroundImageViewModelIndex = Models.Personalization.BackgroundImageIndex;
        public int BackgroundImageViewModelIndex
        {
            get { return _BackgroundImageViewModelIndex; }
            set
            {
                Set(ref _BackgroundImageViewModelIndex, value);
                Models.Personalization.BackgroundImageIndex = value;
            }
        }
    }
}
