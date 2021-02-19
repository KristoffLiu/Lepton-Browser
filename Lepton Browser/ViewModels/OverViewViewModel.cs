using Lepton_Browser.Models;
using Lepton_Browser.Services;
using Lepton_Browser.Views;
using Lepton_Library.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace Lepton_Browser.ViewModels
{
    public class OverViewViewModel : PageViewModelBase
    {
        public static OverViewViewModel Current;

        public Guid Window_ID;
        public OverView View;
        public ObservableCollection<TabGridViewItemViewModel> TabGridViewItems = new ObservableCollection<TabGridViewItemViewModel>();

        public OverViewViewModel(OverView overView)
        {
            View = overView;
            Current = this;
            //Add(Guid.NewGuid(), "http://www.baidu.com", "title");
        }


        public string AvastarUri(string _uri)
        {
            var __uri = new Uri(_uri);
            return __uri.Scheme + "://" + __uri.Host.ToString() + "/favicon.ico";
        }

        public void Add(TabGridViewItemViewModel viewModel)
        {
            TabGridViewItems.Add(viewModel);
            View.TabsGridView.SelectedItem = viewModel;
            View.SelectedModel = viewModel;
            UpdateLayout();
        }

        public void Select(Guid tabID)
        {
            if(TabGridViewItems.Count >= 1)
            {
                TabGridViewItemViewModel selectedItem = null;
                foreach (var item in TabGridViewItems)
                {
                    if (item.Id == tabID)
                    {
                        selectedItem = item;
                        break;
                    }
                }
                if (selectedItem != null)
                {
                    View.TabsGridView.SelectedItem = selectedItem;
                    View.SelectedModel = selectedItem;
                }
                UpdateLayout();
            }
        }

        public void Update(TabModel tabModel)
        {
            foreach (var item in TabGridViewItems)
            {
                if (item.Id == tabModel.ID)
                {
                    item.Title = tabModel.Title == null ? item.Title : tabModel.Title;
                    item.Avastar = tabModel.Avastar == null ? item.Avastar : AvastarUri(tabModel.Avastar);
                    break;
                }
            }
            UpdateLayout();
        }

        public void UpdateLayout()
        {
            var count = TabGridViewItems.Count;
            switch (count)
            {
                case 0:
                    break;
                case 1:
                    UpdateTabGridViewItemSize(View.ActualWidth - 300, (View.ActualWidth - 500) / 1.25);
                    break;
                case 2:
                    UpdateTabGridViewItemSize((View.ActualWidth - 400)/2, (View.ActualWidth - 400) / 2 / 1.5);
                    break;
                case 3:
                    UpdateTabGridViewItemSize(350, 250);
                    break;
                default:
                    UpdateTabGridViewItemSize(350, 250);
                    break;
            }
        }

        public void UpdateTabGridViewItemSize(double width, double height)
        {
            foreach(TabGridViewItemViewModel item in TabGridViewItems)
            {
                item.Width  = width;
                item.Height = height;
            }
        }


        public void Delete(Guid tab_id)
        {
            SwitchBeforeDeletion(tab_id);
            TabGridViewItemViewModel deleteitem = null;
            foreach (var item in TabGridViewItems)
            {
                if (item.Id == tab_id)
                {
                    deleteitem = item;
                }
            }
            if (deleteitem != null)
                TabGridViewItems.Remove(deleteitem);
        }

        public void SwitchBeforeDeletion(Guid tab_id)
        {
            int deleteditem_index = Position(tab_id);
            int selecteditem_index = Position(tab_id);
            if (selecteditem_index == deleteditem_index)
            {
                if (selecteditem_index == 0)
                {
                    if (TabGridViewItems.Count >= 2)
                    {
                        View.TabsGridView.SelectedIndex = 1;
                    }
                    else
                    {
                        TabsService.Current.Add("新建标签页", "https://www.baidu.com");
                    }
                }
                else
                {
                    View.TabsGridView.SelectedIndex = selecteditem_index - 1;
                }

            }
        }

        public int Position(Guid tab_id)
        {
            int index = 0;
            foreach (var item in TabGridViewItems)
            {
                if (item.Id == tab_id)
                {
                    index = TabGridViewItems.IndexOf(item);
                }
            }
            return index;
        }

        int _SelectedIndex = -1;

        public void AddButton_Click()
        {
            TabsService.Current.Add("新建标签页", "https://www.baidu.com");
        }

        public void FocusDeleteButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var fonticon = sender as FontIcon;
            TabsService.Current.Delete(((TabGridViewItemViewModel)fonticon.DataContext).Id);
        }

        public void UpdateCaptureScreenShot(Guid tab_id, BitmapSource bitmapImage)
        {
            foreach (var item in TabGridViewItems)
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
            active_id = TabsService.Current.Active_Tab_ID;
            foreach (var taskbargridviewitem in ((ItemsWrapGrid)Current.View.TabsGridView.ItemsPanelRoot).Children)
            {
                if (((TaskBarItemViewModel)Current.View.TabsGridView.Items[((ItemsWrapGrid)Current.View.TabsGridView.ItemsPanelRoot).Children.IndexOf(taskbargridviewitem)]).Id == active_id)
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


        public double GridViewItemWidth
        {
            get { return TabGridViewItems[0].Width; }
        }
        public double GridViewItemHeight
        {
            get { return TabGridViewItems[0].Height; }
        }

        public double gridOffsetX = 0;
        public double gridOffsetY = 0;
        public bool isSelectMode;

        internal void UpdateTransform(double scale, double offsetX, double offsetY)
        {
            var minScale = GridViewItemWidth / View.ActualWidth;
            //if (scale < minScale) scale = minScale;
            //if (scale > 1) scale = 1;

            offsetX = offsetX * scale;
            offsetY = offsetY * scale;

            var gridViewScale = scale / minScale;

            MainFrameViewModel.Current.View.BrowserSplitViewTransform.TranslateX = offsetX;
            MainFrameViewModel.Current.View.BrowserSplitViewTransform.TranslateY = offsetY;
            MainFrameViewModel.Current.View.BrowserSplitViewTransform.ScaleX = scale;
            MainFrameViewModel.Current.View.BrowserSplitViewTransform.ScaleY = scale;

            View.TabsGridViewTransform.TranslateX = gridOffsetX * gridViewScale + offsetX;
            View.TabsGridViewTransform.TranslateY = gridOffsetY * gridViewScale + offsetY;
            View.TabsGridViewTransform.ScaleX = gridViewScale;
            View.TabsGridViewTransform.ScaleY = gridViewScale;

            var scaleX = View.ActualWidth / GridViewItemWidth;
            var scaleY = View.ActualHeight / GridViewItemHeight;
            View.TabsGridViewTransform.ScaleX = scaleX;
            View.TabsGridViewTransform.ScaleY = scaleY;

            //int count = 0;
            //foreach (GridViewItem item in View.TabsGridView.ItemsPanelRoot.Children)
            //{
            //    count++;
            //    if ((TabGridViewItemViewModel)item.Content == View.TabsGridView.SelectedItem)
            //    {
            //        break;
            //    }
            //}
            //int numOfItemsPerRow = Convert.ToInt32(Math.Floor(View.ActualWidth / (GridViewItemWidth + 30)));
            //int numOfRow = count / numOfItemsPerRow;
            //int OrderOnTheRow = 1;
            //if(count % numOfItemsPerRow == 0)
            //{
            //    OrderOnTheRow = numOfItemsPerRow;
            //}
            //else
            //{
            //    numOfRow++;
            //    OrderOnTheRow = count % numOfItemsPerRow;
            //}

            //View.TabsGridViewTransform.TranslateX = scaleX * - OrderOnTheRow * GridViewItemWidth;
            //View.TabsGridViewTransform.TranslateY = 0; //scaleY * (scaleY * View.TabsGridView.ActualHeight - numOfRow * GridViewItemHeight)
            ////View.TabsGridViewTransform.ScaleX = gridViewScale;
            ////View.TabsGridViewTransform.ScaleY = gridViewScale;
        }

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

        public void ResetTransform(bool isSelect, bool resetAnimationState = true)
        {
            if (isSelect)
            {
                View.TabsGridViewTransform.TranslateX = 0;
                View.TabsGridViewTransform.TranslateY = 0;
                View.TabsGridViewTransform.ScaleX = 1;
                View.TabsGridViewTransform.ScaleY = 1;
                View.TabsGridView.Visibility = Visibility.Collapsed;
            }
            else
            {
                View.TabsGridViewTransform.TranslateX = 0;
                View.TabsGridViewTransform.TranslateY = 0;
                View.TabsGridViewTransform.ScaleX = 1;
                View.TabsGridViewTransform.ScaleY = 1;
            }

            gridOffsetX = 0;
            gridOffsetY = 0;
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
            var clickeditem = e.ClickedItem as TabGridViewItemViewModel;
            TabsService.Current.Select(clickeditem.Id);
            ResetTransformWhenSelectionChanged();

            if (MainFrameViewModel.Current.isAnimating) return;

            ScrollViewer.SetVerticalScrollMode(View.TabsGridView, ScrollMode.Disabled);

            View.SelectedModel = e.ClickedItem as TabGridViewItemViewModel;

            var container = View.TabsGridView.ContainerFromItem(View.SelectedModel) as FrameworkElement;
            UpdateListFinalOffset(container);

            //View.TabsGridViewTransform.TranslateX = - gridOffsetX;
            //View.TabsGridViewTransform.TranslateY = - gridOffsetY;

            MainFrameViewModel.Current.View.BrowserSplitView.Visibility = Visibility.Visible;

            AnimateToSelect();
        }

        private void AnimateToSelect()
        {
            MainFrameViewModel.Current.isAnimating = true;

            var gridViewScale = 1 * View.ActualWidth / GridViewItemWidth;
            var duration = TimeSpan.FromSeconds(0.4d);
            var easing = new CircleEase()
            {
                EasingMode = EasingMode.EaseInOut
            };

            var sb = new Storyboard();
            DoubleAnimation SplitViewFadeInAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.2d),
                To = 1,
                EasingFunction = easing
            };
            Storyboard.SetTarget(SplitViewFadeInAnimation, MainFrameViewModel.Current.View.BrowserSplitView);
            Storyboard.SetTargetProperty(SplitViewFadeInAnimation, "Opacity");
            sb.Children.Add(SplitViewFadeInAnimation);

            sb.Children.Add(CreateAnimation(MainFrameViewModel.Current.View.BrowserSplitViewTransform, "ScaleX", 1, duration, easing));
            sb.Children.Add(CreateAnimation(MainFrameViewModel.Current.View.BrowserSplitViewTransform, "ScaleY", 1, duration, easing));

            sb.Children.Add(CreateAnimation(MainFrameViewModel.Current.View.BrowserSplitViewTransform, "TranslateX", 0, duration, easing));
            sb.Children.Add(CreateAnimation(MainFrameViewModel.Current.View.BrowserSplitViewTransform, "TranslateY", 0, duration, easing));

            sb.Children.Add(CreateAnimation(View.TabsGridViewTransform, "ScaleX", gridViewScale, duration, easing, 1));
            sb.Children.Add(CreateAnimation(View.TabsGridViewTransform, "ScaleY", gridViewScale, duration, easing, 1));

            sb.Children.Add(CreateAnimation(View.TabsGridViewTransform, "TranslateX", gridOffsetX * gridViewScale, duration, easing, 0));
            sb.Children.Add(CreateAnimation(View.TabsGridViewTransform, "TranslateY", gridOffsetY * gridViewScale, duration, easing, 0));
            DoubleAnimation FadeOutAnimation = new DoubleAnimation()
            {
                BeginTime = TimeSpan.FromSeconds(0.1d),
                Duration = TimeSpan.FromSeconds(0.3d),
                From = 1,
                To = 0,
                EasingFunction = easing
            };
            Storyboard.SetTarget(FadeOutAnimation, View.OverViewHeader);
            Storyboard.SetTargetProperty(FadeOutAnimation, "Opacity");
            sb.Children.Add(FadeOutAnimation);
            sb.Completed += AnimationToSelect_Completed;
            sb.Begin();
        }

        private void AnimationToSelect_Completed(object sender, object e)
        {
            if (sender is Storyboard sb)
            {
                sb.Completed -= MainFrameViewModel.Current.AnimationToList_Completed;
            }
            ScrollViewer.SetVerticalScrollMode(View.TabsGridView, ScrollMode.Enabled);
            MainFrameViewModel.Current.ResetTransform(true);
        }


        private Timeline CreateAnimation(DependencyObject target, string path, double toValue, TimeSpan duration, EasingFunctionBase easingFunc, double? stopValue = null)
        {
            Timeline an;

            if (!stopValue.HasValue || stopValue.Value == toValue)
            {
                an = new DoubleAnimation()
                {
                    To = toValue,
                    Duration = duration,
                    EasingFunction = easingFunc
                };
            }
            else
            {
                TimeSpan fixedDuration = duration;
                TimeSpan endDuration = duration;

                if (duration > TimeSpan.FromMilliseconds(10))
                {
                    fixedDuration = duration - TimeSpan.FromMilliseconds(10);
                }
                else
                {
                    endDuration = duration + TimeSpan.FromMilliseconds(10);
                }

                an = new DoubleAnimationUsingKeyFrames()
                {
                    KeyFrames =
                    {
                        new EasingDoubleKeyFrame()
                        {
                            Value = toValue,
                            KeyTime = duration,
                            EasingFunction = easingFunc,
                        },
                        new DiscreteDoubleKeyFrame()
                        {
                            Value = stopValue.Value,
                            KeyTime = duration + TimeSpan.FromMilliseconds(10)
                        }
                    }
                };
            }

            Storyboard.SetTarget(an, target);
            Storyboard.SetTargetProperty(an, path);

            return an;
        }


        public void ScrollIntoView()
        {
            View.TabsGridView.Visibility = Visibility.Visible;
            View.TabsGridView.ScrollIntoView(View.TabsGridView.SelectedItem);
            View.TabsGridView.UpdateLayout();

            var container = View.TabsGridView.ContainerFromItem(View.TabsGridView.SelectedItem) as FrameworkElement;
            UpdateListFinalOffset(container);

            ScrollViewer.SetVerticalScrollMode(View.TabsGridView, ScrollMode.Disabled);
        }

        private void UpdateListFinalOffset(FrameworkElement container, Point? centerPoint = null)
        {
            View.UpdateLayout();

            var offset = this.View.TransformToVisual(container).TransformPoint(default);

            if (centerPoint.HasValue)
            {
                var minScale = GridViewItemWidth / View.ActualWidth;
                //var dx = offset.X / (1 - minScale);
                //var dy = offset.Y / (1 - minScale);

                var dx = centerPoint.Value.X;
                var dy = centerPoint.Value.Y;

                var r = new Point(dx / View.ActualWidth, dy / View.ActualHeight);

                //SelectedRect.RenderTransformOrigin = r;
                View.TabsGridView.RenderTransformOrigin = r;

                gridOffsetX = offset.X + this.View.ActualWidth * r.X - View.SelectedModel.Width * r.X;
                gridOffsetY = offset.Y + this.View.ActualHeight * r.Y - View.SelectedModel.Height * r.Y;

            }
            else
            {
                //SelectedRect.RenderTransformOrigin = new Point(0.5, 0.5);
                View.TabsGridView.RenderTransformOrigin = new Point(0.5, 0.5);

                gridOffsetX = offset.X + this.View.ActualWidth / 2 - GridViewItemWidth / 2;
                gridOffsetY = offset.Y + this.View.ActualHeight / 2 - GridViewItemHeight / 2;
            }
        }

        public void ResetTransformWhenSelectionChanged()
        {
            var container = View.TabsGridView.ContainerFromItem(View.SelectedModel) as FrameworkElement;
            var selectScaleX = GridViewItemWidth / View.ActualWidth;
            var selectScaleY = GridViewItemHeight / View.ActualHeight;
            var selectedTransX = -this.View.TransformToVisual(container).TransformPoint(default).X;
            var selectedTransY = -this.View.TransformToVisual(container).TransformPoint(default).Y;

            MainFrameViewModel.Current.View.BrowserSplitViewTransform.TranslateX = selectedTransX;
            MainFrameViewModel.Current.View.BrowserSplitViewTransform.TranslateY = selectedTransY;
            MainFrameViewModel.Current.View.BrowserSplitViewTransform.ScaleX = selectScaleX;
            MainFrameViewModel.Current.View.BrowserSplitViewTransform.ScaleY = selectScaleY;
        }
    }

    public class TabGridViewItemViewModel : ViewModelBase
    {
        string _Avastar;
        string _Title;
        BitmapSource _CapturedImage;
        Guid _Id;
        double _width;
        double _height;

        public TabGridViewItemViewModel(TabModel tabModel)
        {
            Id      = tabModel.ID;
            Avastar = tabModel.Avastar;
            Title   = tabModel.Title;
            Width   = 350;
            Height  = 250;
        }

        public Guid Id
        {
            get { return _Id; }
            set { Set(ref _Id, value); }
        }

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

        public double Width
        {
            get { return _width; }
            set { Set(ref _width, value); }
        }

        public double Height
        {
            get { return _height; }
            set { Set(ref _height, value); }
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
