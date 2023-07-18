#region

using System;
using System.Runtime.InteropServices;

#endregion

/*
* Copyright 2023 Plextora
* Licensed under the GPL-3.0 license (https://www.gnu.org/licenses/gpl-3.0.en.html#license-text)
*/

namespace Railgun.Utils.Classes
{
    public static class ClickEmulation
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        public static void SimulateMouseClick()
        {
            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = 0;
            mouseUpInput.mi.dwFlags = MouseEventFlags.MOUSEEVENT_LEFTUP;
            SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));

            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = 0;
            mouseDownInput.mi.dwFlags = MouseEventFlags.MOUSEEVENT_LEFTDOWN;
            SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        }

        [Flags]
        public enum MouseEventFlags : uint
        {
            MOUSEEVENT_LEFTDOWN = 0x02,
            MOUSEEVENT_LEFTUP = 0x04
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
    }
}
