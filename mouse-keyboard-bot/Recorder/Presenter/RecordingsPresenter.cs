using mouse_keyboard_bot.Model;
using mouse_keyboard_bot.Recorder.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mouse_keyboard_bot.Recorder.Presenter
{
    public class RecordingsPresenter
    {
        Recordings _view;
        Subscribe _subscribe;
        UserActionsRecorder recorder;
        Replier replier;
        public RecordingsPresenter(Recordings view, Subscribe subscribe)
        {
            _view = view;
            _subscribe = subscribe;

            recorder = new UserActionsRecorder(_subscribe);
            replier = new Replier();

            Init();
        }

        private void Init()
        {
            _view.AttachToSubscribe(_subscribe);

            _view.Start += recorder.StartRecording;
            _view.End += view_End;
            _view.ReplyStart += () => replier.Start(recording);
            _view.ReplyEnd += () => replier.Finish();

            _view.Setialize += _view_Setialize;

            recording = Recording.Deserialize();
        }

        private void _view_Setialize()
        {
            Recording.Serialize(recording);
        }

        Recording recording;
        private void view_End()
        {
            recording = recorder.FinishRecording();
        }
    }
}
