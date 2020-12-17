using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Lepton_Library.Storage
{
    public static class AppService
    {
        public static StorageFolder folder;
        public const string AppServicePrefix = "ms-AppService:///";
        static AppService()
        {
            folder = ApplicationData.Current.LocalFolder;
        }
        
        public async static void CreatFolder(string desiredName)
        {
            await folder.CreateFolderAsync(desiredName, CreationCollisionOption.ReplaceExisting);
        }

        public async static void Load(string uriString)
        {
            // ReadFileFromAbsolutePath
            StorageFile fileRead = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString, UriKind.Absolute));
            string textContent = await FileIO.ReadTextAsync(fileRead);
        }

        public async static void ReadFromAppService(string uriString)
        {
            var relativestr = uriString;
            if (relativestr.Contains("ms-AppService:///local/"))
            {
                relativestr = uriString;
            }
            else
            {
                relativestr = "ms-AppService:///local/" + uriString;
            }
            StorageFile fileRead = await StorageFile.GetFileFromApplicationUriAsync(new Uri( relativestr , UriKind.Relative));
            string textContent = await FileIO.ReadTextAsync(fileRead);
        }
    }
}
