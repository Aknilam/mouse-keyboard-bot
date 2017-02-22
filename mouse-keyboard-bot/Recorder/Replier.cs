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
            long prevTime = _recording.StartTime;
            for (int i = 0; i < _recording.Events.Count - skip && run; i++)
            {
                var evt = _recording.Events[i];

                await Task.Delay((int)(evt.Time - prevTime));
                prevTime = evt.Time;
                if (evt.Type == DetailsType.Mouse)
                {
                    MouseMove.ProcessMouseEvent(_recording.Events[i] as MouseDetails);
                }
                else if (_recording.Events[i].Type == DetailsType.Keyboard)
                {
                    KeyPress.ProcessKeyEvent(_recording.Events[i] as KeyboardDetails);
                }
            }
        }

        public void Finish()
        {
            run = false;
        }
    }
}
