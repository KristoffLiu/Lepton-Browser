using Lepton_Library.Common;
using Lepton_Browser.Models;
using Lepton_Browser.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Lepton_Browser.ViewModels
{
    public class MainFrameViewModel : PageViewModelBase
    {
        public static MainFrameViewModel Current;

        public MainFrame View;
        public OverView TaskView;
        public Grid MainFrame_Grid;
        public MainFrameViewModel(MainFrame mainFrame) //OverView taskView, Grid grid
        {
            this.View = mainFrame;
            Current = this;
        }

        public void InputUserControl(OverView taskView, Grid grid)
        {
            TaskView = taskView;
            MainFrame_Grid = grid;
        }

        //public ManipulationBarViewModel ManipulationBarViewModel { get; set; }
        //public SideBarViewModel SideBarViewModel { get; set; }
        //public TabPageViewModel TabPageViewModel { get; set; }
        //public TabsSetViewModel TabsSetViewModel { get; set; }


        bool _IsSideBarPaneOpen = false;

        public bool IsSideBarPaneOpen
        {
            get { return _IsSideBarPaneOpen; }
            set { Set(ref _IsSideBarPaneOpen, value); }
        }

        //Visibility _TaskViewVisibility = Visibility.Collapsed;
        //public Visibility TaskViewVisibility
        //{
        //    get { return _TaskViewVisibility; }
        //    set { Set(ref _TaskViewVisibility, value); }
        //}

        bool _isTaskViewVisible = false;
        public bool IsTaskViewVisible
        {
            get { return _isTaskViewVisible; }
            set { Set(ref _isTaskViewVisible, value); }
        }

        public bool isAnimating = false;

        public static void OpenTimeLine()
        {
            Current.openTimeLine();
        }

        public void openTimeLine()
        {
            if (isAnimating) return;
            OverViewViewModel.Current.ScrollIntoView();
            UpdateTransform(1, 0, 0);
            AnimateToList(false);
        }
        private void UpdateTransform(double scale, double offsetX, double offsetY)
        {
            
            //if (scale < minScale) scale = minScale;
            //if (scale > 1) scale = 1;

            offsetX = offsetX * scale;
            offsetY = offsetY * scale;

            View.BrowserSplitViewTransform.TranslateX = offsetX;
            View.BrowserSplitViewTransform.TranslateY = offsetY;
            View.BrowserSplitViewTransform.ScaleX = scale;
            View.BrowserSplitViewTransform.ScaleY = scale;

            OverViewViewModel.Current.UpdateTransform(scale, offsetX, offsetY);
        }

        private void AnimateToList(bool fromTouch = true)
        {
            isAnimating = true;

            var selectScaleX = OverViewViewModel.Current.GridViewItemWidth / View.ActualWidth;
            var selectScaleY = OverViewViewModel.Current.GridViewItemHeight / View.ActualHeight;
            var duration = TimeSpan.FromSeconds(0.4d);
            var easing = new CircleEase()
            {
                EasingMode = fromTouch ? EasingMode.EaseOut : EasingMode.EaseInOut
            };

            var sb = new Storyboard();
            sb.Children.Add(CreateAnimation(View.BrowserSplitViewTransform, "ScaleX", selectScaleX, duration, easing));
            sb.Children.Add(CreateAnimation(View.BrowserSplitViewTransform, "ScaleY", selectScaleY, duration, easing));

            var container = OverViewViewModel.Current.View.TabsGridView.ContainerFromItem(OverViewViewModel.Current.View.SelectedModel) as FrameworkElement;
            sb.Children.Add(CreateAnimation(View.BrowserSplitViewTransform, "TranslateX", - this.View.TransformToVisual(container).TransformPoint(default).X, duration, easing));
            sb.Children.Add(CreateAnimation(View.BrowserSplitViewTransform, "TranslateY", - this.View.TransformToVisual(container).TransformPoint(default).Y, duration, easing));

            sb.Children.Add(CreateAnimation(OverViewViewModel.Current.View.TabsGridViewTransform, "ScaleX", 1, duration, easing));
            sb.Children.Add(CreateAnimation(OverViewViewModel.Current.View.TabsGridViewTransform, "ScaleY", 1, duration, easing));

            sb.Children.Add(CreateAnimation(OverViewViewModel.Current.View.TabsGridViewTransform, "TranslateX", 0, duration, easing));
            sb.Children.Add(CreateAnimation(OverViewViewModel.Current.View.TabsGridViewTransform, "TranslateY", 0, duration, easing));
            DoubleAnimation HeaderFadeInAnimation = new DoubleAnimation()
            {
                BeginTime = TimeSpan.FromSeconds(0.3d),
                Duration = TimeSpan.FromSeconds(0.2d),
                To = 1,
                EasingFunction = easing
            };
            Storyboard.SetTarget(HeaderFadeInAnimation, OverViewViewModel.Current.View.OverViewHeader);
            Storyboard.SetTargetProperty(HeaderFadeInAnimation, "Opacity");

            DoubleAnimation SplitViewFadeOutAnimation = new DoubleAnimation()
            {
                BeginTime = TimeSpan.FromSeconds(0.3d),
                Duration = TimeSpan.FromSeconds(0.1d),
                To = 0,
                EasingFunction = easing
            };
            Storyboard.SetTarget(SplitViewFadeOutAnimation, View.BrowserSplitView);
            Storyboard.SetTargetProperty(SplitViewFadeOutAnimation, "Opacity");

            sb.Children.Add(HeaderFadeInAnimation);
            sb.Children.Add(SplitViewFadeOutAnimation);
            sb.Completed += AnimationToList_Completed;
            sb.Begin();
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

        public void AnimationToList_Completed(object sender, object e)
        {
            if (sender is Storyboard sb)
            {
                sb.Completed -= AnimationToList_Completed;
            }
            ScrollViewer.SetVerticalScrollMode(OverViewViewModel.Current.View.TabsGridViewTransform, ScrollMode.Enabled);
            ResetTransform(false);
        }

        public void ResetTransform(bool isSelect, bool resetAnimationState = true)
        {
            if (isSelect)
            {
                View.BrowserSplitView.Visibility = Visibility.Visible;

                View.BrowserSplitViewTransform.TranslateX = 0;
                View.BrowserSplitViewTransform.TranslateY = 0;
                View.BrowserSplitViewTransform.ScaleX = 1;
                View.BrowserSplitViewTransform.ScaleY = 1;

                OverViewViewModel.Current.ResetTransform(isSelect, resetAnimationState);
            }
            else
            {
                View.BrowserSplitView.Visibility = Visibility.Collapsed;
                var container = OverViewViewModel.Current.View.TabsGridView.ContainerFromItem(OverViewViewModel.Current.View.SelectedModel) as FrameworkElement;
                var selectScaleX = OverViewViewModel.Current.GridViewItemWidth / View.ActualWidth;
                var selectScaleY = OverViewViewModel.Current.GridViewItemHeight / View.ActualHeight;
                var selectedTransX = -this.View.TransformToVisual(container).TransformPoint(default).X;
                var selectedTransY = -this.View.TransformToVisual(container).TransformPoint(default).Y;

                View.BrowserSplitViewTransform.TranslateX = selectedTransX;
                View.BrowserSplitViewTransform.TranslateY = selectedTransY;
                View.BrowserSplitViewTransform.ScaleX = selectScaleX;
                View.BrowserSplitViewTransform.ScaleY = selectScaleY;

                OverViewViewModel.Current.ResetTransform(isSelect, resetAnimationState);
            }

            if (resetAnimationState)
            {
                isAnimating = false;
            }
        }
    }
}
