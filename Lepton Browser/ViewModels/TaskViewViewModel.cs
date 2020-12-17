using Lepton_Library.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Lepton_Browser.ViewModels
{
    public class TaskViewViewModel : PageViewModelBase
    {
        int _TabAdaptiveGridViewIndex = 0;
        ObservableCollection<TaskItem> _TabAdaptiveGridModels = new ObservableCollection<TaskItem>();

        public int TabAdaptiveGridViewIndex
        {
            get { return _TabAdaptiveGridViewIndex; }
            set { Set(ref _TabAdaptiveGridViewIndex, value); }
        }

        public ObservableCollection<TaskItem> TabAdaptiveGridModels
        {
            get { return _TabAdaptiveGridModels; }
            set { Set(ref _TabAdaptiveGridModels, value); }
        }

        public void TabsAdaptiveGridView_Loaded()
        {

        }

        public void TabsAdaptiveGridView_DragItemsStarting()
        {

        }

        public void TabsAdaptiveGridView_DropCompleted()
        {

        }

        public void TabsAdaptiveGridView_ItemClick()
        {

        }




    }
    public class TaskItem : ViewModelBase
    {
        BitmapImage _Avastar;
        string _TaskTitle;
        string _Foreground;
        string _Background;
        BitmapImage _CapturedImage;
        string _Id;

        public BitmapImage Avastar
        {
            get { return _Avastar; }
            set { Set(ref _Avastar, value); }
        }
        public string TaskTitle
        {
            get { return _TaskTitle; }
            set { Set(ref _TaskTitle, value); }
        }
        public string Foreground
        {
            get { return _Foreground; }
            set { Set(ref _Foreground, value); }
        }
        public string Background
        {
            get { return _Background; }
            set { Set(ref _Background, value); }
        }
        public BitmapImage CapturedImage
        {
            get { return _CapturedImage; }
            set { Set(ref _CapturedImage, value); }
        }
        public string Id
        {
            get { return _Id; }
            set { Set(ref _Id, value); }
        }

        public void Grid_PointerPressed()
        {

        }

        public void TabAdaptiveGridViewCloseButton_Click()
        {

        }

        public void AdaptiveGridView_PointerEntered()
        {

        }

        public void AdaptiveGridView_PointerExited()
        {

        }

    }

}
