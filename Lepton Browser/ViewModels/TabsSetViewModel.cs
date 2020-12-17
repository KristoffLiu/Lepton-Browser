using Lepton_Library.Common;
using Lepton_Browser.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace Lepton_Browser.ViewModels
{
    public class TabsSetViewModel : PageViewModelBase
    {
        public static TabsSetViewModel Current;
        public TabsSetViewModel()
        {
            Current = this;
            //Add(Guid.NewGuid(), "http://www.baidu.com", "title");
        }

        public void InputListView(ListView listView)
        {
            listView.SelectionChanged += ListView_SelectionChanged;
            TaskBarListView = listView;
        }

        public Guid Window_ID;
        public ListView TaskBarListView;
        public GridView TaskBarGridView;
        public ObservableCollection<TaskBarItemViewModel> TaskBarViewItems = new ObservableCollection<TaskBarItemViewModel>();

        public void SetTaskGridView(GridView gridView)
        {
            TaskBarGridView = gridView;
        }



        public string AvastarUri(string _uri)
        {
            var __uri = new Uri(_uri);
            return __uri.Scheme + "://" + __uri.Host.ToString() + "/favicon.ico";
        }




        public void Add(Guid id,string uri, string title)
        {
            var newuri = AvastarUri(uri);
            var item = new TaskBarItemViewModel() { Id = id, Avastar = newuri , Title = title };
            TaskBarViewItems.Add(item);
            TaskBarListView.SelectedItem = item;
        }

        public void Switch(Guid tab_id)
        {
            TaskBarItemViewModel switchitem = null;
            foreach(var item in TaskBarViewItems)
            {
                if(item.Id == tab_id && ((TaskBarItemViewModel)(TaskBarListView.SelectedItem)).Id != tab_id)
                {
                    switchitem = item;
                }
            }
            if (switchitem != null)
            {
                TaskBarListView.SelectedItem = switchitem;
            }
        }

        public void Update(TabPageInfo tabPageInfo)
        {
            foreach (var item in TaskBarViewItems)
            {
                if (item.Id == tabPageInfo.ID)
                {
                    item.Title = tabPageInfo.Title == null ? item.Title : tabPageInfo.Title;
                    item.Avastar = tabPageInfo.Avastar == null ? item.Avastar : AvastarUri(tabPageInfo.Avastar);
                }
            }
        }

        public void Delete(Guid tab_id)
        {
            SwitchBeforeDeletion(tab_id);
            TaskBarItemViewModel deleteitem = null;
            foreach (var item in TaskBarViewItems)
            {
                if (item.Id == tab_id)
                {
                    deleteitem = item;
                }
            }
            if(deleteitem != null)
            TaskBarViewItems.Remove(deleteitem);
        }

        public void SwitchBeforeDeletion(Guid tab_id)
        {
            int deleteditem_index = Position(tab_id);
            int selecteditem_index = Position(tab_id);
            if (selecteditem_index == deleteditem_index)
            {
                if(selecteditem_index == 0)
                {
                    if (TaskBarViewItems.Count >= 2)
                    {
                        TaskBarListView.SelectedIndex = 1;
                    }
                    else
                    {
                                AppManager.Current.AddNewTabPage("新建标签页", "https://www.baidu.com");
                    }
                }
                else
                {
                    TaskBarListView.SelectedIndex = selecteditem_index - 1;
                }

            }
        }

        public int Position(Guid tab_id)
        {
            int index = 0;
            foreach (var item in TaskBarViewItems)
            {
                if (item.Id == tab_id)
                {
                    index = TaskBarViewItems.IndexOf(item);
                }
            }
            return index;
        }

        int _SelectedIndex = -1;
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listview = sender as ListView;
                if (TaskBarListView.SelectedItem != null)
                {
                AppManager.Current.SwitchTabPage(((TaskBarItemViewModel)TaskBarListView.SelectedItem).Id);
                }
        }

        public void AddButton_Click()
        {
            AppManager.Current.AddNewTabPage("新建标签页", "https://www.baidu.com");
        }

        public void FocusDeleteButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var fonticon = sender as FontIcon;
            AppManager.Current.DeleteTabPage(((TaskBarItemViewModel)fonticon.DataContext).Id);
        }

        public void UpdateCaptureScreenShot(Guid tab_id, BitmapSource bitmapImage)
        {
            foreach (var item in TaskBarViewItems)
            {
                if (item.Id == tab_id)
                {
                    item.CapturedImage = bitmapImage;
                }
            }
        }
               
        public static Image ReturnActiveWebview()
        {
            Image result = null;
            Guid active_id;
            active_id = AppManager.Current.Active_Tab_ID;
            foreach(var taskbargridviewitem in ((ItemsWrapGrid)Current.TaskBarGridView.ItemsPanelRoot).Children)
            {
                if ( ((TaskBarItemViewModel)Current.TaskBarGridView.Items[((ItemsWrapGrid)Current.TaskBarGridView.ItemsPanelRoot).Children.IndexOf(taskbargridviewitem)]).Id == active_id)
                {
                    var itemm = taskbargridviewitem as GridViewItem;
                    result = ((Image)((Grid)((Grid)((Grid)itemm.ContentTemplateRoot).Children[1]).Children[0]).Children[0]);
                    break;
                }
            }
            return result;
        }


        Visibility _TaskBarVisibility = Visibility.Visible;
        public Visibility TaskBarVisibility
        {
            get { return _TaskBarVisibility; }
            set { Set(ref _TaskBarVisibility, value); }
        }

        Visibility _TaskViewHeadlineVisibility = Visibility.Collapsed;
        public Visibility TaskViewHeadlineVisibiliity
        {
            get { return _TaskViewHeadlineVisibility; }
            set { Set(ref _TaskViewHeadlineVisibility, value); }
        }


        public static void SwitchTaskView()
        {
            if (Current.TaskBarVisibility == Visibility.Collapsed)
            {
                Current.TaskBarVisibility = Visibility.Visible;
                Current.TaskViewHeadlineVisibiliity = Visibility.Collapsed;
            }
            else
            {
                Current.TaskViewHeadlineVisibiliity = Visibility.Visible;
                Current.TaskBarVisibility = Visibility.Collapsed;
            }
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

        public void TabsAdaptiveGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickeditem = e.ClickedItem as TaskBarItemViewModel;
            AppManager.Current.SwitchTabPage(clickeditem.Id);
            MainFrameViewModel.SwitchTaskView(Window_ID);
        }
    }

    public class TaskBarItemViewModel : ViewModelBase
    {
        string _Avastar;
        string _Title;
        BitmapSource _CapturedImage;
        Guid _Id;

        public string Avastar
        {
            get { return _Avastar; }
            set { Set(ref _Avastar, value); }
        }
        public string Title
        {
            get { return _Title; }
            set { Set(ref _Title, value); }
        }
        public BitmapSource CapturedImage
        {
            get { return _CapturedImage; }
            set { Set(ref _CapturedImage, value); }
        }
        public Guid Id
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
