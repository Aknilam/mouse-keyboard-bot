using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using mouse_keyboard_bot.Model;

namespace mouse_keyboard_bot.HostActions
{
    class KeyPress
    {
        const UInt32 WM_KEYDOWN = 0x0100;
        const int VK_F5 = 0x74;

        //[DllImport("user32.dll")]
        //static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        //PostMessage(proc.MainWindowHandle, WM_KEYDOWN, VK_F5, 0);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        public const int VK_LCONTROL = 0xA2; //Left Control key code
        public const int A = 0x41; //A key code
        public const int C = 0x43; //C key code

        public static void KeyDown(byte virtualKeyCode)
        {
            keybd_event(virtualKeyCode, 0, KEYEVENTF_EXTENDEDKEY, 0);
        }
        public static void KeyUp(byte virtualKeyCode)
        {
            keybd_event(virtualKeyCode, 0, KEYEVENTF_KEYUP, 0);
        }
        public static void PressKey(byte virtualKeyCode)
        {
            keybd_event(virtualKeyCode, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(virtualKeyCode, 0, KEYEVENTF_KEYUP, 0);
        }

        public static void ProcessKeyEvent(KeyboardDetails keyboardDetails)
        {
            try
            {
                byte key;
                checked
                {
                    key = (byte)keyboardDetails.Code.virtualKeyCode;
                }

                switch (keyboardDetails.EventType)
                {
                    case KeyEventType.Press:
                        PressKey(key);
                        return;
                    case KeyEventType.Down:
                        KeyDown(key);
                        return;
                    case KeyEventType.Up:
                        KeyUp(key);
                        return;
                }
            }
            catch { }
        }

        //public static void PressKeys()
        //{
        //    // Hold Control down and press A
        //    keybd_event(VK_LCONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
        //    keybd_event(A, 0, KEYEVENTF_EXTENDEDKEY, 0);
        //    keybd_event(A, 0, KEYEVENTF_KEYUP, 0);
        //    keybd_event(VK_LCONTROL, 0, KEYEVENTF_KEYUP, 0);

        //    // Hold Control down and press C
        //    keybd_event(VK_LCONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
        //    keybd_event(C, 0, KEYEVENTF_EXTENDEDKEY, 0);
        //    keybd_event(C, 0, KEYEVENTF_KEYUP, 0);
        //    keybd_event(VK_LCONTROL, 0, KEYEVENTF_KEYUP, 0);
        //}
    }
}
