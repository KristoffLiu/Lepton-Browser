using Lepton_Library.Common;
using Lepton_Browser.Models;
using Lepton_Browser.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Lepton_Browser.ViewModels
{
    public class TabPageViewModel : PageViewModelBase
    {
        public static TabPageViewModel Current;
        public TabPageViewModel()
        {
            Current = this;
            webFrameItems.CollectionChanged += webFrameItems_CollectionChanged;
        }

        public void InputGrid(Grid _grid)
        {
            TabPageFrameGrid = _grid;
        }

        public Guid Window_ID;
        public Guid ActiveTabPageItemID;
        public Grid TabPageFrameGrid { get; set; }

        public ObservableCollection<TabPageFrameItemViewModel> webFrameItems = new ObservableCollection<TabPageFrameItemViewModel>();

        public void webFrameItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        public void Add(TabPageInfo tabPageInfo)
        {
            var _viewmodel = new TabPageFrameItemViewModel()
            {
                ID = tabPageInfo.ID,
                Category = tabPageInfo.Category,
                Title = tabPageInfo.Title,
                Avastar = tabPageInfo.Avastar,
                Uri = tabPageInfo.Url
            };
            var newframe = new Frame();
            newframe.Tag = _viewmodel.ID;
            if (_viewmodel.Category == TabPageCategory.FormPage)
            {
                
            }
            else if(_viewmodel.Category == TabPageCategory.WebPage)
            {
                newframe.Navigate(typeof(WebPage), tabPageInfo);
            }
            TabPageFrameGrid.Children.Add(newframe);
            webFrameItems.Add(_viewmodel);
            HideAndShow(_viewmodel.ID);
        }

        public void Switch(Guid tab_id)
        {
            HideAndShow(tab_id);
        }

        public void HideAndShow(Guid tab_id)
        {
            ActiveTabPageItemID = tab_id;
            foreach (var controlitem in TabPageFrameGrid.Children)
            {
                if (((Guid)((Frame)controlitem).Tag) == tab_id)
                {
                    controlitem.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    controlitem.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
        }

        public async void Delete(Guid tab_id)
        {
            foreach (var controlitem in TabPageFrameGrid.Children)
            {
                if (((Guid)((Frame)controlitem).Tag) == tab_id)
                {
                    TabPageFrameGrid.Children.Remove(controlitem);
                     //   await WebView.ClearTemporaryWebDataAsync();
                }
            }
            //HideAndShow(tab_id);
        }

        public void Update()
        {

        }

        public void GoBack()
        {
            ActiveWebPageViewModel(ActiveTabPageItemID).GoBack();
        }

        public void GoForward()
        {
            ActiveWebPageViewModel(ActiveTabPageItemID).GoForward();
        }

        public void Refresh()
        {
            ActiveWebPageViewModel(ActiveTabPageItemID).Refresh();
        }

        public WebPageViewModel ActiveWebPageViewModel(Guid tab_id)
        {
            WebPageViewModel _viewmodel = null;
            foreach (var item in TabPageFrameGrid.Children)
            {
                var viewmodel = (WebPageViewModel)(((WebPage)(((Frame)item).Content)).DataContext);
                if (viewmodel.ID == tab_id)
                {
                    _viewmodel = viewmodel;
                }
            }
            return _viewmodel;
        }
    }

    public class TabPageFrameItemViewModel : ViewModelBase
    {
        Guid _ID;
        TabPageCategory _category;
        string _Title;
        string _Avastar;
        string _uri;

        public Guid ID
        {
            get { return _ID; }
            set { Set(ref _ID, value); }
        }
        public TabPageCategory Category
        {
            get { return _category; }
            set { Set(ref _category, value); }
        }
        public string Title
        {
            get { return _Title; }
            set { Set(ref _Title, value); }
        }
        public string Avastar
        {
            get { return _Avastar; }
            set { Set(ref _Avastar, value); }
        }
        public string Uri
        {
            get { return _uri; }
            set { Set(ref _uri, value); }
        }
        public Frame TabPageFrame { get; set; }
    }
}
