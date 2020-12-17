using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Lepton_Library.Storage
{
    public class SQLiteManagerBase : ISQLManager
    {
        /* 
         * Every entity has its own specific private property,
         * So it is not possible to query all sorts of data
         * By using a universal class method.
         * Therefore, we need to override it later on.
         * The instructions of the method in this base class
         * is just a standard template for me to override it 
         */
        //private static String DB_NAME = "SQLiteSample.db";
        private static String TABLE_NAME = "SampleTable";
        private static String SQL_CREATE_TABLE = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " (Key TEXT,Value TEXT);";
        private static String SQL_QUERY_VALUE = "SELECT Value FROM " + TABLE_NAME + " WHERE Key = (?);";
        private static String SQL_INSERT = "INSERT INTO " + TABLE_NAME + " VALUES(?,?);";
        private static String SQL_UPDATE = "UPDATE " + TABLE_NAME + " SET Value = ? WHERE Key = ?";
        private static String SQL_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Key = ?";

        public static SQLiteConnection Connection;

        public static void InitDatabase()
        {
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "LeptonSQLLocal.sqlite");
            Connection = new SQLiteConnection(new SQLitePlatformWinRT(), path);
        }

        public static async Task InitDatabaseAsync()
        {
            await Task.Run(() => InitDatabase());
        }
        public void Add(object _object)
        {
            Connection.Insert(_object);
        }
        public void AddGroup<T>(List<T> _items)
        {
            foreach(var item in _items)
            {
                Add(item);
            }
        }
        /// <summary>
        /// The name of the table will as same as the name of the method defaultly
        /// The following method works equally as above
        /// Connection.CreateTable(typeof(DataTemple));
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void CreateTableIfNotExist<T>()
        {
            Connection.CreateTable<T>();
        }
        public void Delete(object _object)
        {
            Connection.Delete(_object);
        }
        public void DeleteAll<T>()
        {
            Connection.DeleteAll<T>();
        }
        public void DeleteGroup<T>(List<T> _items)
        {
            foreach(var item in _items)
            {
                Delete(item);
            }
        }
        public object Query(object _Property_One)
        {
            return new object();
        }
        public object Query(dynamic _Property_One,dynamic _Order_One)
        {
            return new object();
        }
        public object Query(dynamic _Property_One, dynamic _Property_Two, dynamic _Order_One)
        {
            return new object();
        }
        public object Query(dynamic _Property_One, dynamic _Property_Two, dynamic _Property_Three, dynamic _Order_One)
        {
            return new object();
        }
        public List<T> QueryAll<T>() where T : class, new()
        {
            {
                T value = new T();
                try
                {
                    return Connection.Query<T>("select * from " + value.GetType().Name);
                }
                catch
                {
                    return new List<T>();
                }
            }
        }
        public object QueryMax<T>(dynamic Max_Element) where T : class, new()
        {
            T value = new T();
            try
            {
                return Connection.Query<T>("select MAX(" + Max_Element + ") * from" + value.GetType().Name);
            }
            catch
            {
                return new List<T>();
            }
        }
        public object QueryMin<T>(dynamic Max_Element) where T : class, new()
        {
            T value = new T();
            try
            {
                return Connection.Query<T>("select MIN(" + Max_Element + ") * from" + value.GetType().Name);
            }
            catch
            {
                return new List<T>();
            }
        }
        public void Update<T>(T _object)
        {
            Connection.Update(_object);
        }
        public void UpdateAll<T>()
        {
            throw new NotImplementedException();
            //Connection.UpdateAll();
        }
        public void UpdateGroup<T>(List<T> _items)
        {
            throw new NotImplementedException();
        }
    }
}
