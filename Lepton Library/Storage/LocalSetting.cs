using Lepton_Library.Common;
using Lepton_Library.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Lepton_Library.Storage
{
    public class LocalSetting : Observable
    {
        #region Stored Variable
        const string IsFirstInitializedSetting = "IsFirstInitialized";
        #endregion

        protected static ApplicationDataContainer _settings = ApplicationData.Current.LocalSettings;
        [DataMember(Name = "Settings")]
        protected static ApplicationDataContainer Settings
        {
            #region Settings
            get
            {
                return _settings;
            }
            set
            {
                if (_settings != null && Equals(_settings, value)) return;
                _settings = value;
            }
            #endregion
        }
        protected StorageFolder LocalStorage = ApplicationData.Current.LocalFolder;


        public static void AddOrUpdateValue(string key, object value)
        {
            if (Settings.Values.ContainsKey(key))
            {
                if (Settings.Values[key] != null)
                {
                    Settings.Values[key] = value;
                }
            }
            else
            {
                Settings.Values.Add(key, value);
            }
        }

        public static void AddOrUpdateValue(string key, DateTime dateTime)
        {
            if (Settings.Values.ContainsKey(key))
            {
                if (Settings.Values[key] != null)
                {
                    Settings.Values[key] = dateTime.ToBinary();
                }
            }
            else
            {
                Settings.Values.Add(key, dateTime.ToBinary());
            }
        }

        public static void AddOrUpdateValue(string key, BitmapImage bitmapImage)
        {
            if (Settings.Values.ContainsKey(key))
            {
                if (Settings.Values[key] != null)
                {

                    Settings.Values[key] = ByteHelper.ToBase64Async(bitmapImage);
                }
            }
            else
            {
                Settings.Values.Add(key, ByteHelper.ToBase64Async(bitmapImage));
            }
        }

        public static void AddOrUpdateValue(string key, Enum _enum)
        {
            if (Settings.Values.ContainsKey(key))
            {
                if (Settings.Values[key] != null)
                {
                    Settings.Values[key] = Convert.ToInt32(_enum);
                }
            }
            else
            {
                Settings.Values.Add(key, Convert.ToInt32(_enum));
            }
        }

        public static T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;
            try
            {
                if (Settings.Values.ContainsKey(key))
                {
                    value = (T)Settings.Values[key];
                }
                else
                {
                    value = defaultValue;
                }
                return value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public void HandleEnum<T>(T _enum)
        {

        }

        public static DateTime GetValueOrDefault(string key, DateTime defaultValue)
        {
            DateTime value;
            try
            {
                if (Settings.Values.ContainsKey(key))
                {
                    value = Convert.ToDateTime((long)Settings.Values[key]);
                }
                else
                {
                    value = defaultValue;
                }
                return value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static BitmapImage GetValueOrDefault(string key, BitmapImage defaultValue)
        {
            BitmapImage value;
            try
            {
                if (Settings.Values.ContainsKey(key))
                {
                    value = (BitmapImage)ByteHelper.FromBase64((string)Settings.Values[key]).Result;
                    return value;
                }
                else
                {
                    value = defaultValue;
                }
                return value;
            }
            catch
            {
                return defaultValue;
            }
        }

        protected static void AddOrUpdateJsonValue(string key, object value)
        {
            if (Settings.Values.ContainsKey(key))
            {
                if (Settings.Values[key] != null)
                {
                    Settings.Values[key] = JsonConvert.SerializeObject(value);
                }
            }
            else
            {
                Settings.Values.Add(key, JsonConvert.SerializeObject(value));
            }
        }

        public static T GetJsonValueOrDefault<T>(string key, T defaultValue)
        {
            T value;
            try
            {
                if (Settings.Values.ContainsKey(key))
                {
                    var str = Settings.Values[key].ToString();
                    var json = JsonSerializer.Create();
                    value = json.Deserialize<T>(new JsonTextReader(new StringReader(str)));
                }
                else
                {
                    value = defaultValue;
                }

                return value;
            }
            catch
            {
                return defaultValue;
            }
        }

        protected async void SaveStringFile(string FolderName, string FileName, string SavedObject)
        {
            var Folder = await LocalStorage.CreateFolderAsync(FolderName, CreationCollisionOption.OpenIfExists);
            var File = await Folder.CreateFileAsync(FileName + ".json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(File, SavedObject);
        }

        /// <summary>
        /// Check Whether the Local Setting contains the value of this Key
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="InitialValue"></param>
        /// <returns></returns>
        public bool IsExist(string Key, string InitialValue)
        {
            if (Settings.Values.ContainsKey(Key))
            {
                return true;
            }
            else
            {
                Settings.Values[Key] = InitialValue;
                return false;
            }
        }

        public void Initialization()
        {

        }

    }
}
