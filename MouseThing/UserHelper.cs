using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MouseThing
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x { get; set; }
        public int y { get; set; }
        public POINT(int x, int y) => (this.x, this.y) = (x, y);
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

    }
    class User32
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        protected static extern bool GetCursorPos(out POINT pt);

        [DllImport("user32.dll", SetLastError = true)]
        protected static extern bool ScreenToClient(IntPtr hwnd, ref POINT lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        protected static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpR);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        protected static extern IntPtr FindWindowByCaption(IntPtr zeroO, string lpWindowName);
        [DllImport("user32.dll")]
        protected static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        protected static extern int GetAsyncKeyState(int value);

    }
    class PinVokeHelper : User32
    {
        public static POINT GetCursorPosition(POINT lpPoint, int processID)
        {
            GetCursorPos(out lpPoint);
            return lpPoint;
        }
        public static RECT GetWindowRectangle(IntPtr handle, RECT lpR)
        {
            GetWindowRect(handle, ref lpR);
            return lpR;
        }
        public static RECT GetWindowRectangle(string name, RECT lpR)
        {
            GetWindowRect(FindWindowByCaption(IntPtr.Zero, name), ref lpR);
            return lpR;
        }
        public static bool SetMousePositions(int x, int y)
        {
            return SetCursorPos(x, y);
        }
        public static bool OnInput(ConsoleKey key)
        {
            return GetAsyncKeyState((int)key) != 0;
        }
    }
}
