using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Storage
{
    public static class a
    {
        public static MySQLManagerBase mySQLManagerBase = new MySQLManagerBase();

    }
    public class MySQLManagerBase
    {
        public MySQLManagerBase()
        {
            a();
        }

        public void a()
        {
            string M_str_sqlcon = "server=10.xxx.xx.xxx;user id=foo;password=bar;database=baz";
            MySqlConnection mysqlcon = new MySqlConnection(M_str_sqlcon);
            MySqlCommand mysqlcom = new MySqlCommand("select * from table1", mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                Debug.WriteLine(mysqlread.GetString(0) + ":" + mysqlread.GetString(1));
            }
            mysqlcon.Close();
        }

        public void Add(object _object)
        {
            MySqlCommand mysqlcom = new MySqlCommand("select * from table1", mysqlcon);
        }

        public void AddGroup<T>(List<T> _items)
        {
            foreach (var item in _items)
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
            foreach (var item in _items)
            {
                Delete(item);
            }
        }

        public object Query(object _Property_One)
        {
            return new object();
        }

        public object Query(dynamic _Property_One, dynamic _Order_One)
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
