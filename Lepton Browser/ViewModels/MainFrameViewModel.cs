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
        public OverView TaskView;
        public Grid MainFrame_Grid;
        public MainFrameViewModel() //OverView taskView, Grid grid
        {
            Current = this;
            ManipulationBarViewModel = new ManipulationBarViewModel();
            SideBarViewModel = new SideBarViewModel();
            TabPageViewModel = new TabPageViewModel();
            TabsSetViewModel = new TabsSetViewModel();
        }

        public void InputUserControl(OverView taskView, Grid grid)
        {
            TaskView = taskView;
            MainFrame_Grid = grid;
        }

        public ManipulationBarViewModel ManipulationBarViewModel { get; set; }
        public SideBarViewModel SideBarViewModel { get; set; }
        public TabPageViewModel TabPageViewModel { get; set; }
        public TabsSetViewModel TabsSetViewModel { get; set; }


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

        public static void SwitchTaskView(Guid windows_id)
        {
            Current.SwitchTaskView();
        }

        public void SwitchTaskView()
        {
            if (IsTaskViewVisible == false)
            {
                MainFrame_Grid.Children.Move(1, Convert.ToUInt32(MainFrame_Grid.Children.IndexOf(MainFrame_Grid.Children.Last())));

                //ConnectedAnimationService.GetForCurrentView().DefaultDuration = new TimeSpan(0, 0, 0, 0, 800);
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", WebPageViewModel.ReturnActiveWebview());
                var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
                if (anim != null)
                {
                    //anim.Configuration = new DirectConnectedAnimationConfiguration();
                    anim.TryStart(TabsSetViewModel.ReturnActiveWebview());
                }

                TabsSetViewModel.SwitchTaskView();

                IsTaskViewVisible = true;
            }
            else
            {
                //ConnectedAnimationService.GetForCurrentView().DefaultDuration = new TimeSpan(0, 0, 0, 800);
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackwardConnectedAnimation", TabsSetViewModel.ReturnActiveWebview());

                MainFrame_Grid.Children.Move(Convert.ToUInt32(MainFrame_Grid.Children.IndexOf(MainFrame_Grid.Children.Last())), 1);

                var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("BackwardConnectedAnimation");
                if (anim != null)
                {
                    //anim.Configuration = new DirectConnectedAnimationConfiguration();
                    anim.TryStart(WebPageViewModel.ReturnActiveWebview());
                }

                TabsSetViewModel.SwitchTaskView();


                IsTaskViewVisible = false;
            }
        }

        public void AddNewTabPage(TabPageInfo info)
        {
            TabsSetViewModel.Add(info.ID, info.Url, info.Title);
            TabPageViewModel.Add(info);
        }

        public void SwitchTabPage(Guid tab_id)
        {
            TabsSetViewModel.Switch(tab_id);
            TabPageViewModel.Switch(tab_id);
        }

        public void DeleteTabPage(Guid tab_id)
        {
            TabsSetViewModel.Delete(tab_id);
            TabPageViewModel.Delete(tab_id);
        }

        public void UpdateTabPage(TabPageInfo tabPageInfo)
        {
            TabsSetViewModel.Update(tabPageInfo);
        }



    }
}
