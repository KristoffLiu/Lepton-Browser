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
    public static class EvalJSService
    {
        private const string filehead_URL = "ms-appx:///Assets/EvalJSCode/";

        private const string fileName_ContextMenuHandler = "ContextMenuHandler.js";
        private const string fileName_Zoom               = "Zoom.js";
        private const string fileName_DragDropEvent      = "DragDropEvent.js";
        private const string fileName_FaviconGetter      = "FaviconGetter.js";

        private static string jscode_ContextMenuHandler       = "";
        private static string jscode_Zoom                     = "";
        private static string jscode_DragDropEvent            = "";
        private static string jscode_FaviconGetter            = "";

        public static async void InitServiceAsync()
        {
            StorageFile file_ContextMenuHandler = await StorageFile.GetFileFromApplicationUriAsync( new Uri( filehead_URL + fileName_ContextMenuHandler   ));
            StorageFile file_Zoom               = await StorageFile.GetFileFromApplicationUriAsync( new Uri( filehead_URL + fileName_Zoom                 ));
            StorageFile file_DragDropEvent      = await StorageFile.GetFileFromApplicationUriAsync( new Uri( filehead_URL + fileName_DragDropEvent        ));
            StorageFile file_FaviconGetter      = await StorageFile.GetFileFromApplicationUriAsync( new Uri( filehead_URL + fileName_FaviconGetter        ));
            jscode_ContextMenuHandler  = await FileIO.ReadTextAsync( file_ContextMenuHandler );
            jscode_Zoom                = await FileIO.ReadTextAsync( file_Zoom               );
            jscode_DragDropEvent       = await FileIO.ReadTextAsync( file_DragDropEvent      );
            jscode_FaviconGetter       = await FileIO.ReadTextAsync( file_FaviconGetter      );
        }

        public static string InitContextMenuHandler
        {
            get { return jscode_ContextMenuHandler; }
        }

        public static string InitFaviconGetter
        {
            get { return jscode_FaviconGetter; }
        }

        public static string InitZoom
        {
            get { return jscode_Zoom; }
        }
        public static string InitDragDropEvent
        {
            get { return jscode_DragDropEvent; }
        }

        public static string Zoom()
        {
            return Zoom(100);
        }

        public static string Zoom(int zoomValue)
        {
            return "ZoomFunction("+ Convert.ToString(100) + ")";
        }

        public static string ZoomRegister(int zoomValue)
        {
            return "RegisterZoomMonitor.TryRegisterZoomMonitor('')";
        }

        public static string EvalDragDropEvent(int zoomValue)
        {
            return jscode_DragDropEvent;
        }

        public static string DragDropEventRegister(int zoomValue)
        {
            return "RegisterDragDropHandler.TryRegisterDragDropHandler('')";
        }
    }
}
