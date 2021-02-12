using Lepton_Browser.ViewModels;
using Lepton_Browser.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Browser.Models
{
    public class AppManager
    {
        public static AppManager Current;
        public MainFrameViewModel MainFrameViewModel;

        public AppManager()
        {
            Current = this;
            ID = Guid.NewGuid();
            MainFrameViewModel = new MainFrameViewModel();
            //AddTabPage();
        }

        public Guid ID;
        public Guid Active_Tab_ID;

        public List<TabPageInfo> TabPages = new List<TabPageInfo>();
        public List<int> Index = new List<int>();


        public void AddNewTabPage(string Title, string web_url)
        {
            var item = new TabPageInfo()
            {
                ID = Guid.NewGuid(),
                Category = TabPageCategory.WebPage,
                Title = Title,
                Url = web_url
            };
            TabPages.Add(item);

            MainFrameViewModel.AddNewTabPage(item);
        }

        public void SwitchTabPage(Guid tab_id)
        {
            Active_Tab_ID = tab_id;
            MainFrameViewModel.SwitchTabPage(tab_id);
        }

        public void DeleteTabPage(Guid tab_id)
        {
            MainFrameViewModel.DeleteTabPage(tab_id);
        }

        public void UpdateTabPage(TabPageInfo tabPageInfo)
        {
            MainFrameViewModel.UpdateTabPage(tabPageInfo);
        }

    }
}
