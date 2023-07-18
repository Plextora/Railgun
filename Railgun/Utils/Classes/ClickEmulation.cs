#region

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
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void SimulateMouseClick()
        {
            MousePoint mousePos = GetCursorPosition();
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)mousePos.X, (uint)mousePos.Y, 0, 0);
        }

        public static MousePoint GetCursorPosition()
        {
            bool gotPoint = GetCursorPos(out MousePoint currentMousePoint);
            if (!gotPoint) currentMousePoint = new MousePoint(0, 0);
            return currentMousePoint;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
