

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Native
{
    public class IMEOperator
    {

        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr hIMC, ref int mode, ref int text);

        public static IMEStatus GetCurrentStatus(IntPtr hWnd)
        {
            if (true)//hWnd != null)
            {
                IntPtr pIME = ImmGetContext(hWnd);
                int mode = 0;
                int text = 0;
                if (ImmGetConversionStatus(pIME, ref mode, ref text))
                {
                    switch (mode)
                    {
                        case 1025:
                        case 1033:
                        case 1:
                        case 9:
                        case -2147482623:
                        case -2147482615:
                        case -2147483647:
                            //case -2147483839:
                            return IMEStatus.CN;
                        case 1024:
                        case 1032:
                        case 0:
                        case 8:
                        case -2147482624:
                        case -2147482616:
                        case -2147483648:
                        case -2147483640:
                            return IMEStatus.EN;
                        default:
                            return IMEStatus.Other;
                    }
                }
                else
                {
                    return IMEStatus.EN;
                }
            }
            else
            {
                return IMEStatus.EN;
            }
        }

    }

    public enum IMEStatus
    {
        CN,
        EN,
        Other
    }
}
