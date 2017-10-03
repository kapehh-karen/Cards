using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Core.Utils
{
    public static class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);
    }
}
