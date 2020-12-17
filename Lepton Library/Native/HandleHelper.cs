

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Native
{
        [ComImport, Guid("45D64A29-A63E-4CB6-B498-5781D298CB4F")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ICoreWindowInterop
        {
            IntPtr WindowHandle { get; }
            bool MessageHandled { set; }
        }

    public class NativeHelper
    {

        private static IntPtr hwnd;
        public static IntPtr CurrentHandle
        {
            get
            {
                if (hwnd != IntPtr.Zero)
                    return hwnd;
                else
                {
                    hwnd = GetHandle();
                    return hwnd;
                }
            }
        }

        private static IntPtr GetHandle()
        {
            //coreWnd = Windows.UI.Core.CoreWindow.GetForCurrentThread();
            var mainViews = Windows.ApplicationModel.Core.CoreApplication.MainView;
            dynamic coreWnd = mainViews.CoreWindow; //Windows.UI.Core.CoreWindow.GetForCurrentThread();
            if (coreWnd != null)
            {
                var interop = (ICoreWindowInterop)coreWnd;
                var handle = interop.WindowHandle;

                return handle;
            }
            else
            {
                return IntPtr.Zero;
            }
        }
    }
}
