using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Helper
{
    public class ObservableCollectionHelper
    {
        public static ObservableCollection<T> GetObservableCollectionFromList<T>(List<T> inputlist)
        {
            var observablecollection = new ObservableCollection<T>();
            inputlist.ForEach(x => observablecollection.Add(x));
            return observablecollection;
        }
        public static ObservableCollection<T> GetObservableCollectionFromArray<T>(T[] inputarray) where T : new()
        {
            if(inputarray != null)
            {
                var observablecollection = new ObservableCollection<T>();
                foreach (var item in inputarray)
                {
                    observablecollection.Add(item);
                }
                return observablecollection;
            }
            else
            {
                return new ObservableCollection<T>();
            }
        }//辣鸡代码，需要整改

        public static ObservableCollection<string> GetObservableCollectionFromArray(string[] inputarray)
        {
            var observablecollection = new ObservableCollection<string>();
            if (inputarray != null)
            {
                foreach (var item in inputarray)
                {
                    observablecollection.Add(item);
                }
                return observablecollection;
            }
            else
            {
                observablecollection.Add("");
                return observablecollection;
            }
        }
    }
}
