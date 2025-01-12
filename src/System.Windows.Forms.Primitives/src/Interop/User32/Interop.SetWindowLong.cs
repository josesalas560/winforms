﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class User32
    {
        // We only ever call this on 32 bit so IntPtr is correct
        [DllImport(Libraries.User32, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr SetWindowLongW(IntPtr hWnd, GWL nIndex, nint dwNewLong);

        [DllImport(Libraries.User32, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SetWindowLongPtrW(IntPtr hWnd, GWL nIndex, nint dwNewLong);

        public static IntPtr SetWindowLong(IntPtr hWnd, GWL nIndex, nint dwNewLong)
        {
            if (!Environment.Is64BitProcess)
            {
                return SetWindowLongW(hWnd, nIndex, dwNewLong);
            }

            return SetWindowLongPtrW(hWnd, nIndex, dwNewLong);
        }

        public static IntPtr SetWindowLong(IHandle hWnd, GWL nIndex, nint dwNewLong)
        {
            IntPtr result = SetWindowLong(hWnd.Handle, nIndex, dwNewLong);
            GC.KeepAlive(hWnd);
            return result;
        }

        public static IntPtr SetWindowLong(IHandle hWnd, GWL nIndex, HandleRef dwNewLong)
        {
            IntPtr result = SetWindowLong(hWnd.Handle, nIndex, dwNewLong.Handle);
            GC.KeepAlive(hWnd);
            GC.KeepAlive(dwNewLong.Wrapper);
            return result;
        }

        public static IntPtr SetWindowLong(IHandle hWnd, GWL nIndex, WNDPROC dwNewLong)
        {
            IntPtr pointer = Marshal.GetFunctionPointerForDelegate(dwNewLong);
            IntPtr result = SetWindowLong(hWnd, nIndex, pointer);
            GC.KeepAlive(dwNewLong);
            return result;
        }

        public static IntPtr SetWindowLong(IntPtr hWnd, GWL nIndex, WNDPROCINT dwNewLong)
        {
            IntPtr pointer = Marshal.GetFunctionPointerForDelegate(dwNewLong);
            IntPtr result = SetWindowLong(hWnd, nIndex, pointer);
            return result;
        }
    }
}
