using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Lepton_Browser.Services
{
    public class EvalJSService
    {
        public string JSString = "";

        public EvalJSService()
        {
            InitAsync();
        }

        private async void InitAsync()
        {
            Uri uri = new Uri("ms-appx:///Assets/EvalJSCode.txt");
            StorageFile jsStringFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            JSString = await Windows.Storage.FileIO.ReadTextAsync(jsStringFile);
        }
    }
}
