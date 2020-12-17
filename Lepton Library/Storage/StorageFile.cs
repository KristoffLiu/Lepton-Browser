using Lepton_Library.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml.Media.Imaging;

namespace Lepton_Library.Storage
{
    public class FileStorage
    {
        private StorageFolder _myFolder = null;

        // Create Folder
        private async void CreateFolder()
        {
            StorageFolder picturesFolder = await KnownFolders.GetFolderForUserAsync(null, KnownFolderId.PicturesLibrary);
            _myFolder = await picturesFolder.CreateFolderAsync("MyFolder", CreationCollisionOption.OpenIfExists);

            // Tips: Another alternative way 
            // _myFolder = await picturesFolder.CreateFolderAsync(@"MyFolder\sub\subsub", CreationCollisionOption.OpenIfExists);
        }

        // Create A File in a particular folder
        private async void CreateFile()
        {
            if (_myFolder != null)
            {
                Windows.Storage.StorageFile myFile = await _myFolder.CreateFileAsync("MyFile", CreationCollisionOption.OpenIfExists);

                // 创建文件时也可以按照下面这种方式指定子目录（目录不存在的话会自动创建）
                // StorageFile myFile = await _myFolder.CreateFileAsync(@"folder1\folder2\MyFile", CreationCollisionOption.OpenIfExists);

                //lblMsg.Text = "在指定的文件夹中创建了文件";
            }
        }

        // Copy the file
        private async void CopyFile()
        {
            if (_myFolder != null)
            {
                try
                {
                    StorageFile myFile = await _myFolder.GetFileAsync("MyFile");
                    StorageFile myFile_copy = await myFile.CopyAsync(_myFolder, "MyFile_Copy", NameCollisionOption.FailIfExists);
                    //lblMsg.Text = "复制了文件";
                }
                catch (Exception)
                {
                    //lblMsg.Text = ex.ToString();
                }
            }
        }

        /// <summary>
        /// Image which has been cropped will be saved here
        /// </summary>
        public static StorageFile CroppedImage { get; set; }

        public async static Task<BitmapImage> LoadPictureFromApplicationUriAsync(string uriString)
        {
            StorageFile storageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uriString));
            return await ImageHelper.LoadBitmapImage(storageFile);
        }

        public async static Task<BitmapImage> LoadPicture()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary; picker.FileTypeFilter.Add(".jpg"); picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".gif");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            var inputFile = SharedStorageAccessManager.AddFile(file);
            CroppedImage = await ApplicationData.Current.LocalFolder.CreateFileAsync("Cropped.jpg", CreationCollisionOption.ReplaceExisting);
            var destinationFile = SharedStorageAccessManager.AddFile(CroppedImage);
            var options = new LauncherOptions();
            options.TargetApplicationPackageFamilyName = "Microsoft.Windows.Photos_8wekyb3d8bbwe";
            var parameters = new ValueSet();
            parameters.Add("InputToken", inputFile);
            parameters.Add("DestinationToken", destinationFile);
            parameters.Add("ShowCamera", false);
            parameters.Add("EllipticalCrop", true);
            parameters.Add("CropWidthPixals", 300);
            parameters.Add("CropHeightPixals", 300);

            var result = await Launcher.LaunchUriForResultsAsync(new Uri("microsoft.windows.photos.crop:"), options, parameters);

            return await ImageHelper.LoadBitmapImage(CroppedImage);
        }

        public async static Task<bool> UpdateProfilePicture()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

            //Limit the format of the file
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            var inputFile = SharedStorageAccessManager.AddFile(file);
            CroppedImage = await ApplicationData.Current.LocalFolder.CreateFileAsync("Cropped.jpg", CreationCollisionOption.ReplaceExisting);
            var destinationFile = SharedStorageAccessManager.AddFile(CroppedImage);
            var options = new LauncherOptions();
            options.TargetApplicationPackageFamilyName = "Microsoft.Windows.Photos_8wekyb3d8bbwe";
            var parameters = new ValueSet();
            parameters.Add("InputToken", inputFile);
            parameters.Add("DestinationToken", destinationFile);
            parameters.Add("ShowCamera", false);
            parameters.Add("EllipticalCrop", true);
            parameters.Add("CropWidthPixals", 300);
            parameters.Add("CropHeightPixals", 300);

            var result = await Launcher.LaunchUriForResultsAsync(new Uri("microsoft.windows.photos.crop:"), options, parameters);

            StorageFolder a = ApplicationData.Current.LocalFolder;
            StorageFolder b = await a.CreateFolderAsync("User", CreationCollisionOption.OpenIfExists);

            StorageFile c = await CroppedImage.CopyAsync(b, "ProfilePicture.png", NameCollisionOption.ReplaceExisting);
            return true;
        }
    }
}
