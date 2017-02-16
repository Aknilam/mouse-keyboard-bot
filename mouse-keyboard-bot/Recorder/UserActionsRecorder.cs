using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using mouse_keyboard_bot.HostActions;

namespace mouse_keyboard_bot.Recorder
{
    public class UserActionsRecorder
    {
        private Subscribe subscribe;

        public UserActionsRecorder(Subscribe subscribe)
        {
            this.subscribe = subscribe;

            AttachSubscribe();
        }

        private void AttachSubscribe()
        {
            subscribe.MouseClickAt += Subscribe_MouseClickAt;
            subscribe.KeyboardKey += Subscribe_KeyboardKey; ;
        }

        MouseDetails mouseDetails;
        private void Subscribe_MouseClickAt(MouseButtons button, int px, int py)
        {
            mouseDetails = new MouseDetails
            {
                Button = button,
                X = px,
                Y = py
            };
        }

        private void Subscribe_KeyboardKey(char key, Gma.System.MouseKeyHook.VirtualKeyCode code)
        {
            if ((char)key == 'j')
            {
                var hnld = ForegroundProcess.GetForegroundWindow();
                Maximize.Max(hnld);
                SendKeys.Send("A");
            }
        }
    }

    class MouseDetails
    {
        public int X;
        public int Y;
        public MouseButtons Button;
    }
}
