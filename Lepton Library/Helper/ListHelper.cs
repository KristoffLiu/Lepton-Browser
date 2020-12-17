using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Helper
{
    public class ListHelper
    {
        public static List<T> GetListFromObservableCollection<T>(ObservableCollection<T> inputobservablecollection)
        {
            var list = new List<T>();
            foreach (var item in inputobservablecollection)
            {
                list.Add(item);
            }
            return list;
        }

        public static List<T> GetListFromArray<T>(T[] inputarray) where T : new()
        {
            if (inputarray != null)
            {
                var list = new List<T>();
                list = inputarray.ToList();
                return list;
            }
            else
            {
                return new List<T>();
            }
        }

        public static List<string> GetListFromArray(string[] inputarray)
        {
            if (inputarray != null)
            {
                var list = new List<string>();
                list = inputarray.ToList();
                return list;
            }
            else
            {
                return new List<string>();
            }
        }
    }
}
