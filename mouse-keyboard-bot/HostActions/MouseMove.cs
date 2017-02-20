using mouse_keyboard_bot.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mouse_keyboard_bot.HostActions
{
    public class MouseMove
    {
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        public const int MOUSEEVENTF_MIDDLEUP = 0x40;

        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //This simulates a left mouse click
        private static void MouseClick(int down, int up, int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(down, xpos, ypos, 0, 0);
            mouse_event(up, xpos, ypos, 0, 0);
        }
        private static void MouseClick(int down, int up, Point point)
        {
            MouseClick(down, up, point.X, point.Y);
        }
        private static void MouseSingleEvent(int eventType, int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(eventType, xpos, ypos, 0, 0);
        }
        public static void LeftMouseClick(Point point)
        {
            MouseClick(MOUSEEVENTF_LEFTDOWN, MOUSEEVENTF_LEFTUP, point);
        }
        public static void MiddleMouseClick(Point point)
        {
            MouseClick(MOUSEEVENTF_MIDDLEDOWN, MOUSEEVENTF_MIDDLEUP, point);
        }
        public static void RightMouseClick(Point point)
        {
            MouseClick(MOUSEEVENTF_RIGHTDOWN, MOUSEEVENTF_RIGHTUP, point);
        }

        private static void MouseSingleEvent(int eventType, Point point)
        {
            MouseSingleEvent(eventType, point.X, point.Y);
        }
        public static void LeftMouseDown(Point point)
        {
            MouseSingleEvent(MOUSEEVENTF_LEFTDOWN, point);
        }
        public static void MiddleMouseDown(Point point)
        {
            MouseSingleEvent(MOUSEEVENTF_MIDDLEDOWN, point);
        }
        public static void RightMouseDown(Point point)
        {
            MouseSingleEvent(MOUSEEVENTF_RIGHTDOWN, point);
        }
        public static void LeftMouseUp(Point point)
        {
            MouseSingleEvent(MOUSEEVENTF_LEFTUP, point);
        }
        public static void MiddleMouseUp(Point point)
        {
            MouseSingleEvent(MOUSEEVENTF_MIDDLEUP, point);
        }
        public static void RightMouseUp(Point point)
        {
            MouseSingleEvent(MOUSEEVENTF_RIGHTUP, point);
        }

        private static void ProcessLeftMouseEvent(MouseDetails mouseDetails)
        {
            switch (mouseDetails.EventType)
            {
                case MouseEventType.Click:
                    LeftMouseClick(mouseDetails.Point);
                    return;
                case MouseEventType.Down:
                    LeftMouseDown(mouseDetails.Point);
                    return;
                case MouseEventType.Up:
                    LeftMouseUp(mouseDetails.Point);
                    return;
            }
        }

        private static void ProcessMiddleMouseEvent(MouseDetails mouseDetails)
        {
            switch (mouseDetails.EventType)
            {
                case MouseEventType.Click:
                    MiddleMouseClick(mouseDetails.Point);
                    return;
                case MouseEventType.Down:
                    MiddleMouseDown(mouseDetails.Point);
                    return;
                case MouseEventType.Up:
                    MiddleMouseUp(mouseDetails.Point);
                    return;
            }
        }

        private static void ProcessRightMouseEvent(MouseDetails mouseDetails)
        {
            switch (mouseDetails.EventType)
            {
                case MouseEventType.Click:
                    RightMouseClick(mouseDetails.Point);
                    return;
                case MouseEventType.Down:
                    RightMouseDown(mouseDetails.Point);
                    return;
                case MouseEventType.Up:
                    RightMouseUp(mouseDetails.Point);
                    return;
            }
        }

        public static void ProcessMouseEvent(MouseDetails mouseDetails)
        {
            switch (mouseDetails.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    ProcessLeftMouseEvent(mouseDetails);
                    return;
                case System.Windows.Forms.MouseButtons.Middle:
                    ProcessMiddleMouseEvent(mouseDetails);
                    return;
                case System.Windows.Forms.MouseButtons.Right:
                    ProcessRightMouseEvent(mouseDetails);
                    return;
            }
        }

        //[DllImport("User32.Dll")]
        //public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        //[StructLayout(LayoutKind.Sequential)]
        //public struct POINT
        //{
        //    public int x;
        //    public int y;
        //}
    }
}
