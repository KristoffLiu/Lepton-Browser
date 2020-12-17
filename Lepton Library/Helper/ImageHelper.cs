using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Lepton_Library.Helper
{
    public class ImageHelper
    {
        /// <summary>
        /// LoadBitmapImage
        /// </summary>
        /// <param name="storagefile">Input The File</param>
        /// <returns></returns>
        public async static Task<BitmapImage> LoadBitmapImage(StorageFile storagefile)
        {
            BitmapImage bitmap = new BitmapImage();
            try
            {
                var stream = await storagefile.OpenReadAsync();
                await bitmap.SetSourceAsync(stream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + ex.StackTrace);
            }
            return bitmap;
        }
    }
}
