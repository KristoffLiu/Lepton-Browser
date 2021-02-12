using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Lepton_Browser.Services
{
    /// <summary>
    /// Service for 
    /// </summary>
    public class EvalJSService
    {
        private const string file_URL = "ms-appx:///Assets/EvalJSCode";
        
        private static string code_ContextMenuHandler = "";

        public EvalJSService()
        {
            InitAsync();
        }

        private async void InitAsync()
        {
            Uri uri = new Uri("ms-appx:///Assets/EvalJSCode.txt");
            StorageFile jsStringFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            _initialJSCode = await Windows.Storage.FileIO.ReadTextAsync(jsStringFile);
        }

        public string InitialJSCode
        {
            get { return _initialJSCode; }
        }



        public 


    }
}
