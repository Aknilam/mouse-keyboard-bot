using mouse_keyboard_bot.App;
using mouse_keyboard_bot.HostActions;
using mouse_keyboard_bot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mouse_keyboard_bot.Recorder
{
    public class Replier
    {
        Recording _recording;
        public Replier()
        {
        }

        public void Start(Recording recording)
        {
            _recording = recording;
            if (_recording != null)
            {
                Run();
            }
        }

        volatile bool run = false;
        private async void Run()
        {
            int skip = 2;// Config.SkipLast.Value;
            run = true;
            for (int i = 0; i < _recording.Events.Count - skip && run; i++)
            {
                if (_recording.Events[i].Type == DetailsType.Mouse)
                {
                    MouseMove.ProcessMouseEvent(_recording.Events[i] as MouseDetails);
                }
                else if (_recording.Events[i].Type == DetailsType.Keyboard)
                {
                    KeyPress.ProcessKeyEvent(_recording.Events[i] as KeyboardDetails);
                }
                await Task.Delay(500);
            }
        }

        public void Finish()
        {
            run = false;
        }
    }
}
