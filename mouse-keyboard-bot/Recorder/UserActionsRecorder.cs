using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using mouse_keyboard_bot.HostActions;
using mouse_keyboard_bot.Model;
using mouse_keyboard_bot.App;

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
            subscribe.KeyPress += Subscribe_KeyboardKey;
            subscribe.KeyDown += Subscribe_KeyDown;
            subscribe.KeyUp += Subscribe_KeyUp;

            subscribe.MouseClickAt += Subscribe_MouseClickAt;
            subscribe.MouseDown += Subscribe_MouseDown;
            subscribe.MouseUp += Subscribe_MouseUp;
        }

        private void Subscribe_KeyUp(Keys key, Gma.System.MouseKeyHook.VirtualKeyCode code)
        {
            details.Events.Add(new KeyboardDetails { Code = code, EventType = KeyEventType.Up });
        }

        private void Subscribe_KeyDown(Keys key, Gma.System.MouseKeyHook.VirtualKeyCode code)
        {
            details.Events.Add(new KeyboardDetails { Code = code, EventType = KeyEventType.Down });
        }

        private void Subscribe_MouseUp(MouseDetails mouseDetails)
        {
            details.Events.Add(mouseDetails);
        }

        private void Subscribe_MouseDown(MouseDetails mouseDetails)
        {
            details.Events.Add(mouseDetails);
        }

        private void Subscribe_MouseClickAt(MouseDetails mouseDetails)
        {
            //details.Events.Add(mouseDetails);
        }

        private void Subscribe_KeyboardKey(char key, Gma.System.MouseKeyHook.VirtualKeyCode code)
        {
            //details.Events.Add(new KeyboardDetails { Code = code, EventType = KeyEventType.Press });

            /*if (key == 'j')
            {
                var hnld = ForegroundProcess.GetForegroundWindow();
                Maximize.Max(hnld);
                SendKeys.Send("A");
            }*/
        }

        Recording details = new Recording();
        public void StartRecording()
        {
            details = new Recording();
        }

        Recording recordedDetails;
        public Recording FinishRecording()
        {
            recordedDetails = details;
            details = new Recording();
            return recordedDetails;
        }
    }
}
