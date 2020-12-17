using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Storage
{
    public interface ISQLManager
    {
        void CreateTableIfNotExist<T>();

        void Add(object _object);
        void AddGroup<T>(List<T> _items);

        void Delete(object _object);
        void DeleteGroup<T>(List<T> _items);
        void DeleteAll<T>();

        void Update<T>(T _object);
        void UpdateGroup<T>(List<T> _items);
        void UpdateAll<T>();

        object Query(object _Property_One);
        object Query(dynamic _Property_One, dynamic _Order_One);
        object Query(dynamic _Property_One, dynamic _Property_Two, dynamic _Order_One);
        object Query(dynamic _Property_One, dynamic _Property_Two, dynamic _Property_Three, dynamic _Order_One);
        List<T> QueryAll<T>() where T : class, new();

        object QueryMax<T>(dynamic Max_Element) where T : class, new();
        object QueryMin<T>(dynamic Min_Element) where T : class, new();
    }
}



