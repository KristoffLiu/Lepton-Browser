using Lepton_Browser.Models;
using Lepton_Browser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Browser.Services
{
    public class TabsService
    {
        public static TabsService Current;
        public List<TabModel> Tabs;

        public Guid ID;
        public Guid Active_Tab_ID;
        public TabsService()
        {
            Current = this;
            Tabs = new List<TabModel>();
        }

        public void Add(string Title, string web_url)
        {
            var item = new TabModel()
            {
                ID = Guid.NewGuid(),
                Category = TabPageCategory.WebPage,
                Title = Title,
                Url = web_url,
                Avastar = AvastarUri(web_url)
            };
            Tabs.Add(item);
            TaskBarViewModel.Current.Add(new TaskBarItemViewModel(item));
            OverViewViewModel.Current.Add(new TabGridViewItemViewModel(item));
            TabPageViewModel.Current.Add(new TabPageFrameItemViewModel(item));
        }

        public string AvastarUri(string _uri)
        {
            var __uri = new Uri(_uri);
            return __uri.Scheme + "://" + __uri.Host.ToString() + "/favicon.ico";
        }

        public void Delete(Guid tab_id)
        {
            for(int i = 0; i < Tabs.Count; i++)
            {
                if(Tabs[i].ID == tab_id)
                {
                    Tabs.RemoveAt(i);
                    break;
                }
            }
        }

        public void Select(Guid tab_id)
        {
            Active_Tab_ID = tab_id;
            TaskBarViewModel.Current.Select(tab_id);
            OverViewViewModel.Current.Select(tab_id);
            TabPageViewModel.Current.Select(tab_id);
        }

        public void Move(Guid tab_id, int index)
        {
            for (int i = 0; i < Tabs.Count; i++)
            {
                if (Tabs[i].ID == tab_id)
                {
                    Tabs.RemoveAt(i);
                    break;
                }
            }
        }

        public void Update(TabModel tabModel)
        {
            for (int i = 0; i < Tabs.Count; i++)
            {
                if (Tabs[i].ID == tabModel.ID)
                {
                    Tabs[i] = tabModel;
                    break;
                }
            }
        }
    }
}
